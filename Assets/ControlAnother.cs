using UnityEngine;
using System.Collections;

public class ControlAnother : MonoBehaviour {
	// 速度
	public float SPEED = 6.0f;
	// 目的地座標
	private Vector3 posTarget = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = posTarget - transform.position;
		rigidbody.velocity = vel.normalized * SPEED;
	}

	// 目的地を設定する関数
	public void setTarget(Vector3 targ) {
		posTarget = targ;
	}
}
