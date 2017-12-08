using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimerScript : MonoBehaviour
{
    public int minite;
    public float second;
    private int oldSecond;
    private bool timerFlag = true;
    public int dayTime = 0; //0 = 朝 1 = 昼 2 = 夕方
    GameObject gameObject;

    // Use this for initialization
    void Start()
    {
        minite = 0;
        second = 0;
        oldSecond = 0;
        gameObject =  GameObject.Find("StatusTextDebug").transform.Find("BlackFade").gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale > 0)
        {
            second += Time.deltaTime;
            if (second >= 60.0f)
            {
                second = second - 60;
                minite++;
            }
        }
        if(dayTime == 0)//朝
        {
            if((minite >= 1) && second >= 30)
            {
                second = 0;
                minite = 0;
                dayTime = 1;
                //フェードインを呼ぶスクリプト
                gameObject.SetActive(true);
            }
        }
        else if (dayTime == 1)//昼
        {
            if(minite >= 2)
            {
                second = 0;
                minite = 0;
                dayTime = 2;
                //フェードインを呼ぶスクリプト
                gameObject.SetActive(true);
            }
        }
        else if(dayTime == 2)//夕方
        {
            if ((minite >= 1) && second >= 30)
            {
                second = 0;
                minite = 0;
                //フェードインを呼ぶスクリプト
                //中間へ
            }
        }
        this.GetComponent<Text>().text = minite.ToString("00") + ":" + second.ToString("00");
    }
}
