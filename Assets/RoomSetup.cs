using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomSetup : MonoBehaviour {

	public int roomNo;

	private GameStats gameStats;

	// Use this for initialization
	void Start () {
		gameStats = FindObjectOfType<GameStats> ();
		if(!gameStats.currentRoom.spawned)
			setupNewRoom ();

		setupRoom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setupNewRoom() {
		List<GameObject> doorSystems = GameObject.FindGameObjectsWithTag ("Doorsystem").ToList();
		if (doorSystems.Count > 0) {
			doorSystems = doorSystems.OrderBy (x => x.GetComponent<DoorSystem> ().ID).ToList ();
		}
		gameStats.SetDoorsystems (doorSystems);
		gameStats.currentRoom.spawned = true;
	}

	void setupRoom () {
		List<GameObject> doorSystems = gameStats.currentRoom.doorSystems;
		List<GameObject> dsig = GameObject.FindGameObjectsWithTag ("Doorsystem").ToList();
		if (dsig.Count > 0)
			dsig.OrderBy (x => x.GetComponent<DoorSystem> ().ID).ToList ();

		for (int i = 0; i < dsig.Count; i++) {
			dsig [i] = doorSystems [i];
		}
	}
}
