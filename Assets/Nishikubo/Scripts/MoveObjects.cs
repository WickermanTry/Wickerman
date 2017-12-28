using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ObjectState
{
    None,   //何もしない
    Idle,   //通常時
    Carry,  //持ち運ぶ（重いもの）
    Hide,   //隠す
    Have,   //持っている（軽いもの）
}

//動かせる（盗める）もの
public class MoveObjects : MonoBehaviour {

    public ObjectState state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField, Tooltip("状態遷移")]
    private ObjectState m_state = ObjectState.None;  //遷移

    private GameObject m_player;
    private BoxCollider m_boxCol;

    private Vector3 m_velocity = Vector3.zero;

    RaycastHit hit;

    private HavePosition m_havePosition = HavePosition.None;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_boxCol = this.GetComponent<BoxCollider>();
        m_havePosition = this.GetComponent<ObjectStatus>().state;

        m_state = ObjectState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
        switch (m_state)
        {
            case ObjectState.None:
                break;
            case ObjectState.Idle:
                gameObject.SetActive(true);
                m_boxCol.isTrigger = false;
                transform.parent = null;//仮

                break;
            case ObjectState.Carry:
                switch (m_havePosition)
                {
                    case HavePosition.None:
                        break;
                    case HavePosition.Before://仮
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Behind://仮
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Side://仮
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Push://仮
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Pull:
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Up://仮
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    default:
                        Debug.Log(m_havePosition);
                        break;
                }


                break;
            case ObjectState.Hide:
                transform.parent = null;//仮
                gameObject.SetActive(false);
                break;
            case ObjectState.Have:
                m_boxCol.isTrigger = true;
                transform.parent = m_player.transform;
                transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Error:" + m_state);
                break;
        }



    }

    //void OnDrawGizmos()
    //{

    //    Vector3 pos = new Vector3(transform.position.x, transform.position.y , transform.position.z);
    //    //Vector3 scale = this.GetComponent<BoxCollider>().size / 2;


    //    //var isHit = Physics.BoxCast(pos, scale, -transform.up, out hit, transform.rotation, 0.5f);
    //    //Physics.Raycast(transform.position, Vector3.down, out floorhit

    //    Ray ray = new Ray(pos, -transform.up);
    //    var isHit = Physics.Raycast(ray,out hit,0.1f);

    //    //if ((Physics.Raycast(ray, out hit, 2)) && damageFlag == false)


    //    if (isHit)
    //    {
    //        Gizmos.DrawRay(pos, (-transform.up*0.1f) * hit.distance);
    //        //Gizmos.DrawWireCube(pos + -transform.up * hit.distance, scale * 2);
    //        if (hit.collider.tag == "Floor")
    //        {
    //            Debug.Log(hit.collider.name);
    //        }
    //    }
    //    else {
    //        //　重力をy方向の速さに足していく
    //        //m_velocity.y += Physics.gravity.y * Time.deltaTime;
    //        ////　１秒間辺りの移動距離を考慮して移動
    //        //transform.position = new Vector3(transform.position.x, m_velocity.y*Time.deltaTime, transform.position.z);
            
    //        ////cCon.Move(m_velocity * Time.deltaTime);

    //        Gizmos.DrawRay(pos, -transform.up);
    //    }



    //}


}
