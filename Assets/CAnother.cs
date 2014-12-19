using UnityEngine;
using System.Collections;

public class CAnother : MonoBehaviour {
	public float SPEED = 6.0f;
	private Vector3 posTarget = Vector3.zero;

	// Update is called once per frame
	void Update () {
		Vector3 vel = posTarget - transform.position;
		rigidbody.velocity = vel.normalized*SPEED;
	}

	public void setTarget(float x,float y,float z) {
		posTarget.x = x;
		posTarget.y = y;
		posTarget.z = z;
	}
}
