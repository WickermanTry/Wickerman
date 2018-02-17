using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 村のカメラ
/// </summary>
public class MainCameraScript : MonoBehaviour
{
    [Header("Player Object")]
    private GameObject mPlayer;

    [Header("Playerを見る角度")]
    public float _cameraAngle = 40;

    [SerializeField, Header("カメラの高さ")]
    private float _cameraHeight = 10;

    public bool test;

    void Start()
    {
        mPlayer = GetComponent<MainCameraManager>().mPlayer;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position = mPlayer.transform.position - transform.forward * _cameraHeight;
        transform.LookAt(mPlayer.transform.position);
        
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Confined;

            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(mPlayer.transform.position, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}