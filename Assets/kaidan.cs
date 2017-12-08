using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaidan : MonoBehaviour
{
    enum KaidanState
    {
        itikai,
        nikai
    }
    public bool a = true;
    public float X;
    public float Y;
    public float Z;
    private KaidanState kaidanState;

    public void Start()
    {
        kaidanState = KaidanState.itikai;
    }

    public void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (kaidanState==KaidanState.itikai)
            {
                other.gameObject.transform.position = new Vector3(X, Y, Z);
            }
            else if (kaidanState == KaidanState.nikai)
            {
                print("kita");
                GameObject.Find("1kai").transform.Find("House1").gameObject.SetActive(true);
                GameObject.Find("2kai").transform.Find("House2").gameObject.SetActive(false);
                kaidanState = KaidanState.itikai;
            }
        }
    }

}
