using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomSetup : MonoBehaviour {

	private GameStats gameStats;

	// Use this for initialization
	void Start () {
		gameStats = FindObjectOfType<GameStats> ();

		if (gameStats.GetRoomList() != null && gameStats.GetRoomList().Length != 0) {
			gameStats.currentRoom.roomNo = gameStats.GetCurrentRoomNo();
			gameStats.SetRoom (gameStats.currentRoom, gameStats.currentRoom.roomNo);
		}
		if(!gameStats.currentRoom.spawned)
			setupNewRoom ();

		setupRoom ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void setupNewRoom() {
		GameObject interactive = GameObject.FindGameObjectWithTag ("Interactive");
		List<GameObject> io = new List<GameObject> ();
		Dictionary<int,bool> itemsInfo = new Dictionary<int, bool>();
		foreach (GameObject go in interactive) {
			switch (go.tag) {
			case "Doorsystem":
				DoorSystem dscript = go.GetComponent<DoorSystem> ();
				itemsInfo.Add (dscript.ID, dscript.isOpen);
				break;
			case "Pellet":
			case "Diamond":
				Collective cscript = go.GetComponent<Collective>();
				itemsInfo.Add(cscript.ID,cscript.isTaken);
				break;
			default:
				break;
			}
		}
		gameStats.UpdateItemsInfo (itemsInfo);
		gameStats.currentRoom.spawned = true;
	}

	void setupRoom () {
		foreach
	}
}
