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
    private List<string> mPatrolMurabitoList = new List<string>();

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

    [Header("シーンチェンジ確認用:ゲーム外、ゲーム→ゲーム")]
    public bool isSceneChange1 = false;
    [Header("シーンチェンジ確認用:ゲーム→ゲーム外")]
    public bool isSceneChange2 = false;

    [Header("----------")]

    // テキストデータ用変数
    private string[,] data, murabitoData;

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

        //SceneManager.sceneLoaded += SceneLoaded;
        //SceneManager.sceneUnloaded += SceneUnloaded;
        //SceneManager.activeSceneChanged += ActiveSceneChanged; 
    }

    //private void SceneUnloaded(UnityEngine.SceneManagement.Scene arg0)
    //{
    //    print("unload");
    //}

    //private void ActiveSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    //{
    //    print("change");
    //}

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

        // むらびとのルートをルート1からルート5にチェンジ
        // 途中でルート変更するかわからないので保留
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

        // ※デバッグ用 
        if (isSceneChange1)
        {
            SceneManager.LoadScene("Patrol");

            isSceneChange1 = false;
        }
        else if (isSceneChange2)
        {
            SceneManager.LoadScene("Test");

            // リストのデータを初期化
            DataReset();

            isSceneChange2 = false;
        }

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
        TextAsset csv = Resources.Load("Kosugi/PatrolStatus") as TextAsset;
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
    /// <param name="num">巡回するむらびとを出す家番号</param>
    public void SetMurabito(int num)
    {
        /* 0列目:家の番号
         * 1列目:家の名前
         * 2列目:むらびとの名前
         * 3列目:むらびとの番号
         * 4列目:振り向きの方向
         * 5列目:巡回ルートの番号
         * 6列目:巡回開始までの待機時間
         * 7列目:各家庭を巡回開始する時間
         * 
         * 0行目:null
         * 1行目:主人公のデータ(実質null
         * 2行目～は各むらびとのデータ
         */

        // 列番号を指定
        int murabitoNumColumn = 3;

        // 配列のデータ指定ミスや重複を排除
        if (num <= 1 || murabitoData.GetLength(0) < num)
        {
            Debug.LogWarning("指定された家番号は範囲外です");
            return;
        }
        if (mPatrolMurabitoList.Contains("murabito" + data[num, murabitoNumColumn]))
        {
            Debug.LogWarning("指定した家番号のむらびとはListに格納済みです");
            return;
        }

        // 指定された家番号のむらびとのデータを格納
        for (int i = 0; i < data.GetLength(1); i++)
        {
            murabitoData[mPatrolMurabitoList.Count, i] = data[num, i];
        }
        mPatrolMurabitoList.Add("murabito" + murabitoData[mPatrolMurabitoList.Count, murabitoNumColumn]);
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

        print("巡回するむらびと");
        for (int i = 1; i < mPatrolMurabitoList.Count; i++)
        {
            print(i + "人目：" + mPatrolMurabitoList[i]);

            MurabitoPatrol murabito = GameObject.Find(mPatrolMurabitoList[i]).GetComponent<MurabitoPatrol>();
            murabito.StartSetting(transform.GetChild(murabito.GetRouteNumber()).GetComponent<RoutePositionSave>().mRoutePosition);
            if (i < 3)
            {
                isSwitchable = true;
            }
        }
        isRouteSet = false;
    }

    /// <summary>
    /// 巡回担当の配列にセットされているむらびとに必要なデータをセット
    /// </summary>
    void SetMurabitoData()
    {
        /*
         * 0列目:家の番号
         * 1列目:家の名前
         * 2列目:むらびとの名前
         * 3列目:むらびとの番号
         * 4列目:振り向きの方向
         * 5列目:巡回ルートの番号
         * 6列目:巡回開始までの待機時間
         * 7列目:各家庭を巡回開始する時間
         *
         * 0行目:null
         * 1行目:主人公のデータ(実質null
         * 2行目～は各むらびとのデータ
         */

        // 列番号を指定
        int murabitoNumColumn = 3;
        int swingDirectionColumn = 4;
        int patrolRouteNumColumn = 5;
        int patrolIntervalColumn = 6;
        int housePatrolIntervalColumn = 7;

        // むらびとに各データを渡す
        for (int i = 1; i < mPatrolMurabitoList.Count; i++)
        {
            int swing = int.Parse(murabitoData[i, swingDirectionColumn]);
            int route = int.Parse(murabitoData[i, patrolRouteNumColumn]);
            float interval = float.Parse(murabitoData[i, patrolIntervalColumn]);
            float homeInterval = float.Parse(murabitoData[i, housePatrolIntervalColumn]);
            GameObject.Find("murabito" + murabitoData[i, murabitoNumColumn]).GetComponent<MurabitoPatrol>().SetData(swing, route, interval, homeInterval);
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
