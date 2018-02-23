using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour {

    private ItemData[] itemLists = new ItemData[21];


    void Awake()
    {
        setIcon();
    }


    public ItemData[] GetItemData()
    {
        if (itemLists[0] == null)
        {
            setIcon();
        }
        return itemLists;
    }

    public int GetItemTotal()
    {
        return itemLists.Length;
    }

    public void setIcon()
    {
        //　アイテムの全情報を作成
        itemLists[0] = new ItemData(0, Resources.Load("ItemIcon/atomball", typeof(Sprite)) as Sprite, "矢の束", MyItemStatus.Item.Arrows, HavePosition.None, 1, true);
        itemLists[1] = new ItemData(1, Resources.Load("ItemIcon/axe", typeof(Sprite)) as Sprite, "斧", MyItemStatus.Item.Ax, HavePosition.None, 2, true);
        itemLists[2] = new ItemData(2, Resources.Load("ItemIcon/bow", typeof(Sprite)) as Sprite, "樽", MyItemStatus.Item.Barrel, HavePosition.Behind, 3, false);
        itemLists[3] = new ItemData(3, Resources.Load("ItemIcon/closeTreasurebox", typeof(Sprite)) as Sprite, "ベッド", MyItemStatus.Item.Bed, HavePosition.Pull, 30, false);
        itemLists[4] = new ItemData(4, Resources.Load("ItemIcon/coin", typeof(Sprite)) as Sprite, "弓", MyItemStatus.Item.Bow, HavePosition.None, 1, true);
        itemLists[5] = new ItemData(5, Resources.Load("ItemIcon/crystal", typeof(Sprite)) as Sprite, "椅子", MyItemStatus.Item.Chair, HavePosition.Before, 2, false);
        itemLists[6] = new ItemData(6, Resources.Load("ItemIcon/crystalball", typeof(Sprite)) as Sprite, "箪笥", MyItemStatus.Item.ChestOfDrawers, HavePosition.Push, 30, false);
        itemLists[7] = new ItemData(7, Resources.Load("ItemIcon/dragon", typeof(Sprite)) as Sprite, "水晶", MyItemStatus.Item.CrystalBall, HavePosition.None, 1, true);
        itemLists[8] = new ItemData(8, Resources.Load("ItemIcon/gold", typeof(Sprite)) as Sprite, "入れ歯", MyItemStatus.Item.Denture, HavePosition.None, 1, true);
        itemLists[9] = new ItemData(9, Resources.Load("ItemIcon/goldDenter", typeof(Sprite)) as Sprite, "毛皮", MyItemStatus.Item.Fur, HavePosition.None, 1, true);
        itemLists[10] = new ItemData(10, Resources.Load("ItemIcon/head", typeof(Sprite)) as Sprite, "布団", MyItemStatus.Item.Futon, HavePosition.Before, 2, false);
        itemLists[11] = new ItemData(11, Resources.Load("ItemIcon/holy", typeof(Sprite)) as Sprite, "壺", MyItemStatus.Item.Jar, HavePosition.Before, 3, false);
        itemLists[12] = new ItemData(12, Resources.Load("ItemIcon/houseki", typeof(Sprite)) as Sprite, "桑", MyItemStatus.Item.Mulberry, HavePosition.None, 2, true);
        itemLists[13] = new ItemData(13, Resources.Load("ItemIcon/magicpot", typeof(Sprite)) as Sprite, "鍋", MyItemStatus.Item.Pot, HavePosition.Up, 2, true);
        itemLists[14] = new ItemData(14, Resources.Load("ItemIcon/openTreasurebox", typeof(Sprite)) as Sprite, "自画像", MyItemStatus.Item.SelfPortrait, HavePosition.Side, 2, false);
        itemLists[15] = new ItemData(15, Resources.Load("ItemIcon/painting", typeof(Sprite)) as Sprite, "剣", MyItemStatus.Item.Sword, HavePosition.None, 3, true);
        itemLists[16] = new ItemData(16, Resources.Load("ItemIcon/sheld", typeof(Sprite)) as Sprite, "花瓶", MyItemStatus.Item.Vase, HavePosition.None, 2, true);
        itemLists[17] = new ItemData(17, Resources.Load("ItemIcon/silver", typeof(Sprite)) as Sprite, "野菜", MyItemStatus.Item.Vegetables, HavePosition.None, 1, true);
        itemLists[18] = new ItemData(18, Resources.Load("ItemIcon/silverDenter", typeof(Sprite)) as Sprite, "村長の像", MyItemStatus.Item.VillageHeadmanImage, HavePosition.Push, 30, false);
        itemLists[19] = new ItemData(19, Resources.Load("ItemIcon/wickerman", typeof(Sprite)) as Sprite, "ウィッカーマンの像", MyItemStatus.Item.WickermanImage, HavePosition.Push, 30, false);
        itemLists[20] = new ItemData(20, Resources.Load("ItemIcon/sword", typeof(Sprite)) as Sprite, "木箱", MyItemStatus.Item.WoodenBox, HavePosition.Before, 3, false);
    }
}
