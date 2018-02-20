using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// 村人の巡回の管理
/// </summary>
public class PatrolManager : MonoBehaviour
{
    [SerializeField, Header("巡回担当のむらびと達(List[0]はnullで使用しない)")]
    private List<GameObject> mPatrolMurabitoList = new List<GameObject>();
    [SerializeField]
    private List<int> mListNum = new List<int>();

    [Header("----------")]

    [Header("※デバッグ用 巡回担当を出す家番号"), Range(1, 14)]
    public int _houseNum = 0;

    [Header("※デバッグ用 巡回担当のむらびとをセット")]
    public bool isMurabitoSet = false;

    [Header("----------")]

    [Header("※デバッグ用 巡回する人数(1～13)"), Range(1, 13)]
    public int _patrolValue;

    [Header("※デバッグ用 巡回担当にルートをセット(ルートの再設定機能は未実装)")]
    public bool isRouteSet = false;

    [Header("----------")]

    [Header("家を巡回するルートに変更")]
    public bool isRouteChange = false;

    [SerializeField, Header("ルートチェンジ可能か")]
    private bool isSwitchable = false;

    [Header("----------")]

    // テキストデータ用変数
    private string[,] data, murabitoData;

    public int _dayCheck = 0;

    // DontDestroyOnLoad用
    static PatrolManager patrolManager = null;
    /// <summary>
    /// DontDestroyOnLoad用
    /// </summary>
    static PatrolManager Instance
    {
        get { return patrolManager ?? (patrolManager = FindObjectOfType<PatrolManager>()); }
    }

    //[Space(10)]
    //[SerializeField, Header("*デバッグ用*")]
    //[Header("欠員にする村人の配列番号")]
    //public int debugLostNum;
    //[Header("欠員実行")]
    //public bool debugLostDo;

    private void Awake()
    {
        // オブジェクトが重複しているかのチェック
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // テキストデータを読み込む
        DataExport();

        // (念のため)Listをリセットする 0番目は使わないのでnull
        mPatrolMurabitoList.Clear();
        mPatrolMurabitoList.Add(null);

        mListNum.Clear();
        mListNum.Add(0);
    }

    void Update ()
	{
        // ※デバッグ用 巡回担当のむらびとをセット
        if (isMurabitoSet)
        {
            SetMurabito(_houseNum);
            isMurabitoSet = false;
        }

        // ※デバッグ用 巡回担当にルートをセット
        if (isRouteSet)
        {
            SetRoute();// _patrolValue);
        }

        if (_dayCheck == AwakeData.Instance.dayTime_)
        {
            _dayCheck = AwakeData.Instance.dayTime_ + 1;
            for(int i = 1; i < mPatrolMurabitoList.Count; i++)
            {
                mPatrolMurabitoList[i].GetComponent<MurabitoPatrol>().DataReset();
            }
        }

        // むらびとのルートをチェンジ
        // 保留
        //if (isRouteChange && isSwitchable)
        //{
        //    // 4人未満(ルート1の人数しかいない)場合は処理を進めない
        //    if (mPatrolMurabito.Count <= 4)
        //    {
        //        Debug.LogWarning("ルート5にシフトする為のむらびとが足りません");
        //        isRouteChange = false;
        //        return;
        //    }

            //    for (int i = 1; i <= 3; i++)
            //    {
            //        MurabitoPatrol murabito = GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>();
            //        //murabito.StartSetting(transform.Find("Route" + 5).GetComponent<RoutePositionSave>().mRoutePosition);
            //    }

            //    isRouteChange = false;
            //}

            // 欠員発生時の処理(必要無くなった)
            //if (debugLostDo)
            //{
            //    if (debugLostNum < 0 || mPatrolMurabito.Count < debugLostNum)
            //    {
            //        Debug.LogWarning("デバッグ対象番号が対応外です！");
            //        debugLostDo = false;
            //        return;
            //    }

            //    print("欠員対象村人：" + mPatrolMurabito[debugLostNum]);
            //    string nameValue = mPatrolMurabito[debugLostNum].name;
            //    mPatrolMurabito[debugLostNum] = mPatrolMurabito[mPatrolValue];
            //    mPatrolMurabito.RemoveAt(mPatrolValue);
            //    Destroy(GameObject.Find(nameValue));
            //    mSet = true;
            //    debugLostDo = false;
            //}
    }

