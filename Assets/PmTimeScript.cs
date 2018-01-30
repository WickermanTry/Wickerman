using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PmTimeScript : MonoBehaviour
{
    public string Scene_;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        AwakeData.Instance.worldTime_ += Time.deltaTime;

        if (AwakeData.Instance.worldTime_ >= 60.0f)
        {
            AwakeData.Instance.worldTime_ = AwakeData.Instance.worldTime_ - 60;
            AwakeData.Instance.worldMinite_++;
        }
        if (AwakeData.Instance.worldMinite_ >= 5)
        {
            SceneManager.LoadScene(Scene_);
            AwakeData.Instance.worldTime_ = 0;
            AwakeData.Instance.worldMinite_ = 0;
            AwakeData.Instance.houseNum_ = 0;
            AwakeData.Instance.dayTime_ = 0;
            AwakeData.Instance.dayNum_ += 1;
        }
        this.GetComponent<Text>().text = AwakeData.Instance.worldMinite_.ToString("00") + ":" + AwakeData.Instance.worldTime_.ToString("00");
    }
}
