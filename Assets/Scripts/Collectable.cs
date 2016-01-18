using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	public bool isTaken;
	public int ID;

	GameStats gameStats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStats == null)
			gameStats = FindObjectOfType<GameStats> ();
	}

	public void SetIsTaken (bool b) {
		isTaken = b;
		if (b)
			this.gameObject.SetActive (false);
	}

	public void Collect () {
		isTaken = true;
		this.gameObject.SetActive (false);
		gameStats.currentRoom.itemsInfo [ID] = true;
	}
}
