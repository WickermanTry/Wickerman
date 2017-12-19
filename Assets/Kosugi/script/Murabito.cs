//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using System.IO;
//using Novel;
//using UnityEngine.UI;

//public class Murabito : MonoBehaviour
//{
//    [SerializeField, Header("村人の番号")]
//    protected static int mMurabitoNum;

//    //イベントの有無チェック
//    private bool eventCheck = false;
//    //イベント番号
//    private int eventNumber = 0;
//    //前日に家族が消えたかどうか
//    private bool familyCheck = false;
//    //ファミリー番号
//    private int familyNumber = 0;
//    //前日に友達が消えたかどうか
//    private bool friendCheck = false;
//    //フレンド番号
//    private int friendNumber = 0;
//    //テキストID
//    protected string TextID, JumpID;
//    //プレイヤーポジション
//    private float Px, Py, Pz;
//    //このキャラ個人の変化のある数値
//    [Header("数値")]   
    
//    [SerializeField]
//    protected int fearCheck01;
//    [SerializeField]
//    protected int fearCheck02, fearCheck03, doutCheck,
//        myFearStats, myDoutStats, myFdout,
//        myFamily_1, myFamily_2, myFamily_3, myFamily_4, myFamily_5,
//        myFriend_1, myFriend_2, myFriend_3, myFriend_4, myFriend_5;

//    private void Awake()
//    {
//        mMurabitoNum = int.Parse(gameObject.name.Substring(8));

//        // Assets/Resources配下のKosugiフォルダから読込
//        TextAsset csv = Resources.Load("Kosugi/MurabitoStatus") as TextAsset;
//        StringReader reader = new StringReader(csv.text);

//        while (reader.Peek() > -1)
//        {
//            string line = reader.ReadToEnd().Replace('\n', '/');
//            string[] murabito = line.Split('/');

//            string[,] status = new string[murabito.Length, murabito[0].Split(',').Length];

//            for (int i = 0; i < 31; i++)
//            {
//                string[] data = murabito[i].Split(',');
//                for (int j = 0; j < 5; j++)
//                {
//                    status[i, j] = data[j];
//                }
//            }

//            fearCheck01 = int.Parse(status[mMurabitoNum, 1]);
//            fearCheck02 = int.Parse(status[mMurabitoNum, 2]);
//            fearCheck03 = int.Parse(status[mMurabitoNum, 3]);
//            doutCheck = int.Parse(status[mMurabitoNum, 4]);
//        }
//    }

//    void Start()
//    {
//        PositionSet();

//        // 村人の番号ごとの数値を挿入
//        myFearStats = AwakeData.Instance.fearList[mMurabitoNum];
//        myDoutStats = AwakeData.Instance.doutList[mMurabitoNum];
//        myFdout = AwakeData.Instance.FdoutList[mMurabitoNum];

//        //開いてるデータはMLife_0で埋めるぜったいtrueにならないデータ。
//        myFamily_1 = AwakeData.Instance.MlifeList[0];
//        myFamily_2 = AwakeData.Instance.MlifeList[0];
//        myFamily_3 = AwakeData.Instance.MlifeList[0];
//        myFamily_4 = AwakeData.Instance.MlifeList[0];
//        myFamily_5 = AwakeData.Instance.MlifeList[0];
//        myFriend_1 = AwakeData.Instance.MlifeList[0];
//        myFriend_2 = AwakeData.Instance.MlifeList[0];
//        myFriend_3 = AwakeData.Instance.MlifeList[0];
//        myFriend_4 = AwakeData.Instance.MlifeList[0];
//        myFriend_5 = AwakeData.Instance.MlifeList[0];
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        myFearStats = AwakeData.Instance.fearList[mMurabitoNum];
//        myDoutStats = AwakeData.Instance.doutList[mMurabitoNum];
//        myFdout = AwakeData.Instance.FdoutList[mMurabitoNum];
//    }
//    public void SetTextIDMethod()
//    {
//        JumpID = "";

//        //データを取り出してリストに追加するスクリプト
//        //☆＊ここに数字＊を消して当てはまる数字を入れる
//        var mList = new MurabitoDataScript.MList("Texts/Murabito" + mMurabitoNum.ToString() + "Data", true);

//        //(現在は仮です)全てに仮で村人20入れておきます
//        if (familyCheck == true)
//        {
//            TextID = mList.ToList().Find(x => x.Id == "family_1").Num1;
//            for (int i = 0; i == familyNumber; i++)
//            {
//                JumpID = "*family_" + i;
//            }
//            return;
//        }
//        else if (friendCheck == true)
//        {
//            TextID = mList.ToList().Find(x => x.Id == "family_1").Num1;
//            for (int i = 0; i == familyNumber; i++)
//            {
//                JumpID = "*family_" + i;
//            }
//            return;
//        }
//        else if (doutCheck <= myDoutStats && myFdout == 1)
//        {
//            TextID = mList.ToList().Find(x => x.Id == "dout").Num1;
//            AwakeData.Instance.FdoutList[mMurabitoNum] = 0;
//            myFdout = 0;
//            return;
//        }
//        else if (fearCheck03 <= myFearStats)
//        {
//            if (eventCheck == true)
//            {
//                for (int i = 1; i == eventNumber; i++)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
//                    JumpID = "*event_3_" + i;
//                }
//                return;
//            }
//            else
//            {
//                if (AwakeData.Instance.dayTime_ == 0)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num1;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_3" + i;
//                    }
//                }
//                else if (AwakeData.Instance.dayTime_ == 1)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num2;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_3" + i;
//                    }
//                }
//                else
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_3").Num3;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_3" + i;
//                    }
//                }
//                return;
//            }
//        }
//        else if (fearCheck02 <= myFearStats)
//        {
//            if (eventCheck == true)
//            {
//                for (int i = 1; i == eventNumber; i++)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
//                    JumpID = "*event_2" + i;
//                }
//                return;
//            }
//            else
//            {
//                TextID = mList.ToList().Find(x => x.Id == "fear_2").Num1;
//                int check = Random.Range(1, 3);
//                for (int i = 1; i == check; i++)
//                {
//                    JumpID = "*fear_2_" + i;
//                }
//                return;
//            }
//        }
//        else if (fearCheck01 <= myFearStats)
//        {

