using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSing : MonoBehaviour {

    private Player player;
    public MurabitoNight murabito;//隠された村人
    public GameObject ui;
    public bool isHide = false;//隠されてるか

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ui.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.state == PlayerState.Trailing)
        {
            ui.SetActive(true);
            //isHide = true;
        }

        //if (other.gameObject.tag == "Murabito" && player.state == PlayerState.Trailing && !isHide)
        //{
        //    ui.SetActive(true);

        //    if (murabito==null)
        //    {
        //        murabito = other.gameObject.GetComponent<MurabitoNight>();
        //    }
        //    else if(murabito!=null)
        //    {
        //        if (murabito.GetState() == MurabitoNight.MurabitoState.Hide)
        //        {
        //            Debug.Log("かくせたよ");
        //            isHide = true;

        //        }
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ui.SetActive(false);
            //isHide = false;
        }
        //if (other.gameObject.tag == "Murabito" && !isHide)
        //{
        //    ui.SetActive(false);
        //    murabito = null;
        //}
    }

    //public bool IsHide()
    //{
    //    return isHide;
    //}
}
