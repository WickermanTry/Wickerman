using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPouse : MonoBehaviour {

    public bool visible = false;
    public GameObject obj;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!visible && Input.GetKeyDown(KeyCode.P))
        {
            obj.SetActive(true);
            visible = true;
            //Debug.Log("aaa");
        }
        else if (visible && Input.GetKeyDown(KeyCode.P))
        {
            obj.SetActive(false);
            visible = false;
            //Debug.Log("bbb");
        }
    }
}
