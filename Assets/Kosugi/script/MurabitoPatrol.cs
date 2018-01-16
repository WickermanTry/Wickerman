using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// 巡回プログラム
/// </summary>
public class MurabitoPatrol : MonoBehaviour
{
    [Header("巡回担当に振り分けられたか(巡回しているかの判断)")]
    public bool isPatrolShift;

    [SerializeField, Header("※デバッグ用 巡回ルートの名前")]
    private string mPatrolRouteName;

    [SerializeField, Header("巡回ルートの各地点")]
    private List<Vector3> mPatrolPositions;

    [SerializeField, Header("巡廻地点用カウンター")]
    private int _counter = 0;

    [SerializeField, Header("巡回時の振り向きパターン")]
    private string mSwingPattern = "";

    [Header("振り向き時の回転角度")]
    private float _angle = 0;

    [SerializeField, Header("振り向きにかける時間(とりあえず3s～10sまで)"), Range(3, 10)]
    private int _swingTime = 0;

    private bool isNotPointWarp = false;

    /*---内部データ---*/
    Animator mAnim; // Animator取得用

    Transform mModel; // 14.!Root取得用

    NavMeshAgent mNav; // NavMeshAgent取得用

	void Start ()
	{
        mNav = GetComponent<NavMeshAgent>();
        mNav.isStopped = true;

        mAnim = GetComponent<Animator>();
        mModel = transform.Find("14.!Root");
    }

	void Update ()
	{
        // 巡回担当になっていない場合は巡回処理をしない
        if (!isPatrolShift) return;

        Patrol();
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
            mNav.SetDestination(mPatrolPositions[_counter]);
        }

        // 巡回地点の総数以上のカウントになったら1に戻す
        if (_counter >= mPatrolPositions.Count)
        {
            _counter = 0;
        }

        // NavMeshが動いてるかどうか
        if (!mNav.isStopped && !mAnim.GetBool("walk"))
        {
            mAnim.SetBool("walk", true);
        }
        else if(mNav.isStopped && mAnim.GetBool("walk"))
        {
            mAnim.SetBool("walk", false);
        }
    }

    /// <summary>
    /// 巡回ルートをまとめた親から巡回ルートの地点のListをセット
    /// 巡回するのに必要な準備の処理を呼び出す
    /// </summary>
    /// <param name="route"></param>
    /// <param name="num"></param>
    public void SetPatrolRoute(string name, List<Vector3> route)
    {
        mPatrolRouteName = name;
        mPatrolPositions = route;

        // 巡回ルートのデータをセットされたら準備の処理を起動
        StartSetting();
    }

    /// <summary>
    /// 準備用の処理
    /// </summary>
    void StartSetting()
    {
        // NavMeshを起動させる
        mNav.isStopped = false;

        // 必要なプログラムを作動
        SetSwingPattern();

        // 初期設定時のみ自分の位置を巡回地点の開始地点(position0)に移動する
        if (!isNotPointWarp)
        {
            transform.position = mPatrolPositions[0];
            isNotPointWarp = false;
        }

        // 開始地点から次の巡回地点をセット
        mNav.SetDestination(mPatrolPositions[_counter]);

        // モデルの向きを次の巡回地点にする
        transform.LookAt(mPatrolPositions[_counter]);
        mModel.LookAt(mPatrolPositions[_counter]);

        // 巡回担当に振り分けられたフラグを立てる
        isPatrolShift = true;
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

            /*
             * 0列目:家の番号--------使う(_houseListNum
             * 1列目:家の名前--------使わない
             * 2列目:むらびとの名前--使わない
             * 3列目:むらびとの番号--使う(_murabitoListNum
             * 4列目:振り向きの向き--使う(_swingListNum
             */

            int _murabitoListNum = 3;
            int _swingListNum = 4;

            float _swingAngle = 90f;

            for (int i = 0; i < data.Length; i++)
            {
                string[] value = data[i].Split(',');
                for (int j = 0; j < value.Length; j++)
                {
                    swing[i, j] = value[j];
                }

                if (swing[i, _murabitoListNum] == gameObject.name.Substring(8))
                {
                    switch (int.Parse(swing[i, _swingListNum]))
                    {
                        case 0:
                            mSwingPattern = "front";
                            _angle = 0;
                            break;
                        case 1:
                            mSwingPattern = "left";
                            _angle = -_swingAngle;
                            break;
                        case 2:
                            mSwingPattern = "right";
                            _angle = _swingAngle;
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

        // 指定した時間振り向く
        yield return new WaitForSeconds(_swingTime);

        mAnim.SetBool(mSwingPattern, false);

        AngleSetting();
    }

    void AngleSetting()
    {
        Quaternion a = Quaternion.LookRotation(mPatrolPositions[_counter] - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.deltaTime / 2);
        mNav.isStopped = false;
    }

    /// <summary>
    /// 巡回ルートを再設定するためにいろいろ値をリセット
    /// </summary>
    public void RouteReset()
    {
        isPatrolShift = false;
        mPatrolRouteName = null;
        mPatrolPositions.Clear();
        _counter = 0;
        mSwingPattern = "";

        isNotPointWarp = true;
    }
}
