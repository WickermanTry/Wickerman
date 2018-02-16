using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestGoal : MonoBehaviour {

    public class MRequestGoal
    {
        [CsvColumnAttribute(0, "")]
        public string Id { get; set; }
        [CsvColumnAttribute(1, "")]
        public string Num1 { get; set; }
        [CsvColumnAttribute(2, "")]
        public string Num2 { get; set; }
        public override string ToString()
        {
            return string.Format("[MRequestGoal:Id={0}, Num1={1}, Num2={2}", Id, Num1,Num2);
        }
    }
    public class NList : ClassList<MRequestGoal>
    {
        public NList(string _path, bool _skipFirstLine) : base(_path, _skipFirstLine) { }
    }
}