//            if (eventCheck == true)
//            {
//                for (int i = 1; i == eventNumber; i++)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "event_" + i).Num3;
//                    JumpID = "*event_1" + i;
//                }
//                return;
//            }
//            else
//            {
//                //現在ここに来ている
//                if (AwakeData.Instance.dayTime_ == 0)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num1;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_1_" + i;
//                    }
//                    return;
//                }
//                else if (AwakeData.Instance.dayTime_ == 1)
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num2;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_1_" + i;
//                    }
//                    return;
//                }
//                else
//                {
//                    TextID = mList.ToList().Find(x => x.Id == "fear_1").Num3;
//                    int check = Random.Range(1, 3);
//                    for (int i = 1; i == check; i++)
//                    {
//                        JumpID = "*fear_1_" + i;
//                    }
//                    return;
//                }
//            }
//        }
//    }
//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            if (Input.GetKeyDown(KeyCode.O))
//            {

//                SetTextIDMethod();
//                NovelSingleton.StatusManager.callJoker(TextID, JumpID);
//            }
//        }
//    }
//    public void PositionSet()
//    {
//        //☆＊ここに数字＊を消して当てはまる数字を入れる
//        var mMoveList = new MurabitoMoveDataScript.MMoveList("Texts/M" + mMurabitoNum.ToString() + "MoveData", true);

//        if (doutCheck <= myDoutStats)
//        {
//            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
//            {
//                SetEventMethod();
//                SetFamilyCheckMethod();
//                Px = mMoveList.ToList().Find(x => x.Id == "不審度_1_" + i).posX;
//                Py = mMoveList.ToList().Find(x => x.Id == "不審度_1_" + i).posY;
//                Pz = mMoveList.ToList().Find(x => x.Id == "不審度_1_" + i).posZ;

//                this.transform.position = new Vector3(Px, Py, Pz);
//            }
//            return;
//        }
//        else if (fearCheck03 <= myFearStats)
//        {
//            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
//            {
//                SetEventMethod();

//                Px = mMoveList.ToList().Find(x => x.Id == "恐怖度_3_" + i).posX;
//                Py = mMoveList.ToList().Find(x => x.Id == "恐怖度_3_" + i).posY;
//                Pz = mMoveList.ToList().Find(x => x.Id == "恐怖度_3_" + i).posZ;

//                this.transform.position = new Vector3(Px, Py, Pz);
//            }
//            return;
//        }
//        else if (fearCheck02 <= myFearStats)
//        {
//            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
//            {
//                SetEventMethod();

//                Px = mMoveList.ToList().Find(x => x.Id == "恐怖度_2_" + i).posX;
//                Py = mMoveList.ToList().Find(x => x.Id == "恐怖度_2_" + i).posY;
//                Pz = mMoveList.ToList().Find(x => x.Id == "恐怖度_2_" + i).posZ;

//                this.transform.position = new Vector3(Px, Py, Pz);
//            }
//            return;
//        }
//        else if (fearCheck01 <= myFearStats)
//        {
//            //時間によって場所移動
//            for (int i = 0; i == AwakeData.Instance.dayTime_; i++)
//            {
//                SetEventMethod();

//                Px = mMoveList.ToList().Find(x => x.Id == "恐怖度_1_" + i).posX;
//                Py = mMoveList.ToList().Find(x => x.Id == "恐怖度_1_" + i).posY;
//                Pz = mMoveList.ToList().Find(x => x.Id == "恐怖度_1_" + i).posZ;

//                this.transform.position = new Vector3(Px, Py, Pz);
//            }
//            return;
//        }
//    }
//    public void SetEventMethod()
//    {
//        //
//        if (AwakeData.Instance.MlifeList[mMurabitoNum] == 2)
//        {
//            eventCheck = true;
//            eventNumber = 1;
//        }
//    }
//    public void SetFamilyCheckMethod()
//    {
//        if (myFamily_1 == 2)
//        {
//            familyCheck = true;
//            familyNumber = 1;
//        }
//        else if (myFamily_2 == 2)
//        {
//            familyCheck = true;
//            familyNumber = 2;
//        }
//        else if (myFamily_3 == 2)
//        {
//            familyCheck = true;
//            familyNumber = 3;
//        }
//        else if (myFamily_4 == 2)
//        {
//            familyCheck = true;
//            familyNumber = 4;
//        }
//        else if (myFamily_5 == 2)
//        {
//            familyCheck = true;
//            familyNumber = 5;
//        }
//    }
//    public void SetFriendCheckMethod()
//    {
//        if (myFriend_1 == 2)
//        {
//            friendCheck = true;
//            friendNumber = 1;
//        }
//        else if (myFriend_2 == 2)
//        {
//            friendCheck = true;
//            friendNumber = 2;
//        }
//        else if (myFriend_3 == 2)
//        {
//            friendCheck = true;
//            friendNumber = 3;
//        }
//        else if (myFriend_4 == 2)
//        {
//            friendCheck = true;
//            friendNumber = 4;
//        }
//        else if (myFriend_5 == 2)
//        {
//            friendCheck = true;
//            friendNumber = 5;
//        }
//    }

//    public int GetMurabitoNum()
//    {
//        return mMurabitoNum;
//    }
//}
