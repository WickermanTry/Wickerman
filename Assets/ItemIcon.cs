using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIcon : MonoBehaviour {

    //番号
    private int number;
    //　アイテムのImage画像
    private Sprite iconSprite;
    //　アイテムの名前
    private string iconName;
    private ChangeIcon.TextureID textureType;

    /// <summary>
    /// アイテムの各種設定
    /// </summary>
    /// <param name="image">表示するイメージ</param>
    public ItemIcon(int number, Sprite image, string itemName, ChangeIcon.TextureID textureType)
    {
        this.number = number;
        this.iconSprite = image;
        this.iconName = itemName;
        this.textureType = textureType;
    }

    public int GetIconNumber()
    {
        return number;
    }

    public Sprite GetIconSprite()
    {
        return iconSprite;
    }
    public string GetIconName()
    {
        return iconName;
    }

    public ChangeIcon.TextureID GetIconType()
    {
        return textureType;
    }
}
