using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Text;

public class ChangeIcon : MonoBehaviour {

    public enum TextureID {
        Texture01,
        Texture02,
        Texture03,
        Texture04,
        Texture05,
        Texture06,
        Texture07,
        Texture08,
        Texture09,
        Texture10,
        Texture11,
        Texture12,
        Texture13,
        Texture14,
        Texture15,
        Texture16,
        Texture17,
        Texture18,
        Texture19,
        Texture20,
        Texture21,
        Texture22,
    };

    private bool[] iconFlags = new bool[21];
    public bool[] GetItemFlagTotal
    {
        get { return iconFlags; }
    }


    public bool GeticonFlag(TextureID texture)
    {
        return iconFlags[(int)texture];
    }

    public bool SeticonFlag(int num, bool flag)
    {
        return iconFlags[num] = flag;
    }
}
