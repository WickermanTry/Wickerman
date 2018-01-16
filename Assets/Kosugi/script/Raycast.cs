using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを直視したかどうか(視線系の処理)
/// </summary>
public class Raycast : MonoBehaviour
{
    RaycastHit hitLeft, hitRight;

    [SerializeField, Header("目線の長さ")]
    private float _rayLength = 0;

    [SerializeField]
    bool isEnable = false;

    void OnDrawGizmos()
    {
        if (isEnable == false)
            return;

        var scale = transform.lossyScale.x * 0.5f;

        var isHitLeft = Physics.Raycast(transform.position - transform.right * 0.1f, transform.forward, out hitLeft, _rayLength);
        var isHitRight = Physics.Raycast(transform.position + transform.right * 0.1f, transform.forward, out hitRight, _rayLength);

        if (isHitLeft)
        {
            if (hitLeft.collider.tag == "Player")
            {
                Gizmos.color = Color.red;
                print("プレイヤー発見！");
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawRay(transform.position - transform.right * 0.1f, transform.forward * hitLeft.distance);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position - transform.right * 0.1f, transform.forward * _rayLength);
        }

        if (isHitRight)
        {
            if (hitRight.collider.tag == "Player")
            {
                Gizmos.color = Color.red;
                print("プレイヤー発見！");
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawRay(transform.position + transform.right * 0.1f, transform.forward * hitRight.distance);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position + transform.right * 0.1f, transform.forward * _rayLength);
        }
    }
}
