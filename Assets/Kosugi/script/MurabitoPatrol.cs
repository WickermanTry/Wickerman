using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private MurabitoNight mMurabitoNight;

    //NavMeshAgent
    NavMeshAgent mNav;

	void Start ()
	{
        SetPatrolPosition();
        mMurabitoNight = GetComponent<MurabitoNight>();
        mNav = GetComponent<NavMeshAgent>();
        Debug.LogError("STOP");
        mNav.enabled = true;

        transform.position = mPatrolPositions[0];
        mNav.SetDestination(mPatrolPositions[0]); 
	}

	void Update ()
	{
        if (mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Idle
            && mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Walk) return;

        if (mMurabitoNight.GetState() != MurabitoNight.MurabitoState.Walk)
            mMurabitoNight.SetState(MurabitoNight.MurabitoState.Walk);

        if (mNav.remainingDistance < 0.1f)
        {
            mNav.SetDestination(mPatrolPositions[mCounter]);

            mCounter++;
        }
        else
        {
            transform.LookAt(mNav.destination);   
        }

        if(mCounter>= mPatrolPositions.Count)
        {
            mCounter = 0;
        }
	}

    void SetPatrolPosition()
    {
        for (int i = 0; i < mPatrolRoute.transform.childCount; i++)
        {
            mPatrolPositions.Add(mPatrolRoute.transform.Find("position" + i).transform.position);
        }
    }

    public void SetPatrolRoute(Transform route,int num)
    {
        mPatrolRoute = route.Find("PatrolRoute" + num).gameObject;
    }
}
