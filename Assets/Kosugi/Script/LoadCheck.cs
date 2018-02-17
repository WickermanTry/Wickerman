﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCheck : MonoBehaviour
{
    //[HideInInspector]
    public int _count = 0;

    public int _dayCheck = 0;

    // DontDestroyOnLoad用
    static LoadCheck loadCheck = null;
    /// <summary>
    /// DontDestroyOnLoad用
    /// </summary>
    static LoadCheck Instance
    {
        get { return loadCheck ?? (loadCheck = FindObjectOfType<LoadCheck>()); }
    }

    void Awake()
	{
        // オブジェクトが重複しているかのチェック
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (_dayCheck==AwakeData.Instance.dayTime_)
        {
            _dayCheck = AwakeData.Instance.dayTime_ + 1;
            _count = 0;
            GameObject.Find("PatrolManager").GetComponent<PatrolManager>().SetRoute();
            print("RouteSet");
        }
    }

    /// <summary>
    /// // DontDestroyOnLoad用
    /// </summary>
    private void OnDestroy()
    {
        if (this == Instance) loadCheck = null;
    }
}