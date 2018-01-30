using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    const float TEXT_SPEED = 0.5F;
    const float TEXT_SPEED_STRING = 0.05F;
    const float COMPLETE_LINE_DELAY = 0.3F;

    [SerializeField]
    Text lineText;      // 文字表示Text
    [SerializeField]
    string[] scenarios; // 会話内容

    float textSpeed = 0;                    // 表示速度
    float completeLineDelay = COMPLETE_LINE_DELAY;  // 表示し終えた後の待ち時間
    int currentLine = 0;                    // 表示している行数
    string currentText = string.Empty;      // 表示している文字
    bool isCompleteLine = false;            // １文が全部表示されたか？

    private Merchant merchant;
    private ItemDataBase m_ItemDataBase;
    private RequestDataBase m_RequestDataBase;

    private Player player;
    [SerializeField]
    private PlayerState beforeState;//遷移前のプレイヤーの状態

    void Start()
    {
        merchant = GameObject.Find("Merchant").GetComponent<Merchant>();
        m_ItemDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDataBase>();
        m_RequestDataBase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RequestDataBase>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //プレイヤーの動きを止める
        beforeState = player.state;
        player.state = PlayerState.None;
        Show();
    }


    /// <summary>
    /// 会話シーン表示
    /// </summary>
    void Show()
    {
        TalkText();
        Initialize();
        StartCoroutine(ScenarioCoroutine());
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize()
    {
        isCompleteLine = false;
        lineText.text = "";
        currentText = scenarios[currentLine];

        textSpeed = TEXT_SPEED + (currentText.Length * TEXT_SPEED_STRING);

        LineUpdate();
    }

    /// <summary>
    /// 会話シーン更新
    /// </summary>
    /// <returns>The coroutine.</returns>
    IEnumerator ScenarioCoroutine()
    {
        while (true)
        {
            yield return null;

            // 次の内容へ
            if (isCompleteLine && Input.GetMouseButton(0))
            {
                yield return new WaitForSeconds(completeLineDelay);

                if (currentLine > scenarios.Length - 1)
                {
                    ScenarioEnd();
                    yield break;
                }

                Initialize();
            }

            // 表示中にボタンが押されたら全部表示
            else if (!isCompleteLine && Input.GetMouseButton(0))
            {
                iTween.Stop();
                TextUpdate(currentText.Length); // 全部表示
                TextEnd();
                yield return new WaitForSeconds(completeLineDelay);
            }
        }
    }

    /// <summary>
    /// 文字を少しずつ表示
    /// </summary>
    void LineUpdate()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 0,
            "to", currentText.Length,
            "time", textSpeed,
            "onupdate", "TextUpdate",
            "oncompletetarget", this.gameObject,
            "oncomplete", "TextEnd"
        ));
    }

    /// <summary>
    /// 表示文字更新
    /// </summary>
    /// <param name="lineCount">Line count.</param>
    void TextUpdate(int lineCount)
    {
        lineText.text = currentText.Substring(0, lineCount);
    }

    /// <summary>
    /// 表示完了
    /// </summary>
    void TextEnd()
    {
        Debug.Log("表示完了");
        isCompleteLine = true;
        currentLine++;

    }

    /// <summary>
    /// 会話終了
    /// </summary>
    void ScenarioEnd()
    {
        Debug.Log("会話終了");
        player.state = beforeState;
        SceneNavigator.Instance.Unload();
    }

    /// <summary>
    /// 会話文
    /// </summary>
    void TalkText()
    {
        if(!merchant.isAchieved)//達成前
        {
            if (merchant.day <= 5)
            {
                scenarios[0] = "私は商人である。お前に依頼する品はこれだ。";
                scenarios[1] = RequestTalk(m_ItemDataBase.GetItemData());
                scenarios[2] = "夜までにもってくるのだ。";
            }
            else if (merchant.day <= 10)
            {
                scenarios[0] = "よく続いているな。今日お前に依頼する品はこれだ。";
                scenarios[1] = RequestTalk(m_ItemDataBase.GetItemData());
                scenarios[2] = "少し難しいかもしれないが、夜までにもってくるのだ。";
            }
            else if (merchant.day <= 15)
            {
                scenarios[0] = "いつもありがとう。今回お前に依頼する品はこれだ。";
                scenarios[1] = RequestTalk(m_ItemDataBase.GetItemData());
                scenarios[2] = "いつも通り、夜までにもってくるのだ。期待している。";
            }

        }
        else if(merchant.isAchieved)//達成後
        {
            scenarios[0] = "よく持ってきてくれた。これで依頼は達成だ。";

        }


    }

    //依頼品の取得
    private string RequestTalk(ItemData[] itemData)
    {
        string requestItem = "";
        string item1 = "";
        string item2 = "";
        foreach (var item in itemData)
        {
            //依頼品は１つ
            if (merchant.requestItem1 == merchant.requestItem2)
            {
                if (merchant.requestItem1 == item.GetItemType())
                {
                    //アイテム1の名前
                    item1 = item.GetItemName();
                }
                requestItem = item1;
            }
            //依頼品が二つ
            else if (merchant.requestItem1 != merchant.requestItem2)
            {
                if (merchant.requestItem1 == item.GetItemType())
                {
                    //アイテム1の名前
                    item1 = item.GetItemName();
                }
                if (merchant.requestItem2 == item.GetItemType())
                {
                    //アイテム2の名前
                    item2 = item.GetItemName();
                }
                requestItem = item1 + " , " + item2;
            }

        }
        return requestItem;
    }
}
