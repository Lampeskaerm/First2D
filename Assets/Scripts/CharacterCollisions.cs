using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterCollisions : MonoBehaviour {

	public Text scoreText;
	public GameObject hearts;

	public LayerMask whatIsKillable;
	public Transform hitCheck;
	bool killed;
	float hitRadius = 0.2f;

	GameStats gameStats;
	HeartScript heartScript;
	//The values of the pick-up elements.
	int lowestValue = 1, mediumValue = 2, highValue = 4, premiumValue = 10;
	//The Damage values of the different obstacles
	float lavaDamage = 1, flyDamage = 0.5f;
	//The initial position of the character
	Vector3 initPos;

	UIItemsBehaviour uiGems, uiKeys;

	//We need a cooldown for the interaction clicks when you enter the interaction trigger
	//Else there will be registered a lot of clicks even though you only mean to click once
	bool cdForInteraction, collisionDeactivated;
	float cdEndTime, cdRate = 0.5f;

	// Use this for initialization
	void Start () {
		heartScript = hearts.GetComponent<HeartScript> ();
		UIItemsBehaviour[] uis = FindObjectsOfType<UIItemsBehaviour> ();
		foreach (UIItemsBehaviour u in uis) {
			if (u.gameObject.name == "Gems")
				uiGems = u;
			else
				uiKeys = u;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStats == null) {
			gameStats = FindObjectOfType<GameStats> ();
			heartScript.SetHearts (gameStats.GetHealth ());
			initPos = gameStats.GetInitialPosition ();
		}
	}

	void FixedUpdate () {
		killed = Physics2D.OverlapCircle (hitCheck.position, hitRadius, whatIsKillable);
	}

	void OnCollisionEnter2D (Collision2D other) {
		string tag = other.transform.tag;
		string color = "";
		switch (tag) {
		case "BronzeCoin":
		case "SilverCoin":
		case "GoldCoin":
			CollectPoint (other.gameObject);
			break;
		case "Key":
			CollectPoint (other.gameObject);
			color = other.gameObject.name.Replace ("Key", "");
			uiKeys.SetFound (color);
			break;
		case "Gem":
			CollectPoint (other.gameObject);
			color = other.gameObject.name.Replace("Gem","");
			uiGems.SetFound (color);
			break;
		case "Fly":
			if (!killed) {
				LoseHP (flyDamage);
			} else {
				KillEnemy (other.gameObject);
				Collider2D[] colliders = GetComponents<Collider2D> ();
				foreach (Collider2D col in colliders) {
					Physics2D.IgnoreCollision (other.gameObject.GetComponent<Collider2D> (), col);
				}
			}
			break;
		default:
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		string tag = col.tag;
		switch (tag) {
		case "Lava":
			LoseHP (lavaDamage);
			break;
		default:
			break;
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		string tag = col.tag;
		switch (tag) {
		case "Door":
			GameObject obj = col.gameObject;
			DoorHandler dh = obj.GetComponent<DoorHandler> ();
			if (!dh.IsOpen ()) {
				string color = obj.name.Replace ("Door", "");
				if (Input.GetAxis ("Interaction") != 0 && uiKeys.GetTaken ().Contains ("hud_key" + color))
					dh.OpenDoor ();
			} else {
				dh.GoThrough ();
			}
			break;
		default:
			break;
		}
	}

	void CollectPoint(GameObject point){
		int score = gameStats.GetScore();
		switch (point.tag) {
		case "BronzeCoin":
			score += lowestValue;
			break;
		case "SilverCoin":
			score += mediumValue;
			break;
		case "GoldCoin":
			score += highValue;
			break;
		case "BlueGem":
			score += premiumValue;
			break;
		}
		gameStats.SetScore (score);
		point.GetComponent<Collectable> ().Collect ();
	}

	void LoseHP (float hp) {
		gameStats.ReduceHealth (hp);
		heartScript.SetHearts (gameStats.GetHealth());
	}

	void KillEnemy (GameObject go) {
		switch (go.tag) {
		case "Fly":
			FlyBehaviour script = go.GetComponent<FlyBehaviour> ();
			script.Die ();
			break;
		default:
			break;
		}
	}

	void Die() {
		gameStats.ReduceHealth (gameStats.GetHealth ());
	}
}
