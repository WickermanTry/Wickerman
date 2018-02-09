using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestTest : MonoBehaviour {
    public GameObject Request;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Request.SetActive(true);
        }
	}
}
