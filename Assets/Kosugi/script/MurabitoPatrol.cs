using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// むらびとの巡回プログラム
/// 
/// 01/16
/// 巡回する人数・ルート・開始時間etcを自由に設定できるようにする
/// 依頼品毎に日付が変わる(今のところ
/// </summary>
public class MurabitoPatrol : MonoBehaviour
{
    [Header("巡回担当に振り分けられたか(巡回しているかの判断)")]
    public bool isPatrolShift;

    [Header("----------")]

    [SerializeField, Header("※デバッグ用 巡回ルートの名前")]
    private string mPatrolRouteName = "";

    [SerializeField, Header("巡回ルートの番号")]
    private int _patrolRouteNum = 0;

    [SerializeField, Header("巡回ルートの各地点")]
    private List<Vector3> mPatrolPositions;

    [SerializeField, Header("巡回地点用カウンター")]
    private int _counter = 0;

    [Header("----------")]

    [SerializeField, Header("巡回時に振り向く方向(1:左,2:右)")]
    private int mSwingDirection = 0;

    [SerializeField, Header("巡回開始するまでの待機時間")]
    private float _patrolInterval = 0;

    [SerializeField, Header("各家庭を巡回開始する時間(0は巡回しない)")]
    private float _housePatrolInterval = 0;

    [SerializeField, Header("振り向きにかける時間(とりあえず3s～10sまで)"), Range(3, 10)]
    private int _swingTime = 0;

    private bool isNotPointWarp = false;

    /*---内部データ---*/
    Animator mAnim; // Animator取得用

    Transform mModel; // 14.!Root取得用

    NavMeshAgent mNav; // NavMeshAgent取得用

    private void Awake()
    {
        print("murabito");
        transform.parent.GetComponent<LoadCheck>()._count++;
    }

    void Start ()
	{
        mNav = GetComponent<NavMeshAgent>();

        mAnim = GetComponent<Animator>();
        mModel = transform.Find("14.!Root");
    }

	void Update ()
	{
        // 巡回担当になっていない場合は巡回処理をしない
        if (!isPatrolShift) return;

        if (_patrolInterval > 0)
        {
            _patrolInterval -= Time.deltaTime;
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
    }

    /// <summary>
    /// 準備用の処理
    /// </summary>
    public void StartSetting(List<Vector3> route)
    {
        if (isPatrolShift) return;

        mPatrolPositions = route;

        // 必要なプログラムを作動
        //SetData();

        // 初回のみ自分の位置を巡回地点の開始地点(position0)に移動する
        if (!isNotPointWarp)
        {
            transform.position = mPatrolPositions[0];
            isNotPointWarp = false;
        }

        // NavMeshを起動させる
        mNav.enabled = true;
        mNav.isStopped = false;

        // 開始地点から次の巡回地点をセット
        mNav.SetDestination(mPatrolPositions[_counter]);

        // モデルの向きを次の巡回地点にする
        transform.LookAt(mPatrolPositions[_counter]);
        mModel.LookAt(mPatrolPositions[_counter]);

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
    public void SetData(int swingDirection,int patrolRouteNum, float patrolInterval, float housePatrolInterval)
    {
        _patrolRouteNum = patrolRouteNum;
        _patrolInterval = patrolInterval;
        mSwingDirection = swingDirection;
        _housePatrolInterval = housePatrolInterval;

        mAnim.SetInteger("swing_num", mSwingDirection);
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
        mNav.isStopped = false;
    }

    // 巡回ルート番号取得用
    public int GetRouteNumber()
    {
        return _patrolRouteNum;
    }
}
