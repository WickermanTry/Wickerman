using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconImage : MonoBehaviour {
    //アイテム情報のスロット
    [SerializeField]
    private GameObject slot;
    //主人公のステータス
    [SerializeField]
    private ChangeIcon changeIcon;
    //アイコンデータベース
    [SerializeField]
    private ItemIconDate iconData;
	
    void OnEnable()
    {
        CreateSlot(iconData.GetIconData());
    }

	public void CreateSlot(ItemIcon[] itemLists)
    {
        int i = 0;
        foreach(var item in itemLists)
        {
            if (changeIcon.GeticonFlag(item.GetIconType()))
            {
                //スロットのインスタンス化
                var instanceSlot = Instantiate(slot) as GameObject;
                //スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "IconSlot" + i++;
                //親をMainにする
                instanceSlot.transform.SetParent(transform);
                //Scaleを設定しないと０になるので設定
                instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                //アイテム情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<ProcessingIconSlot>().SetIconData(item);
            }
        }
    }
}
