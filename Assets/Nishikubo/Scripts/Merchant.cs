using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//商人クラス
public class Merchant : MonoBehaviour {

    
    //日付
    [SerializeField]
    private int m_day;
    public int day
    {
        get { return m_day; }
    }
    //表示するテキスト
    private string m_talkText;
    public string talkText
    {
        get { return m_talkText; }
    }
    //依頼品1
    [SerializeField]
    private MyItemStatus.Item m_requestItem1;
    public MyItemStatus.Item requestItem1
    {
        get { return m_requestItem1; }
    }
    //依頼品2
    [SerializeField]
    private MyItemStatus.Item m_requestItem2;
    public MyItemStatus.Item requestItem2
    {
        get { return m_requestItem2; }
    }
    //達成したかどうか
    private bool m_isAchieved;
    public bool isAchieved
    {
        get { return m_isAchieved; }
    }

    private ItemDataBase m_ItemDataBase;
    private RequestDataBase m_RequestDataBase;
    private RequestData m_RequestData;
    private MyItemStatus m_myItemStatus;

    private bool m_falg = false;//1度のみ



    void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        m_ItemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
        m_RequestDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RequestDataBase>();
        m_myItemStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<MyItemStatus>();
        m_day = AwakeData.Instance.dayNum_;

    }

    // Update is called once per frame
    void Update () {
        m_day = AwakeData.Instance.dayNum_;
        RequestItems(m_RequestDataBase.GetRequeatData());

        //SetRequestClear(m_RequestDataBase.GetRequeatData(),m_ItemDataBase.GetItemData());
    }

    //データの格納用
    public void RequestItems(RequestData[] requestLists)
    {
        foreach (var request in requestLists)
        {
            m_RequestData = request;
            if(m_RequestData.GetDay()==m_day)
            {
                //日付にあったデータを入れる
                m_day = m_RequestData.GetDay();
                m_talkText = m_RequestData.GetTalkText();
                m_requestItem1 = m_RequestData.GetRequestItem1();
                m_requestItem2 = m_RequestData.GetRequestItem2();
                m_isAchieved = m_RequestData.GetIsAchieved();
            }
        }
    }

    //達成してるかどうか
    public void SetRequestClear(RequestData requestData, ItemData[] itemData)
    {
        foreach (var item in itemData)
        {
            //依頼品がアイテムデータベースにある&&所持している
            if ((item.GetItemType() == requestItem1) && m_myItemStatus.GetItemFlag(item.GetItemType()))
            {
                //依頼品を達成している
                //その依頼品のフラグをtrue
                Debug.Log("ok");

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_falg = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("in");
            if (/*Input.GetButtonDown("Steal") &&*/ !m_falg)
            {
                SceneNavigator.Instance.Additive("Talk",0.5f);
                m_falg = true;
            }
        }
    }

}
