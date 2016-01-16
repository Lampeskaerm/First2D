using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public Transform target;
	public int minX, maxX;
	public float dampTime = 2f;
	public CharController targetScript;

	float width, height, margin;
	Vector3 velocity = Vector3.zero;
	Rigidbody2D rb;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		rb = GetComponent<Rigidbody2D> ();
		targetScript = target.GetComponent<CharController> ();
		width = cam.pixelWidth;
		height = cam.pixelHeight;
		margin = width / 5;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 screenPos = cam.WorldToScreenPoint (target.position);
		if (screenPos.x <= margin && !targetScript.IsFacingRight())
			rb.velocity = new Vector2 (targetScript.GetMovement (), rb.velocity.y);
		else if (screenPos.x >=(width - margin) && targetScript.IsFacingRight())
			rb.velocity = new Vector2 (targetScript.GetMovement (), rb.velocity.y);
		else
			rb.velocity = Vector2.zero;
	}
}
