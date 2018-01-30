using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestProcessingSlot : MonoBehaviour {

    //　アイテムの名前を表示するテキストUIプレハブ
    [SerializeField]
    private GameObject titleUI;
    //　アイテム名を表示するテキストUIをインスタンス化した物を入れておく
    private GameObject uiObj;
    //　自身のアイテムデータを入れておく
    [SerializeField]
    public ItemData myItemData;
    //依頼品のデータを入れる
    [SerializeField]
    public RequestData myRequestData;
    //　主人公のステータス
    [SerializeField]
    private MyItemStatus m_myItemStatus;



    void OnEnable()
    {
        m_myItemStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<MyItemStatus>();

    }

    //　スロットが非アクティブになったら削除
    void OnDisable()
    {
        if (uiObj != null)
        {
            Destroy(uiObj);
        }
        Destroy(gameObject);
    }

    //　依頼品データ１をセット
    public void SetRequestData(RequestData requestData, ItemData[] itemData)
    {
        myRequestData = requestData;
        foreach (var item in itemData)
        {
            if (item.GetItemType() == myRequestData.GetRequestItem1())
            {
                myItemData = item;
                transform.GetChild(0).GetComponent<Image>().sprite = item.GetItemSprite();
                //すでに入手していたら
                if (m_myItemStatus.GetItemFlag(item.GetItemType()))
                {
                    //依頼品を達成している
                    transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);//灰色に
                }

            }
        }
    }

    //　依頼品データ２をセット
    public void SetRequestData2(RequestData requestData, ItemData[] itemData)
    {
        myRequestData = requestData;
        foreach (var item in itemData)
        {
            if (item.GetItemType() == myRequestData.GetRequestItem2())
            {
                myItemData = item;
                transform.GetChild(0).GetComponent<Image>().sprite = item.GetItemSprite();
                //すでに入手していたら
                if (m_myItemStatus.GetItemFlag(item.GetItemType()))
                {
                    //依頼品を達成している
                    transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);//灰色に
                }

            }
        }
    }


    public void MouseOver()
    {
        if (uiObj != null)
        {
            Destroy(uiObj);
        }
        //　アイテムの名前を表示するUIを位置を調整してインスタンス化
        uiObj = Instantiate(titleUI, new Vector2(transform.position.x - 25f, transform.position.y + 25f), Quaternion.identity) as GameObject;
        //　アイテム表示UIの親をPanelにする
        uiObj.transform.SetParent(transform.parent.parent);
        Text ui = uiObj.GetComponentInChildren<Text>();
        //　アイテム表示テキストに自身のアイテムの名前を表示
        ui.text = myItemData.GetItemName();

    }

    public void MouseExit()
    {
        //　マウスポインタがアイテムスロットを出たらアイテム表示UIを削除
        if (uiObj != null)
        {
            //informationText.text = "";
            Destroy(uiObj);
        }
    }



}
