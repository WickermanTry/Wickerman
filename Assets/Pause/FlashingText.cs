using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {
    [SerializeField, Tooltip("適用するText")]
    public Text _promptText;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _promptText.color = new Color(0, 150, 255,1);
    }
}
