using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxRay : MonoBehaviour
{
    RaycastHit hit;

    public bool isDoorOpen;

    public GameObject mDoor;

    /// <summary>
    /// 実際はUI表示になるのでrayは映さない
    /// </summary>
    void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + GetComponent<BoxCollider>().size.y, transform.position.z);
        Vector3 scale = this.GetComponent<BoxCollider>().size;

        //var isBoxCol = Physics.OverlapBox(pos, scale);

        var isHit = Physics.BoxCast(pos, scale, transform.forward, out hit, transform.rotation, 1.0f);

        if (isHit)
        {
            Gizmos.DrawRay(pos, transform.forward * hit.distance);
            Gizmos.DrawWireCube(pos + transform.forward * hit.distance, scale);

            // ドアにレイが当たっていれば
            if (hit.collider.tag == "DoorCollide")
            {
                isDoorOpen = true;
                mDoor = hit.collider.gameObject;
            }
        }
        else
        {
            Gizmos.DrawRay(pos, transform.forward * 1.0f);
            isDoorOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isDoorOpen && !AwakeData.Instance.isDoorMove)
        {
            mDoor.GetComponent<Door>().DoorOpen();
        }
    }

    private void OnCollisionStay(Collision col)
    {
        // ドアに身体が当たっていれば
        if (col.collider.tag == "DoorCollide")
        {
            isDoorOpen = true;
            mDoor = col.collider.gameObject;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "DoorCollide")
        {
            isDoorOpen = false;
        }
    }
}