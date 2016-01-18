using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {

	public float rotationSpeed, jumpForce, jumpDistance, waitTime;

	Vector3 initPos, endPos;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		initPos = transform.position;
		endPos = new Vector3 (initPos.x, initPos.y + jumpDistance, initPos.z);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		if (transform.position.y < initPos.y && rb != null) {
			rb.AddForce (new Vector2 (0, jumpForce));
		}
		transform.Rotate (Vector3.forward * rotationSpeed);
	}
}
