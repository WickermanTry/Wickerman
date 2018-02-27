using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour {

    private ItemData[] itemLists = new ItemData[22];

    void Awake()
    {
        //　アイテムの全情報を作成
        //itemLists[0] = new ItemData(0, Resources.Load("Prefabs/Arrows", typeof(Sprite)) as Sprite, "弓矢", MyItemStatus.Item.Arrow, HavePosition.Behind, 1, true);
        itemLists[0] = new ItemData(0, Load(0), "弓矢", MyItemStatus.Item.Arrow, HavePosition.Behind, 1, true);
        itemLists[1] = new ItemData(1, Load(1), "斧", MyItemStatus.Item.Ax, HavePosition.Behind, 3, true);
        itemLists[2] = new ItemData(2, Load(2), "天体模型みたいな玉", MyItemStatus.Item.CelestialModel, HavePosition.None, 2, true);
        itemLists[3] = new ItemData(3, Load(3), "閉じた宝箱", MyItemStatus.Item.CloseTreasureChest, HavePosition.Before, 3, false);
        itemLists[4] = new ItemData(4, Load(4), "コイン", MyItemStatus.Item.Coin, HavePosition.None, 2, true);
        itemLists[5] = new ItemData(5, Load(5), "クリスタル", MyItemStatus.Item.Crystal, HavePosition.None, 2, true);
        itemLists[6] = new ItemData(6, Load(6), "水晶玉", MyItemStatus.Item.CrystalBall, HavePosition.None, 2, true);
        itemLists[7] = new ItemData(7, Load(7), "竜の置物", MyItemStatus.Item.DragonImage, HavePosition.Before, 3, false);
        itemLists[8] = new ItemData(8, Load(8), "金の入れ歯", MyItemStatus.Item.GoldDenture, HavePosition.None, 1, true);
        itemLists[9] = new ItemData(9, Load(9), "金塊", MyItemStatus.Item.GoldIngot, HavePosition.None, 3, true);
        itemLists[10] = new ItemData(10, Load(10), "聖杯", MyItemStatus.Item.HolyGrail, HavePosition.None, 1, true);
        itemLists[11] = new ItemData(11, Load(11), "宝石", MyItemStatus.Item.Jewelry, HavePosition.None, 1, true);
        itemLists[12] = new ItemData(12, Load(12), "魔法のランプ", MyItemStatus.Item.MagicLamp, HavePosition.None, 1, true);
        itemLists[13] = new ItemData(13, Load(13), "中身が見える宝箱", MyItemStatus.Item.OpenTreasureChest, HavePosition.Before, 3, false);
        itemLists[14] = new ItemData(14, Load(14), "村長の自画像", MyItemStatus.Item.SelfPortrait, HavePosition.Side, 2, false);
        itemLists[15] = new ItemData(15, Load(15), "盾", MyItemStatus.Item.Shield, HavePosition.Behind, 3, true);
        itemLists[16] = new ItemData(16, Load(16), "銀の入れ歯", MyItemStatus.Item.SilverDenture, HavePosition.None, 1, true);
        itemLists[17] = new ItemData(17, Load(17), "銀塊", MyItemStatus.Item.SilverIngot, HavePosition.None, 3, true);
        itemLists[18] = new ItemData(18, Load(18), "剣", MyItemStatus.Item.Sword, HavePosition.Behind, 3, true);
        itemLists[19] = new ItemData(19, Load(19), "トゲ玉", MyItemStatus.Item.ThornBall, HavePosition.None, 2, true);
        itemLists[20] = new ItemData(20, Load(20), "村長の像", MyItemStatus.Item.VillageHeadmanImage, HavePosition.Push, 30, false);
        itemLists[21] = new ItemData(21, Load(21), "ウィッカーマンの像", MyItemStatus.Item.WickermanImage, HavePosition.Push, 30, false);

    }


    public ItemData[] GetItemData()
    {
        return itemLists;
    }

    public int GetItemTotal()
    {
        return itemLists.Length;
    }

    public Sprite Load(int spriteNum)
    {
        // Resoucesから対象のテクスチャから生成したスプライト一覧を取得
        Sprite[] sprites = Resources.LoadAll<Sprite>("Prefabs/stealItem");
        // 対象のスプライトを取得
        return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals("stealItem_"+spriteNum.ToString()));
    }
}
