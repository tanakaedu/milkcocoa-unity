using UnityEngine;
using System.Collections;

public class CMilkcocoa : MonoBehaviour {
	public GameObject prefPlayer;
	public GameObject prefAnother;
	public static float SendMilliSec = 100;

	// Use this for initialization
	void Start () {
		StartCoroutine("SendMilkcocoa");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator SendMilkcocoa() {
		while (true) {
			if (myPlayer != null) {
				CPlayer pl = myPlayer.GetComponent<CPlayer>();
				string senddata = WWW.EscapeURL(pl.getPlayerName())
					+","+WWW.EscapeURL(pl.getMessage())
					+","+pl.transform.position.x
					+","+pl.transform.position.y
					+","+pl.transform.position.z;
				Application.ExternalCall("sendMyData",senddata);
			}
			yield return new WaitForSeconds(SendMilliSec/1000.0f);
		}
	}

	private GameObject myPlayer = null;
	public void login(string nm) {
		// 同じ名前のプレイヤーを探す
		if (getPlayerByName (nm) != null) {
			// 見つかった場合はエラー
			Application.ExternalCall("login_error","");
			return;
		}
		
		// 登録
		myPlayer = (GameObject)GameObject.Instantiate(prefPlayer);
		myPlayer.SendMessage ("setPlayerName", nm);
		myPlayer.SendMessage ("setMessage", "");
		Application.ExternalCall ("login_ok", "");
	}

	GameObject [] goPlayers = null;
	GameObject getPlayerByName(string nm) {
		if (goPlayers == null) {
			goPlayers = GameObject.FindGameObjectsWithTag ("Player");
		}
		foreach (GameObject go in goPlayers) {
			CPlayer pl = go.GetComponent<CPlayer>();
			if (pl.getPlayerName().CompareTo(nm) == 0) {
				return go;
			}
		}
		return null;
	}

	void setMessage(string mes) {
		if (myPlayer != null) {
			myPlayer.SendMessage("setMessage",mes);
		}
	}

	public void updateData(string dt) {
		string [] sepa = {","};
		string [] param = dt.Split(sepa,System.StringSplitOptions.None);
		param [0] = WWW.UnEscapeURL (param [0]);
		param [1] = WWW.UnEscapeURL (param [1]);

		// プレイヤーと同一の時は処理しない
		if ((myPlayer != null)
						&& (myPlayer.GetComponent<CPlayer> ().getPlayerName ().CompareTo (param [0]) == 0)) {
			return;
		}

		GameObject go = getPlayerByName(param[0]);
		if (go == null) {
			// 他のプレイヤーとして登録させる
			go = (GameObject)Instantiate(prefAnother);
			go.SendMessage("setPlayerName",param[0]);
			goPlayers = GameObject.FindGameObjectsWithTag ("Player");
		}
		// データを設定
		go.SendMessage("setMessage",param[1]);
		go.GetComponent<CAnother>().setTarget(
			float.Parse(param[2]),
			float.Parse(param[3]),
			float.Parse(param[4]));
	}

}
