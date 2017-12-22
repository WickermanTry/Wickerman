using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// 巡回プログラム ※スタートのカウントを1にする＝ルートのポイントも1から始める
/// </summary>
public class MurabitoPatrol : MonoBehaviour
{
    [SerializeField, Header("パトロール経路")]
    private GameObject mPatrolRoute;

    [SerializeField, Header("パトロール経路の通過地点")]
    private List<Vector3> mPatrolPositions;

    [SerializeField, Header("巡廻ポイント用カウンター")]
    private int mCounter = 1;

    [SerializeField, Header("パトロール時の首振りパターン番号")]
    private int mSwingPatternNum = 0;
    [SerializeField, Header("パトロール時の首振りパターン")]
    private List<string> mSwingPattern = new List<string>();

    private MurabitoNight mMurabitoNight;

    [Header("アニメーター")]
    Animator mAnim;

    //NavMeshAgent
    NavMeshAgent mNav;

	void Start ()
	{
        mMurabitoNight = GetComponent<MurabitoNight>();
        mNav = GetComponent<NavMeshAgent>();
        mAnim = GetComponent<Animator>();

        StartSetting();
    }

	void Update ()
	{
        // 目指す巡回ポイントとの距離が0.1f以下になったら次の巡回ポイントをセット
        float min_Distance = 0.1f;
        if (mNav.remainingDistance < min_Distance)
        {
            mNav.SetDestination(mPatrolPositions[mCounter]);
            StartCoroutine(Swing(mCounter));
            mCounter++;
        }
        else
        {
            transform.LookAt(mNav.destination);
        }

        // 巡回ポイント以上のカウントになったら0に戻す
        if(mCounter>= mPatrolPositions.Count)
        {
            mCounter = 0;
        }

        // NavMeshが動いていればwalkアニメーションを再生
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
    /// 巡回用首振り
    /// </summary>
    /// <returns></returns>
    IEnumerator Swing(int num)
    {
        print(mSwingPattern[num]);
        
        mNav.isStopped = true;
        mAnim.SetBool(mSwingPattern[num], true);
        mAnim.SetTrigger("swing");

        yield return new WaitForSeconds(5);

        mAnim.SetBool(mSwingPattern[num], false);
        mNav.isStopped = false;
    }

    /// <summary>
    /// 巡回担当に振り分けられたら呼び出してデータをセット
    /// </summary>
    void StartSetting()
    {
        SetPatrolPosition();
        SetSwingPattern();

        transform.position = mPatrolPositions[0];

        mNav.enabled = true;
        mNav.SetDestination(mPatrolPositions[0]);
    }

    /// <summary>
    /// 巡回ルートをセット
    /// </summary>
    /// <param name="route"></param>
    /// <param name="num"></param>
    public void SetPatrolRoute(Transform route, int num)
    {
        mPatrolRoute = route.Find("PatrolRoute" + num).gameObject;
    }
    /// <summary>
    /// 巡回ポイントのデータをセット
    /// </summary>
    void SetPatrolPosition()
    {
        for (int i = 0; i < mPatrolRoute.transform.childCount; i++)
        {
            mPatrolPositions.Add(mPatrolRoute.transform.Find("position" + i).transform.position);
        }
    }
    void SetSwingPattern()
    {
        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/PatrolSwingPattern") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            // テキストデータを , と / 区切りに変換
            string line = reader.ReadToEnd().Replace('\n', '/');
            // lineに格納したデータを / で分割し配列に再格納
            string[] data = line.Split('/');

            int dataLength = data[mSwingPatternNum].Split(',').Length;

            for (int i = 0; i < dataLength; i++)
            {
                mSwingPattern.Add(data[mSwingPatternNum].Split(',')[i]);
            }
        }
    }
    
    /// <summary>
    /// 巡回用首振りのデータをセット
    /// </summary>
    /// <param name="num"></param>
    public void SetSwingPatternNumber(int num)
    {
        mSwingPatternNum = num;
    }
}
