using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStats : MonoBehaviour {

	public Room currentRoom;
	public Room[] roomList;
	public static int levelScore = 0;
	public static float charPosX = -4.44f, charPosY = -2.69f;

	private static bool spawned = false;

	// Use this for initialization
	void Start () {
		if (!spawned) {
			spawned = true;
			roomList = new Room[10];

			DontDestroyOnLoad (this);

			roomList[0] = currentRoom;

		} else {
			DestroyImmediate (this);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetDoorsystems (List<GameObject> doorSystems) {
		currentRoom.doorSystems = doorSystems;
		roomList [currentRoom.roomNo] = currentRoom;
	}

	public Room NewRoom (int no) {
		return new Room(no);
	}
}

[System.Serializable]
public class Room {

	public int roomNo;
	public bool spawned;
	public List<GameObject> doorSystems = new List<GameObject>();

	public Room (int no) {
		roomNo = no;
	}
}