using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ドアの開け閉め
/// </summary>
public class Door : MonoBehaviour
{
    public GameObject mPlayer;

    [Header("床の高さ")]
    private float _floorHeight = 0.3f;

    [Header("家の番号"), SerializeField]
    private int _houseNum;

    [Header("フェードにかける時間"), SerializeField]
    private float _fadeTime = 1;


    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// ドアを開ける処理
    /// </summary>
    public void DoorOpen()
    {
        AwakeData.Instance.isDoorMove = true;

        /* ここでフェードを入れる + 時間経過も止める */

        StartCoroutine(DoorMove(_fadeTime));
    }

    /// <summary>
    /// ドアを開けた後の処理
    /// </summary>
    /// <returns></returns>
    IEnumerator DoorMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        print(AwakeData.Instance.cameraPosition_);

        // 家の外から中に入る場合
        if (!AwakeData.Instance.isHouse)
        {
            mPlayer.transform.position
                = new Vector3(transform.position.x, transform.position.y, transform.position.z)
                + transform.forward * 2f;
            mPlayer.transform.forward = transform.forward;
            
            AwakeData.Instance.houseNum_ = _houseNum;

            AwakeData.Instance.isHouse = !AwakeData.Instance.isHouse;
            AwakeData.Instance.isDoorMove = false;
            SceneManager.LoadScene("House" + AwakeData.Instance.houseNum_);
        }
        // 家の中から外に出る場合
        else
        {
            mPlayer.transform.position = transform.position - transform.forward * 2f;
            mPlayer.transform.forward = -transform.forward;

            AwakeData.Instance.houseNum_ = 0;

            AwakeData.Instance.isHouse = !AwakeData.Instance.isHouse;
            AwakeData.Instance.isDoorMove = false;
            SceneManager.LoadScene("hiru" + AwakeData.Instance.dayNum_);
        }

        yield return null;
    }
}