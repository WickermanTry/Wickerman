using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmTimeScript : MonoBehaviour
{
    public string Scene_;
    GameObject gameObject;
    //public bool inout_=true;
    // Use this for initialization
    void Start()
    {
        gameObject = GameObject.Find("StatusTextDebug").transform.Find("BlackFade").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        print(AwakeData.Instance.dayTime_);
        if (AwakeData.Instance.worldTime_ >= 60.0f)
        {
            AwakeData.Instance.worldTime_ = AwakeData.Instance.worldTime_ - 60;
            AwakeData.Instance.worldMinite_++;
        }

        if (AwakeData.Instance.dayTime_ == 0)//朝
        {
            if ((AwakeData.Instance.worldMinite_ >= 1) && AwakeData.Instance.worldTime_ >= 30)
            {
                AwakeData.Instance.worldTime_ = 0;
                AwakeData.Instance.worldMinite_ = 0;
                AwakeData.Instance.dayTime_ = 1;
                //フェードインを呼ぶスクリプト
                gameObject.SetActive(true);
            }
        }
        else if (AwakeData.Instance.dayTime_ == 1)//昼
        {
            if (AwakeData.Instance.worldMinite_ >= 2)
            {
                AwakeData.Instance.worldTime_ = 0;
                AwakeData.Instance.worldMinite_ = 0;
                AwakeData.Instance.dayTime_ = 2;
                //フェードインを呼ぶスクリプト
                gameObject.SetActive(true);
            }
        }
        else if (AwakeData.Instance.dayTime_ == 2)//夕方
        {
            if ((AwakeData.Instance.worldMinite_ >= 1) && AwakeData.Instance.worldTime_ >= 30)
            {
                AwakeData.Instance.dayTime_ = 3;
                AwakeData.Instance.worldTime_ = 0;
                AwakeData.Instance.worldMinite_ = 0;

                //フェードインを呼ぶスクリプト
                //中間へ
                SceneManager.LoadScene(Scene_);
                AwakeData.Instance.houseNum_ = 0;
            }
        }
        AwakeData.Instance.worldTime_ += Time.deltaTime;
        this.GetComponent<Text>().text = AwakeData.Instance.worldMinite_.ToString("00") + ":" + Mathf.Floor(AwakeData.Instance.worldTime_).ToString("00");
    }
}
