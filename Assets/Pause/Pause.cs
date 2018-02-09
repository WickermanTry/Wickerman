using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject Pausemenu;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 0;
            Pausemenu.SetActive(true);
        }
    }
}
