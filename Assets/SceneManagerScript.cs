using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        if (AwakeData.Instance.houseNum_ == 0)
        {
            //SceneManager.LoadScene("hiru" + AwakeData.Instance.dayNum_);
            SceneNavigator.Instance.Change("hiru" + AwakeData.Instance.dayNum_);

        }
        else
        {
            //SceneManager.LoadScene("House" + AwakeData.Instance.houseNum_);
            SceneNavigator.Instance.Change("House" + AwakeData.Instance.houseNum_);

        }
    }
}
