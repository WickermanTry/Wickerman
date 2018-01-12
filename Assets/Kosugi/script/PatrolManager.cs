using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 村人の巡回の管理
/// </summary>
public class PatrolManager : MonoBehaviour
{
    [Header("※各動作はデバッグ状態です(ここから任意で動作を起動)")]

    [Header("巡回担当を出す家番号"), Range(1, 14)]
    public int _houseNum = 0;

    [Header("巡回担当のむらびとをセット")]
    public bool isMurabitoSet = false;

    [SerializeField, Header("巡回担当のむらびと達(List[0]はnullで使用しない)")]
    private List<GameObject> mPatrolMurabito = new List<GameObject>();

    [Header("----------")]

    [Header("巡回する人数(1～13)"), Range(1, 13)]
    public int _patrolValue;

    [Header("巡回担当にルートをセット(ルートの再設定機能は未実装)")]
    public bool isRouteSet = false;

    [Header("----------")]

    [Header("各家を巡回する担当にルートをセット")]
    public bool isRoute5Set = false;

    [Header("ルート1→ルート5になる人数(ルート1担当の人数)")]
    public int _count = 0;

    //[Space(10)]
    //[SerializeField, Header("*デバッグ用*")]
    //[Header("欠員にする村人の配列番号")]
    //public int debugLostNum;
    //[Header("欠員実行")]
    //public bool debugLostDo;

    void Awake()
    {
        mPatrolMurabito.Clear();
        mPatrolMurabito.Add(null);

        // 仮
        //SetPatrol_All();
    }

	void Update ()
	{
        // 一緒に行動する人数
        int mMember = 3;

        if (isMurabitoSet)
        {
            SetPatrol_Murabito(_houseNum);
            isMurabitoSet = false;
        }

        if (isRouteSet)
        {
            // List[0]のみの場合は処理を進めない
            if (mPatrolMurabito.Count <= 1)
            {
                Debug.LogWarning("Listにむらびとが格納されていません");
                isRouteSet = false;
                return;
            }

            print("巡回するむらびと");
            for (int i = 1; i <= _patrolValue; i++)
            {
                print(i + "人目：" + mPatrolMurabito[i]);

                switch (i)
                {
                    case 1:
                    case 2:
                    case 3:
                        // 人数が1～3
                        mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                            (transform.Find("Route" + 1).gameObject);
                        _count = i;
                        break;
                    case 4:
                    case 5:
                    case 6:
                        // 人数が4～6
                        if (i < mMember)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 1).gameObject);
                        }
                        else
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 2).gameObject);
                        }
                        break;
                    case 7:
                    case 8:
                    case 9:
                        // 人数が7～9
                        if (i < mMember)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 1).gameObject);
                        }
                        else if (i < mMember * 2)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 2).gameObject);
                        }
                        else
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 3).gameObject);
                        }
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        // 人数が10～
                        if (i < mMember)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 1).gameObject);
                        }
                        else if (i < mMember * 2)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 2).gameObject);
                        }
                        else if (i < mMember * 3)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 3).gameObject);
                        }
                        else
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 4).gameObject);
                        }
                        break;
                }
            }
            isRouteSet = false;
        }

        if (isRoute5Set)
        {
            // List[0]のみの場合は処理を進めない
            if (mPatrolMurabito.Count <= 1)
            {
                Debug.LogWarning("Listにむらびとが格納されていません");
                isRoute5Set = false;
                return;
            }

            for (int i = 1; i <= _count; i++)
            {
                mPatrolMurabito[i].GetComponent<MurabitoPatrol>().RouteReset();
                mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 5).gameObject);
            }

            isRoute5Set = false;
        }

        // 欠員発生時の処理(必要無い)
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

    void SetPatrol_All()
    {
        /*テキストのデータをすべて格納する*/

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
                mPatrolMurabito.Add(GameObject.Find("Murabito" + swing[i, 2]));
            }
        }
    }
    void SetPatrol_Murabito(int num)
    {
        /*家番号からテキストのデータを指定して格納する*/

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

            if (num < 1 && data.Length < num)
            {
                break;
            }

            /*
             * 0列目:家の番号--------使う(_houseListNum
             * 1列目:家の名前--------使わない
             * 2列目:むらびとの名前--使わない
             * 3列目:むらびとの番号--使う(_murabitoListNum
             * 4列目:振り向きの向き--使う(_swingListNum
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
            if (mPatrolMurabito.Contains(GameObject.Find("Murabito" + swing[_houseListNum, _murabitoListNum])))
            {
                Debug.LogWarning("指定した家番号のむらびとはListに格納済みです");
                break;
            }

            print("Murabito" + swing[_houseListNum, _murabitoListNum]); 
            mPatrolMurabito.Add(GameObject.Find("Murabito" + swing[_houseListNum, _murabitoListNum]));
        }
    }
}
