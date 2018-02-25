using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlot : MonoBehaviour {

    //　アイテム情報のスロットプレハブ
    [SerializeField]
    private GameObject slot;
    //　主人公のステータス
    [SerializeField]
    private MyItemStatus myItemStatus;
    //　アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;


    //　アクティブになった時
    void OnEnable()
    {
        if (itemDataBase == null || myItemStatus == null)
        {
            itemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
            myItemStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<MyItemStatus>();
        }

        //　アイテムデータベースに登録されているアイテム用のスロットを全作成
        CreateSlots(itemDataBase.GetItemData());
    }

    //　アイテムスロットの作成
    public void CreateSlots(ItemData[] itemLists)
    {

        int i = 0;

        foreach (var item in itemLists)
        {
            if (myItemStatus.GetItemFlag(item.GetItemType()))
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
                instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
            }
        }
    }
}
