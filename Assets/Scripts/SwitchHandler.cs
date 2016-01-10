using UnityEngine;
using System.Collections;

public class SwitchHandler : MonoBehaviour {
	public Sprite stateInit, stateFinal, stateOther;
	public enum State {OPEN, CLOSED};
	State currState = State.CLOSED;
	//true for open, false for closed
	public State status;

	private int ID;
	private GameStats gameStats;

	// Use this for initialization
	void Start () {
		ID = this.GetComponentInParent<DoorSystem> ().ID;
		gameStats = FindObjectOfType<GameStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState () {
		Debug.Log (currState.ToString ());
		switch (currState) {
		case State.OPEN:
			//Sets the doorstatus for open.
			gameStats.currentRoom.doorSystems [ID].GetComponent<SwitchHandler> ().status = State.CLOSED;

			GetComponent<SpriteRenderer>().sprite = stateInit;
			currState = State.CLOSED;
			break;
		case State.CLOSED:
			//Sets the doorstatus for open.
			gameStats.currentRoom.doorSystems [ID].transform.GetChild(1).GetComponent<SwitchHandler> ().status = State.OPEN;

			GetComponent<SpriteRenderer>().sprite = stateFinal;
			currState = State.OPEN;
			break;
		default:
			break;
		}
	}

	public State GetState () {
		return currState;
	}

}
