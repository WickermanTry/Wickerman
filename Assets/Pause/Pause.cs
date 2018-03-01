using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject Pausemenu;
    private int counter = 0;
    private bool counterflag = false;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.X)&& counter == 0 && !AwakeData.Instance.menuflag)
        {
            Time.timeScale = 0;
            Pausemenu.SetActive(true);
            counterflag = true;
            AwakeData.Instance.menuflag = true;
        }else if(Input.GetKeyDown(KeyCode.X) && counter > 15 && AwakeData.Instance.menuflag)
        {
            Time.timeScale = 1;
            Pausemenu.SetActive(false);
            counter = 0;
            counterflag = false;
            AwakeData.Instance.menuflag = false;
        }
        if(counterflag)
        counter++;
    }
}
