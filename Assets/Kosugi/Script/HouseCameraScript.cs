using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 室内用カメラ
/// </summary>
public class HouseCameraScript : MonoBehaviour
{
    [Header("Player Object")]
    private GameObject mPlayer;
    [SerializeField, Header("Player Position")]
    private Vector3 mPlayerPosition;
    [SerializeField, Header("Player Rotation")]
    private Quaternion mPlayerRotation;

    [SerializeField, Header("Playerからどれくらい後ろの位置か(倍率)")]
    private float _leaveValue = 3;

    [SerializeField, Header("カメラの高さ")]
    private float _cameraHeight = 5.0f;

    void Start()
    {
        mPlayer = GetComponent<MainCameraManager>().mPlayer;
        mPlayerPosition = mPlayer.transform.position;
        mPlayerRotation = mPlayer.transform.rotation;

        // 初期位置設定
        transform.position = new Vector3(mPlayerPosition.x, mPlayerPosition.y + _cameraHeight, mPlayerPosition.z) - mPlayer.transform.forward * _leaveValue;
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