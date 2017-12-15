using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 村人パトロールの管理
/// </summary>
public class PatrolManager : MonoBehaviour
{
    [SerializeField, Header("パトロール担当の村人達")]
    private List<GameObject> mPatrolMurabito = new List<GameObject>();

    [Header("優先度順の村人の番号")]
    private string[] mMurabitoNum;

    [Header("パトロールする人数"), Range(3, 7)]
    public int mPatrolValue;

    [Header("パトロール要員のセット")]
    public bool mSet = false;

    [Space(10)]
    [SerializeField, Header("*デバッグ用*")]
    [Header("欠員にする村人の配列番号")]
    public int debugLostNum;
    [Header("欠員実行")]
    public bool debugLostDo;

    void Start()
    {
        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/Patrol") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');
            for (int i = 0; i < data.Length; i++)
            {
                //該当する番号の村人をシーン内からFindで探して格納
                mPatrolMurabito.Add(GameObject.Find("Murabito" + data[i]));
            }
        }
    }

	void Update ()
	{
        if (mSet)
        {
            if (mPatrolValue < 0 || mPatrolMurabito.Count < mPatrolValue)
            {
                Debug.LogWarning("パトロール対象人数が対応外です！");
                mSet = false;
                return;
            }

            print("パトロールする村人");
            for (int i = 0; i < mPatrolValue; i++)
            {
                print((i + 1) + "人目：" + mPatrolMurabito[i]);

                if (mPatrolMurabito[i].GetComponent<MurabitoPatrol>().enabled == false)
                {
                    mPatrolMurabito[i].GetComponent<MurabitoPatrol>().enabled = true;
                    mPatrolMurabito[i].GetComponent<MurabitoPatrol>().SetPatrolRoute
                        (transform.Find(mPatrolValue + "MurabitoVer"), i + 1);
                }
            }

            Active();

            mSet = false;
        }

        if (debugLostDo)
        {
            if (debugLostNum < 0 || mPatrolMurabito.Count < debugLostNum)
            {
                Debug.LogWarning("デバッグ対象番号が対応外です！");
                debugLostDo = false;
                return;
            }

            print("欠員対象村人：" + mPatrolMurabito[debugLostNum]);
            string nameValue = mPatrolMurabito[debugLostNum].name;
            mPatrolMurabito[debugLostNum] = mPatrolMurabito[mPatrolValue];
            mPatrolMurabito.RemoveAt(mPatrolValue);
            Destroy(GameObject.Find(nameValue));
            mSet = true;
            debugLostDo = false;
        }
	}

    void Active()
    {
        transform.Find(mPatrolValue + "MurabitoVer").gameObject.SetActive(true);
    }
}
