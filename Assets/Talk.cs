//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Talk : MonoBehaviour
//{

//    bool a = true;

//    // Use this for initialization
//    void Start()
//    {
//        GameObject.Find("Panel").GetComponent<CanvasRenderer>().SetAlpha(0);
//        GameObject.Find("TalkText").GetComponent<CanvasRenderer>().SetAlpha(0);
//        a = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            if (Input.GetKeyDown(KeyCode.O))
//            {
//                if (a == true)
//                {
//                    GameObject.Find("Panel").GetComponent<CanvasRenderer>().SetAlpha(1);
//                    GameObject.Find("TalkText").GetComponent<CanvasRenderer>().SetAlpha(1);
//                    a = false;
                   
//                }
//                else if (a == false)
//                {
//                    GameObject.Find("Panel").GetComponent<CanvasRenderer>().SetAlpha(0);
//                    GameObject.Find("TalkText").GetComponent<CanvasRenderer>().SetAlpha(0);
//                    a = true;
                   
//                }

//            }
//        }

//    }
//}
