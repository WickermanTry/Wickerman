using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    void Awake()
    {
        //日付
        AwakeData.Instance.dayNum_ = 0;
        //不審度の会話をしたかどうか
        for (int i = 0; i < 31; i++)//31体分(0はnull用)
        {
            AwakeData.Instance.FdoutList.Add(1);
        }
        //村人ライフ
        for (int i = 0; i < 31; i++)//31体分(0はnull用)
        {
            AwakeData.Instance.MlifeList.Add(1);
        }
        //個人の恐怖値
        for (int i = 1; i < 31; i++)//30体分
        {
            AwakeData.Instance.fearList.Add(0);
        }
        //個人の不審度
        for (int i = 1; i < 31; i++)//30体分
        {
            AwakeData.Instance.doutList.Add(0);
        }

        AwakeData.Instance.playerPosition_ = new Vector3(3.66f, 0.1f, 0.19f);
        AwakeData.Instance.playerRotation_ = new Quaternion();
        AwakeData.Instance.cameraPosition_ = new Vector3(3.66f, 7.1f, -1.31f);
        AwakeData.Instance.cameraRotate_ = new Quaternion();
        AwakeData.Instance.indoorcameraPosition_ = new Vector3(0.0f, 3.0f, 1.0f);
        AwakeData.Instance.worldTime_ = 0.0f;
        AwakeData.Instance.dayTime_ = 0;
        AwakeData.Instance.posSet = false;
        AwakeData.Instance.checkNum = 1;
        AwakeData.Instance.sacrificeCount = 0;
        AwakeData.Instance.inout_ = true;
        AwakeData.Instance.worldMinite_ = 0;
        AwakeData.Instance.cameraFrag_ = false;

        SceneManager.LoadScene("Maptest1222");
    }
}
