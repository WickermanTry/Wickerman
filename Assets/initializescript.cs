using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class initialize : MonoBehaviour
//{

//    //const float TEXT_SPEED = 0.5F;
//    //const float TEXT_SPEED_STRING = 0.05F;
//    //const float COMPLETE_LINE_DELAY = 0.3F;

//    //[SerializeField]
//    //Text lineText;      // 文字表示Text
//    //[SerializeField]
//    //string[] scenarios; // 会話内容

//    //float textSpeed = 0;                    // 表示速度
//    //float completeLineDelay = COMPLETE_LINE_DELAY;  // 表示し終えた後の待ち時間
//    //int currentLine = 0;                    // 表示している行数
//    ///string currentText = string.Empty;      // 表示している文字
//    //bool isCompleteLine = false;            // １文が全部表示されたか？

//    private Merchant merchant;
//    private ItemDataBase m_ItemDataBase;
//    private RequestDataBase m_RequestDataBase;
//    private ItemData m_itemData;
//    private Player player;
//    private PlayerState beforeState;//遷移前のプレイヤーの状態


//    void Start()
//    {
//        merchant = GameObject.Find("Merchant").GetComponent<Merchant>();
//        m_ItemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
//        m_RequestDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RequestDataBase>();
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//        //プレイヤーの動きを止める
//        beforeState = player.state;
//        player.state = PlayerState.None;
//        AwakeData.Instance.talkFlag = true;
//    }
//    //依頼品の取得
//    private string RequestTalk(ItemData[] itemData)
//    {
//        string requestItem = "";
//        string item1 = "";
//        string item2 = "";
//        foreach (var item in itemData)
//        {
//            //依頼品は１つ
//            if (merchant.requestItem1 == merchant.requestItem2)
//            {
//                if (merchant.requestItem1 == item.GetItemType())
//                {
//                    //アイテム1の名前
//                    item1 = item.GetItemName();
//                }
//                requestItem = item1;
//            }
//            //依頼品が二つ
//            else if (merchant.requestItem1 != merchant.requestItem2)
//            {
//                if (merchant.requestItem1 == item.GetItemType())
//                {
//                    //アイテム1の名前
//                    item1 = item.GetItemName();
//                }
//                if (merchant.requestItem2 == item.GetItemType())
//                {
//                    //アイテム2の名前
//                    item2 = item.GetItemName();
//                }
//                requestItem = item1 + " , " + item2;
//            }

//        }
//        return requestItem;
//    }

//}
