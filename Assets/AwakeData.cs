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
    public List<int> fearList = new List<int>();
    //不審度
    public List<int> doutList = new List<int>();
    //過ぎた時間
    public float worldTime_;
    //過ぎた時間(分)
    public float worldMinite_;
    //プレイヤーのポジション保存位置
    public Vector3 playerPosition_;
    //プレイヤーのローテーション保存
    public Quaternion playerRotation_;
    //カメラポジション保存位置
    public Vector3 cameraPosition_;
    //カメラ角度保存位置
    public Quaternion cameraRotate_;
    //朝昼夕
    public int dayTime_;
    //ポジションセットすべきかどうか
    public bool posSet;

    public int checkNum;
    //室内、室外の移動のfadein　fadeout用
    public bool inout_ = true;

    public List<string> stoleObj = new List<string>();   //盗まれたもの
    public int maxMass;//最大限もてる重さ
    public int mass;//現在の重さ

    public GameObject player;
}