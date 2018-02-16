﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject Pausemenu;

    public bool menu = false;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.X) && menu == false)
        {
            Time.timeScale = 0;
            Pausemenu.SetActive(true);
            menu = true;
        }
        else if(Input.GetKeyDown(KeyCode.X) && menu == true)
        {
            Time.timeScale = 1;
            Pausemenu.SetActive(false);
            menu = false;
        }
    }
}
