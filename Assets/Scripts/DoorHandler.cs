using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour {
	public Sprite open, closed;
	public string sceneName;

	int ID;
	bool goThrough, isOpen;
	GameStats gameStats;

	// Use this for initialization
	void Start () {
		if (this.tag == "Door") {
			ID = this.GetComponentInParent<DoorSystem> ().ID;
			gameStats = FindObjectOfType<GameStats> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (goThrough) {
			if (gameStats == null)
				gameStats = FindObjectOfType<GameStats> ();
			
			goThrough = false;
			char c = sceneName [sceneName.Length - 1];
			int sceneNo = (int)char.GetNumericValue (c);

			gameStats.ChangeScene (sceneNo);
			if (gameStats.GetRoom(sceneNo) == null)
				gameStats.SetRoom(gameStats.NewRoom (sceneNo), sceneNo);
			
			gameStats.SetCurrentRoomNo (sceneNo);
			gameStats.currentRoom = gameStats.GetRoom(sceneNo);
			Application.LoadLevel(sceneName);
		}
	}

	public void GoThroughDoor(){

		goThrough = true;
	}

	public void SetSprite (string state) {
		if (state == "Open") {
			this.GetComponent<SpriteRenderer> ().sprite = open;
		} else if (state == "Closed") {
			this.GetComponent<SpriteRenderer> ().sprite = closed;
		}
	}

	public bool IsOpen () {
		return isOpen;
	}

	public void SetIsOpen(bool b) {
		isOpen = b;
	}
}
