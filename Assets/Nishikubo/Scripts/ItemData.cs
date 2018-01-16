using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : object {

    //番号
    private int number;
    //　アイテムのImage画像
    private Sprite itemSprite;
    //　アイテムの名前
    private string itemName;
    //　アイテムのタイプ
    private MyItemStatus.Item itemType;
    //　アイテムの情報
    //private string itemInformation;
    //持つときの位置指定
    private HavePosition havePosition;
    //重さ
    private int mass;
    //複数もてるか
    private bool isMultiple;

    /// <summary>
    /// アイテムの各種設定
    /// </summary>
    /// <param name="image">表示するイメージ</param>
    /// <param name="itemName">アイテム名</param>
    /// <param name="itemType">どのアイテムか</param>
    /// <param name="havePosition">持つ位置</param>
    /// <param name="mass">重さ</param>
    /// <param name="isMultiple">true:複数持てる,false:複数持てない</param>
    public ItemData(int number, Sprite image, string itemName, MyItemStatus.Item itemType/*, string information*/,HavePosition havePosition,int mass,bool isMultiple)
    {
        this.number = number;
        this.itemSprite = image;
        this.itemName = itemName;
        this.itemType = itemType;
        //this.itemInformation = information;
        this.havePosition = havePosition;
        this.mass = mass;
        this.isMultiple = isMultiple;
    }

    public int GetItemNumber()
    {
        return number;
    }

    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public MyItemStatus.Item GetItemType()
    {
        return itemType;
    }

    //public string GetItemInformation()
    //{
    //    return itemInformation;
    //}

    public HavePosition GetHavePosition()
    {
        return havePosition;
    }

    public int GetItemMass()
    {
        return mass;
    }

    public bool GetIsMultiple()
    {
        return isMultiple;
    }


}
