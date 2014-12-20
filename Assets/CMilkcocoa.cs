using UnityEngine;
using System.Collections;

public class CMilkcocoa : MonoBehaviour {
	public static float SendMilliSec = 100;
	public GameObject prefAnother=null;

	// Use this for initialization
	void Start () {
		StartCoroutine ("SendMilkcocoa");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// プレイヤーのプレハブ
	public GameObject prefPlayer = null;

	// 自分のインスタンス
	private GameObject myPlayer = null;

	// ログイン処理
	public void login(string nm) {
			// 同じ名前のプレイヤーを探す
			if (getPlayerByName (nm) != null) {
					// 同じ名前のプレイヤーがいるので、JavaScriptのエラー関数を呼び出す
					Application.ExternalCall ("login_error", "");
					return;
			}

			// 新しいオブジェクトが追加されたので、次回のプレイヤー検索ではリストを更新する
			goPlayers = null;
			// 登録
			myPlayer = (GameObject)Instantiate (prefPlayer);
			myPlayer.SendMessage ("setPlayerName", nm);
			myPlayer.SendMessage ("setMessage", "");
			Application.ExternalCall ("login_ok");
	}

	// Playerタグを持ったゲームオブジェクト
	GameObject [] goPlayers = null;

	// 指定の名前のゲームオブジェクトを返す。見つからない時はnullを返す
	GameObject getPlayerByName(string nm) {
		if (goPlayers == null) {
			goPlayers = GameObject.FindGameObjectsWithTag ("Player");
		}
		foreach (GameObject go in goPlayers) {
			if (go.GetComponent<CPlayer>().getPlayerName().CompareTo(nm) == 0) {
				return go;
			}
		}
		return null;
	}

	// JavaScriptから呼び出す関数
	public void setMessage(string mes) {
		if (myPlayer != null) {
			myPlayer.SendMessage("setMessage",mes);
		}
	}

	// 一定時間ごとにMilkcocoaに自分のデータを送信するコルーチン
	IEnumerator SendMilkcocoa() {
		while (true) {
			if (myPlayer != null) {
				CPlayer pl = myPlayer.GetComponent<CPlayer>();
				// 名前,メッセージ,x,y,zの文字列を生成。WWW.EscapeURL()でURLエンコード
				string senddata = WWW.EscapeURL(pl.getPlayerName())
					+","+WWW.EscapeURL(pl.getMessage())
						+","+pl.transform.position.x
						+","+pl.transform.position.y
						+","+pl.transform.position.z;
				// JavaScriptのsendMyData()関数にsenddataを送る
				Application.ExternalCall("sendMyData",senddata);
			}
			// 指定のミリ秒待つ
			yield return new WaitForSeconds(SendMilliSec/1000.0f);
		}
	}

	// JavaScriptから呼び出すsendで他から届いたデータを処理する関数
	public void updateData(string dt) {
		// ,で文字列を分割して、名前とメッセージはURLデコードする
		string [] sepa = {","};
		string [] param = dt.Split(sepa,System.StringSplitOptions.None);
		param [0] = WWW.UnEscapeURL (param [0]);
		param [1] = WWW.UnEscapeURL (param [1]);

		// プレイヤー検索
		GameObject go = getPlayerByName(param[0]);
		// プレイヤーと同一の時は処理しない
		if (go == null) {
				// 追加するので、リストをクリア
				goPlayers = null;

				// 他のプレイヤーとして登録させる
				go = (GameObject)Instantiate (prefAnother);
				go.SendMessage ("setPlayerName", param [0]);
		} else if (go == myPlayer) {
			// プレイヤーと等しい場合は届いたデータは処理不要
			return;
		}

		// データを設定
		go.SendMessage("setMessage",param[1]);
		go.SendMessage ("setTarget",
			new Vector3(
				float.Parse(param[2]),
				float.Parse(param[3]),
				float.Parse(param[4])));
	}
}

