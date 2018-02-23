using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// むらびとの視線の管理
/// </summary>
public class SectorManager : MonoBehaviour
{
    RaycastHit hitLeft, hitRight, hit;

    [SerializeField, Header("目線の長さ")]
    private float _rayLength = 0;

    [Header("プレイヤーが範囲に入ったか")]
    public bool isFind = false;

    /// <summary>
    /// 視線用レイ
    /// </summary>
    void OnDrawGizmos()
    {
        if (!isFind)
            return;

        Vector3 playerPos = (GameObject.Find("Player").transform.position - transform.position).normalized;
        playerPos.y = 0;
        
        var isHit = Physics.Raycast(transform.position, playerPos, out hit, _rayLength);
        if (isHit)
        {
            if (hit.collider.tag == "Player")
            {
                Gizmos.color = Color.yellow;
                // sekizui1 -> center -> 14!Root -> Murabito
                GameObject me = transform.parent.parent.parent.gameObject;
                me.GetComponent<MurabitoPatrol>().mAnim.SetBool("Find", true);
                
                Debug.LogWarning(me.name + "プレイヤー発見！");
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawRay(transform.position, playerPos * hit.distance);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, playerPos * _rayLength);
        }
    }
}
