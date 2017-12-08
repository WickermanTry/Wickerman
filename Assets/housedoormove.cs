using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class housedoormove : MonoBehaviour {

    bool inflag = false;
    public int HomeNum = 1; //家の番号

    void Start()
    {

    }

    void Update()
    {

        HomeNum.ToString();
        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {

            //シーンの名前 + 番号
            SceneManager.LoadScene("Maptest1117");
            print("移動");

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") inflag = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inflag = false;
    }
}
