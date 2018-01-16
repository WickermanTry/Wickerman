using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//盗む・隠す等のアイコン表示用
public class UIDisplay : MonoBehaviour {

    private List<GameObject> m_image = new List<GameObject>();

    // Use this for initialization
    void Start () {

        foreach (Transform child in transform)
        {
            m_image.Add(child.gameObject);
        }
    }

    /// <summary>
    /// UIの表示用
    /// </summary>
    /// <param name="num">0:隠す, 1:盗む</param>
    /// <param name="visible">表示するか</param>
    public void ImageActive(int num, bool visible)
    {
        m_image[num].SetActive(visible);
    }

}
