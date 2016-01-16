using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameStats : MonoBehaviour {

	public Level currentLevel;
	public List<Level> levelList = new List<Level>();
	public Room currentRoom;
	public static int currentRoomNo = 0;
	public static float charPosX = -4.44f, charPosY = -2.69f;

	private static bool spawned = false;

	// Use this for initialization
	void Start () {
		if (!spawned) {
			spawned = true;
			currentLevel = new Level (0);
			currentLevel.SetRoomList(new Dictionary<int,Room> ());

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
		return currentLevel.GetRoomList()[no];
	}

	public void SetRoom (Room room, int no) {
		Dictionary<int, Room> rl = currentLevel.GetRoomList ();
		rl [no] = room;
		currentLevel.SetRoomList(rl);
	}

	public Dictionary<int, Room> GetRoomList () {
		return currentLevel.GetRoomList();
	}

	public void UpdateItemsInfo (Dictionary<int,bool> itemsInfo) {
		currentRoom.itemsInfo = itemsInfo;
		Dictionary<int,Room> rl = currentLevel.GetRoomList ();
		rl [currentRoom.roomNo] = currentRoom;
		currentLevel.SetRoomList(rl);
	}

	public void ChangeRoom (int no) {
		
		try {
			currentRoom = GetRoom(no);
		} catch (KeyNotFoundException e) {
			SetRoom(new Room (no), no);
			currentRoom = GetRoom(no);
		}


		currentRoomNo = no;
	}

	public void SetScore (int no) {
		currentLevel.score = no;
	}

	public int GetScore () {
		return currentLevel.score;
	}

	public void ReduceHealth (float health) {
		currentLevel.health -= health;
		CheckForDeath ();
	}

	public float GetHealth () {
		return currentLevel.health;
	}

	private void CheckForDeath () {
		if (currentLevel.health <= 0)
			Debug.Log ("You are dead!");
	}
}

[System.Serializable]
public class Room {

	public int roomNo;
	public bool spawned;
	public Dictionary<int,bool> itemsInfo = new Dictionary<int,bool>();

	public Room (int no) {
		roomNo = no;
	}
}

[System.Serializable]
public class Level {

	public int score, ID;
	public float health = 3;
	public static Dictionary<int, Room> roomList;

	public Level (int no) {
		this.ID = no;
	}

	public Dictionary<int,Room> GetRoomList () {
		return roomList;
	}

	public void SetRoomList (Dictionary<int,Room> rl) {
		roomList = rl;
	}

}