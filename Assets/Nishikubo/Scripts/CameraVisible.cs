using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnWillRenderObject()
    {

#if UNITY_EDITOR

        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
#endif

        {
            Debug.Log(Camera.current.name);
            // 処理
            this.gameObject.SetActive(true);
        }
        //else
        //{
        //    this.gameObject.SetActive(false);
        //}
    }


}
