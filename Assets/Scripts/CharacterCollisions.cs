using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterCollisions : MonoBehaviour {

	public Text scoreText;

	private GameStats gameStats;
	int lowestValue = 1, mediumValue = 2, highValue = 4, premiumValue = 10;
	float lavaDamage = 3;

	//We need a cooldown for the interaction clicks when you enter the interaction trigger
	//Else there will be registered a lot of clicks even though you only mean to click once
	bool cdForInteraction;
	float cdEndTime, cdRate = 0.5f;

	// Use this for initialization
	void Start () {
		gameStats = FindObjectOfType<GameStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStats == null) {
			gameStats = FindObjectOfType<GameStats> ();
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		string tag = other.transform.tag;
		switch (tag) {
		case "Bronze":
		case "Silver":
		case "Gold":
		case "Diamond":
			CollectPoint(other.gameObject);
			break;
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
		case "Switch":
			CheckInteraction(col.gameObject);
			break;
		case "Door":
			CheckInteraction(col.gameObject);
			break;
		case "Entrance":
			CheckInteraction(col.gameObject);
			break;
		default:
			break;
		}
	}

	void CheckInteraction (GameObject obj) {
		SwitchHandler switchScript = obj.GetComponent<SwitchHandler> ();
		DoorHandler doorScript = obj.GetComponent<DoorHandler> ();
		if (Input.GetAxis ("Interaction") > 0) {
			if(obj.tag == "Switch") {
				//Checks if the switch is on cooldown
				//If it's not then it ini
				if(cdForInteraction){
					if(Time.time > cdEndTime) {
						cdForInteraction = false;
					}
				} else {
					//Initiate cooldown for the switch
					cdEndTime = Time.time + cdRate;
					cdForInteraction = true;

					//Change the state of the switch
					switchScript.ChangeState();
				}
			} else if (obj.tag == "Door") {
				if(doorScript.IsOpen()){
					//Save character position for next level.
					GameStats.charPosX = transform.position.x;
					GameStats.charPosY = transform.position.y;

					//Go through the door
					doorScript.GoThroughDoor();
				}
			} else {
				//Save character position for next level.
				GameStats.charPosX = transform.position.x;
				GameStats.charPosY = transform.position.y;

				//Go through the door
				obj.GetComponent<DoorHandler>().GoThroughDoor();
				Debug.Log("YOU WENT THROUGH THE DOOR! :D");
			}
		}
	}

	void CollectPoint(GameObject point){
		int score = gameStats.GetScore();
		switch (point.tag) {
		case "Bronze":
			score += lowestValue;
			break;
		case "Silver":
			score += mediumValue;
			break;
		case "Gold":
			score += highValue;
			break;
		case "Diamond":
			score += premiumValue;
			break;
		}
		gameStats.SetScore (score);
		point.GetComponent<Collective> ().Collect ();
	}

	void LoseHP (float hp) {
		gameStats.ReduceHealth (hp);
	}
}
