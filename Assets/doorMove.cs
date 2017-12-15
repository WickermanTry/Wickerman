using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class doorMove : MonoBehaviour
{
    bool inflag = false;
    public int HomeNum = 1; //家の番号
    public float fadeTimer_ = 1.0f;
    public bool door_ = false;
    public GameObject a;
    void Start()
    {
        AwakeData.Instance.inout_ = true;
    }

    void Update()
    {
        print(AwakeData.Instance.inout_);
        HomeNum.ToString();
        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {
            AwakeData.Instance.inout_ = false;
            door_ = true;
        }
        if (door_ == true) fadeTimer_ -= Time.deltaTime;
        if (fadeTimer_ <= 0)
        {
            //シーンの名前 + 番号
            SceneManager.LoadScene("House" + HomeNum);
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
