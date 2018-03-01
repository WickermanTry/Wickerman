using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuaseUI : MonoBehaviour {

    [SerializeField]
    GameObject[] _Text;
    private int _SelectNum = 0;
    private bool _CursolMoveUp = false;
    private bool _CursolMoveDown = false;

    public GameObject Item;
    public GameObject Map;
    public GameObject Pause;
    public GameObject Pausemenu;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("VVertical") >= 1.0f)
        {
            if (!_CursolMoveUp)
            {
                _SelectNum--;
                _CursolMoveUp = true;
            }
        }
        else if (Input.GetAxis("VVertical") <= 0.0f) _CursolMoveUp = false;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("VVertical") <= -1.0f)
        {
            if (!_CursolMoveDown)
            {
                _SelectNum++;
                _CursolMoveDown = true;
            }
        }
        else if (Input.GetAxis("VVertical") >= 0.0f) _CursolMoveDown = false;

        if (_SelectNum >= _Text.Length)
        {
            _SelectNum = 0;
        }
        if (_SelectNum < 0)
        {
            _SelectNum = _Text.Length - 1;
        }

        for (var i = 0; i < _Text.Length; i++)
        {
            if (i == _SelectNum)
            {
                _Text[i].GetComponent<FlashingText>().enabled = true;
            }
            else
            {
                _Text[i].GetComponent<FlashingText>().enabled = false;
                _Text[i].GetComponent<Text>().color = new Color(255,255, 255, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            switch (_SelectNum)
            {
                case 0:
                    Item.SetActive(true);
                    Pause.SetActive(false);
                    break;
                case 1:
                    Map.SetActive(true);
                    Pause.SetActive(false);
                    AwakeData.Instance._mapcamera = true;
                    break;
                case 2:
                    Time.timeScale = 1;
                    Pausemenu.SetActive(false);
                    break;

                case 3:
                    SceneManager.LoadScene("Title");
                    break;
                default:
                    break;

            }
        }
    }
}
