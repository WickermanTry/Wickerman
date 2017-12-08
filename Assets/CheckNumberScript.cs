using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNumberScript : MonoBehaviour {

    public int checkNumber_;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AwakeData.Instance.checkNum = checkNumber_;
	}
}
