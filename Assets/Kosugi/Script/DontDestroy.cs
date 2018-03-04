using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DontDestroy : MonoBehaviour
{
    // DontDestroyOnLoad用
    static DontDestroy dontDestroy = null;

    [SerializeField, Header("NavMeshのデータ")]
    private NavMeshData navMeshData;

    /// <summary>
    /// DontDestroyOnLoad用
    /// </summary>
    static DontDestroy Instance
    {
        get { return dontDestroy ?? (dontDestroy = FindObjectOfType<DontDestroy>()); }
    }

    private void Awake()
    {
        // オブジェクトが重複しているかのチェック
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);

        NavMesh.AddNavMeshData(navMeshData);
    }

    /// <summary>
    /// // DontDestroyOnLoad用
    /// </summary>
    private void OnDestroy()
    {
        if (this == Instance) dontDestroy = null;
    }
}