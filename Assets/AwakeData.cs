using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwakeData
{
    public readonly static AwakeData Instance = new AwakeData();
    //日付
    public int dayNum_;
    
    //村人のライフ
    public List<int> MlifeList = new List<int>();
    //不審度特殊会話したかどうか
    public List<int> FdoutList = new List<int>();
    //恐怖値
    public List<int> fearList  = new List<int>();
    //不審度
    public List<int> doutList  = new List<int>();
    //過ぎた時間
    public float worldTime_;
    //プレイヤーのポジション保存位置
    public Vector3 playerPosition_;
    //朝昼夕
    public int dayTime_;//0:朝1:昼2:夜
    //ポジションセットすべきかどうか
    public bool posSet;
    //デバック用
    public int checkNum;

    public int maxMass;//最大限もてる重さ
    public int mass;//現在の重さ



    public int sacrificeCount;//生贄のカウント
    
    public bool inout_;
    //時間分
    public int worldMinite_;

    public GameObject player;

    //室内かどうかtrueで室内falseで屋外
    public bool indoorCheck_;
    //家のナンバー0～15 0はnull
    public int houseNum_;
}