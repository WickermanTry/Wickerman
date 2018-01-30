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
    public GameObject player_;
    public Vector3 playerChangePosition_;
    public Quaternion playerChangeRotation_;
    public Vector3 cameraChangePosition_;
    public Quaternion cameraChangeRotation_;
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
            //AwakeData.Instance.playerPosition_ = playerChangePosition_;
            //AwakeData.Instance.cameraPosition_ = cameraChangePosition_;
            //AwakeData.Instance.cameraRotate_ = cameraChangeRotation_;
            AwakeData.Instance.inout_ = false;
            door_ = true;
        }
        if (door_ == true) fadeTimer_ -= Time.deltaTime;
        if (fadeTimer_ <= 0)
        {
            player_ = GameObject.FindGameObjectWithTag("Player");
            player_.transform.position = playerChangePosition_;
            player_.transform.rotation = playerChangeRotation_;
            AwakeData.Instance.cameraPosition_ = cameraChangePosition_;
            AwakeData.Instance.cameraRotate_ = cameraChangeRotation_;
            //AwakeData.Instance.playerPosition_ = player_.transform.position;
            //シーンの名前 + 番号
            AwakeData.Instance.houseNum_ = HomeNum;
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
