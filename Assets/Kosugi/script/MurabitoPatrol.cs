using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;


/// <summary>
/// パトロール用
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

    //NavMeshAgent
    NavMeshAgent mNav;

	void Start ()
	{
        mMurabitoNight = GetComponent<MurabitoNight>();
        mNav = GetComponent<NavMeshAgent>();

        StartSetting();
    }

	void Update ()
	{
        if (mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Idle
            && mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Walk) return;

        if (mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Walk)
            mMurabitoNight.SetState(MurabitoNight.MurabitoState.Walk);

        float min_Distance = 0.1f;
        if (mNav.remainingDistance < min_Distance)
        {
            mNav.SetDestination(mPatrolPositions[mCounter]);
            mCounter++;
            StartCoroutine(Swing());  
        }
        else
        {
            transform.LookAt(mNav.destination);   
        }

        if(mCounter>= mPatrolPositions.Count)
        {
            mCounter = 0;
        }

        if (mNav.isStopped)
        {
            switch (mSwingPattern[mCounter])
            {
                case "left":
                    print(gameObject.name + "left");
                    break;
                case "right":
                    print(gameObject.name + "right");
                    break;
                case "forward":
                    print(gameObject.name + "forward");
                    break;
            }
        }
	}

    IEnumerator Swing()
    {
        print(mSwingPattern[mCounter]);
        mNav.isStopped = true;

        yield return new WaitForSeconds(5);
        mNav.isStopped = false;
    }

    void StartSetting()
    {
        SetPatrolPosition();
        SetSwingPattern();

        transform.position = mPatrolPositions[0];

        mNav.enabled = true;
        mNav.SetDestination(mPatrolPositions[0]);
    }

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
    public void SetPatrolRoute(Transform route,int num)
    {
        mPatrolRoute = route.Find("PatrolRoute" + num).gameObject;
    }
    public void SetSwingPatternNumber(int num)
    {
        mSwingPatternNum = num;
    }
}
