using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum StateList
{
    Normal,
    falsePoint
};

public class junkai : MonoBehaviour
{
    private StateList pointlist;

    public Transform[] m_PatrolPoint;

    NavMeshAgent m_Agent;
    Animator m_Animator;

    public bool ActionTime = false;

    int m_CurrentPatrolPointIndex = -1;
    public float m_Speed = 1f;

    void Start()
    {
        pointlist = StateList.Normal;                
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        SetNewPatrolPointToDestination();


        //point = GameObject.Find("point");

        //enemy = GameObject.FindWithTag("Enemy");
    }

    void Update()
    {

        if (HasArrived() && ActionTime == false)
        {
            SetNewPatrolPointToDestination();
        }

      
       
    }
    void  SetNewPatrolPointToDestination()
    {
        m_CurrentPatrolPointIndex = (m_CurrentPatrolPointIndex + 1) % m_PatrolPoint.Length;
        m_Agent.destination = m_PatrolPoint[m_CurrentPatrolPointIndex].position;
        m_Animator.SetBool("Walk", true);
        
    }

    bool HasArrived()
    {
        return (Vector3.Distance(m_Agent.destination, transform.position) < 0.5f);
    }

    

    public void OnTriggerEnter(Collider other)
    {
        //オブジェクトPoint(2)
        if(other.tag == "point" && pointlist == StateList.Normal)
        {
            Debug.Log("a");
            AlartMethod();
        }
        //オブジェクトPoint
        else if (other.tag == "stopPoint" && pointlist == StateList.falsePoint)
        {
            Alart();
        }
        //オブジェクトPoint(1)
        else if (other.tag == "DisappearPoint" && pointlist == StateList.falsePoint)
        {
            Disappear();
        }
    }
    void AlartMethod()
    {

        if(ActionTime == false)
        {
            Debug.Log("b");
            GetComponent<NavMeshAgent>().enabled = false;
            m_Animator.SetBool("kubihuri", true);
            m_Animator.SetBool("Walk", false);
            ActionTime = true;
        }
        Debug.Log("c");
        Invoke("DelayMethod", 3);
    }
    private void DelayMethod()
    {
        Debug.Log("d");
        m_Animator.SetBool("kubihuri", false);
        if (ActionTime == true)
        {
            pointlist = StateList.falsePoint;
            GetComponent<NavMeshAgent>().enabled = true;
            m_Animator.SetBool("Walk", true);
            ActionTime = false;
        }
        
    }
    void Alart()
    {
        if(ActionTime == false)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            m_Animator.SetBool("Walk", false);
            ActionTime = true;
        }
        Invoke("Delay",2);
    }
    private void Delay()
    {
        if(ActionTime == true)
        {
            pointlist = StateList.Normal;
            GetComponent<NavMeshAgent>().enabled = true;
            ActionTime = false;
        }
    }


    private void Disappear()
    {
        if (ActionTime == false)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            m_Animator.SetBool("Walk", false);
            ActionTime = true;
        }
        Invoke("disappearPoint", 2);
    }
    private void disappearPoint()
    {
        if (ActionTime == true)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            ActionTime = false;
        }
    }
}
