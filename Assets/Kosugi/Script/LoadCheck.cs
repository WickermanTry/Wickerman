using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCheck : MonoBehaviour
{
    //[HideInInspector]
    public int _count = 0;

    public int _today = 99;

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
        if (_count == GameObject.Find("PatrolManager").GetComponent<PatrolManager>().ListCount() && _today != AwakeData.Instance.dayNum_)
        {
            _today = AwakeData.Instance.dayNum_;
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