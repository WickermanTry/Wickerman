using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMurabito : MonoBehaviour {
    public int murabitoNum = 0;
    GameObject textObj_;
    // Use this for initialization
    void Start () {
        textObj_ = GameObject.Find("TextManager");
        
    }
	
	// Update is called once per frame
	void Update () {
        TextManagerScript T1 = textObj_.GetComponent<TextManagerScript>();
        T1.textManagerNum = murabitoNum;

    }
}
