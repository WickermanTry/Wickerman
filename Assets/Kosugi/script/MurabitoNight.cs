using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurabitoNight : MonoBehaviour
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

    //プレイヤー
    private GameObject PlayerPosition;

    void Start()
    {
        murabitoState = MurabitoState.Idle;
        animator = GetComponent<Animator>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
    }


    void Update()
    {
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
                break;
            //引きずり
            case MurabitoState.Drag:
                //アニメーション
                animator.SetBool("drag", true);

                transform.Find("DownArea").gameObject.SetActive(false);


                transform.parent = PlayerPosition.transform;


                transform.position = GameObject.Find("TrailingPosition").transform.position;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                break;

            case MurabitoState.Hide:
                this.gameObject.SetActive(false);
                break;

        }
    }

    public MurabitoState GetState()
    {
        return murabitoState;
    }
    public void SetState(MurabitoState state)
    {
        murabitoState = state;
    }
}
