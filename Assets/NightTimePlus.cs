using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTimePlus : MonoBehaviour {

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        GameObject gameObject = GameObject.Find("Text");
        
        AwakeData.Instance.worldTime_ += 10;
    }
}
