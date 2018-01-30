using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugDay : MonoBehaviour {

    private Text dayText;
    private int dayNum;

	// Use this for initialization
	void Start () {
        dayText = this.GetComponent<Text>();
        dayNum = AwakeData.Instance.dayNum_;


    }

    // Update is called once per frame
    void Update () {
        dayText.text = dayNum.ToString()+"日目";
	}

    public void OnClickDayUp()
    {
        dayNum++;
        AwakeData.Instance.dayNum_ = dayNum;
    }

    public void OnClickDayDown()
    {
        dayNum--;
        AwakeData.Instance.dayNum_ = dayNum;
    }
}
