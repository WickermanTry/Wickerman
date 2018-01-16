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
    public Transform target_;
    private Vector3 cameraPos_ = new Vector3(0.0f, 0.0f, 0.0f);
    private Quaternion cameraRota_ = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

    void Start()
    {
        AwakeData.Instance.inout_ = true;
        AwakeData.Instance.cameraFrag_ = false;
    }

    void Update()
    {
        camera_ = GameObject.Find("Main Camera");
        cameraPos_ = camera_.transform.position;
        cameraPos_.x = cameraPos_.x + 3.8f;
        cameraPos_.y = cameraPos_.y + 4f;
        
        if(cameraRota_.y < 90)
        {
            Debug.Log("b");
            cameraPos_.z = cameraPos_.z + 1f;
            
        }
        else
        {
            Debug.Log("c");
            cameraPos_.z = cameraPos_.z - 2.3f;
        }
        AwakeData.Instance.cameraPosition_ = cameraPos_;

        cameraRota_ = camera_.transform.rotation;
        Vector3 cameraRotation = AwakeData.Instance.cameraRotate_.eulerAngles;
        cameraRota_.y = cameraRotation.y;
        AwakeData.Instance.cameraRotate_ = camera_.transform.rotation;


        print(AwakeData.Instance.inout_);

        if (Input.GetKeyDown(KeyCode.C) && inflag)
        {
            AwakeData.Instance.cameraFrag_ = true;
            AwakeData.Instance.inout_ = false;
            door_ = true;
        }
        if (door_ == true) fadeTimer_ -= Time.deltaTime;
        if (fadeTimer_ <= 0)
        {
            //シーンの名前 + 番号
            AwakeData.Instance.houseNum_ = 0;
            SceneManager.LoadScene("LoadSceneManager");        
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

