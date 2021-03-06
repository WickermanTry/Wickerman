﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public int[] a = new int[16];
    void Awake()
    {
        //日付
        AwakeData.Instance.dayNum_ = 1;
        //不審度の会話をしたかどうか
        for (int i = 0; i < 31; i++)//31体分(0はnull用)
        {
            AwakeData.Instance.FdoutList.Add(1);
        }
        for (int i = 0; i < 16; i++)//ハウスナンバー
        {
            AwakeData.Instance.stealList.Add(false);
        }
        for(int i = 0; i < 16; i++)//ハウスナンバー
        {
            AwakeData.Instance.stealNumList.Add(a[i]);
        }
        for(int i = 0; i < 23; i++)//識別番号
        {
            AwakeData.Instance.stealTypeList.Add(false);
        }
        //村人ライフ
        for (int i = 0; i < 31; i++)//31体分(0はnull用)
        {
            AwakeData.Instance.MlifeList.Add(1);
        }
        //個人の恐怖値
        /* 31体分
         * 村人の数値を番号に基づきfearList[1]から取っており
         * i=1だとfearList[0]～fearList[29]になってしまい番号通りに取れなくなるのでi=0に変更しました
         * fearList[0]はnull(使用しない)でお願いします
         */
        for (int i = 0; i < 31; i++)
        {
            AwakeData.Instance.fearList.Add(0);
        }
        //個人の不審度
        /* 31体分
         * 村人の数値を番号に基づきdoutList[1]から取っており
         * i=1だとdoutList[0]～doutList[29]になってしまい番号通りに取れなくなるのでi=0に変更しました
         * doutList[0]はnull(使用しない)でお願いします
         */
        for (int i = 0; i < 31; i++)
        {
            AwakeData.Instance.doutList.Add(0);
        }
        AwakeData.Instance.playerPosition_ = new Vector3(18.0f, 0.2f, 63.0f);
        AwakeData.Instance.cameraPosition_ = new Vector3(213.4f, 13.0f, 120.7f);
        AwakeData.Instance.cameraRotate_ = new Quaternion(0.5f,0.0f,0.0f,0.8660254f);
        AwakeData.Instance.worldTime_ = 0.0f;
        AwakeData.Instance.dayTime_ = 0;
        AwakeData.Instance.posSet = false;
        AwakeData.Instance.checkNum = 1;
        AwakeData.Instance.inout_ = true;
        AwakeData.Instance.worldMinite_ = 0;
        AwakeData.Instance.maxMass = 30;
        AwakeData.Instance.mass = 0;
        AwakeData.Instance.item1_ = false;
        AwakeData.Instance.item2_ = false;
        AwakeData.Instance.talkFlag = false;
        AwakeData.Instance.talkTimeFlag = false;
        AwakeData.Instance.merchantNum = 0;
        AwakeData.Instance.firstkind = false;
        AwakeData.Instance.secondkind = false;
        AwakeData.Instance.thirdkind = false;
        AwakeData.Instance.beforeState = PlayerState.Idle;
        //if (GameObject.FindGameObjectWithTag("Player") == null)
        //{
        //    AwakeData.Instance.player = Instantiate((GameObject)Resources.Load("Prefabs/Player"));
        //    DontDestroyOnLoad(AwakeData.Instance.player);
        //}

        SceneManager.LoadScene("LoadSceneManager");



    }
}
