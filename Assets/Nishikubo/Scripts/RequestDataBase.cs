using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//商人のデータベース
public class RequestDataBase : MonoBehaviour {

    private RequestData[] requestLists = new RequestData[15];

    void Awake()
    {
        //商人の全情報作成
        requestLists[0] = new RequestData(1, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.CrystalBall, false);
        requestLists[1] = new RequestData(2, "merchant", MyItemStatus.Item.Ax, MyItemStatus.Item.Ax, false);
        requestLists[2] = new RequestData(3, "merchant", MyItemStatus.Item.Barrel, MyItemStatus.Item.Barrel, false);
        requestLists[3] = new RequestData(4, "merchant", MyItemStatus.Item.Bow, MyItemStatus.Item.Chair, false);
        requestLists[4] = new RequestData(5, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[5] = new RequestData(6, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[6] = new RequestData(7, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[7] = new RequestData(8, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[8] = new RequestData(9, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[9] = new RequestData(10, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[10] = new RequestData(11, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[11] = new RequestData(12, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[12] = new RequestData(13, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[13] = new RequestData(14, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);
        requestLists[14] = new RequestData(15, "merchant", MyItemStatus.Item.Arrows, MyItemStatus.Item.Arrows, false);

    }

    public RequestData[] GetRequeatData()
    {
        return requestLists;
    }

}
