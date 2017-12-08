using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabetouka : MonoBehaviour {
    public GameObject main_Camera;
    public Shader defaultShader; // 通常時
    public Shader alphaShader; // 透明時
    //RectTransform m_RectTransform;
    //private GameObject a;

    // Use this for initialization
    void Start () {
        //m_RectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        //Rayの飛ばせる距離
        float distance = 8.0f;
        Vector3 direction = (main_Camera.transform.position -
            transform.position).normalized;
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, direction);
        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
        //Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            //if (hit.collider.tag == "kieruObj")
            //    hit.collider.gameObject.SetActive(false);
           
        }
        //float dis = Vector3.Distance(main_Camera.transform.position, Player_.transform.position);
        //Ray ray = new Ray(main_Camera.transform.position, main_Camera.transform.forward * dis);
        //Debug.DrawLine(ray.origin, ray.direction * dis ,Color.red);


        //if (Physics.Raycast(ray))
        //{
        //    RaycastHit[] hit = Physics.RaycastAll(ray);
        //    for(int i=0; i < hit.Length; i++)
        //    {
        //        if (hit[i].collider.tag == "kieruObj")
        //        {

        //        }
        //    }
        //}
    }
}
