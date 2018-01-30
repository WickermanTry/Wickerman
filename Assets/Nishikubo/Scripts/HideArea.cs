using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//隠せる場所用クラス
public class HideArea : MonoBehaviour {

    private Player m_player;
    private UIDisplay m_uiDisplay;
    private MyItemStatus m_myItemStatus;

    private int m_hideCountMax = 3;//最大の隠せる数
    [SerializeField]
    private int m_hideCount = 0;//隠している数
    public int hideCount
    {
        get { return m_hideCount; }
        set { m_hideCount++; }
    }

    private bool m_isSteal;


    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_uiDisplay = GameObject.Find("PlayerCanvas").GetComponent<UIDisplay>();
        m_myItemStatus = m_player.GetComponent<MyItemStatus>();
    }

    void Update()
    {
        if(m_hideCountMax<=m_hideCount)
        {
            //もう隠せない
            Debug.Log("いっぱいです");
        }
        else if(m_hideCount<m_hideCountMax)
        {
            Debug.Log("まだ隠せる " + m_hideCount);
        }

        for (int i = 0; i < m_myItemStatus.GetItemFlagTotal.Length; i++)
        {
            //一つでも盗んでたら
            if (m_myItemStatus.GetItemFlagTotal[i] == true)
            {
                m_isSteal = true;
                Debug.Log("trueーーーー");
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        //隠せるエリア内&&盗んでる
        if (other.gameObject.tag == "Player" && m_isSteal)
        {
            m_uiDisplay.ImageActive(0, true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        //隠せるエリア内
        if (other.gameObject.tag == "Player")
        {
            m_uiDisplay.ImageActive(0, false);
        }
    }


}
