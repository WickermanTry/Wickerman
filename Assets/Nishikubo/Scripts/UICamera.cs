using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICamera : MonoBehaviour {


    //[SerializeField]
    //private Transform targetTfm;

    //private RectTransform m_RectPos;
    //private Vector3 offset = new Vector3(0, 1.5f, 0);

    private Player player;
    public GameObject ui;
    private bool isHide = false;

    // Use this for initialization
    void Start () {
        //m_RectPos = GetComponent<RectTransform>();
        //m_RectPos.LookAt(Camera.main.transform);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //m_RectPos.LookAt(Camera.main.transform);

        //m_RectPos.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player" && player.state == PlayerState.Trailing)
        {
            ui.SetActive(true);
            isHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ui.SetActive(false);
            isHide = false;
        }
    }

    public bool IsHide()
    {
        return isHide;
    }
}
