using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public float walkSpeed, runSpeed, jumpForce;

	bool facingRight = true;
	float speed;
	Rigidbody2D rb;
	Animator anim;

	//Fall variables
	public Transform groundCheck;
	public LayerMask whatIsGround;
	bool grounded, soundPlayed;
	float groundRadius = 0.2f;

	//Audio Sources
	AudioSource jumpSound;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		jumpSound = transform.GetComponentInChildren<AudioSource> ();

		transform.position = new Vector2 (GameStats.charPosX, GameStats.charPosY);
	}
	
	// Update is called once per frame
	void Update () {
		if (grounded && rb.velocity.y == 0) {
			soundPlayed = false;
		}
		//Jump
		if (grounded && Input.GetAxis ("Jump") > 0) {
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0, jumpForce));
			if (!soundPlayed) {
				jumpSound.Play ();
				soundPlayed = true;
			}
		}
	}

	void FixedUpdate() {
		//Checks if we are grounded
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		//Checks if we can jump
		anim.SetFloat ("VSpeed", rb.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		
		if (Input.GetAxis ("Run") > 0)
			speed = runSpeed;
		else 
			speed = walkSpeed;

		rb.velocity = new Vector2 (move * speed, rb.velocity.y);

		anim.SetFloat ("Speed", Mathf.Abs (rb.velocity.x));

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
