﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 02/09 小杉
/// 家の出入りの処理を作り直す課程で
/// カメラの変数名や処理等に一部手を加えました。
/// 私個人がわかりやすくなるような変更ばかりなので
/// 不都合あれば戻したり変えたりしても大丈夫です。
/// </summary>
public class MainCameraScript : MonoBehaviour
{
    [Header("Player Object")]
    private GameObject mPlayer;
    [SerializeField, Header("Player Position")]
    private Vector3 mPlayerPosition;
    [SerializeField, Header("Player Rotation")]
    private Quaternion mPlayerRotation;

    [SerializeField, Header("カメラの高さ"), Range(5, 10)]
    private float _cameraHeight = 10;

    void Start()
    {
        mPlayer = GetComponent<MainCameraManager>().mPlayer;
        mPlayerPosition = mPlayer.transform.position;
        mPlayerRotation = mPlayer.transform.rotation;

        // 初期位置設定
        transform.position = new Vector3(mPlayerPosition.x, _cameraHeight, mPlayerPosition.z - 10);
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += mPlayer.transform.position - mPlayerPosition;
        mPlayerPosition = mPlayer.transform.position;
        transform.LookAt(mPlayerPosition);
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Confined;

            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(mPlayerPosition, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}