using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePositionSave : MonoBehaviour
{
    [Header("配下のルート")]
    public List<Vector3> mRoutePosition = new List<Vector3>();

	void Awake ()
	{
        for (int i = 0; i < transform.childCount; i++)
        {
            mRoutePosition.Add(transform.Find("position" + i).transform.position);
        }
    }
}
