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

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "view")
        {
            obj.SetActive(false);
        }
        else if (other.tag == "Reset")
        {
            obj.SetActive(true);
        }
    }
}
