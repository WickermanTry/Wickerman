using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Novel;
using UnityEngine.UI;

public class Murabito : MonoBehaviour
{
    [SerializeField, Header("村人の番号")]
    private int mMurabitoNum;

    //イベントの有無チェック
    private bool eventCheck = false;
    //イベント番号
    private int eventNumber = 0;
    //前日に家族が消えたかどうか
    private bool familyCheck = false;
    //ファミリー番号
    private int familyNumber = 0;
    //前日に友達が消えたかどうか
    private bool friendCheck = false;
    //フレンド番号
    private int friendNumber = 0;
    //テキストID
    protected string TextID, JumpID;
    //ポジション
    private float Px, Py, Pz;
    private int posTime_;
    //ジャンプナンバー(random)
    private int jumpNum_;
    //このキャラ個人の変化のある数値
    [Header("数値")]

    [SerializeField]
    protected int fearCheck01;
    [SerializeField]
    protected int fearCheck02, fearCheck03, doutCheck,
        myFearStats, myDoutStats, myFdout,
        myFamily_1, myFamily_2, myFamily_3, myFamily_4, myFamily_5,
        myFriend_1, myFriend_2, myFriend_3, myFriend_4, myFriend_5;

    private void Awake()
    {
        mMurabitoNum = int.Parse(gameObject.name.Substring(8));

        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/MurabitoStatus") as TextAsset;
        StringReader reader = new StringReader(csv.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadToEnd().Replace('\n', '/');
            string[] murabito = line.Split('/');

            string[,] status = new string[murabito.Length, murabito[0].Split(',').Length];

            for (int i = 0; i < 31; i++)
            {
                string[] data = murabito[i].Split(',');
                for (int j = 0; j < 5; j++)
                {
                    status[i, j] = data[j];
                }
            }

            fearCheck01 = int.Parse(status[1, 1]);
            fearCheck02 = int.Parse(status[1, 2]);
            fearCheck03 = int.Parse(status[1, 3]);
            doutCheck = int.Parse(status[1, 4]);
        }
    }

