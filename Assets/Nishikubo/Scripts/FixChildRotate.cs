using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixChildRotate : MonoBehaviour {

    private Vector3 m_def;

	// Use this for initialization
	void Start () {
        m_def = transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 parent = transform.parent.transform.localRotation.eulerAngles;

        float x = parent.z + m_def.x;
        float y = parent.x + m_def.y;
        float z = parent.y + m_def.z;

        transform.localRotation = Quaternion.Euler(new Vector3(x, y, z));
    }
}
