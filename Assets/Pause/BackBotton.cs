using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBotton : MonoBehaviour {

    public bool requestGoal_ = true;
    public GameObject Canvas_;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        Canvas_.SetActive(true);
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            canvas.SetActive(true);
            Canvas_.SetActive(false);         
        }
	}
}
