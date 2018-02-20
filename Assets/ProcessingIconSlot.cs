using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingIconSlot : MonoBehaviour
{
    public ItemIcon itemIcon;

    public void SetIconData(ItemIcon icon)
    {
        itemIcon = icon;
        transform.GetChild(0).GetComponent<Image>().sprite = itemIcon.GetIconSprite();
    }
}