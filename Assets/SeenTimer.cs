using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeenTimer : MonoBehaviour {

    [SerializeField]float nextTimeOclock;

    public static bool timerStop = false;
    Text m_Text;
    float timeMinints = 0;
    [SerializeField]int minints = 0;
    [SerializeField]int timerOclock;

    
	// Use this for initialization
	void Start () {
        m_Text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

       

        if (Input.GetKeyDown(KeyCode.Z)) timerStop = !timerStop;


        if (!timerStop) timeMinints += Time.deltaTime+Time.deltaTime;
       
        if(timeMinints >=5)
        {
            timeMinints = 0;
            minints+=5;
        }       
        if (minints >= 60)
        {
            timerOclock++;
            minints -= 60;
        }
       
        if (nextTimeOclock<=timerOclock)
        {
            SceneManager.LoadScene("House1");
        }
        m_Text.text = timerOclock.ToString("D2") +":"+minints.ToString("00");
        print(timeMinints);
    }
}
