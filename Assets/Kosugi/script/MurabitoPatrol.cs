using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// 巡回プログラム
/// アニメーション周り不安定なので要改善
/// 首振り→巡回が怪しい
/// 振り向きはキャラで固定らしい
/// </summary>
public class MurabitoPatrol : MonoBehaviour
{
    [Header("巡回担当に振り分けられたか")]
    public bool mIsPatrolShift;

    [SerializeField, Header("巡回経路の総括オブジェクト")]
    private GameObject mPatrolRoute;

    [SerializeField, Header("巡回経路の各地点")]
    private List<Vector3> mPatrolPositions;

    [SerializeField, Header("巡廻地点用カウンター")]
    private int mCounter = 0;

    [SerializeField, Header("巡回時の振り向きパターン")]
    private string mSwingPattern = "";
    [Header("振り向き時の回転角度")]
    private float mAngle = 0;

    /*---内部データ---*/
    Animator mAnim;

    Transform mModel; // 14.!Root取得用

    NavMeshAgent mNav;

	void Start ()
	{
        mNav = GetComponent<NavMeshAgent>();
        mNav.isStopped = true;

        mAnim = GetComponent<Animator>();
        mModel = transform.Find("14.!Root");
    }

	void Update ()
	{
        if (!mIsPatrolShift) return;

        // 目指す巡回地点との距離が0.1未満になったら次の巡回地点をセット
        float min_Distance = 0.1f;
        if (mNav.remainingDistance < min_Distance && !mNav.isStopped)
        {
            StartCoroutine(Swing());
            mCounter++;
            mNav.SetDestination(mPatrolPositions[mCounter]);
        }

        // 巡回地点の総数以上のカウントになったら1に戻す
        if (mCounter>= mPatrolPositions.Count)
        {
            mCounter = 1;
        }

        // NavMeshが動いてるかどうか
        if (!mNav.isStopped)
        {
            mAnim.SetBool("walk", true);
        }
        else
        {
            mAnim.SetBool("walk", false);
        }
    }


    /// <summary>
    /// 巡回ルートをまとめている親をセット
    /// 同時に下記のStartSettingを呼び出す
    /// </summary>
    /// <param name="route"></param>
    /// <param name="num"></param>
    public void SetPatrolRoute(GameObject route)
    {
        mPatrolRoute = route;
        StartSetting();
    }

    /// <summary>
    /// 巡回担当に振り分けられたら呼び出しデータをセット
    /// </summary>
    void StartSetting()
    {
        // NavMeshを稼働させる
        mNav.isStopped = false;

        // 必要なプログラムを作動
        SetPatrolPosition();
        SetSwingPattern();

        // 自分の位置を巡回地点の開始地点(position0)に移動する
        transform.position = mPatrolPositions[0];

        // 開始地点から次の巡回地点をセット
        mNav.SetDestination(mPatrolPositions[mCounter]);

        // モデルの向きを次の巡回地点にする
        transform.LookAt(mPatrolPositions[mCounter]);
        mModel.LookAt(mPatrolPositions[mCounter]);

        // 巡回担当に振り分けられたフラグを立てる
        mIsPatrolShift = true;
    }
    /// <summary>
    /// ①巡回地点のデータをセット
    /// </summary>
    void SetPatrolPosition()
    {
        //巡回ルートの親から巡回ルートのデータをセット
        mPatrolPositions = mPatrolRoute.GetComponent<RoutePositionSave>().mRoutePosition;
    }
    /// <summary>
    /// ②テキストデータから振り向き方向をセット
    /// 二列ぐらいで、一列目に村人の番号・二列目に振り向きの向きをテキストデータで作成
    /// それを取得する
    /// </summary>
    void SetSwingPattern()
    {
        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/PatrolPattern") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            // テキストデータを , と / 区切りに変換
            string line = reader.ReadToEnd().Replace('\n', '/');
            // lineに格納したデータを / で分割し配列に再格納
            string[] data = line.Split('/');
            //
            string[,] swing = new string[data.Length, data[0].Split(',').Length];

            for (int i = 0; i < data.Length; i++)
            {
                string[] value = data[i].Split(',');
                for (int j = 0; j < value.Length; j++)
                {
                    swing[i, j] = value[j];
                }

                if (swing[i, 2] == gameObject.name.Substring(8))
                {
                    switch (int.Parse(swing[i, 3]))
                    {
                        case 0:
                            mSwingPattern = "front";
                            mAngle = 0;
                            break;
                        case 1:
                            mSwingPattern = "left";
                            mAngle = -90f;
                            break;
                        case 2:
                            mSwingPattern = "right";
                            mAngle = 90f;
                            break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// 巡回用振り向き動作
    /// </summary>
    /// <returns></returns>
    IEnumerator Swing()
    {
        mNav.isStopped = true;
        
        yield return new WaitForSeconds(1);

        mAnim.SetBool(mSwingPattern, true);

        yield return new WaitForSeconds(5);

        mAnim.SetBool(mSwingPattern, false);

        AngleSetting();
    }

    void AngleSetting()
    {
        Quaternion a = Quaternion.LookRotation(mPatrolPositions[mCounter] - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.deltaTime / 2);
        mNav.isStopped = false;
    }
}
