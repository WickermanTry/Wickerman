using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    None,       //何もしない
    Idle,       //待機状態
    Walk,       //歩き状態
    Trailing,   //引きずる状態
    Dead,       //死亡状態
}

//プレイヤークラス
public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("状態遷移")]
    private PlayerState m_state = PlayerState.None;  //遷移
    public PlayerState state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    private Animator m_animator;
    private PlayerMove m_playerMove;
    private UIDisplay m_uiDisplay;

    private RaycastHit m_objectHit;//モノがヒットしてるか
    private MoveObjects m_objectKeep;//盗んだモノの保持用
    private float m_trailingCount = 0.0f;
    private GameObject hideArea;//隠せる場所を保存用

    public Quaternion checkQuaternion_;
  
    [SerializeField,Tooltip("フェードにかける時間")]
    private float m_fadeTime = 2.0f;

    //public int m_dayTime;//0:朝,1:昼,2:夜

    private ItemDataBase m_itemDataBase;
    private ItemData m_itemData;


    //void Awake()
    //{
    //    if (GameObject.FindGameObjectWithTag("Player") == null)
    //    {
    //        DontDestroyOnLoad(this);
    //    }
    //}
    private bool m_isHideArea;
    public bool isHideArea
    {
        get { return m_isHideArea; }
    }

    // Use this for initialization
    void Start () {
        m_state = PlayerState.Idle;

        m_animator = this.GetComponent<Animator>();
        m_playerMove = this.GetComponent<PlayerMove>();
        m_uiDisplay = GameObject.Find("PlayerCanvas").GetComponent<UIDisplay>();
        m_itemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();

        this.transform.position = AwakeData.Instance.playerPosition_;
        //m_dayTime = AwakeData.Instance.sacrificeCount;

    }

    // Update is called once per frame
    void Update () {
        switch (m_state)
        {
            case PlayerState.None: break;
            case PlayerState.Idle: IdleState(); break;
            case PlayerState.Walk: WalkState(); break;
            case PlayerState.Trailing: TrailingState(); break;
            case PlayerState.Dead: DeadState(); break;
            default: break;
        }
    }

    /// <summary>
    /// 待機状態
    /// </summary>
    private void IdleState()
    {
        m_animator.SetBool("run_check", false);//歩いていない
        m_playerMove.CarryPosition(HavePosition.None);
        HitObject();

        //移動入力されてるかチェック
        if (!m_playerMove.IsInput())
        {
            m_state = PlayerState.Walk;
        }
    }

    /// <summary>
    /// 歩き状態
    /// </summary>
    private void WalkState()
    {
        m_animator.SetBool("run_check", true);//歩き
        m_playerMove.CarryPosition(HavePosition.None);
        HitObject();

        //移動入力されてるかチェック
        if (m_playerMove.IsInput())
        {
            m_state = PlayerState.Idle;
        }
    }

    /// <summary>
    /// 引きずる状態(盗んだものを)
    /// </summary>
    private void TrailingState()
    {
        m_uiDisplay.ImageActive(1,false);

        m_animator.SetBool("hikizuri", true);
        //プレイヤーの向きを反転
        m_playerMove.CarryPosition(m_objectKeep.havePosition);
        m_trailingCount++;

        if(m_trailingCount > 1.0f && Input.GetButtonDown("Steal"))//space
        {
            if (hideArea == null)
            {
                //放置(置けるポジションを用意する予定)
                AwakeData.Instance.mass = AwakeData.Instance.mass - m_objectKeep.mass;
                //地面にレイ飛ばしてポジションをそこの上にする
                RaycastHit hit;
                bool isHit = Physics.Raycast(m_objectKeep.transform.position, -transform.up, out hit);
                if (isHit)
                {
                    Debug.Log(hit.collider.name +"  "+ hit.distance);
                    m_objectKeep.transform.position = new Vector3(m_objectKeep.transform.position.x, hit.distance, m_objectKeep.transform.position.z);
                    if (m_objectKeep.havePosition == HavePosition.Side)
                    {
                        m_objectKeep.transform.localRotation = Quaternion.Euler(new Vector3(m_objectKeep.transform.eulerAngles.x, -90.0f, 0.0f));
                        m_objectKeep.transform.position = new Vector3(m_objectKeep.transform.position.x, hit.transform.position.y + 1.0f, m_objectKeep.transform.position.z);
                    }

                    if (hit.collider.name== "Terrain")
                    {
                        m_objectKeep.transform.position = new Vector3(m_objectKeep.transform.position.x, 0.0f, m_objectKeep.transform.position.z);

                    }

                }

                m_objectKeep.state = ObjectState.Idle;
            }
            else
            {
                hideArea.GetComponent<HideArea>().hideCount++;

                //隠せる
                SceneNavigator.Instance.Fade(m_fadeTime);

                AwakeData.Instance.mass = AwakeData.Instance.mass - m_objectKeep.mass;
                m_objectKeep.state = ObjectState.Hide;

                hideArea = null;
                m_uiDisplay.ImageActive(0, false);
            }
            ItemFlag(m_itemDataBase.GetItemData(), false);
            m_animator.SetBool("hikizuri", false);
            m_objectKeep = null;
            m_trailingCount = 0.0f;

            m_state = PlayerState.Idle;

        }
    }

    /// <summary>
    /// 死亡状態
    /// </summary>
    private void DeadState()
    {
        Debug.Log("Dead");
    }


    /// <summary>
    /// 盗めるモノが目の前にあるか
    /// </summary>
    private void HitObject()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + this.GetComponent<BoxCollider>().center.y, transform.position.z);
        Vector3 scale = this.GetComponent<BoxCollider>().size / 2;

        int layerMask = 1 << 10;
        bool isHit = Physics.BoxCast(pos, scale, transform.forward/* * 0.5f*/, out m_objectHit, transform.rotation, 0.5f, layerMask);
        bool checkbox = Physics.CheckBox(pos, scale, transform.rotation, layerMask);

        if ((isHit) || (isHit && checkbox))
        {
            if(m_objectKeep==null)
            {
                m_uiDisplay.ImageActive(1, true);

            }

            if (Input.GetButtonDown("Steal") && m_objectKeep==null /*&& isHit*/)//space
            {
                
                m_objectKeep = m_objectHit.collider.gameObject.GetComponent<MoveObjects>();
                ItemFlag(m_itemDataBase.GetItemData(),true);
                AwakeData.Instance.mass = AwakeData.Instance.mass + m_objectKeep.mass;
                m_objectKeep.state = ObjectState.Carry;
                if (!m_objectKeep.isMultiple)//重いものだったら
                {
                    m_state = PlayerState.Trailing;//盗んだ状態
                }
                else if(m_objectKeep.isMultiple)//軽いものだったら
                {
                    m_objectKeep = null;                   
                }
            }
        }
        else
        {
            m_uiDisplay.ImageActive(1,false);
        }
    }


    void OnTriggerStay(Collider other)
    {
        //引きずってる状態&&隠せるエリア内
        if (other.gameObject.tag == "HideArea" && m_state==PlayerState.Trailing)
        {
            hideArea = other.gameObject;
            //m_uiDisplay.ImageActive(0, true);
        }
        //if(other.gameObject.tag=="HideArea")
        //{
        //    m_isHideArea = true;
        //}

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HideArea" && m_state == PlayerState.Trailing)
        {
            hideArea = null;
            //m_uiDisplay.ImageActive(0, false);
        }
    }


    /// <summary>
    /// アイテムを盗めたかどうか
    /// </summary>
    /// <param name="itemLists">アイテム</param>
    /// <param name="flag">true:盗めた ,false:盗んでない</param>
    private void ItemFlag(ItemData[] itemLists,bool flag)
    {
        foreach (var item in itemLists)
        {
            m_itemData = item;
            //配列にヒットした要素があるか
            //var itemtypeString = m_itemData.GetItemType().ToString();
            if ((m_itemData.GetItemType().ToString() == m_objectKeep.name) || (m_itemData.GetItemType().ToString() + "(Clone)" == m_objectKeep.name))
            {
                //フラグを立てて盗んだ状態
                var t = this.GetComponent<MyItemStatus>();
                t.SetItemFlag(m_itemData.GetItemNumber(), flag);
            }
        }
    }

    //Trailing状態終了時に呼ぶ
    public void AfterAchieving(string request)
    {
        if(m_objectKeep.gameObject.name == request || m_objectKeep.gameObject.name + "(Clone)" == request)
        {
            m_animator.SetBool("hikizuri", false);
            m_objectKeep = null;
            m_trailingCount = 0.0f;

            m_state = PlayerState.Idle;

        }

    }
}
