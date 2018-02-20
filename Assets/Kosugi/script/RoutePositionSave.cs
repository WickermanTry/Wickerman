using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePositionSave : MonoBehaviour
{
    [Header("配下のルート")]
    public List<Vector3> mRoutePosition = new List<Vector3>();

    [SerializeField, Header("このルートをむらびとが既に巡回しているか")]
    private bool isAlreadyPatrol = false;

	void Awake ()
	{
        for (int i = 0; i < transform.childCount; i++)
        {
            mRoutePosition.Add(transform.GetChild(i).position);
        }
    }

    /// <summary>
    /// 巡回者がいるかどうかのフラグ受け取り用
    /// </summary>
    /// <returns></returns>
    public bool GetAlreadyPatrolFlag()
    {
        return isAlreadyPatrol;
    }
    /// <summary>
    /// 巡回者がいるかどうかのフラグを変更(基本false→trueだけ
    /// </summary>
    /// <param name="flag"></param>
    public void SetAlreadyPatrolFlag(bool flag)
    {
        isAlreadyPatrol = flag;

        Debug.LogError("stop");
    }
}
