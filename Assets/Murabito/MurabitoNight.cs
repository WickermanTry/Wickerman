using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurabitoNighttest : MonoBehaviour
{
    //状態
    public enum MurabitoState
    {
        Idle,
        Walk,
        Down,
        Drag,
        Hide//隠された状態
    }

    //村人の状態
    private MurabitoState murabitoState;

    //アニメーター
    private Animator animator;
    private BoxCollider col;
    private Rigidbody rb;

    //[SerializeField, Tooltip("状態遷移")]
    //protected State m_state = State.None;  //遷移


    //プレイヤーの引きずる座標オブジェクト
    //public GameObject targetObj;
    //
    //Vector3 targetPos;
    //引きずられる座標
    //public Transform _target;
    //プレイヤー
    private GameObject PlayerPosition;


    

    void Start()
    {
        murabitoState = MurabitoState.Idle;
        animator = GetComponent<Animator>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player");
        //targetObj = PlayerPosition.transform.Find("HikizuriPosition").gameObject;
        //targetPos = targetObj.transform.position;
        col = this.GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        //targetObj = GameObject.Find("HikizuriPosition");
        

        switch (murabitoState)
        {
            //通常
            case MurabitoState.Idle:
                animator.SetBool("walk", false);
                col.isTrigger = false;
                rb.isKinematic = true;
                break;
            //移動
            case MurabitoState.Walk:
                animator.SetBool("walk", true);
                break;
            //ダウン
            case MurabitoState.Down:
                //アニメーション
                animator.SetBool("down", true);
                animator.SetBool("drag", false);

                transform.Find("DownArea").gameObject.SetActive(true);
                col.isTrigger = true;
                rb.isKinematic = false;

                transform.parent = null;

                //ダウン→引きずり
                //if (Input.GetKeyDown("v"))
                //{
                //    ////村人のダウン状態のエリアのアクティベート
                //    //transform.Find("EnemyDownArea").gameObject.SetActive(true);
                //    //村人の状態を"Drag"に変更
                //    murabitoState = MurabitoState.Drag;
                //    ////アニメーション
                //    //animator.SetBool("drag", true);
                //}


                break;
            //引きずり
            case MurabitoState.Drag:
                //アニメーション
                animator.SetBool("drag", true);

                transform.Find("DownArea").gameObject.SetActive(false);


                transform.parent = PlayerPosition.transform;


                transform.position = GameObject.Find("TrailingPosition").transform.position;
                //transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y*0.0f, transform.eulerAngles.z);
                transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));



                //_target = targetObj.gameObject.transform;
                //Vector3 target = _target.position;
                //target.y = this.transform.position.y;

                //transform.position = PlayerPosition.transform.position;
                ////targetPos = targetObj.transform.position;
                //this.transform.LookAt(target);


                //if (Input.GetKeyDown("v"))
                //{
                //    ////村人のダウン状態のエリアのアクティベート
                //    //transform.Find("EnemyDownArea").gameObject.SetActive(true);
                //    //村人の状態を"Drag"に変更
                //    murabitoState = MurabitoState.Down;
                //    //アニメーション
                //    //animator.SetBool("drag", false);
                //}

                break;

            case MurabitoState.Hide:
                transform.parent = null;

                this.gameObject.SetActive(false);

                break;

        }
        //print(murabitoState);
    }
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag != "Player") return;

    //    //通常or移動→ダウン
    //    //if ((murabitoState == MurabitoState.Idle || murabitoState == MurabitoState.Walk)
    //    //    && Input.GetKeyDown("v"))
    //    //{
    //    //    //村人の裏取り判定の撤去
    //    //    transform.Find("Behind").gameObject.SetActive(false);
    //    //    //村人のダウン状態のエリアのアクティベート
    //    //    transform.Find("DownArea").gameObject.SetActive(true);
    //    //    //村人の状態を"Down"に変更
    //    //    murabitoState = MurabitoState.Down;
    //    //    ////アニメーション
    //    //    //animator.SetBool("down", true);
    //    //}

    //    ////ダウン→引きずり
    //    //if (murabitoState == MurabitoState.Down && Input.GetKeyDown("v"))
    //    //{
    //    //    ////村人のダウン状態のエリアのアクティベート
    //    //    //transform.Find("EnemyDownArea").gameObject.SetActive(true);
    //    //    //村人の状態を"Drag"に変更
    //    //    murabitoState = MurabitoState.Drag;
    //    //    ////アニメーション
    //    //    //animator.SetBool("drag", true);
    //    //}
    //    //引きずり→ダウン
    //    //else if (murabitoState == MurabitoState.Drag && Input.GetKeyDown("v"))
    //    //{
    //    //    ////村人のダウン状態のエリアのアクティベート
    //    //    //transform.Find("EnemyDownArea").gameObject.SetActive(true);
    //    //    //村人の状態を"Drag"に変更
    //    //    murabitoState = MurabitoState.Down;
    //    //    //アニメーション
    //    //    animator.SetBool("drag", false);
    //    //}
    //}

    public MurabitoState GetState()
    {
        return murabitoState;
    }
    public void SetState(MurabitoState state)
    {
        murabitoState = state;
    }
}
