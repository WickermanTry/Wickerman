using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class housedoormove : MonoBehaviour
{

    bool inflag = false;
    public string ChangeScene_;
    public float fadeTimer_ = 1.0f;
    public bool door_ = false;

    void Start()
    {
        AwakeData.Instance.inout_ = true;
    }

    void Update()
    {
        print(AwakeData.Instance.inout_);

        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {
            AwakeData.Instance.inout_ = false;
            door_ = true;
        }
        if (door_ == true) fadeTimer_ -= Time.deltaTime;
        if (fadeTimer_ <= 0)
        {
            //シーンの名前 + 番号
            SceneManager.LoadScene(ChangeScene_);
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

