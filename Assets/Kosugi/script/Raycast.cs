using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit hitLeft, hitRight;

    [SerializeField]
    bool isEnable = false;

    void OnDrawGizmos()
    {
        if (isEnable == false)
            return;

        var scale = transform.lossyScale.x * 0.5f;

        var isHitLeft = Physics.Raycast(transform.position - transform.right * 0.1f, transform.forward, out hitLeft, 100);
        var isHitRight = Physics.Raycast(transform.position + transform.right * 0.1f, transform.forward, out hitRight, 100);

        if (isHitLeft)
        {
            if (hitLeft.collider.tag == "Player")
            {
                Gizmos.color = Color.red;
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
            Gizmos.DrawRay(transform.position - transform.right * 0.1f, transform.forward * 100);
        }

        if (isHitRight)
        {
            if (hitRight.collider.tag == "Player")
            {
                Gizmos.color = Color.red;
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
            Gizmos.DrawRay(transform.position + transform.right * 0.1f, transform.forward * 100);
        }
    }
}
