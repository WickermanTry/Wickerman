using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    PatrolManager mPatrolManager;

    [Header("巡回担当を出す家番号")]
    public int _houseNum = 0;

    [Header("シーンチェンジ確認用")]
    public bool isSceneChange = false;

	void Start ()
	{
        DontDestroyOnLoad(gameObject);
        mPatrolManager = GameObject.Find("PatrolManager").GetComponent<PatrolManager>();
	}

	void Update ()
	{
        if (isSceneChange)
        {
            //for(int i = 1; i < 15; i++)
            //{
            //    mPatrolManager.SetMurabito(i);
            //}
            mPatrolManager.SetMurabito(_houseNum);
            mPatrolManager.isSceneChange1 = true;
            isSceneChange = false;
        }
	}
}
