using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//商人のデータ
public class RequestData : object {

    //日付
    private int day;
    //表示するテキスト
    private string talkText;
    //依頼品1
    private MyItemStatus.Item requestItem1;
    //依頼品2
    private MyItemStatus.Item requestItem2;
    //達成したかどうか
    private bool isAchieved;

    /// <summary>
    /// 商人の各種設定
    /// </summary>
    /// <param name="day">日付</param>
    /// <param name="talkText">表示するテキスト</param>
    /// <param name="requestItem1">依頼品1</param>
    /// <param name="requestItem2">依頼品2</param>
    /// <param name="isAchieved">達成したかどうか</param>
    public RequestData(int day,string talkText,MyItemStatus.Item requestItem1,MyItemStatus.Item requestItem2,bool isAchieved)
    {
        this.day = day;
        this.talkText = talkText;
        this.requestItem1 = requestItem1;
        this.requestItem2 = requestItem2;
        this.isAchieved = isAchieved;
    }

    public int GetDay()
    {
        return day;
    }

    public string GetTalkText()
    {
        return talkText;
    }

    public MyItemStatus.Item GetRequestItem1()
    {
        return requestItem1;
    }

    public MyItemStatus.Item GetRequestItem2()
    {
        return requestItem2;
    }

    public bool GetIsAchieved()
    {
        return isAchieved;
    }

    public bool SetIsAchieved(bool flag)
    {
        return isAchieved = flag;
    }

}
