using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
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
    }
    public void PushUp(int mStatus)
    {
        if (mStatus == 0)
        {
            AwakeData.Instance.fearList[mMurabitoNum] += 5;
        }
        else if (mStatus == 1)
        {
            AwakeData.Instance.doutList[mMurabitoNum] += 5;
        }
    }
    public void PushDown(int mStatus)
    {
        if (mStatus == 0)
        {
            AwakeData.Instance.fearList[mMurabitoNum] -= 5;
        }
        else if (mStatus == 1)
        {
            AwakeData.Instance.doutList[mMurabitoNum] -= 5;
        }
    }

    public void Plus()
    {
        int num = int.Parse(GameObject.Find("MurabitoNumber").GetComponent<Text>().text);
        num++;
        GameObject.Find("MurabitoNumber").GetComponent<Text>().text = num.ToString();
    }
    public void Minus()
    {
        int num = int.Parse(GameObject.Find("MurabitoNumber").GetComponent<Text>().text);
        num--;
        GameObject.Find("MurabitoNumber").GetComponent<Text>().text = num.ToString();
    }
}
