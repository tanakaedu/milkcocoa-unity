using UnityEngine;
using System.Collections;

public class CPlayer : MonoBehaviour {
	TextMesh textName;
	TextMesh textMes;

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {

	}
	
	public string getPlayerName() {
		return textName.text;
	}
	
	public void setPlayerName(string nm) {
		if (textName == null) {
			Start ();
		}
		textName.text = nm;
	}

	public string getMessage() {
		return textMes.text;
	}

	public void setMessage(string mes) {
		textMes.text = mes;
	}
}