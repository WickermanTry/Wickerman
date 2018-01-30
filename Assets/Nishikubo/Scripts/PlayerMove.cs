using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ものによっての速度変更と重さ管理
public class PlayerMove : MonoBehaviour {

    private Player m_player;

    private int m_maxMass;
    [SerializeField]
    private int m_mass;//現在の持っている重さ
    private bool m_pluralityHave = false;//複数持っているかどうか

    private float m_inputH, m_inputV = 0.0f;    //移動入力チェック用
    [SerializeField, Tooltip("移動速度")]
    private float m_speed = 3.0f;
    private float m_sp;//スピード保持
    private Rigidbody m_rb;


    // Use this for initialization
    void Start () {
        m_player = this.GetComponent<Player>();
        //this.transform.position = AwakeData.Instance.playerPosition_;
        //this.transform.rotation = AwakeData.Instance.playerRotation_;

        m_maxMass = AwakeData.Instance.maxMass;
        m_mass = AwakeData.Instance.mass;

        m_sp = m_speed;
        m_rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

    }

    public void CarryPosition(HavePosition m_state)
    {
        switch (m_state)
        {
            case HavePosition.None:
                Move(1);
                break;
            case HavePosition.Before://仮
                Move(1);
                break;
            case HavePosition.Behind://仮
                Move(1);
                break;
            case HavePosition.Side://仮
                Move(1);
                break;
            case HavePosition.Push://仮
                Move(1);
                break;
            case HavePosition.Pull:
                Move(-1);
                break;
            case HavePosition.Up://仮
                Move(1);
                break;
            default:
                Debug.Log("Error:" + m_state);
                break;
        }
        
    }


    /// <summary>
    /// 通常の移動処理
    /// </summary>
    /// <param name="s">+か-で向き変化</param>
    private void Move(float s)
    {
        m_inputH = Input.GetAxis("Horizontal");
        m_inputV = Input.GetAxis("Vertical");

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * m_inputV + Camera.main.transform.right * m_inputH;
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        m_rb.velocity = moveForward * Speed() + new Vector3(0, m_rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward * s);
        }
    }

    public bool IsInput()
    {
        if (m_inputH == 0 && m_inputV == 0)
        {
            return true;
        }
        return false;
    }

    //重さによってスピードを変える
    private float Speed()
    {
        m_mass = AwakeData.Instance.mass;

        if (!SceneNavigator.Instance.IsFading)
        {

            if (0 <= m_mass && m_mass <= 4)//普通
            {
                m_speed = m_sp;
            }
            else if (5 <= m_mass && m_mass <= 9)//少し遅い
            {
                m_speed = m_sp / 1.2f;
            }
            else if (10 <= m_mass && m_mass <= 14)//遅い
            {
                m_speed = m_sp / 1.5f;
            }
            else if (15 <= m_mass && m_mass <= 29)//結構遅い
            {
                m_speed = m_sp / 2.0f;
            }
            else if (30 <= m_mass)//論外
            {
                m_speed = m_sp / 9.0f;
            }
        }
        else
        {
            m_speed = 0;
        }

        return m_speed;
    }

}
