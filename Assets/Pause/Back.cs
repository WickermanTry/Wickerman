using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour {

    [SerializeField, Tooltip("適用するText")]
    public Text _button;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _button.color = new Color(0, 150, 255, 1);
    }
}
