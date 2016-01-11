using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStats : MonoBehaviour {

	public Room currentRoom;
	public static Room[] roomList;
	public static int levelScore = 0, currentRoomNo = 0;
	public static float charPosX = -4.44f, charPosY = -2.69f;

	private static bool spawned = false;

	// Use this for initialization
	void Start () {
		if (!spawned) {
			spawned = true;
			roomList = new Room[10];

			DontDestroyOnLoad (this);

		} else {
			DestroyImmediate (this);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetCurrentRoomNo (int no){
		currentRoomNo = no;
	}

	public int GetCurrentRoomNo () {
		return currentRoomNo;
	}

	public Room GetRoom (int no) {
		return roomList[no];
	}

	public void SetRoom (Room room, int no) {
		roomList [no] = room;
	}

	public Room[] GetRoomList () {
		return roomList;
	}

	public void SetDoorsystems (List<GameObject> doorSystems) {
		currentRoom.doorSystems = doorSystems;
		roomList [currentRoom.roomNo] = currentRoom;
	}

	public Room NewRoom (int no) {
		return new Room(no);
	}

	public void ChangeScene (int no) {
		if(

		if (gameStats.GetRoom(sceneNo) == null)
			gameStats.SetRoom(gameStats.NewRoom (sceneNo), sceneNo);

		gameStats.SetCurrentRoomNo (sceneNo);
		gameStats.currentRoom = gameStats.GetRoom(sceneNo);
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