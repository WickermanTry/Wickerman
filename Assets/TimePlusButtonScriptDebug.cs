using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlusButtonScriptDebug : MonoBehaviour
{
    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        GameObject gameObject = GameObject.Find("TimeTextDebug");
        SetTimerScript setTime = gameObject.GetComponent<SetTimerScript>();
        setTime.second += 10;
    }
}

