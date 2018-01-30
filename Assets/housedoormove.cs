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
    GameObject camera_;
    GameObject player_;
    private Vector3 cameraPos_ = new Vector3(0.0f, 0.0f, 0.0f);
    private Quaternion cameraRota_ = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

    void Start()
    {
        AwakeData.Instance.inout_ = true;
    }

    void Update()
    {
        camera_ = GameObject.Find("Main Camera");
        cameraPos_ = camera_.transform.position;
        cameraPos_.x = cameraPos_.x + 3.6f;
        cameraPos_.y = cameraPos_.y + 4.23f;
        cameraPos_.z = cameraPos_.z + 0.7f;
        //AwakeData.Instance.cameraPosition_ = cameraPos_;
        print(AwakeData.Instance.inout_);

        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {
            AwakeData.Instance.inout_ = false;
            door_ = true;
        }
        if (door_ == true) fadeTimer_ -= Time.deltaTime;
        if (fadeTimer_ <= 0)
        {
            //シーンの名前
            AwakeData.Instance.houseNum_ = 0;
            SceneManager.LoadScene("LoadSceneManager");

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

