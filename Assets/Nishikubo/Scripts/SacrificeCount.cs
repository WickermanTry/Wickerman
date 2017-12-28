using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生贄の数によって
/// </summary>
public class SacrificeCount : MonoBehaviour {

    private Player m_player;
    private int m_sacrificeCount;
    [SerializeField, Tooltip("フェードする時間")]
    private float m_fadeTime = 2.0f;

    // Use this for initialization
    void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //m_sacrificeCount = AwakeData.Instance.sacrificeCount;
    }

    //生贄が増える
    public void CountUp()
    {
        m_sacrificeCount += 1;
        //AwakeData.Instance.sacrificeCount = m_sacrificeCount;

        if (m_sacrificeCount >= 15)
        {
            m_sacrificeCount = 15;
            //ゲームクリア
            //シーン遷移
            SceneNavigator.Instance.Change("GameClear", m_fadeTime);
        }
        else
        {
            SceneNavigator.Instance.Fade(m_fadeTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && m_player.state==PlayerState.Trailing)
        {
            if (m_sacrificeCount <= 14)
            {
                CountUp();
            }
        }
    }
}
