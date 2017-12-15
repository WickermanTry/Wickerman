using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

enum ValueName
{
    Fear,
    Dout,
    MurabitoNum
}

public class DebugValue : MonoBehaviour
{
    [SerializeField, Header("ボタンの種類")]
    ValueName mValueName;
    [SerializeField, Header("村人の番号")]
    private int mMurabitoNum;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mMurabitoNum = int.Parse(GameObject.Find("MurabitoNumber").GetComponent<Text>().text);

        switch (mValueName)
        {
            case ValueName.Fear:
                Fear();
                break;
            case ValueName.Dout:
                Dout();
                break;
            case ValueName.MurabitoNum:
                Murabito();
                break;
        }
    }

    void Fear()
    {
        if (AwakeData.Instance.fearList[mMurabitoNum] > 100)
        {
            AwakeData.Instance.fearList[mMurabitoNum] = 100;
        }
        if (AwakeData.Instance.fearList[mMurabitoNum] < 0)
        {
            AwakeData.Instance.fearList[mMurabitoNum] = 0;
        }

        GetComponent<Text>().text = "恐怖度:" + AwakeData.Instance.fearList[mMurabitoNum].ToString();
    }
    void Dout()
    {
        if (AwakeData.Instance.doutList[mMurabitoNum] > 100)
        {
            AwakeData.Instance.doutList[mMurabitoNum] = 100;
        }
        if (AwakeData.Instance.doutList[mMurabitoNum] < 0)
        {
            AwakeData.Instance.doutList[mMurabitoNum] = 0;
        }

        GetComponent<Text>().text = "不審度:" + AwakeData.Instance.doutList[mMurabitoNum].ToString();
    }
    void Murabito()
    {
        if (int.Parse(GetComponent<Text>().text) >= 30)
        {
            GetComponent<Text>().text = 30.ToString();
        }
        if (int.Parse(GetComponent<Text>().text) <= 1)
        {
            GetComponent<Text>().text = 1.ToString();
        }
    }
}