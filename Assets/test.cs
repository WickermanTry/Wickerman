using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    Color alpha = new Color(0, 0, 0, 0.01f);
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Renderer>().material.color -= alpha;
        }
    }
}
