using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Text;

public class Request : MonoBehaviour {

    //テキストID
    protected string TextID;
    public int DayNum_ = 1;
    public int a = 1;
    public Text text;
    public string check_;

    // Use this for initialization
    public void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        AwakeData.Instance.dayNum_ = a;
        DayNum_ = AwakeData.Instance.dayNum_;
        SetTextIDMethod();
    }
    public void SetTextIDMethod()
    {
        check_ = "day" + AwakeData.Instance.dayNum_.ToString();
        //データを取り出してリストに追加するスクリプト
        //☆＊ここに数字＊を消して当てはまる数字を入れる
        var nList = new RequestGoal.NList("Texts/RequestGoal", true);
        Debug.Log("a");
        TextID = nList.ToList().Find(x => x.Id == check_).Num1;
        this.GetComponent<Text>().text = TextID;
    }
}
