using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//盗めるモノの状態
public class ObjectStatus : MonoBehaviour {

    [SerializeField, Tooltip("持つときの位置指定")]
    private HavePosition m_state = HavePosition.None;
    public HavePosition state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField, Tooltip("重さ")]
    private int m_mass = 1;
    public int mass { get { return m_mass; } }

    [SerializeField,Tooltip("true:複数持てる,false:複数持てない")]
    private bool m_isMultiple = false;
    public bool isMultiple  {   get { return m_isMultiple; } }

}
