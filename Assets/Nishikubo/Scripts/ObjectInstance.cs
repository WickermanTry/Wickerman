using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstance : MonoBehaviour {

    private ItemDataBase itemDataBase;

    void Awake()
    {
        itemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();

        for (int i = 0; i < itemDataBase.GetItemTotal(); i++)
        {
            var obj = Instantiate((GameObject)Resources.Load("Prefabs/StealObjects/" + itemDataBase.GetItemData()[i].GetItemType().ToString()));
            obj.name = itemDataBase.GetItemData()[i].GetItemType().ToString();

        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void CreateObject(ItemData[] itemLists)
    //{

    //    foreach (var item in itemLists)
    //    {
    //            //　スロットのインスタンス化
    //            var instanceSlot = Instantiate(slot) as GameObject;
    //            //　スロットゲームオブジェクトの名前を設定
    //            instanceSlot.name = "ItemSlot" + i++;
    //            //　親をMainにする
    //            instanceSlot.transform.SetParent(transform);
    //            //　Scaleを設定しないと0になるので設定
    //            instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
    //            //　アイテム情報をスロットのProcessingSlotに設定する
    //            instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
    //    }
    //}

}
