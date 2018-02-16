using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconDate : MonoBehaviour {

    private ItemIcon[] itemIcon = new ItemIcon[21];

    void Awake()
    {
        //　アイテムの全情報を作成
        itemIcon[0] = new ItemIcon(0, Resources.Load("まっしろ/atomball", typeof(Sprite)) as Sprite, "矢の束", ChangeIcon.TextureID.Texture01);
        itemIcon[1] = new ItemIcon(1, Resources.Load("まっしろ/axe", typeof(Sprite)) as Sprite, "斧", ChangeIcon.TextureID.Texture02);
        itemIcon[2] = new ItemIcon(2, Resources.Load("まっしろ/bow", typeof(Sprite)) as Sprite, "樽", ChangeIcon.TextureID.Texture03);
        itemIcon[3] = new ItemIcon(3, Resources.Load("まっしろ/closeTreasurebox", typeof(Sprite)) as Sprite, "ベッド", ChangeIcon.TextureID.Texture04);
        itemIcon[4] = new ItemIcon(4, Resources.Load("まっしろ/coin", typeof(Sprite)) as Sprite, "弓", ChangeIcon.TextureID.Texture05);
        itemIcon[5] = new ItemIcon(5, Resources.Load("まっしろ/crystal", typeof(Sprite)) as Sprite, "椅子", ChangeIcon.TextureID.Texture06);
        itemIcon[6] = new ItemIcon(6, Resources.Load("まっしろ/crystalball", typeof(Sprite)) as Sprite, "箪笥", ChangeIcon.TextureID.Texture07);
        itemIcon[7] = new ItemIcon(7, Resources.Load("まっしろ/dragon", typeof(Sprite)) as Sprite, "水晶", ChangeIcon.TextureID.Texture08);
        itemIcon[8] = new ItemIcon(8, Resources.Load("まっしろ/gold", typeof(Sprite)) as Sprite, "入れ歯", ChangeIcon.TextureID.Texture09);
        itemIcon[9] = new ItemIcon(9, Resources.Load("まっしろ/goldDenter", typeof(Sprite)) as Sprite, "毛皮", ChangeIcon.TextureID.Texture10);
        itemIcon[10] = new ItemIcon(10, Resources.Load("まっしろ/head", typeof(Sprite)) as Sprite, "布団", ChangeIcon.TextureID.Texture11);
        itemIcon[11] = new ItemIcon(11, Resources.Load("まっしろ/holy", typeof(Sprite)) as Sprite, "壺", ChangeIcon.TextureID.Texture12);
        itemIcon[12] = new ItemIcon(12, Resources.Load("まっしろ/houseki", typeof(Sprite)) as Sprite, "桑", ChangeIcon.TextureID.Texture13);
        itemIcon[13] = new ItemIcon(13, Resources.Load("まっしろ/magicpot", typeof(Sprite)) as Sprite, "鍋", ChangeIcon.TextureID.Texture14);
        itemIcon[14] = new ItemIcon(14, Resources.Load("まっしろ/openTreasurebox", typeof(Sprite)) as Sprite, "自画像", ChangeIcon.TextureID.Texture15);
        itemIcon[15] = new ItemIcon(15, Resources.Load("まっしろ/painting", typeof(Sprite)) as Sprite, "剣", ChangeIcon.TextureID.Texture16);
        itemIcon[16] = new ItemIcon(16, Resources.Load("まっしろ/sheld", typeof(Sprite)) as Sprite, "花瓶", ChangeIcon.TextureID.Texture17);
        itemIcon[17] = new ItemIcon(17, Resources.Load("まっしろ/silver", typeof(Sprite)) as Sprite, "野菜", ChangeIcon.TextureID.Texture18);
        itemIcon[18] = new ItemIcon(18, Resources.Load("まっしろ/silverDenter", typeof(Sprite)) as Sprite, "村長の像", ChangeIcon.TextureID.Texture19);
        itemIcon[19] = new ItemIcon(19, Resources.Load("まっしろ/wickerman", typeof(Sprite)) as Sprite, "ウィッカーマンの像", ChangeIcon.TextureID.Texture20);
        itemIcon[20] = new ItemIcon(20, Resources.Load("まっしろ/sword", typeof(Sprite)) as Sprite, "木箱", ChangeIcon.TextureID.Texture21);
    }


    public ItemIcon[] GetItemData()
    {
        return itemIcon;
    }

    public int GetItemTotal()
    {
        return itemIcon.Length;
    }
}
