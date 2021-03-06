﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Novel;


//商人クラス
public class Merchant : MonoBehaviour
{


    //日付
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

    // Use this for initialization
    void Start()
    {
        if(AwakeData.Instance.merchantNum < AwakeData.Instance.dayNum_)
        {
            AwakeData.Instance.firstkind = false;
            AwakeData.Instance.secondkind = false;
            AwakeData.Instance.thirdkind = false;
            AwakeData.Instance.merchantNum = AwakeData.Instance.dayNum_;
        }
        m_ItemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
        m_RequestDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RequestDataBase>();
        if (m_myItemStatus == null)
        {
            m_myItemStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<MyItemStatus>();
        }
        m_day = AwakeData.Instance.dayNum_;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_myItemStatus == null)
        {
            return;
        }
        m_day = AwakeData.Instance.dayNum_;
        RequestItems(m_RequestDataBase.GetRequeatData());

        //if(m_ItemDataBase!=null)
        m_isAchieved = RequestCleared(m_ItemDataBase.GetItemData());
    }

    //データの格納用
    public void RequestItems(RequestData[] requestLists)
    {
        foreach (var request in requestLists)
        {
            m_RequestData = request;
            if (m_RequestData.GetDay() == m_day)
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
    public bool RequestCleared(ItemData[] itemData)
    {
        bool item1 = false;
        bool item2 = false;
        AwakeData.Instance.item1_ = false;
        AwakeData.Instance.item2_ = false;
        foreach (var item in itemData)
        {
            //依頼品がアイテムデータベースにある&&所持している
            if ((item.GetItemType() == requestItem1) && m_myItemStatus.GetItemFlag(requestItem1))
            {
                //Debug.Log("item1は所持");
                item1 = true;
                AwakeData.Instance.item1_ = true;
            }
        }
        foreach (var item in itemData)
        {
            //依頼品がアイテムデータベースにある&&所持している
            if ((item.GetItemType() == requestItem2) && m_myItemStatus.GetItemFlag(requestItem2))
            {
                //Debug.Log("item2は所持");
                item2 = true;
                AwakeData.Instance.item2_ = true;
            }
        }
        return (item1 && item2);
    }

    public void talkMethod()
    {
        //はじめの会話
        if (!AwakeData.Instance.firstkind)
        {
            AwakeData.Instance.firstkind = true;
            NovelSingleton.StatusManager.callJoker("wide/merchant_text/start_" + AwakeData.Instance.dayNum_, "");
        }
        //両方持ってないとき1回目
        else if (!AwakeData.Instance.item1_ && !AwakeData.Instance.item2_ && !AwakeData.Instance.secondkind)
        {
            AwakeData.Instance.secondkind = true;
            NovelSingleton.StatusManager.callJoker("wide/merchant_text/nothing_" + AwakeData.Instance.dayNum_, "");
        }
        //片方だけ初めて持ってきたとき一回目
        else if (AwakeData.Instance.item1_ && !AwakeData.Instance.item2_ && !AwakeData.Instance.thirdkind || !AwakeData.Instance.item1_ && AwakeData.Instance.item2_ && !AwakeData.Instance.thirdkind)
        {
            AwakeData.Instance.thirdkind = true;
            NovelSingleton.StatusManager.callJoker("wide/merchant_text/not", "");
        }
        //すべてが集まってるとき
        else if (AwakeData.Instance.item1_ && AwakeData.Instance.item2_)
        {
            AwakeData.Instance.talkFlag = true;
            NovelSingleton.StatusManager.callJoker("wide/merchant_text/clear", "");

        }
        //集まりきってないときの二回めの会話
        else
        {
            NovelSingleton.StatusManager.callJoker("wide/merchant_text/nothing", "");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            AwakeData.Instance.talkTimeFlag = true;
            talkMethod();
        }
    }
}