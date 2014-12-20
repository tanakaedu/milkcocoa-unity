using UnityEngine;
using System.Collections;

public class CPlayer : MonoBehaviour {
	// 名前の3D Textを記録
	TextMesh textName = null;
	// メッセージの3D Textを記録
	TextMesh textMes = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// プレイヤー名を返す
	public string getPlayerName() {
		return textName.text;
	}

	// プレイヤー名を設定する
	public void setPlayerName(string nm) {
		// インスタンスを取得していない時は、取得する
		if (textName == null) {
			TextMesh[] txts = gameObject.GetComponentsInChildren<TextMesh> ();
			foreach (TextMesh txt in txts) {
				if (txt.name.CompareTo("Name")==0) {
					textName = txt;
				}
				else if (txt.name.CompareTo("Mes") == 0) {
					textMes = txt;
				}
			}
		}
		// データを設定する
		textName.text = nm;
	}

	// メッセージを返す
	public string getMessage() {
		return textMes.text;
	}

	// メッセージを設定する
	public void setMessage(string mes) {
		textMes.text = mes;
	}
}
