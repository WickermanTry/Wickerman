﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static bool mListSave;
    public bool mListDebug;
    public bool flag;

    void Awake()
    {
        if (mListSave) return;

        //リスト初期化
        AwakeData.Instance.FdoutList.Clear();
        AwakeData.Instance.MlifeList.Clear();
        AwakeData.Instance.fearList.Clear();
        AwakeData.Instance.doutList.Clear();

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

        mListSave = true;

        //AwakeData.Instance.playerPosition_ = new Vector3(0.0f, 0.0f, 0.0f);
        //AwakeData.Instance.worldTime_ = 0.0f;
        //AwakeData.Instance.dayTime_ = 0;
        //AwakeData.Instance.posSet = false;
        //AwakeData.Instance.checkNum = 1;
        //SceneManager.LoadScene("Maptest1117");
    }
    private void Update()
    {
        mListDebug = mListSave;
        if (flag)
        {
            mListSave = false;
        }
    }
}
