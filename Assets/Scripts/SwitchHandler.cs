using UnityEngine;
using System.Collections;

public class SwitchHandler : MonoBehaviour {
	public Sprite stateInit, stateFinal, stateOther;
	private bool isOpen = false;

	private int ID;
	private DoorSystem parent;
	private DoorHandler doorHandler;
	private GameStats gameStats;

	// Use this for initialization
	void Start () {
		Setup ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStats == null)
			Setup ();
	}

	public void ChangeState () {
		Debug.Log (isOpen);
		if (isOpen) {
			//Sets the doorstatus for open.
			gameStats.currentRoom.doorSystems [ID].GetComponent<DoorSystem>().CloseDoorSystem();
			parent.CloseDoorSystem ();

			GetComponent<SpriteRenderer> ().sprite = stateInit;
			doorHandler.SetSprite ("Closed");
		} else {
			gameStats.currentRoom.doorSystems [ID].transform.FindChild("Switch").GetComponent<SwitchHandler> ().isOpen = true;
			parent.OpenDoorSystem ();

			GetComponent<SpriteRenderer>().sprite = stateFinal;
			doorHandler.SetSprite ("Open");
		}
	}

	public void SetIsOpen (bool b) {
		isOpen = b;
		gameStats.currentRoom.doorSystems [ID].transform.FindChild("Switch").GetComponent<SwitchHandler> ().isOpen = isOpen;
	}

	public bool IsOpen () {
		return isOpen;
	}

	private void Setup () {
		parent = this.GetComponentInParent<DoorSystem> ();
		doorHandler = parent.transform.FindChild ("Door").GetComponent<DoorHandler> ();
		ID = parent.ID;
		gameStats = FindObjectOfType<GameStats> ();
	}
}
