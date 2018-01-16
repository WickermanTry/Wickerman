using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 村人の巡回の管理
/// </summary>
public class PatrolManager : MonoBehaviour
{
    [SerializeField, Header("パトロール担当の村人達")]
    private List<GameObject> mPatrolMurabito = new List<GameObject>();
    
    [Header("パトロールする人数"), Range(1, 13)]
    public int mPatrolValue;

    [Header("パトロール要員のセット")]
    public bool mSet = false;

    //[Space(10)]
    //[SerializeField, Header("*デバッグ用*")]
    //[Header("欠員にする村人の配列番号")]
    //public int debugLostNum;
    //[Header("欠員実行")]
    //public bool debugLostDo;

    void Awake()
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
                mPatrolMurabito.Add(GameObject.Find("Murabito" + swing[i, 2]));
            }
        }
    }

	void Update ()
	{
        if (mSet)
        {
            // 一緒に行動する人数
            int mMember = 3;
            print("巡回する村人");
            for (int i = 1; i <= mPatrolValue; i++)
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
                        break;
                    case 4:
                    case 5:
                    case 6:
                        // 人数が4～6
                        if (i < mMember)
                        {
                            mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                                (transform.Find("Route" + 1).gameObject);
                            print(mPatrolMurabito[i].name + "はRoute5も巡回");
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
            mSet = false;
        }

        // 欠員発生時の処理(新仕様により必要無いかも？)
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
}
