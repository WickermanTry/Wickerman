using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum ObjectState
{
    None,   //何もしない
    Idle,   //通常時
    Carry,  //持ち運ぶ（重いもの）
    Hide,   //隠す
    //Have,   //持っている（軽いもの）
}

//動かせる（盗める）もの
public class MoveObjects : MonoBehaviour {

    [SerializeField, Tooltip("状態遷移")]
    private ObjectState m_state = ObjectState.Idle;  //遷移
    public ObjectState state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    private GameObject m_player;
    private BoxCollider m_boxCol;
    //private Vector3 m_velocity = Vector3.zero;

    private ItemDataBase m_itemDataBase;
    private ItemData m_itemData;
    [SerializeField, Tooltip("重さ")]
    private int m_mass;//重さ
    public int mass
    {
        get { return m_mass; }
    }
    [SerializeField, Tooltip("位置")]
    private HavePosition m_havePosition;//位置  
    public HavePosition havePosition
    {
        get { return m_havePosition; }
    }
    [SerializeField, Tooltip("複数もてるか")]
    private bool m_isMultiple;//複数もてるか
    public bool isMultiple
    {
        get { return m_isMultiple; }
    }

    void Awake()
    {
        //DestoryIfExist(this.name);
        DontDestroyOnLoad(this);

    }

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_boxCol = this.GetComponent<BoxCollider>();
        m_itemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
        //m_state = ObjectState.Idle;
        ObjectStatus(m_itemDataBase.GetItemData());
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
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        gameObject.SetActive(false);

                        break;
                    case HavePosition.Before:
                        if (isMultiple == false)
                        {
                            m_boxCol.isTrigger = false;

                        }
                        else
                        {
                            m_boxCol.isTrigger = true;
                        }
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("BeforePosition").transform.localPosition;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Behind:
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("BehindPosition").transform.localPosition;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Side:
                        if (isMultiple == false)
                        {
                            m_boxCol.isTrigger = false;

                        }
                        else
                        {
                            m_boxCol.isTrigger = true;
                        }
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("SidePosition").transform.localPosition;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 90.0f, 90.0f));

                        break;
                    case HavePosition.Push:
                        m_boxCol.isTrigger = false;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("BeforePosition").transform.localPosition;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Pull://
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("BeforePosition").transform.localPosition;
                        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

                        break;
                    case HavePosition.Up://
                        m_boxCol.isTrigger = true;
                        transform.parent = m_player.transform;
                        transform.localPosition = GameObject.Find("UpPosition").transform.localPosition;
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
            //case ObjectState.Have:
            //    m_boxCol.isTrigger = true;
            //    transform.parent = m_player.transform;
            //    transform.localPosition = GameObject.Find("TrailingPosition").transform.position;
            //    transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z));

            //    gameObject.SetActive(false);
            //    break;
            default:
                Debug.LogError("Error:" + m_state);
                break;
        }



    }

    /// <summary>
    ///生成処理 
    /// </summary>
    private void Inisialize()
    {
        //int count = 0;
        ////すでに存在するとき&&プレイヤーが所持してるとき
        ////生成しない
        //GameObject obj = GameObject.Find(this.name);
        //if(GameObject.Find(this.name).name == this.name)
        //{
        //    count++;
        //    Debug.Log("aaaa "+count);
        //}
        //else
        //{
        //    Debug.Log("EEEE " + count);
        //}
    }

    //public void DestoryIfExist(string name)
    //{
    //    var gameObject = GameObject.Find("Player").transform.FindChild(name);
    //    //var gameObjectclone = GameObject.Find("Player").transform.FindChild(name+"(Clone)");

    //    if (gameObject == null)
    //    {
    //        return;
    //    }
    //    GameObject.Destroy(this.gameObject);

    //    //else
    //    //{
    //    //    Debug.Log("aru");
    //    //}


        
    //}

    /// <summary>
    /// ItamDataBaseから値参照用
    /// </summary>
    /// <param name="itemLists"></param>
    private void ObjectStatus(ItemData[] itemLists)
    {
        foreach (var item in itemLists)
        {
            m_itemData = item;
            //配列にヒットした要素があるか
            if ((m_itemData.GetItemType().ToString() == this.name) || (m_itemData.GetItemType().ToString() + "(Clone)" == this.name))
            {
                m_mass = m_itemData.GetItemMass();//重さ
                m_havePosition = m_itemData.GetHavePosition();//位置                
                m_isMultiple = m_itemData.GetIsMultiple();//複数もてるか
            }
        }
    }


}
