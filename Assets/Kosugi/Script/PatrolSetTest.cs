using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSetTest : MonoBehaviour
{
    public int _houseNum;
    public bool isSet = false;

    [Header("----------")]

    public int _patrolValue;
    public bool isPatrol = false;

    PatrolManager mPatrolManager;

	void Start ()
	{
        mPatrolManager = GameObject.Find("PatrolManager").GetComponent<PatrolManager>();
	}

	void Update ()
	{
        if (isSet)
        {
            mPatrolManager.SetPatrolMurabito(_houseNum);
            isSet = false;
        }

        if (isPatrol)
        {
            mPatrolManager.SetRoute(_patrolValue);
            isPatrol = false;
        }
	}
}
