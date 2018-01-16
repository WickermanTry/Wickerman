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
    private List<string> mPatrolMurabito = new List<string>();

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
    private bool isChangeTrue = false;

    public bool isTest = false;

    //[Space(10)]
    //[SerializeField, Header("*デバッグ用*")]
    //[Header("欠員にする村人の配列番号")]
    //public int debugLostNum;
    //[Header("欠員実行")]
    //public bool debugLostDo;

    void Start()
    {
        // シーン跨いでも破棄せず巡回するむらびとのListデータを確保する
        DontDestroyOnLoad(gameObject);

        // (念のため)Listをリセットする
        mPatrolMurabito.Clear();
        mPatrolMurabito.Add(null);
    }

	void Update ()
	{
        // ※デバッグ用 巡回担当のむらびとをセット
        if (isMurabitoSet)
        {
            SetPatrolMurabito(_houseNum);
            isMurabitoSet = false;
        }

        // 巡回担当にルートをセット
        if (isRouteSet)
        {
            SetRoute(_patrolValue);
        }

        // むらびとのルートをルート1からルート5にチェンジ
        if (isRouteChange && isChangeTrue)
        {
            // 4人未満(ルート1の人数しかいない)場合は処理を進めない
            if (mPatrolMurabito.Count <= 4)
            {
                Debug.LogWarning("ルート5にシフトする為のむらびとが足りません");
                isRouteChange = false;
                return;
            }

            for (int i = 1; i <= 3; i++)
            {
                GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().RouteReset();
                GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().SetPatrolRoute
                                ("Route" + 5, transform.Find("Route" + 5).GetComponent<RoutePositionSave>().mRoutePosition);
            }

            isRouteChange = false;
        }

        // ※デバッグ用 
        if (isTest)
        {
            SceneManager.LoadScene("Test");
            isTest = false;
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

    public void SetRoute(int num)
    {
        // List[0]のみの場合は処理を進めない
        if (mPatrolMurabito.Count <= 1 || mPatrolMurabito.Count < num)
        {
            Debug.LogWarning("巡回担当の人数とListのむらびとの数が合いません");
            isRouteSet = false;
            return;
        }

        print("巡回するむらびと");
        for (int i = 1; i <= num; i++)
        {
            print(i + "人目：" + mPatrolMurabito[i]);

            switch (i)
            {
                case 1:
                case 2:
                case 3:
                    // 人数が1～3
                    GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().SetPatrolRoute
                        ("Route" + 1, transform.Find("Route" + 1).GetComponent<RoutePositionSave>().mRoutePosition);
                    break;
                case 4:
                case 5:
                case 6:
                    // 人数が4～6
                    isChangeTrue = true;
                    GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().SetPatrolRoute
                        ("Route" + 2, transform.Find("Route" + 2).GetComponent<RoutePositionSave>().mRoutePosition);
                    break;
                case 7:
                case 8:
                case 9:
                    // 人数が7～9
                    GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().SetPatrolRoute
                        ("Route" + 3, transform.Find("Route" + 3).GetComponent<RoutePositionSave>().mRoutePosition);
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                    // 人数が10～
                    GameObject.Find(mPatrolMurabito[i]).GetComponent<MurabitoPatrol>().SetPatrolRoute
                        ("Route" + 4, transform.Find("Route" + 4).GetComponent<RoutePositionSave>().mRoutePosition);
                    break;
            }
        }
        isRouteSet = false;
    }

    /// <summary>
    /// ※デバッグ用 
    /// 巡回するすべてのむらびとを格納する(現在使用していない)
    /// </summary>
    void SetPatrol_All()
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

                print("Murabito" + swing[i, 2]);
                mPatrolMurabito.Add("Murabito" + swing[i, 2]);
            }
        }
    }
    /// <summary>
    /// 家番号から巡回するむらびとを指定して格納する
    /// </summary>
    /// <param name="num"></param>
    public void SetPatrolMurabito(int num)
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
            // 適当な行から1行の長さを取得
            string[,] swing = new string[data.Length, data[0].Split(',').Length];

            if (num < 1 || data.Length <= num)
            {
                print("指定された家番号は範囲外です");
                break;
            }

            /*
             * 0列目:家の番号--------使う：_houseListNum
             * 1列目:家の名前--------使わない
             * 2列目:むらびとの名前--使わない
             * 3列目:むらびとの番号--使う：_murabitoListNum
             * 4列目:振り向きの向き--使う：_swingListNum
             */

            int _houseListNum = num - 1;
            int _murabitoListNum = 3;

            string[] value = data[_houseListNum].Split(',');
            for (int j = 0; j < value.Length; j++)
            {
                swing[_houseListNum, j] = value[j];
            }

            // プレイヤーの家を指定orList重複を判定
            if (num == 1)
            {
                Debug.LogWarning("指定した家番号のむらびとはプレイヤーです");
                break;
            }
            if (mPatrolMurabito.Contains("Murabito" + swing[_houseListNum, _murabitoListNum]))
            {
                Debug.LogWarning("指定した家番号のむらびとはListに格納済みです");
                break;
            }

            print("Murabito" + swing[_houseListNum, _murabitoListNum]); 
            mPatrolMurabito.Add("Murabito" + swing[_houseListNum, _murabitoListNum]);
        }
    }
}
