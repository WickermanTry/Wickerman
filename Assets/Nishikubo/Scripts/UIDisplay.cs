using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//盗む・隠す等のアイコン表示用
public class UIDisplay : MonoBehaviour {

    //private Player m_Player;

    //[SerializeField,Tooltip("隠す")]    private GameObject m_HideImage;
    //[SerializeField,Tooltip("盗む")]    private GameObject m_StealImage;

    private List<GameObject> m_image = new List<GameObject>();

    void Awake()
    {

    }

    // Use this for initialization
    void Start () {

        //m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //if(m_HideImage==null || m_StealImage==null)
        //{
        //    m_HideImage = GameObject.Find("HideImage");
        //    m_StealImage = GameObject.Find("StealImage");


        //}

        foreach (Transform child in transform)
        {
            //m_image.Add(child.gameObject.GetComponent<Image>());
            m_image.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    void Update () {
	}

    //public void StealImage(bool visible)
    //{
    //    m_StealImage.SetActive(visible);
    //}

    /// <summary>
    /// UIの表示用
    /// </summary>
    /// <param name="num">0:隠す, 1:盗む</param>
    /// <param name="visible">表示するか</param>
    public void ImageActive(int num, bool visible)
    {
        //m_image[num].gameObject.SetActive(visible);
        m_image[num].SetActive(visible);
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")   //モノを所持
    //    {
    //        ImageActive(0, true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")   //モノを所持
    //    {
    //        ImageActive(0, false);
    //    }
    //}
}
