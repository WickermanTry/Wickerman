using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCheck : MonoBehaviour
{
    //[HideInInspector]
    public int _count = 0;

	void Awake()
	{
        print("check");
        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);
    }

	void Update ()
	{
        if (_count >= transform.childCount)
        {
            _count = 0;
            GameObject.Find("PatrolManager").GetComponent<PatrolManager>().SetRoute();
        }
	}
}