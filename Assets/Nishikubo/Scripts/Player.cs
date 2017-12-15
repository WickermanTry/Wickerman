using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TimeState
//{
//    Morning,    //朝、昼、夕方　昼間　Daytime
//    Night,      //夜　夜中　Midnight
//}

public enum PlayerState
{
    None,       //何もしない
    Idle,       //待機状態
    Walk,       //歩き状態
    Stealthy,   //忍び足
    Trailing,   //引きずる状態
    Sacrifice,  //生贄にする状態
    Dead,       //死亡状態
}

public class Player : MonoBehaviour
{

    public PlayerState state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField, Tooltip("状態遷移")]
    protected PlayerState m_state = PlayerState.None;  //遷移

    //[SerializeField, Tooltip("時間")]
    //protected TimeState m_stateTime = TimeState.Morning;//仮で区別用

    protected Rigidbody m_rb;
    protected Animator m_animator;


    protected float m_inputH, m_inputV = 0.0f;    //移動入力チェック用
    [SerializeField, Tooltip("移動速度")]
    protected float m_speed = 3.0f;

    protected float m_sp;//スピード保持

    public bool murabitoNotice = false;//仮

    private RaycastHit murabitoHit;//村人がヒットしてるか

    public GameObject m_murabitoKeep;//殺した村人の保持用

    public float m_trailingCount = 0.0f;

    public GameObject hideArea;//隠せる場所を保存

    public GameObject m_fade;

    public int m_dayTime;//0:朝1:昼2:夜


    // Use this for initialization
    protected virtual void Start () {
        m_rb = this.GetComponent<Rigidbody>();
        m_animator = this.GetComponent<Animator>();
        m_sp = m_speed;

        m_state = PlayerState.Idle;
        //m_stateTime = TimeState.Morning;

        //m_dayTime = AwakeData.Instance.sacrificeCount;


    }

    // Update is called once per frame
    protected virtual void Update () {

        //仮
        switch (m_dayTime)
        {
            case 0:
                switch (m_state)
                {
                    case PlayerState.None: break;
                    case PlayerState.Idle: IdleState(); break;
                    case PlayerState.Walk: WalkState(); break;
                    default: break;
                }
                break;
            case 1:
                switch (m_state)
                {
                    case PlayerState.None: break;
                    case PlayerState.Idle: IdleState(); break;
                    case PlayerState.Walk: WalkState(); break;
                    default: break;
                }
                break;
            case 2:
                switch (m_state)
                {
                    case PlayerState.None: break;
                    case PlayerState.Idle: IdleState(); break;
                    case PlayerState.Walk: WalkState(); break;
                    case PlayerState.Stealthy: StealthyState(); break;
                    case PlayerState.Trailing: TrailingState(); break;
                    case PlayerState.Sacrifice: SacrificeState(); break;
                    case PlayerState.Dead: DeadState(); break;
                    default: break;
                }

                break;
            default: break;
        }
    }

    /// <summary>
    /// 待機状態
    /// </summary>
    protected virtual void IdleState()
    {
        m_animator.SetBool("run_check", false);//歩いていない

        //移動入力されてるかチェック
        if (!(m_inputH == 0 && m_inputV == 0))
        {
            m_state = PlayerState.Walk;
        }

        Move(1);


    }

    /// <summary>
    /// 歩き状態
    /// </summary>
    protected virtual void WalkState()
    {
        m_animator.SetBool("run_check", true);//歩き

        //移動入力されてるかチェック
        if (m_inputH == 0 && m_inputV == 0)
        {
            m_state = PlayerState.Idle;
        }
        else if(Input.GetButton("Stealthy"))
        {
            m_state = PlayerState.Stealthy;
        }
        Move(1);

    }

    /// <summary>
    /// 忍び状態
    /// </summary>
    protected virtual void StealthyState()
    {

        //忍び状態かチェック
        m_animator.SetBool("shinobi_mode", true);

        m_speed = m_sp / 2;
        
        if(!Input.GetButton("Stealthy"))
        {
            m_animator.SetBool("shinobi_mode", false);
            m_speed = m_sp;
            m_state = PlayerState.Walk;
        }

        
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + this.GetComponent<BoxCollider>().center.y, transform.position.z);
        Vector3 scale = this.GetComponent<BoxCollider>().size / 2;

        bool isHit = Physics.BoxCast(pos, scale, transform.forward * 0.5f, out murabitoHit, transform.rotation,2);

        if (isHit)
        {
            //前にいるのが村人だったら
            if (murabitoHit.collider.tag == "Murabito")
            {
                //気づかれてない
                if (!murabitoNotice && Input.GetKeyDown(KeyCode.Space))//殺せる
                {
                    m_animator.SetTrigger("huiuchi");//アニメーションが終了するまで待つ？

                    murabitoHit.collider.gameObject.GetComponent<MurabitoNight>().SetState(MurabitoNight.MurabitoState.Down);
                }
            }
        }



        Move(1);


    }

    /// <summary>
    /// 引きずる状態
    /// </summary>
    protected virtual void TrailingState()
    {

        m_animator.SetBool("hikizuri", true);
        //プレイヤーの向きを反転
        Move(-1);


        m_trailingCount++;



        if(m_trailingCount > 1.0f && Input.GetKeyDown(KeyCode.V))
        {

            if (hideArea == null)
            {
                //放置
                m_murabitoKeep.GetComponent<MurabitoNight>().SetState(MurabitoNight.MurabitoState.Down);
            }
            else
            {
                //隠せる
                m_fade.SetActive(true);

                m_murabitoKeep.GetComponent<MurabitoNight>().SetState(MurabitoNight.MurabitoState.Hide);

                hideArea = null;
            }
            m_animator.SetBool("hikizuri", false);
            m_murabitoKeep = null;
            m_trailingCount = 0.0f;

            m_state = PlayerState.Idle;
        }



    }

    /// <summary>
    /// 生贄にする状態
    /// </summary>
    protected void SacrificeState()
    {
        m_murabitoKeep.GetComponent<MurabitoNight>().SetState(MurabitoNight.MurabitoState.Hide);

        m_animator.SetBool("hikizuri", false);

        //移動位置を固定で
        GameObject point = GameObject.Find("ExitPoint");
        transform.position = point.transform.position;
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));

        if (SceneNavigator.Instance.IsFading == false)
        {
            m_murabitoKeep = null;

            m_state = PlayerState.Idle;
        }
    }

    /// <summary>
    /// 死亡状態
    /// </summary>
    protected virtual void DeadState()
    {
        Move(0);

    }

    /// <summary>
    /// 朝、昼、夕方（聞き込み時）
    /// </summary>
    //private void MorningState()
    //{

    //}

    /// <summary>
    /// 夜（暗殺時）
    /// </summary>
    //private void NightState()
    //{
    //    //StealthyState();
    //}

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="s">+か-で向き変化</param>
    protected void Move(float s)
    {
        m_inputH = Input.GetAxis("Horizontal");
        m_inputV = Input.GetAxis("Vertical");

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * m_inputV + Camera.main.transform.right * m_inputH;
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        m_rb.velocity = moveForward * m_speed + new Vector3(0, m_rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward *s);
        }
    }


    //protected void CharaAnime(byte num,bool flag)
    //{
    //    switch (num)
    //    {
    //        case 1://待機
    //            m_animator.SetBool("run_check", false);
    //            break;
    //        case 2://歩き
    //            m_animator.SetBool("run_check", true);
    //            break;
    //        case 3://忍び
    //            m_animator.SetBool("shinobi_mode", true);
    //            break;
    //        case 4://不意打ち
    //            m_animator.SetTrigger("huiuchi");
    //            break;
    //        case 5://引きづり
    //            m_animator.SetBool("hikizuri", true);
    //            break;

    //        default:
    //            break;
    //    }
    //}


    protected void OnTriggerStay(Collider other)
    {
        //村人死亡後
        if (other.gameObject.tag == "DownArea")
        {
            if (Input.GetKeyDown(KeyCode.V) && m_murabitoKeep == null)//引きづれる
            {
                m_murabitoKeep = other.gameObject.transform.parent.gameObject;
                m_murabitoKeep.GetComponent<MurabitoNight>().SetState(MurabitoNight.MurabitoState.Drag);


                m_state = PlayerState.Trailing;
            }
        }


        //引きずってる状態&&隠せるエリア内
        if (other.gameObject.tag == "HideArea" && m_state==PlayerState.Trailing)
        {
            hideArea = other.gameObject;
        }

        //生贄にするための範囲内
        if (other.gameObject.tag == "Sacrifice" && m_state == PlayerState.Trailing)
        {
            //音鳴らす
            SoundManager.Instance.PlaySE(0);

            m_state = PlayerState.Sacrifice;

        }
    }


    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HideArea" && m_state == PlayerState.Trailing)
        {
            hideArea = null;
        }
    }

}
