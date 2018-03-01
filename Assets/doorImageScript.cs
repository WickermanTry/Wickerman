using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorImageScript : MonoBehaviour {
    public bool inflag = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") inflag = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inflag = false;
    }
}
