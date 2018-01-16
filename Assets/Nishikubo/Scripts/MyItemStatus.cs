using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムの状態を管理するクラス
public class MyItemStatus : MonoBehaviour {

    public enum Item
    {
        Arrows,
        Ax,
        Barrel,
        Bed,
        Bow,
        Chair,
        ChestOfDrawers,
        CrystalBall,
        Denture,
        Fur,
        Futon,
        Jar,
        Mulberry,
        Pot,
        SelfPortrait,
        Sword,
        Vase,
        Vegetables,
        VillageHeadmanImage,
        WickermanImage,
        WoodenBox,
    }

    //　アイテムを持っているかどうかのフラグ
    [SerializeField]
    private bool[] itemFlags = new bool[21];
    public bool[] GetItemFlagTotal
    {
        get { return itemFlags; }
    }


    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }

    public bool SetItemFlag(int num,bool flag)
    {
        return itemFlags[num] = flag;
    }

}
