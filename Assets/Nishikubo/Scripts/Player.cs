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

    [SerializeField,Tooltip("フェードにかける時間")]
    private float m_fadeTime = 2.0f;

    [SerializeField]
    private List<string> m_stoleObj = new List<string>();   //盗まれたもの
    //public int m_dayTime;//0:朝,1:昼,2:夜


    void Awake()
    {
        //if(GameObject.FindGameObjectWithTag("Player")==null)
        //{
        //    DontDestroyOnLoad(this);
        //}
    }

    // Use this for initialization
    void Start () {
        m_state = PlayerState.Idle;

        m_animator = this.GetComponent<Animator>();
        m_playerMove = this.GetComponent<PlayerMove>();
        m_uiDisplay = GameObject.Find("PlayerCanvas").GetComponent<UIDisplay>();

        m_stoleObj = AwakeData.Instance.stoleObj;
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

        if(Input.GetKeyDown(KeyCode.I))
        {
            Instantiate((GameObject)Resources.Load("Prefabs/StealObjects/" + m_stoleObj[0]));
        }
    }

    /// <summary>
    /// 待機状態
    /// </summary>
    private void IdleState()
    {
        m_animator.SetBool("run_check", false);//歩いていない

        //移動入力されてるかチェック
        if (!m_playerMove.IsInput())
        {
            m_state = PlayerState.Walk;
        }

        m_playerMove.CarryPosition(HavePosition.None);
        HitObject();


    }

    /// <summary>
    /// 歩き状態
    /// </summary>
    private void WalkState()
    {
        m_animator.SetBool("run_check", true);//歩き

        //移動入力されてるかチェック
        if (m_playerMove.IsInput())
        {
            m_state = PlayerState.Idle;
        }

        m_playerMove.CarryPosition(HavePosition.None);
        HitObject();
    }


    /// <summary>
    /// 引きずる状態(盗んだものを)
    /// </summary>
    private void TrailingState()
    {
        m_uiDisplay.ImageActive(1,false);

        m_animator.SetBool("hikizuri", true);
        //プレイヤーの向きを反転
        m_playerMove.CarryPosition(m_objectKeep.GetComponent<ObjectStatus>().state);
        m_trailingCount++;

        if(m_trailingCount > 1.0f && Input.GetButtonDown("Steal"))//space
        {

            if (hideArea == null)
            {
                //放置
                AwakeData.Instance.mass = AwakeData.Instance.mass - m_objectKeep.gameObject.GetComponent<ObjectStatus>().mass;
                m_stoleObj.Remove(m_objectKeep.gameObject.name);
                m_objectKeep.state = ObjectState.Idle;
            }
            else
            {
                //隠せる
                SceneNavigator.Instance.Fade(m_fadeTime);

                AwakeData.Instance.mass = AwakeData.Instance.mass - m_objectKeep.gameObject.GetComponent<ObjectStatus>().mass;
                m_stoleObj.Remove(m_objectKeep.gameObject.name);
                m_objectKeep.state = ObjectState.Hide;

                hideArea = null;
                m_uiDisplay.ImageActive(0, false);

            }
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


    //盗めるモノが目の前にあるか
    private void HitObject()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + this.GetComponent<BoxCollider>().center.y, transform.position.z);
        Vector3 scale = this.GetComponent<BoxCollider>().size / 2;

        int layerMask = 1 << 10;
        bool isHit = Physics.BoxCast(pos, scale, transform.forward/* * 0.5f*/, out m_objectHit, transform.rotation, 0.5f, layerMask);
        bool checkbox = Physics.CheckBox(pos, scale, transform.rotation, layerMask);

        if ((isHit) || (isHit && checkbox))
        {
            m_uiDisplay.ImageActive(1,true);

            if (Input.GetButtonDown("Steal") && m_objectKeep==null /*&& isHit*/)//space
            {
                
                m_objectKeep = m_objectHit.collider.gameObject.GetComponent<MoveObjects>();
                bool isMultiple = m_objectKeep.GetComponent<ObjectStatus>().isMultiple;
                if (!isMultiple)//重いものだったら
                {
                    m_stoleObj.Add(m_objectKeep.gameObject.name);
                    AwakeData.Instance.mass = AwakeData.Instance.mass + m_objectKeep.gameObject.GetComponent<ObjectStatus>().mass;
                    m_objectKeep.state = ObjectState.Carry;
                    m_state = PlayerState.Trailing;//盗んだ状態

                }
                else if(isMultiple)//軽いものだったら
                {
                    m_stoleObj.Add(m_objectKeep.gameObject.name);
                    AwakeData.Instance.mass = AwakeData.Instance.mass + m_objectKeep.gameObject.GetComponent<ObjectStatus>().mass;
                    m_objectKeep.state = ObjectState.Have;
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
            m_uiDisplay.ImageActive(0, true);

        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HideArea" && m_state == PlayerState.Trailing)
        {
            hideArea = null;
            m_uiDisplay.ImageActive(0, false);

        }
    }

}
