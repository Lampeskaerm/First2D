using UnityEngine;
using System.Collections;

public class FlyBehaviour : MonoBehaviour {
	//The fly always starts out by moving right

	//The distance it moves in a direction before it turns around
	public float coverDistance, speed;

	Vector2 initPos, currentPos, endPos;
	bool facingRight, isDead;
	float move, destroyTimer = 2;
	Rigidbody2D rb;
	Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		initPos = transform.position;
		endPos = new Vector2 (initPos.x + coverDistance, initPos.y);
	}
	
	// Update is called once per frame
	void Update () {
		currentPos = transform.position;
	}

	void FixedUpdate() {
		if (!isDead) {
			if (currentPos.x <= initPos.x) {
				Flip ();
				move = speed;
			} else if (currentPos.x >= endPos.x) {
				Flip ();
				move = (-speed);
			}
			float newPosX = transform.position.x + move;
			transform.position = new Vector3 (newPosX, transform.position.y, transform.position.z);
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Die() {
		rb.gravityScale = 1;
		isDead = true;
		anim.SetBool ("Dead", true);
		StartCoroutine (DestroyAfterSeconds (destroyTimer));
	}

	IEnumerator DestroyAfterSeconds (float seconds) {
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
