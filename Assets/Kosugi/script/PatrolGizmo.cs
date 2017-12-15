using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パトロール順路のデバッグ表示
/// </summary>
public class PatrolGizmo : MonoBehaviour
{
    [SerializeField, Header("Gizmoの色")]
    public Color mColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = mColor;
        Vector3 from;
        Vector3 to;

        for (int i = 0; i < transform.childCount; i++)
        {
            from = transform.Find("position" + i).position;

            if(i== transform.childCount-1)
            {
                to = transform.Find("position" + 0).position;
            }
            else
            {
                int num = i + 1;
                to = transform.Find("position" + num).position;
            }

            Gizmos.DrawLine(from, to);
        }
    }
}
