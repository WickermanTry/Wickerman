using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Novel;
using UnityEngine.UI;

/// <summary>
/// 巡回してない時
/// </summary>
public class MurabitoHouse : MonoBehaviour
{
    [SerializeField, Header("むらびとの番号")]
    private int _murabitoNumber;
    [SerializeField, Header("※デバッグ用 むらびとの名前")]
    private string murabitoName;

    [SerializeField, Header("住家の番号")]
    private int _houseNumber;
    [SerializeField, Header("※デバッグ用 住家の名前")]
    private string houseName;

    [SerializeField, Header("アクティブにする子オブジェクト")]
    private List<GameObject> mObject = new List<GameObject>();

    // テキストデータ用変数
    private string[,] data, murabitoData;


    private void Awake()
    {
        _murabitoNumber = int.Parse(gameObject.name.Substring(8));

        for (int i = 0; i < transform.childCount; i++)
        {
            mObject.Add(transform.GetChild(i).gameObject);
        }

        // テキストデータを読み込む
        DataExport(_murabitoNumber);

        SetData();  
    }

    void Start()
    {
        SetPosition(false);
    }

    void Update()
    {

    }

    public void SetPosition(bool flag)
    {
        if (flag == false)
        {
            transform.position = GameObject.Find("house" + _houseNumber).transform.position;
        }
        for (int i = 0; i < mObject.Count; i++)
        {
            mObject[i].SetActive(flag);
        }
    }

    /// <summary>
    /// テキストデータを読み込む
    /// </summary>
    void DataExport(int num)
    {
        // Assets/Resources配下のKosugiフォルダから読込
        TextAsset csv = Resources.Load("Kosugi/MurabitoHouse") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            // テキストデータを , と / 区切りに変換
            string text = reader.ReadToEnd().Replace('\n', '/');
            // textに格納したデータを / で分割し配列に再格納
            string[] line = text.Split('/');
            // 適当な行から1行の長さを取得し配列の長さを指定
            data = new string[line.Length, line[0].Split(',').Length];
            // データ格納で使用する配列の長さを指定
            murabitoData = new string[line.Length, line[0].Split(',').Length];

            // 家番号で指定された行のデータを格納
            string[] value = line[num].Split(',');
            for (int j = 0; j < value.Length; j++)
            {
                data[num, j] = value[j];
            }
        }
    }

    void SetData()
    {
        /*
         * 0列目:むらびとの番号
         * 1列目:むらびとの名前
         * 2列目:住家の番号
         * 3列目:住家の名前
         * 
         * 0行目:null
         * 1行目:主人公の母のデータ
         * 2行目～：各むらびとのデータ
         */

        // データの列番号を指定
        int murabitoNameColumn = 1;
        int houseNumColumn = 2;
        int houseNameColumn = 3;

        murabitoName = data[_murabitoNumber, murabitoNameColumn];
        _houseNumber = int.Parse(data[_murabitoNumber, houseNumColumn]);
        houseName = data[_murabitoNumber, houseNameColumn];
    }
}
