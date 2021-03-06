﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// むらびとの巡回プログラム
/// 
/// 同じルートに二人(以上)担当になったら二人目をルートを半周ずらす
/// （あと当たり判定消す
/// 　じゃないとすれ違えない
/// </summary>
public class MurabitoPatrol : MonoBehaviour
{
    [Header("巡回担当に振り分けられたか(巡回しているかの判断)")]
    public bool isPatrolShift;

    [Header("担当ルートを半周ずれて巡回するか")]
    public bool isRouteDelay;

    [Header("----------")]

    [SerializeField, Header("※デバッグ用 巡回ルートの名前")]
    private string patrolRouteName = "";

    [Header("巡回ルートの番号")]
    private int _patrolRouteNum = 0;

    [SerializeField, Header("巡回ルートの各ポイント")]
    private List<Vector3> mPatrolPositions;

    [SerializeField, Header("巡回ポイント用カウンター")]
    private int _counter = 0;

    [SerializeField, Header("※デバッグ用 目的地になっている巡回ポイント座標")]
    private Vector3 mDestinationPoint;

    [Header("----------")]

    [SerializeField, Header("巡回時に振り向く方向(1:左,2:右)")]
    private int _swingDirection = 0;

    [SerializeField, Header("巡回開始するまでの待機時間")]
    private float _patrolInterval = 0;
    private float _intervalCount = 0;

    [SerializeField, Header("各家庭を巡回開始する時間(0は巡回しない)")]
    private float _housePatrolInterval = 0;

    [SerializeField, Header("振り向きにかける時間(とりあえず3s～10sまで)"), Range(3, 10)]
    private int _swingTime = 3;

    private bool isNotFirstTime = false;

    /*---内部データ---*/
    public Animator mAnim; // Animator取得用

    Transform mModel; // 14.!Root取得用

    NavMeshAgent mNav; // NavMeshAgent取得用

    void Start()
    {
        mNav = GetComponent<NavMeshAgent>();

        mAnim = GetComponent<Animator>();
        mModel = transform.Find("14.!Root");

        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;
        //SceneManager.activeSceneChanged += ActiveSceneChanged;

        // Unloaded -> Changed -> Loadeds
    }

    void SceneLoaded(UnityEngine.SceneManagement.Scene loadScene, LoadSceneMode arg1)
    {
        if (isPatrolShift)
        {
            if (AwakeData.Instance.talkTimeFlag)
            {
                mNav.isStopped = true;
            }
            else
            {
                mNav.isStopped = false;
                mNav.destination = mDestinationPoint;
            }
        }
    }

    private void SceneUnloaded(UnityEngine.SceneManagement.Scene unloadScene)
    {

    }
    //private void ActiveSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    //{
    //    Debug.LogError("changed:" + arg0.name + ", " + arg1.name);
    //}

    void Update ()
	{
        // 巡回担当になっていない場合は巡回処理をしない
        if (!isPatrolShift) return;

        if (_intervalCount > 0)
        {
            _intervalCount -= Time.deltaTime;
        }
        else if (mAnim.GetBool("Find"))
        {
            // 室内シーン中の場合処理を止める 会話中も停止
            if (AwakeData.Instance.talkTimeFlag)
                return;

            transform.LookAt(GameObject.Find("Player").transform.position);
            StopCoroutine("Swing");
            mNav.isStopped = true;
            GameObject.Find("Player").GetComponent<Player>().PlayerFind();
        }
        else
        {
            Patrol();
        }
    }

