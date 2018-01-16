using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パトロール順路のデバッグ表示 ※スタートのカウントを1にする＝ルートのポイントも1から始める
/// </summary>
public class PatrolGizmo : MonoBehaviour
{
    [SerializeField, Header("Gizmoの色")]
    public Color mColor;

    [SerializeField, Header("Gizmoを表示させるか")]
    private bool mIsView = false;

    private void OnDrawGizmos()
    {
        if (!mIsView) return;

        Gizmos.color = mColor;
        Vector3 from;
        Vector3 to;

        for (int i = 0; i < transform.childCount; i++)
        {
            from = transform.Find("position" + i).position;

            if (i == transform.childCount - 1)
            {
                to = transform.Find("position" + 0).position;
            }
            else
            {
                to = transform.Find("position" + (i + 1)).position;
            }

            Gizmos.DrawLine(from, to);
        }
    }
}
