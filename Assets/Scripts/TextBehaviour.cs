using System;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour 
{
	GameStats gameStats;

	void Start ()
	{
		gameStats = FindObjectOfType<GameStats> ();
	}

	void Update () {
		if (gameStats != null)
			GetComponent<Text> ().text = "Score: " + gameStats.GetScore ();
		else
			gameStats = FindObjectOfType<GameStats> ();
	}
}

