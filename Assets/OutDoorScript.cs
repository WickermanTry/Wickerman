using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutDoorScript : MonoBehaviour {

    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数
    public GameObject target;
    doorImageScript DoorImage_;
    // Use this for initialization
    void Start()
    {
        //Panelの色を取得
        alfa = 1;
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        DoorImage_ = target.GetComponent<doorImageScript>();
    }

    // Update is called once per frame
    void Update()
    {

        print(alfa);
        if (DoorImage_.inflag == true)
        {

            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa = 1;

        }
        else if (DoorImage_.inflag == false)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa = 0;

        }
    }
}
