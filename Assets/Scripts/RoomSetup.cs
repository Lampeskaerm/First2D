using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomSetup : MonoBehaviour {

	private GameStats gameStats;
	private bool levelSetUp = false;
	// Use this for initialization
	void Start () {
		gameStats = FindObjectOfType<GameStats> ();

		if (gameStats.GetRoomList() != null && gameStats.GetRoomList().Count != 0) {
			gameStats.currentRoom.roomNo = gameStats.GetCurrentRoomNo();
			gameStats.SetRoom (gameStats.currentRoom, gameStats.currentRoom.roomNo);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!Application.isLoadingLevel && !levelSetUp) {
			if (!gameStats.currentRoom.spawned)
				setupNewRoom ();

			setupRoom ();
			levelSetUp = true;
		}

	}

	void OnLevelWasLoaded (int lvl) {
	}

	void setupNewRoom() {
		GameObject interactive = GameObject.FindGameObjectWithTag ("Interactive");
		Transform[] children = new Transform[0];
		if(interactive != null)
			children = interactive.transform.GetComponentsInChildren<Transform> ();
		Dictionary<int,bool> itemsInfo = new Dictionary<int, bool>();
		foreach (Transform t in children) {
			GameObject go = t.gameObject;
			Collective cscript;
			switch (go.tag) {
			case "Doorsystem":
				DoorSystem dscript = go.GetComponent<DoorSystem> ();
				itemsInfo.Add (dscript.ID, dscript.isOpen);
				break;
			case "Pellet":
				cscript = go.GetComponent<Collective>();
				itemsInfo.Add(cscript.ID,cscript.isTaken);
				break;
			case "Diamond":
				cscript = go.GetComponent<Collective>();
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
		Dictionary<int,bool> itemsInfo = gameStats.currentRoom.itemsInfo;
		GameObject gos = GameObject.FindGameObjectWithTag ("Interactive");
		if (gos != null) {
			Transform[] children = gos.transform.GetComponentsInChildren<Transform> ();
			foreach (Transform t in children) {
				int id;
				bool b;
				GameObject go = t.gameObject;
				switch (go.tag) {
				case "Doorsystem":
					DoorSystem dscript = go.GetComponent<DoorSystem> ();
					id = dscript.ID;
					b = itemsInfo [id];
					dscript.SetIsOpen (b);
					break;
				case "Pellet":
				case "Diamond":
					Collective cscript = go.GetComponent<Collective> ();
					id = cscript.ID;
					b = itemsInfo [id];
					cscript.SetIsTaken (b);
					break;
				default:
					break;
				}
			}
		}
	}
}
