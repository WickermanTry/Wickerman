using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upbutton : MonoBehaviour
{
    public int Number_;
    bool Numberswitch_ = true;
    public int checkNum_;
    // Use this for initialization
    void Start()
    {
        checkNum_ = AwakeData.Instance.checkNum;
        if (Number_ == 0)
        {
            Numberswitch_ = true;
        }
        else if (Number_ == 1)
        {
            Numberswitch_ = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushDown()
    {
        checkNum_ = AwakeData.Instance.checkNum;
        if (Numberswitch_ == true)
        {

            AwakeData.Instance.fearList[checkNum_] += 5;
        }
        if (Numberswitch_ == false)
        {

            AwakeData.Instance.fearList[checkNum_] += 5;
        }
    }
}
