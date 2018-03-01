using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBotton : MonoBehaviour {

    public GameObject mapCamera_;
    public GameObject m_Pause;
   

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            mapCamera_.SetActive(false);
            m_Pause.SetActive(true);
            AwakeData.Instance._mapcamera = false;
            AwakeData.Instance.menuflag = true;
        }
    } 
}
