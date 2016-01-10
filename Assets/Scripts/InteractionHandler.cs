using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour {
	public Sprite stateInit, stateFinal, stateOther;
	public enum States {INITIAL,OTHER,FINAL};
	States currState = States.INITIAL;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState () {
		Debug.Log (currState.ToString ());
		switch (currState) {
		case States.FINAL:
			GetComponent<SpriteRenderer>().sprite = stateInit;
			currState = States.INITIAL;
			break;
		case States.INITIAL:
			GetComponent<SpriteRenderer>().sprite = stateFinal;
			currState = States.FINAL;
			break;
		default:
			break;
		}
	}

	public States GetState () {
		return currState;
	}

}
