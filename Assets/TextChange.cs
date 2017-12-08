using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    // Use this for initialization
    public int checkNum_;
    void Start()
    {
        checkNum_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkNum_ = AwakeData.Instance.checkNum;
        if (AwakeData.Instance.fearList[checkNum_] > 100)
        {
            AwakeData.Instance.fearList[checkNum_] = 100;
        }
        if (AwakeData.Instance.fearList[checkNum_] < 0)
        {
            AwakeData.Instance.fearList[checkNum_] = 0;
        }

        this.GetComponent<Text>().text = "恐怖度" + AwakeData.Instance.fearList[checkNum_];
    }
}
