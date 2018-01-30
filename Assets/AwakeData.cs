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
    //盗品どこの家のものが盗まれたか
    public List<bool> stealList = new List<bool>();
    //家に盗めるものが何個あるか
    public List<int> stealNumList = new List<int>();
    //お宝識別
    public List<bool> stealTypeList = new List<bool>();
    //不審度特殊会話したかどうか
    public List<int> FdoutList = new List<int>();
    //恐怖値
    public List<int> fearList  = new List<int>();
    //不審度
    public List<int> doutList  = new List<int>();
    //過ぎた時間
    public float worldTime_;    //カメラポジション保存位置
    public Vector3 cameraPosition_;
    //カメラ角度保存位置
    public Quaternion cameraRotate_;
    //プレイヤーのポジション保存位置
    public Vector3 playerPosition_;
    //プレイヤーの角度
    public Quaternion playerRotation_;
    //朝昼夕
    public int dayTime_;//0:朝1:昼2:夜
    //ポジションセットすべきかどうか
    public bool posSet;


    public int checkNum;

    public List<string> stoleObj = new List<string>();   //盗まれたもの
    public int maxMass;//最大限もてる重さ
    public int mass;//現在の重さ


    public bool inout_;
    //時間分
    public int worldMinite_;
    //室内かどうかtrueで室内falseで屋外
    public bool indoorCheck_;
    //家のナンバー0～15 0はnull
    public int houseNum_;

    public bool cameraFrag_;

    public GameObject player;

}