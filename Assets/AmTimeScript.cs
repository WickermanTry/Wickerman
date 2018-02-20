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
        if (AwakeData.Instance.worldTime_ >= 60.0f)
        {
            AwakeData.Instance.worldTime_ = AwakeData.Instance.worldTime_ - 60;
            AwakeData.Instance.worldMinite_++;
        }
        if (AwakeData.Instance.worldMinite_ >= 5)
        {
            AwakeData.Instance.worldTime_   = 0;
            AwakeData.Instance.worldMinite_ = 0;
            AwakeData.Instance.dayNum_      += 1;
            AwakeData.Instance.houseNum_ = 0;
            SceneManager.LoadScene(Scene_);
            //フェードインを呼ぶスクリプト
            gameObject.SetActive(true);
        }
        AwakeData.Instance.worldTime_ += Time.deltaTime;
        this.GetComponent<Text>().text = AwakeData.Instance.worldMinite_.ToString("00") + ":" + Mathf.Floor(AwakeData.Instance.worldTime_).ToString("00");
    }
}
