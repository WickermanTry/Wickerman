using UnityEngine;
using System.Collections;
//using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
public class CDataScript : MonoBehaviour
{
    //csvファイルの列に従ってクラスを定義
    public class CData
    {
        [CsvColumnAttribute(0, 0)]
        public int CharaId { get; set; }
        [CsvColumnAttribute(1, 0)]
        public int Id { get; set; }
        [CsvColumnAttribute(2, 0)]
        public string Text { get; set; }
        public override string ToString()
        {
            return string.Format("[CData:CharaId={0}, Id={1}, Text={2}]", CharaId, Id, Text);
        }
    }
    //ClassListを継承したWeaponListを定義
    //継承元クラスのClassListコンストラクタを呼び出す。
    public class CList : ClassList<CData>
    {
        public CList(string _path, bool _skipFirstLine) : base(_path, _skipFirstLine) { }
    }
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
}