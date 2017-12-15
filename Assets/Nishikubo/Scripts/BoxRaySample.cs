using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxRaySample : MonoBehaviour {

    RaycastHit hit;

    [SerializeField]
    bool isEnable = false;


    void OnDrawGizmos()
    {
        if (isEnable == false)
            return;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + this.GetComponent<BoxCollider>().center.y, transform.position.z);
        Vector3 scale = this.GetComponent<BoxCollider>().size / 2;

        //var isBoxCol = Physics.OverlapBox(pos, scale);


        var isHit = Physics.BoxCast(pos, scale, transform.forward, out hit, transform.rotation,2);


        ////村人とプレイヤーがめり込んだとき
        //for (int i = 0; i < isBoxCol.Length; i++)
        //{
        //    if (isBoxCol[i].gameObject.tag == "Murabito")
        //    {
        //        Debug.Log(isBoxCol[i]);
        //        isHit = Physics.BoxCast(pos, scale, transform.forward, out hit, transform.rotation,0);

        //    }
        //    else if (isBoxCol[i].gameObject.tag != "Murabito" && isBoxCol[i].gameObject.tag == "Player")
        //    {
        //        Debug.Log("aaa");
        //    }

        //}

        if (isHit)
        {
            Gizmos.DrawRay(pos, (transform.forward * 0.5f) * hit.distance );
            Gizmos.DrawWireCube(pos + transform.forward * hit.distance,scale * 2);
            if (hit.collider.tag == "Murabito")
            {
                Debug.Log(hit.collider.name);
            }
        }
        else {
            Gizmos.DrawRay(pos, transform.forward * 0.5f);
        }



    }
}
