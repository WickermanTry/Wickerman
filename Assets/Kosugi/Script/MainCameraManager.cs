using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    /*------内部設定(外部からは弄らないこと)------*/
    [SerializeField, Header("前の状態")]
    private bool mBeforeState;
    [SerializeField, Header("現在の状態")]
    private bool mNowState;
    //各状態中の処理はこの配列へ格納
    private Dictionary<bool, MonoBehaviour> mState;


    /*------外部設定------*/
    [Header("---オブジェクトはここで一括管理---")]

    [Header("プレイヤーオブジェクト")]
    public GameObject mPlayer;

    // DontDestroyOnLoad用
    static MainCameraManager mainCameraManager = null;
    /// <summary>
    /// DontDestroyOnLoad用
    /// </summary>
    static MainCameraManager Instance
    {
        get { return mainCameraManager ?? (mainCameraManager = FindObjectOfType<MainCameraManager>()); }
    }

    void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");

        // 初期のステートをセット
        mNowState = AwakeData.Instance.isHouse;

        // 各ステートでアクティブにするスクリプトをセット
        mState = new Dictionary<bool, MonoBehaviour>()
        {
            {false, GetComponent<MainCameraScript>() },
            {true, GetComponent<HouseCameraScript>() },
        };

        // オブジェクトが重複しているかのチェック
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // 現在の状態のみを実行
        Action(AwakeData.Instance.isHouse);
        //Action(mNowState);
    }

    /// <summary>
    /// 指定した状態のみを有効にする
    /// </summary>
    void Action(bool value)
    {
        foreach (KeyValuePair<bool, MonoBehaviour> state in mState)
        {
            if (value == state.Key)
                // move.Valueからだと変更できないみたい
                mState[state.Key].enabled = true;
            else
                mState[state.Key].enabled = false;
        }

        SetState(value);
    }

    /// <summary>
    /// 状態を変更する
    /// </summary>
    public void SetState(bool state)
    {
        // 現在のステートをm_BeforeBrosStateに格納しステートを更新
        mBeforeState = mNowState;
        mNowState = state;

        // 同じ状態への変更は行わない
        if (mBeforeState == mNowState) return;

        // 通常ステートからの遷移
        if (mBeforeState == false)
        {
            if (mNowState == true)
            {
                transform.rotation = Quaternion.Euler(GetComponent<HouseCameraScript>()._cameraAngle, transform.rotation.y, transform.rotation.z);
                print("change");
            }
        }
        if (mBeforeState == true)
        {
            if (mNowState == false)
            {
                transform.rotation = Quaternion.Euler(GetComponent<MainCameraScript>()._cameraAngle, transform.rotation.y, transform.rotation.z);
                print("change");
            }
        }
    }

    /// <summary>
    /// // DontDestroyOnLoad用
    /// </summary>
    private void OnDestroy()
    {
        if (this == Instance) mainCameraManager = null;
    }
}