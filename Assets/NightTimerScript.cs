using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NightTimerScript : MonoBehaviour {
    public string Scene_;
    //public bool inout_=true;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (AwakeData.Instance.inout_ == true)
        {
            AwakeData.Instance.worldTime_ += Time.deltaTime;
        }

        if (AwakeData.Instance.worldTime_ >= 60.0f)
        {
            AwakeData.Instance.worldTime_ = AwakeData.Instance.worldTime_ - 60;
            AwakeData.Instance.worldMinite_++;
        }
        if (AwakeData.Instance.worldMinite_ >= 5)
        {
            SceneManager.LoadScene(Scene_);
        }
        this.GetComponent<Text>().text = AwakeData.Instance.worldMinite_.ToString("00") + ":" + AwakeData.Instance.worldTime_.ToString("00");
    }
}
