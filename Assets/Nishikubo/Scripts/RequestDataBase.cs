using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//商人のデータベース
public class RequestDataBase : MonoBehaviour {

    private RequestData[] requestLists = new RequestData[15];

    void Awake()
    {
        //商人の全情報作成
        requestLists[0] = new RequestData(1, "merchant", MyItemStatus.Item.Coin, MyItemStatus.Item.ThornBall, false);
        requestLists[1] = new RequestData(2, "merchant", MyItemStatus.Item.HolyGrail, MyItemStatus.Item.HolyGrail, false);
        requestLists[2] = new RequestData(3, "merchant", MyItemStatus.Item.GoldDenture, MyItemStatus.Item.CrystalBall, false);
        requestLists[3] = new RequestData(4, "merchant", MyItemStatus.Item.MagicLamp, MyItemStatus.Item.MagicLamp, false);
        requestLists[4] = new RequestData(5, "merchant", MyItemStatus.Item.Shield, MyItemStatus.Item.Crystal, false);
        requestLists[5] = new RequestData(6, "merchant", MyItemStatus.Item.VillageHeadmanImage, MyItemStatus.Item.VillageHeadmanImage, false);
        requestLists[6] = new RequestData(7, "merchant", MyItemStatus.Item.Ax, MyItemStatus.Item.Ax, false);
        requestLists[7] = new RequestData(8, "merchant", MyItemStatus.Item.SelfPortrait, MyItemStatus.Item.SelfPortrait, false);
        requestLists[8] = new RequestData(9, "merchant", MyItemStatus.Item.SilverDenture, MyItemStatus.Item.OpenTreasureChest, false);
        requestLists[9] = new RequestData(10, "merchant", MyItemStatus.Item.Arrow, MyItemStatus.Item.Sword, false);
        requestLists[10] = new RequestData(11, "merchant", MyItemStatus.Item.DragonImage, MyItemStatus.Item.DragonImage, false);
        requestLists[11] = new RequestData(12, "merchant", MyItemStatus.Item.CelestialModel, MyItemStatus.Item.CelestialModel, false);
        requestLists[12] = new RequestData(13, "merchant", MyItemStatus.Item.GoldIngot, MyItemStatus.Item.SilverIngot, false);
        requestLists[13] = new RequestData(14, "merchant", MyItemStatus.Item.CloseTreasureChest, MyItemStatus.Item.Jewelry, false);
        requestLists[14] = new RequestData(15, "merchant", MyItemStatus.Item.WickermanImage, MyItemStatus.Item.WickermanImage, false);

    }

    public RequestData[] GetRequeatData()
    {
        return requestLists;
    }

}
