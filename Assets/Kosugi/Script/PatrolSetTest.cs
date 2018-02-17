using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSetTest : MonoBehaviour
{
    public int _murabitoNum;
    public bool _patrolTest;
	void Start ()
	{
		
	}

	void Update ()
	{
        if (_patrolTest)
        {
            GameObject.Find("PatrolManager").GetComponent<PatrolManager>().SetMurabito(_murabitoNum);
            _patrolTest = false;
        }
	}
}