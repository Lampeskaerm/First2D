using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour {
	public string sceneName;
	public int doorID;

	bool goThrough;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (goThrough) {
			goThrough = false;
			GameStats.doorID = doorID;
			Application.LoadLevel(sceneName);
		}
	}

	public void GoThroughDoor(){
		goThrough = true;
	}

}
