using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurabitoScript : MonoBehaviour
{
    enum MurabitoState
    {
        Normal,
        Down,
        Hikizurare
    }
    private Animator animator;
    private MurabitoState murabitoState;
    public GameObject targetObj;
    Vector3 targetPos;
    public Transform _target;
    private GameObject PlayerPosition;
    // Use this for initialization
    void Start()
    {
        murabitoState = MurabitoState.Normal;
        animator = GetComponent<Animator>();
        targetObj = GameObject.Find("Player").transform.Find("HikizuriPosition").gameObject;
        targetPos = targetObj.transform.position;
        PlayerPosition = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //targetObj = GameObject.Find("HikizuriPosition");
        if (murabitoState == MurabitoState.Normal)
        {

        }
        else if (murabitoState == MurabitoState.Down)
        {

        }
        else if (murabitoState == MurabitoState.Hikizurare)
        {
            print("kita");
            _target = targetObj.gameObject.transform;
            Vector3 target = _target.position;
            target.y = this.transform.position.y;

            transform.position = PlayerPosition.transform.position;
            //targetPos = targetObj.transform.position;
            this.transform.LookAt(target);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerAttack" && murabitoState == MurabitoState.Normal)
        {
            //村人の裏取り判定の撤去
            GameObject.Find("Murabito").transform.Find("EnemyBehind").gameObject.SetActive(false);
            //村人のダウン状態のエリアのアクティベート
            GameObject.Find("Murabito").transform.Find("EnemyDownArea").gameObject.SetActive(true);
            murabitoState = MurabitoState.Down;
            animator.SetBool("down", true);
        }
        if (other.gameObject.tag == "HikizuriPosition" && Input.GetKeyDown("v"))
        {
            //print("in");
            if (murabitoState == MurabitoState.Down)
            {
                GameObject.Find("Murabito").transform.Find("EnemyDownArea").gameObject.SetActive(false);
                murabitoState = MurabitoState.Hikizurare;
                animator.SetBool("Hikizurare", true);
                this.gameObject.tag = "Hikizurare";
            }
            else if (murabitoState == MurabitoState.Hikizurare)
            {
                GameObject.Find("Murabito").transform.Find("EnemyDownArea").gameObject.SetActive(true);
                murabitoState = MurabitoState.Down;
                animator.SetBool("Hikizurare", false);
                this.gameObject.tag = "Murabito";
            }
        }
    }
}
