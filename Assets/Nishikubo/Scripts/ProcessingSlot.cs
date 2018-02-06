using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingSlot : MonoBehaviour {

    //　アイテム情報を表示するテキストUI
    //private Text informationText;
    //　アイテムの名前を表示するテキストUIプレハブ
    [SerializeField]
    private GameObject titleUI;
    //　アイテム名を表示するテキストUIをインスタンス化した物を入れておく
    private GameObject uiObj;
    //　自身のアイテムデータを入れておく
    [SerializeField]
    public ItemData myItemData;

    //private MyItemStatus myItemStatus;
    private GameObject player;

    //生成したかどうか
    public bool isGenerate = false;

    

    //　スロットが非アクティブになったら削除
    void OnDisable()
    {
        if (uiObj != null)
        {
            Destroy(uiObj);
        }
        Destroy(gameObject);
    }

    //　アイテムデータをセット
    public void SetItemData(ItemData itemData)
    {
        myItemData = itemData;
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();
    }

    void Start()
    {
        //informationText = transform.parent.parent.Find("Information").GetChild(0).GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player");

        isGenerate = false;

    }

    public void MouseOver()
    {
        if (uiObj != null)
        {
            Destroy(uiObj);
        }
        if (!isGenerate)//1回だけ生成
        {
            //　アイテムの名前を表示するUIを位置を調整してインスタンス化
            uiObj = Instantiate(titleUI, new Vector2(transform.position.x - 25f, transform.position.y + 25f), Quaternion.identity) as GameObject;
            //　アイテム表示UIの親をPanelにする
            uiObj.transform.SetParent(transform.parent.parent);
            Text ui = uiObj.GetComponentInChildren<Text>();
            //　アイテム表示テキストに自身のアイテムの名前を表示
            ui.text = myItemData.GetItemName();
            //　情報表示テキストに自身のアイテムの情報を表示
            //informationText.text = myItemData.GetItemInformation();
        }


    }

    public void MouseExit()
    {
        //　マウスポインタがアイテムスロットを出たらアイテム表示UIを削除
        if (uiObj != null)
        {
            //informationText.text = "";
            Destroy(uiObj);
        }
    }

    public void MouseDown()
    {
        //盗んだもののアイコンがクリックされたら
        //その盗んだモノを生成
        //持っていたのもを消す
        //現在持ち運んでないとき
        if(!isGenerate)//1回だけ生成
        {
            var obj=Instantiate((GameObject)Resources.Load("Prefabs/StealObjects/" + myItemData.GetItemType().ToString()));
            obj.transform.position = GameObject.Find("TrailingPosition").transform.position;
            AwakeData.Instance.mass = AwakeData.Instance.mass - myItemData.GetItemMass();
            
            //プレイヤーの配下に盗んだモノがあったら
            if (player.transform.FindChild(myItemData.GetItemType().ToString()) != null)
            {
                Destroy(player.transform.FindChild(myItemData.GetItemType().ToString()).gameObject);                
            }
            else if(player.transform.FindChild(myItemData.GetItemType().ToString() + "(Clone)") != null)
            {
                Destroy(player.transform.FindChild(myItemData.GetItemType().ToString() + "(Clone)").gameObject);
            }

            player.GetComponent<MyItemStatus>().SetItemFlag(myItemData.GetItemNumber(), false);
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);//灰色に


            //隠せる場所だったら
            GameObject[] hideArea = GameObject.FindGameObjectsWithTag("HideArea");
            for (int i = 0; i < hideArea.Length; i++)
            {
                if (hideArea[i].GetComponent<HideArea>().isHide)
                {
                    SceneNavigator.Instance.Fade(1.0f);
                    hideArea[i].GetComponent<HideArea>().hideCount++;
                    obj.GetComponent<MoveObjects>().state = ObjectState.Hide;
                }
            }

            isGenerate = true;


        }


    }
}
