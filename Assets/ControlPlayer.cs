using UnityEngine;
using System.Collections;

public class ControlPlayer : MonoBehaviour {
	// プレイヤーの移動速度
	public float SPEED = 6.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);
		rigidbody.velocity = vel.normalized * SPEED;
	}
}
