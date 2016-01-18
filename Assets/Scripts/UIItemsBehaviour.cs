using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIItemsBehaviour : MonoBehaviour {

	List<string> taken = new List<string> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetFound (string color) {
		Image[] arr = GetComponentsInChildren<Image> ();
		foreach (Image img in arr) {
			string name = img.sprite.name;
			if (name.Contains (color)) {
				name = name.Replace ("Empty", "");
				img.sprite = Resources.Load<Sprite>("UI/" + name);
				taken.Add (name);
			}
		}
	}

	public List<string> GetTakenGems () {
		return taken;
	}
}
