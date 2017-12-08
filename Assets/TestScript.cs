using UnityEngine;
using System.Collections;
public class TestScript : MonoBehaviour
{
    //今回読み込むcsvのデータ
    /*
	#itemID,name,atk
	0,木刀,10
	1,鉄の剣,75
	2,銀の剣,75
	3,ミスリルソード,125
	*/
    //csvファイルの列に従ってクラスを定義
    public class TestWeapon
    {
        [CsvColumnAttribute(0, 0)]
        public int Id { get; set; }
        [CsvColumnAttribute(1, "")]
        public string Name { get; set; }
        [CsvColumnAttribute(2, 0)]
        public int Atk { get; set; }
        public override string ToString()
        {
            return string.Format("[TestWeapon: Id={0}, Name={1}, Atk={2}]", Id, Name, Atk);
        }
    }
    //ClassListを継承したWeaponListを定義
    //継承元クラスのClassListコンストラクタを呼び出す。
    public class WeaponList : ClassList<TestWeapon>
    {
        public WeaponList(string _path, bool _skipFirstLine) : base(_path, _skipFirstLine) { }
    }
    // Use this for initialization
    void Start()
    {
        //pathで指定したcsvファイルを読み込み、TestWeaponクラスにマッピング、List化までこの一行でやっちゃう。
        var weaponList = new WeaponList("Texts/weapon_list", true);
        foreach (var weapon in weaponList)
        {
           Debug.Log(weapon.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}