    /// <summary>
    /// テキストデータを読み込む
    /// </summary>
    void DataExport()
    {
        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/MurabitoPatrol") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            // テキストデータを , と / 区切りに変換
            string text = reader.ReadToEnd().Replace('\n', '/');
            // textに格納したデータを / で分割し配列に再格納
            string[] line = text.Split('/');
            // 適当な行から1行の長さを取得し配列の長さを指定
            data = new string[line.Length, line[0].Split(',').Length];
            // データ格納で使用する配列の長さを指定
            murabitoData= new string[line.Length, line[0].Split(',').Length];

            for (int i = 0; i < line.Length; i++)
            {
                // 家番号で指定された行のデータを格納
                string[] value = line[i].Split(',');
                for (int j = 0; j < value.Length; j++)
                {
                    data[i, j] = value[j];
                }
            }
        }
    }

    /// <summary>
    /// データセットに必要な関数①
    /// 巡回するむらびとを指定して巡回担当の配列に格納
    /// </summary>
    /// <param name="num">巡回するむらびとの番号</param>
    public void SetMurabito(int num)
    {
        /*
         * 0列目:むらびとの番号
         * 1列目:巡回ルートの番号
         * 2列目:振り向きの方向
         * 3列目:巡回開始までの待機時間
         * 4列目:各家庭を巡回開始する時間
         * 5列目:家の名前
         * 6列目:むらびとの名前
         * 
         * 0行目:null
         * 1行目:主人公のデータ(実質null
         * 2行目～は各むらびとのデータ
         */

        // 列番号を指定
        int murabitoNumColumn = 0;  //横
        int murabitoNumRow = 0;     //縦

        // 指定したむらびと番号のむらびとが巡回対象か調べる
        // いなかった場合でもメッセージ等は出ない
        for (int i = 0; i < data.GetLength(0); i++)
        {
            if (data[i, murabitoNumColumn] == num.ToString())
            {
                murabitoNumRow = i;
                mListNum.Add(i);
                break;
            }
        }

        // 配列のデータ指定ミスや重複を排除
        if (murabitoNumRow <= 1 || murabitoData.GetLength(0) < murabitoNumRow)
        {
            Debug.LogWarning("指定したむらびと番号は存在しません");
            return;
        }
        if (mPatrolMurabitoList.Contains(GameObject.Find("murabito" + data[murabitoNumRow, murabitoNumColumn])))
        {
            
            Debug.LogWarning("指定したむらびとはListに格納済みです");
            return;
        }

        // 指定された家番号のむらびとのデータを格納
        for (int i = 0; i < data.GetLength(1); i++)
        {
            murabitoData[mPatrolMurabitoList.Count, i] = data[murabitoNumRow, i];
        }
        mPatrolMurabitoList.Add(GameObject.Find("murabito" + murabitoData[mPatrolMurabitoList.Count, murabitoNumColumn]));
        print("Set -> Murabito" + murabitoData[mPatrolMurabitoList.Count - 1, murabitoNumColumn]);
    }

    /// <summary>
    /// データセットに必要な関数②
    /// むらびとの巡回を開始させる
    /// </summary>
    /// <param name="num">巡回させる人数(現在保留)</param>
    public void SetRoute()//int num)
    {
        // 0番目のみの場合は処理を進めない
        if (mPatrolMurabitoList.Count <= 1)// || mPatrolMurabitoList.Count < num)
        {
            Debug.LogWarning("巡回担当の人数とListのむらびとの数が合いません");
            isRouteSet = false;
            return;
        }

        // 配列にいるむらびとにデータを渡す
        SetMurabitoData();

        isRouteSet = false;
    }

    /// <summary>
    /// 巡回担当の配列にセットされているむらびとに必要なデータをセット
    /// 巡回直前で実行しデータを渡す
    /// </summary>
    void SetMurabitoData()
    {
        /*
         * 0列目:むらびとの番号
         * 1列目:巡回ルートの番号
         * 2列目:振り向きの方向
         * 3列目:巡回開始までの待機時間
         * 4列目:各家庭を巡回開始する時間
         * 5列目:家の名前
         * 6列目:むらびとの名前
         *
         * 0行目:null
         * 1行目:主人公のデータ(実質null
         * 2行目～は各むらびとのデータ
         */

        // データの列番号を指定
        int swingDirectionColumn = 2;
        int patrolRouteNumColumn = 1;
        int patrolIntervalColumn = 3;
        int housePatrolIntervalColumn = 4;

        // むらびとに各データを渡す
        print("巡回するむらびと");
        for (int i = 1; i < mPatrolMurabitoList.Count; i++)
        {
            print(i + "人目：" + mPatrolMurabitoList[i]);

            // 振り向きの方向
            int swing = int.Parse(murabitoData[i, swingDirectionColumn]);
            // 巡回ルートの番号
            int route = int.Parse(murabitoData[i, patrolRouteNumColumn]);
            // 巡回開始までの待機時間
            float interval = float.Parse(murabitoData[i, patrolIntervalColumn]);
            // 各家庭の巡回を開始するまでの時間
            float homeInterval = float.Parse(murabitoData[i, housePatrolIntervalColumn]);

            // 巡回ルートを格納している配列のあるRoutePositionSaveスクリプト
            RoutePositionSave patrolRoute = transform.GetChild(route).GetComponent<RoutePositionSave>();
            // 担当の巡回ルートは既に誰か巡回しているかどうか
            bool isAlready = patrolRoute.GetAlreadyPatrolFlag();

            mPatrolMurabitoList[i].GetComponent<MurabitoPatrol>().SetData
                (swing, route, interval, homeInterval, patrolRoute.mRoutePosition, isAlready);

            // 巡回ルートのフラグを立てる
            patrolRoute.SetAlreadyPatrolFlag(true);

            // 各家庭を巡回するルートを別途渡す
            if (i < 3)
            {
                isSwitchable = true;
            }
        }
    }

    /// <summary>
    /// データ初期化用
    /// </summary>
    public void DataReset()
    {
        mPatrolMurabitoList.Clear();
        mPatrolMurabitoList.Add(null);
    }

    /// <summary>
    /// // DontDestroyOnLoad用
    /// </summary>
    private void OnDestroy()
    {
        if (this == Instance) patrolManager = null;
    }
}
