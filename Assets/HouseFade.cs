using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HouseFade : MonoBehaviour
{
    public float speed = 0.1f;  //透明化の速さ
    public float speed2 = 0.1f;  //透明化の速さ
    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数

    // Use this for initialization
    void Start()
    {
        //Panelの色を取得
        alfa = 1;
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        print(alfa);
        if (AwakeData.Instance.inout_ == true)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa -= speed;
            if (alfa <= 0) alfa = 0;
        }
        else if (AwakeData.Instance.inout_ == false)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed2;
            if (alfa >= 1) alfa = 1;
        }
    }
}
