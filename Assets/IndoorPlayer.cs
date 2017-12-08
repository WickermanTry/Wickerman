using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorPlayer : MonoBehaviour
{
    enum PlayerState
    {
        Normal
    }
    private PlayerState playerState;
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    private Animator animator;
    public Transform _target;
    public float moveSpeed = 3f;
    private bool ActionTime = false;
    //private bool shinobimode = false;
    //int i = 0;
    //int l = 0;
    public int EventItem_ = 0;
    //public Transform _murabito;
    //private GameObject murabito;
    //Vector3 murabitoPos;

    void Start()
    {
        playerState = PlayerState.Normal;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerState == PlayerState.Normal)
        {
            if (ActionTime == false)//アクション中じゃないことを確認
            {
                //移動入力されてるかチェック
                //されている
                if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a"))
                {
                    animator.SetBool("run_check", true);
                }
                //されていない
                else
                {
                    animator.SetBool("run_check", false);
                }
                //移動入力を保存
                inputHorizontal = Input.GetAxisRaw("Horizontal");
                inputVertical = Input.GetAxisRaw("Vertical");
            }
            else
            {
                inputHorizontal = 0.0f;
                inputVertical = 0.0f;
            }
        }

    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
    void OnTriggerStay(Collider other)
    {
        //print("hit");
        _target = other.gameObject.transform;
        Vector3 target = _target.position;
        target.y = this.transform.position.y;
        if (other.gameObject.tag == ("Item"))
        {
            if (Input.GetKeyDown("q"))
            {
                animator.SetBool("PickUp", true);
                Destroy(other.gameObject);
                ActionTime = true;
                EventItem_ += 1;
                Invoke("ResetMetod", 1.0f);
            }
        }
        if (other.gameObject.tag == ("TalkArea"))
        {
            if (Input.GetKeyDown("q"))
            {
                Destroy(other.gameObject);
                // EventItem_ += 1;
            }
        }
    }
    void ResetMetod()
    {
        animator.SetBool("PickUp", false);
        ActionTime = false;
    }
}