    /// <summary>
    /// 巡回処理
    /// </summary>
    void Patrol()
    {
        // 室内シーン中の場合処理を止める 会話中も停止
        if (AwakeData.Instance.talkTimeFlag)
            return;

        // 目指す巡回地点との距離が0.1未満になったら次の巡回地点をセット
        float min_Distance = 0.1f;
        if (mNav.remainingDistance < min_Distance && !mNav.isStopped)
        {
            StartCoroutine(Swing());
            _counter++;
            // 巡回地点の総数以上のカウントになったら0に戻す
            if (_counter == mPatrolPositions.Count)
            {
                _counter = 0;
            }
            mNav.SetDestination(mPatrolPositions[_counter]);
        }

        // NavMeshが動いてるかどうか
        if (!mNav.isStopped)// && !mAnim.GetBool("walk"))
        {
            mAnim.SetBool("walk", true);
        }
        else if(mNav.isStopped && mAnim.GetBool("walk"))
        {
            mAnim.SetBool("walk", false);
        }

        mDestinationPoint = mNav.destination;
    }

    /// <summary>
    /// 準備用の処理
    /// </summary>
    public void RouteSetting(List<Vector3> route)
    {
        //if (isPatrolShift) return;

        patrolRouteName = "Route" + _patrolRouteNum;
        mPatrolPositions = route;

        // 初回のみ自分の位置を巡回地点の開始地点(position0)に移動する
        if (!isNotFirstTime)
        {
            if (isRouteDelay)
            {
                transform.position = mPatrolPositions[mPatrolPositions.Count / 2];
                _counter = mPatrolPositions.Count / 2;
            }
            else
            {
                transform.position = mPatrolPositions[0];
            }

            isNotFirstTime = true;
        }

        // NavMeshを起動させる
        mNav.enabled = true;
        mNav.isStopped = false;

        // 開始地点から次の巡回地点をセット
        mNav.SetDestination(mPatrolPositions[_counter]);

        // モデルの向きを次の巡回地点にする
        transform.LookAt(mPatrolPositions[_counter]);
        //mModel.LookAt(mPatrolPositions[_counter]);

        // 巡回開始までの時間をカウント用変数に入れる
        _intervalCount = _patrolInterval;

        // 巡回担当に振り分けられたフラグを立てる
        isPatrolShift = true;
    }

    /// <summary>
    /// むらびとにデータをセット
    /// </summary>
    /// <param name="swingDirection">振り向きの方向</param>
    /// <param name="patrolRouteNum">巡回ルートの番号</param>
    /// <param name="patrolInterval">巡回開始するまでの待機時間</param>
    /// <param name="homePatrolInterval">各家庭を巡回開始する時間</param>
    /// <param name="route">巡回するルート</param>
    /// <param name="isAlready">巡回するルートに既に巡回者がいるかどうか</param>
    public void SetData(int swingDirection,int patrolRouteNum, float patrolInterval, float housePatrolInterval, List<Vector3> route, bool isAlready)
    {
        _patrolRouteNum = patrolRouteNum;
        _patrolInterval = patrolInterval;
        _swingDirection = swingDirection;
        _housePatrolInterval = housePatrolInterval;
        if (!isNotFirstTime)
            isRouteDelay = isAlready;

        RouteSetting(route);

        mAnim.SetBool("Find", false);
        mAnim.SetInteger("swing_num", _swingDirection);
    }

    /// <summary>
    /// 巡回用振り向き動作
    /// </summary>
    /// <returns></returns>
    IEnumerator Swing()
    {
        mNav.isStopped = true;
        
        yield return new WaitForSeconds(1);

        mAnim.SetBool("swing", true);

        // 指定した時間振り向く
        yield return new WaitForSeconds(_swingTime);

        mAnim.SetBool("swing", false);

        AngleSetting();
    }

    // 首の向きをスムーズに正面に戻す
    void AngleSetting()
    {
        Quaternion a = Quaternion.LookRotation(mPatrolPositions[_counter] - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.deltaTime / 2);

        if (AwakeData.Instance.talkTimeFlag)
            return;

        mNav.isStopped = false;
    }

    /// <summary>
    /// 日付跨いだ際のデータリセット用
    /// </summary>
    public void DataReset()
    {
        if (mPatrolPositions.Count > 0)
        {
            isNotFirstTime = false;
            RouteSetting(mPatrolPositions);
        }
        transform.parent.GetComponent<LoadCheck>()._count++;
        print("PatrolReset");
    }
}