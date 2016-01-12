using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour {

	public int ID;
	public bool isOpen;

	private SwitchHandler s;
	private DoorHandler d;
	private GameStats gameStats;

	// Use this for initialization
	void Start () {
		d = this.transform.FindChild ("Door").GetComponent<DoorHandler> ();
		s = this.transform.FindChild ("Switch").GetComponent<SwitchHandler> ();
		DontDestroyOnLoad (this.gameObject);
		gameStats = FindObjectOfType<GameStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStats == null)
			gameStats = FindObjectOfType<GameStats> ();
	
	}

	public void OpenDoorSystem () {
		isOpen = true;
		s.SetIsOpen (true);
		d.SetIsOpen (true);
		gameStats.currentRoom.itemsInfo [ID] = true;
	}

	public void CloseDoorSystem () {
		isOpen = false;
		s.SetIsOpen (false);
		d.SetIsOpen (false);
		gameStats.currentRoom.itemsInfo [ID] = false;
	}

	public void SetIsOpen (bool b) {
		if (b)
			OpenDoorSystem ();
		else
			CloseDoorSystem ();
	}
}
