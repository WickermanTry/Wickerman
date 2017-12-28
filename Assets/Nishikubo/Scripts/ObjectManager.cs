using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectManager : MonoBehaviour {

    private List<GameObject> m_obj = new List<GameObject>();    //盗めるすべてのものを保存
    [SerializeField]
    private List<GameObject> m_stoleObj = new List<GameObject>();   //盗まれたもの
    public List<GameObject> stoleObj
    {
        get { return m_stoleObj; }
        set { m_stoleObj = value; }
    }

    // Use this for initialization
 //   void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
