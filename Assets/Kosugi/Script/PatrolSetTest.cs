using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolSetTest : MonoBehaviour
{
    public List<int> murabitoNum = new List<int>();
    public bool _patrolTest;

    private void Awake()
    {
        // シーン跨いでも破棄しないようにする
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        //SceneManager.sceneUnloaded += SceneUnloaded;
        //SceneManager.activeSceneChanged += ActiveSceneChanged;

        // Unloaded -> Changed -> Loadeds
    }
    void SceneLoaded(UnityEngine.SceneManagement.Scene loadScene, LoadSceneMode arg1)
    {
        print("patrol");      
    }

    void Update ()
	{
        if (!_patrolTest&& GameObject.Find("PatrolManager"))
        {
            for (int i = 0; i < murabitoNum.Count; i++)
            {
                GameObject.Find("PatrolManager").GetComponent<PatrolManager>().SetMurabito(murabitoNum[i]);
            }
            
            _patrolTest = true;
            Destroy(gameObject);
        }
	}
}