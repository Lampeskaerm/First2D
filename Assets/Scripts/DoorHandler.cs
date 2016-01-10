using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour {

	public string sceneName;

	int ID;
	bool goThrough;
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

			if (gameStats.roomList [sceneNo] == null)
				gameStats.roomList [sceneNo] = gameStats.NewRoom (sceneNo);
			
			gameStats.currentRoom = gameStats.roomList[sceneNo];
			Application.LoadLevel(sceneName);
		}
	}

	public void GoThroughDoor(){

		goThrough = true;

		Debug.Log("YOU WENT THROUGH THE DOOR! :D");
	}

}
