using UnityEngine;
using System.Collections;

public class SwitchHandler : MonoBehaviour {
	public Sprite stateInit, stateFinal, stateOther;
	private bool isOpen = false;

	private int ID;
	private DoorSystem parent;
	private DoorHandler doorHandler;

	// Use this for initialization
	void Start () {
		parent = this.GetComponentInParent<DoorSystem> ();
		doorHandler = parent.transform.FindChild ("Door").GetComponent<DoorHandler> ();
		ID = parent.ID;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState () {
		Debug.Log (isOpen);
		if (isOpen) {
			//Sets the doorstatus for open.
			parent.CloseDoorSystem ();
		} else {
			parent.OpenDoorSystem ();
		}
	}

	public void SetIsOpen (bool b) {
		isOpen = b;
		if(!b)
			GetComponent<SpriteRenderer> ().sprite = stateInit;
		else
			GetComponent<SpriteRenderer>().sprite = stateFinal;
	}

	public bool IsOpen () {
		return isOpen;
	}

	private void Setup () {
		
	}
}
