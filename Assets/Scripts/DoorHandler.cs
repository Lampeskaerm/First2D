using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorHandler : MonoBehaviour {
	public string LeadingToScene;

	private bool isOpen;
	private GameStats gameStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStats == null) {
			gameStats = FindObjectOfType<GameStats> ();
		}
	}

	public void OpenDoor() {
		isOpen = true;

	}

	public void GoThrough() {
		char c = LeadingToScene [LeadingToScene.Length - 1];
		int sceneNo = (int)char.GetNumericValue (c);

		gameStats.ChangeRoom (sceneNo);

		SceneManager.LoadScene (LeadingToScene);
	}

	public void SetIsOpen (bool b) {
		isOpen = b;
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer> ();
		string name;
		foreach (SpriteRenderer sr in srs) {
			if (b) {
				if (sr.sprite.name.Contains ("Top"))
					name = "doorOpenTop";
				else
					name = "doorOpen";
				sr.sprite = Resources.Load<Sprite>(name);
			}
		}
	}

	public bool IsOpen () {
		return isOpen;
	}
}
