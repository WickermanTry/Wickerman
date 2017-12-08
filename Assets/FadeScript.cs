using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{

    public float speed = 0.04f;  //透明化の速さ
    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数
    private bool fadeout = true;
    void Start()
    {
        //Panelの色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout == true)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
            if (alfa >= 1.0f)
            {
                //Murabito18Script m18 = murabito18.GetComponent<Murabito18Script>();
                //m18.PositionSet();
                
                Invoke("DelayMethodOff", 0.5f);
            }
        }
        else if (fadeout == false)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa -= speed;
            if (alfa <= 0)
            {
                FadeEndMethod();
            }
        }

    }
    void DelayMethodOff()
    {
        fadeout = false;
       
    }
    void FadeEndMethod()
    {
        fadeout = true;
        this.gameObject.SetActive(false);
    }
}

