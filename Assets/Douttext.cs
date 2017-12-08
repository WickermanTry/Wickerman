using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Douttext : MonoBehaviour {
    // Use this for initialization
    public int checkNum_;
    void Start () {
        checkNum_ = 0;
    }
	
	// Update is called once per frame
	void Update () {
        checkNum_ = AwakeData.Instance.checkNum;
        if (AwakeData.Instance.doutList[checkNum_] > 100)
        {
            AwakeData.Instance.doutList[checkNum_] = 100;
        }
        if (AwakeData.Instance.doutList[checkNum_] < 0)
        {
            AwakeData.Instance.doutList[checkNum_] = 0;
        }

        this.GetComponent<Text>().text = "不審度" + AwakeData.Instance.doutList[checkNum_];
    }
}
