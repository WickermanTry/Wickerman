using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerScript : MonoBehaviour {

    public List<int> lifeList = new List<int>();    //int型のリスト
    void Start () {
        for (int i = 0; i < 31; i++)//31体分のライフ(0はnull用)
        {
            lifeList.Add(1);
        }
        
    }
}
