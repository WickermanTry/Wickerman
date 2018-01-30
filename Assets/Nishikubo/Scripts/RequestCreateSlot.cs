using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//依頼品を表示するクラス
public class RequestCreateSlot : MonoBehaviour {

    //　アイテム情報のスロットプレハブ
    [SerializeField]
    private GameObject slot;
    //　アイテムデータベース
    private ItemDataBase m_ItemDataBase;
    //依頼品データベース
    private RequestDataBase m_RequestDataBase;


    //日付
    [SerializeField]
    private int m_day;


    //　アクティブになった時
    void OnEnable()
    {
        m_day = AwakeData.Instance.dayNum_;

        if (m_ItemDataBase == null || m_RequestDataBase == null)
        {
            m_ItemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
            m_RequestDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RequestDataBase>();

        }

        //依頼品データベースに登録されているアイテム用のスロットを全作成
        CreateSlots(m_RequestDataBase.GetRequeatData());
    }


    //　アイテムスロットの作成
    public void CreateSlots(RequestData[] requestLists)
    {

        int i = 0;

        foreach (var item in requestLists)
        {
            //日付と同じ要素を取り出す
            if (m_day==item.GetDay())
            {
                //　スロットのインスタンス化
                var instanceSlot = Instantiate(slot) as GameObject;
                //　スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "ItemSlot" + i++;
                //　親をMainにする
                instanceSlot.transform.SetParent(transform);
                //　Scaleを設定しないと0になるので設定
                instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                //　アイテム情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<RequestProcessingSlot>().SetRequestData(item, m_ItemDataBase.GetItemData());
                //要素が一つでないとき
                if (item.GetRequestItem1() != item.GetRequestItem2())
                {
                    //　スロットのインスタンス化
                    var instanceSlot2 = Instantiate(slot) as GameObject;
                    //　スロットゲームオブジェクトの名前を設定
                    instanceSlot2.name = "ItemSlot" + i++;
                    //　親をMainにする
                    instanceSlot2.transform.SetParent(transform);
                    //　Scaleを設定しないと0になるので設定
                    instanceSlot2.transform.localScale = new Vector3(1f, 1f, 1f);
                    //　アイテム情報をスロットのProcessingSlotに設定する
                    instanceSlot2.GetComponent<RequestProcessingSlot>().SetRequestData2(item, m_ItemDataBase.GetItemData());
                }

            }

        }

    }


}