    void Start()
    {
        PositionSet();
        // 村人の番号ごとの数値を挿入
        myFearStats = AwakeData.Instance.fearList[1];
        myDoutStats = AwakeData.Instance.doutList[1];
        myFdout = AwakeData.Instance.FdoutList[1];
        posTime_ = 0;
        //開いてるデータはMLife_0で埋めるぜったいtrueにならないデータ。
        myFamily_1 = AwakeData.Instance.MlifeList[0];
        myFamily_2 = AwakeData.Instance.MlifeList[0];
        myFamily_3 = AwakeData.Instance.MlifeList[0];
        myFamily_4 = AwakeData.Instance.MlifeList[0];
        myFamily_5 = AwakeData.Instance.MlifeList[0];
        myFriend_1 = AwakeData.Instance.MlifeList[0];
        myFriend_2 = AwakeData.Instance.MlifeList[0];
        myFriend_3 = AwakeData.Instance.MlifeList[0];
        myFriend_4 = AwakeData.Instance.MlifeList[0];
        myFriend_5 = AwakeData.Instance.MlifeList[0];
    }
    // Update is called once per frame
    void Update()
    {
        myFearStats = AwakeData.Instance.fearList[1];
        myDoutStats = AwakeData.Instance.doutList[1];
        myFdout = AwakeData.Instance.FdoutList[1];
        if(posTime_ == AwakeData.Instance.dayTime_)
        {
            PositionSet();
        }
    }
    public void SetTextIDMethod()
    {
        JumpID = "";

        //データを取り出してリストに追加するスクリプト
        //☆＊ここに数字＊を消して当てはまる数字を入れる
        var mList = new MurabitoDataScript.MList("Texts/Murabito" + mMurabitoNum.ToString() + "Data", true);

        //(現在は仮です)全てに仮で村人20入れておきます
        if (familyCheck == true)
        {
            TextID = mList.ToList().Find(x => x.Id == "family_1").Num1;
            for (int i = 0; i == familyNumber; i++)
            {
                JumpID = "*family_" + i;
            }
            return;
        }
        else if (friendCheck == true)
        {
            TextID = mList.ToList().Find(x => x.Id == "family_1").Num1;
            for (int i = 0; i == familyNumber; i++)
            {
                JumpID = "*family_" + i;
            }
            return;
        }
        else if (doutCheck <= myDoutStats && myFdout == 1)
        {
            TextID = mList.ToList().Find(x => x.Id == "dout").Num1;
            AwakeData.Instance.FdoutList[mMurabitoNum] = 0;
            myFdout = 0;
            return;
        }
        else if (fearCheck03 <= myFearStats)
        {
            if (eventCheck == true)
            {
                for (int i = 1; i == eventNumber; i++)
                {
                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
                    JumpID = "*event_3_" + i;
                }
                return;
            }
            else
            {
                if (AwakeData.Instance.dayTime_ == 0)
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num1;
                    jumpNum_ = Random.Range(1, 3);
                    for (int i = 1; i == jumpNum_; i++)
                    {
                        JumpID = "*fear_3" + i;
                    }
                }
                else if (AwakeData.Instance.dayTime_ == 1)
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num2;
                    jumpNum_ = Random.Range(1, 3);
                    for (int i = 1; i == jumpNum_; i++)
                    {
                        JumpID = "*fear_3" + i;
                    }
                }
                else
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num3;
                    jumpNum_ = Random.Range(1, 3);
                    for (int i = 1; i == jumpNum_; i++)
                    {
                        JumpID = "*fear_3" + i;
                    }
                }
                return;
            }
        }
        else if (fearCheck02 <= myFearStats)
        {
            if (eventCheck == true)
            {
                for (int i = 1; i == eventNumber; i++)
                {
                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
                    JumpID = "*event_2" + i;
                }
                return;
            }
            else
            {
                TextID = mList.ToList().Find(x => x.Id == "fear_2").Num1;
                jumpNum_ = Random.Range(1, 3);
                for (int i = 1; i == jumpNum_; i++)
                {
                    JumpID = "*fear_2_" + i;
                }
                return;
            }
        }
        else if (fearCheck01 <= myFearStats)
        {

            if (eventCheck == true)
            {
                for (int i = 1; i == eventNumber; i++)
                {
                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
                    JumpID = "*event_1" + i;
                }
                return;
            }
            else
            {
                //現在ここに来ている
                if (AwakeData.Instance.dayTime_ == 0)
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num1;
                    jumpNum_ = Random.Range(1, 3);
                    JumpID = "*fear_1_" + jumpNum_;
                    return;
                }
                else if (AwakeData.Instance.dayTime_ == 1)
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num2;
                    jumpNum_ = Random.Range(1, 3);
                    JumpID = "*fear_1_" + jumpNum_;
                    return;
                }
                else
                {
                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num3;
                    jumpNum_ = Random.Range(1, 3);
                    JumpID = "*fear_1_" + jumpNum_;
                    return;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetTextIDMethod();
                NovelSingleton.StatusManager.callJoker(TextID, JumpID);
            }
        }
    }
    public void PositionSet()
    {
        //☆＊ここに数字＊を消して当てはまる数字を入れる
        var mMoveList = new MurabitoMoveDataScript.MMoveList("Texts/M" + mMurabitoNum.ToString() + "MoveData", true);

        //時間によって場所移動
        if(AwakeData.Instance.indoorCheck_ == false)//室外
        {
            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
            {
                SetEventMethod();

                Px = mMoveList.ToList().Find(x => x.Id == "場所セット_0_" + i).posX;
                Py = mMoveList.ToList().Find(x => x.Id == "場所セット_0_" + i).posY;
                Pz = mMoveList.ToList().Find(x => x.Id == "場所セット_0_" + i).posZ;

                this.transform.position = new Vector3(Px, Py, Pz);
                posTime_ += 1;
            }
        }
        else
        {
            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
            {
                SetEventMethod();

                Px = mMoveList.ToList().Find(x => x.Id == "場所セット_"+ AwakeData.Instance.houseNum_ +"_"+ i).posX;
                Py = mMoveList.ToList().Find(x => x.Id == "場所セット_"+ AwakeData.Instance.houseNum_ +"_"+ i).posY;
                Pz = mMoveList.ToList().Find(x => x.Id == "場所セット_"+ AwakeData.Instance.houseNum_ +"_"+ i).posZ;

                this.transform.position = new Vector3(Px, Py, Pz);
                posTime_ += 1;
            }
        }
    }
    public void SetEventMethod()
    {
        //
        if (AwakeData.Instance.MlifeList[mMurabitoNum] == 2)
        {
            eventCheck = true;
            eventNumber = 1;
        }
    }
    public void SetFamilyCheckMethod()
    {
        if (myFamily_1 == 2)
        {
            familyCheck = true;
            familyNumber = 1;
        }
        else if (myFamily_2 == 2)
        {
            familyCheck = true;
            familyNumber = 2;
        }
        else if (myFamily_3 == 2)
        {
            familyCheck = true;
            familyNumber = 3;
        }
        else if (myFamily_4 == 2)
        {
            familyCheck = true;
            familyNumber = 4;
        }
        else if (myFamily_5 == 2)
        {
            familyCheck = true;
            familyNumber = 5;
        }
    }
    public void SetFriendCheckMethod()
    {
        if (myFriend_1 == 2)
        {
            friendCheck = true;
            friendNumber = 1;
        }
        else if (myFriend_2 == 2)
        {
            friendCheck = true;
            friendNumber = 2;
        }
        else if (myFriend_3 == 2)
        {
            friendCheck = true;
            friendNumber = 3;
        }
        else if (myFriend_4 == 2)
        {
            friendCheck = true;
            friendNumber = 4;
        }
        else if (myFriend_5 == 2)
        {
            friendCheck = true;
            friendNumber = 5;
        }
    }

    public int GetMurabitoNum()
    {
        return mMurabitoNum;
    }
}
