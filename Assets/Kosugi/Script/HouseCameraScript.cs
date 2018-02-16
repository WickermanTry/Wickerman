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

    [SerializeField, Header("Playerを見る角度")]
    private float _cameraAngle;

    [SerializeField, Header("カメラの高さ")]
    private float _cameraHeight = 5.0f;

    void Start()
    {
        mPlayer = GetComponent<MainCameraManager>().mPlayer;

        transform.Rotate(new Vector3(1, 0, 0), _cameraAngle);
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