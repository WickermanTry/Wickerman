using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {
	//受け取ったデータをセーブするメソッド
	void SaveGame1(){
		bool result;
		result = PlayerPrefsX.GetBool ("isOK");//データの読み込み
		PlayerPrefsX.Setbool("isOK",result);

		Vector3 resultGrid;
		resultGrid = PlayerPrefsX.GetVector3 ("grid");
		PlayerPrefsX.SetVector3("grid",new Vector3(3.0f,2.0f,1.0f))

		int[] scoreArray=new int[5];
		scoreArray=PlayerPrefsX.GetintArray("score");
		PlayerPrefsX.Setbool("score",scoreArray);
	}
	void SaveGame2(int score){

		//受け取ったデータをセーブ
		PlayerPrefs.SetInt("score",score);

		Debug.Log("score="+score+":セーブしたわよッ!!(・∀・)");

	}
	void SaveGame3(int score){

		//受け取ったデータをセーブ
		PlayerPrefs.SetInt("score",score);

		Debug.Log("score="+score+":セーブしたわよッ!!(・∀・)");

	}
	//データをロードするメソッド
	int LoadScore(){
		 
		  //ロードしたデータを代入する変数
		  int loadedScore;
		 
		  //データをロード
		  loadedScore=PlayerPrefs.GetInt("score");
		 
		  Debug.Log("score="+loadedScore+":ロードしたわよッ!!ヽ(｀Д´#)ﾉ");
		 
		  //ロードした値を参照元に返す
		  return loadedScore;
		 
	}
}
