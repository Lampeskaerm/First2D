using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour {

	int collectedPellets, totalPellets;

	//We need a cooldown for the interaction clicks when you enter the interaction trigger
	//Else there will be registered a lot of clicks even though you only mean to click once
	bool cdForInteraction;
	float cdEndTime, cdRate = 0.5f;

	// Use this for initialization
	void Start () {
		totalPellets = GameObject.FindGameObjectsWithTag ("Pellet").Length;
		Debug.Log ("Total number of pellets:" + totalPellets);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D other) {
		string tag = other.transform.tag;
		switch (tag) {
		case "Pellet":
			CollectPellet(other.gameObject);
			break;
		case "Diamond":
			CollectPellet(other.gameObject);
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

	void CollectPellet(GameObject pellet){
		++collectedPellets;
		pellet.GetComponent<Collective> ().Collect ();
	}
}
