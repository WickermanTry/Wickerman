using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    private Merchant merchant;
    private ItemDataBase m_ItemDataBase;
    private RequestDataBase m_RequestDataBase;
    private ItemData m_itemData;
    private Player player;
    private PlayerState beforeState;//遷移前のプレイヤーの状態
    void Start()
    {
        if (AwakeData.Instance.talkTimeFlag)
            AwakeData.Instance.talkTimeFlag = true;

        if(AwakeData.Instance.talkFlag)
        {
            player.state = beforeState;
            if (merchant.isAchieved)//達成後
            {
                DayReset(m_ItemDataBase.GetItemData());
                AwakeData.Instance.dayNum_++;
            }
            SceneNavigator.Instance.Unload();
        }
        if (AwakeData.Instance.houseNum_ == 0)
        {
            SceneManager.LoadScene("hiru" + AwakeData.Instance.dayNum_);
        }
        else
        {
            SceneManager.LoadScene("House" + AwakeData.Instance.houseNum_);
        }
    }
    //依頼達成後にその日の依頼品を消す
    private void DayReset(ItemData[] itemData)
    {
        foreach (var item in itemData)
        {
            if (merchant.requestItem1 == item.GetItemType())
            {
                //達成した依頼品の重さを引く
                AwakeData.Instance.mass = AwakeData.Instance.mass - item.GetItemMass();
                //持ってるとき
                if (player.state == PlayerState.Trailing)
                {
                    player.AfterAchieving(item.GetItemType().ToString());
                }
                //依頼品がプレイヤーの配下にあったらDelete
                if (player.transform.FindChild(item.GetItemType().ToString()) != null)
                {
                    Destroy(player.transform.FindChild(item.GetItemType().ToString()).gameObject);
                }
                else if (player.transform.FindChild(item.GetItemType().ToString() + "(Clone)") != null)
                {
                    Destroy(player.transform.FindChild(item.GetItemType().ToString() + "(Clone)").gameObject);
                }
                //依頼品のMyItemFlagをFalseに
                player.GetComponent<MyItemStatus>().SetItemFlag(item.GetItemNumber(), false);

            }
        }
        if (merchant.requestItem1 != merchant.requestItem2)
        {
            foreach (var item in itemData)
            {
                if (merchant.requestItem2 == item.GetItemType())
                {
                    //達成した依頼品の重さを引く
                    AwakeData.Instance.mass = AwakeData.Instance.mass - item.GetItemMass();
                    //持ってるとき
                    if (player.state == PlayerState.Trailing)
                    {
                        player.AfterAchieving(item.GetItemType().ToString());
                    }
                    //依頼品がプレイヤーの配下にあったらDelete
                    if (player.transform.FindChild(item.GetItemType().ToString()) != null)
                    {
                        Destroy(player.transform.FindChild(item.GetItemType().ToString()).gameObject);
                    }
                    else if (player.transform.FindChild(item.GetItemType().ToString() + "(Clone)") != null)
                    {
                        Destroy(player.transform.FindChild(item.GetItemType().ToString() + "(Clone)").gameObject);
                    }
                    //依頼品のMyItemFlagをFalseに
                    player.GetComponent<MyItemStatus>().SetItemFlag(item.GetItemNumber(), false);

                }
            }
        }
    }
}
