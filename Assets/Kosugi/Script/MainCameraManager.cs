using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    /*------内部設定(外部からは弄らないこと)------*/
    [Header("最初の状態")]
    private bool mStartState = false;
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
    static PatrolManager patrolManager = null;
    /// <summary>
    /// DontDestroyOnLoad用
    /// </summary>
    static PatrolManager Instance
    {
        get { return patrolManager ?? (patrolManager = FindObjectOfType<PatrolManager>()); }
    }

    void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");

        // 初期のステートをセット
        mNowState = mStartState;

        // 各ステートでアクティブにするスクリプトをセット
        mState = new Dictionary<bool, MonoBehaviour>()
        {
            {false, GetComponent<MainCameraScript>() },
            {true, GetComponent<HouseCameraScript>() },
        };

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

        // 通常ステートからの遷移

    }

    /// <summary>
    /// // DontDestroyOnLoad用
    /// </summary>
    private void OnDestroy()
    {
        if (this == Instance) patrolManager = null;
    }
}