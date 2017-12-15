using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Transparent : MonoBehaviour
{

    public GameObject obj;


    // Use this for initialization
    void Start()
    {
        obj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "view")
        {
            print("消した");
            obj.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "view")
        {
            print("出した");
            obj.SetActive(true);
        }        
    }


}
