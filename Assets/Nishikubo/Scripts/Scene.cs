using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン全体
/// 追加する際はシーンの名前と一致させること
/// </summary>
public enum SceneType
{
    Title,
    GameClear,
    GameOver,
    Credit,
    steal/*,
    Stage01_Noon,
    Stage02_Night,
    Stage03_Noon,
    Stage04_Night
    */
}

/// <summary>
/// シーン遷移クラス
/// </summary>
public class Scene : MonoBehaviour {

    [SerializeField, Tooltip("飛ぶのシーン選択")]
    private SceneType m_nextScene = SceneType.Title;

    /// <summary>
    /// 次のシーンへ
    /// </summary>
    public void OnNext()
    {
        SceneNavigator.Instance.Change(m_nextScene.ToString(), 1.5f);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
    }


}
