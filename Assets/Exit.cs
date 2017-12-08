using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Exit : MonoBehaviour {
    bool inflag = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {
            //シーンの名前 + 番号
            SceneManager.LoadScene("Proto");
            print("移動");
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DoorCollide") inflag = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "DoorCollide") inflag = false;
    }

}
