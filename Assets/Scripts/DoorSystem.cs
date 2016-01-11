using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour {

	public int ID;
	public bool isOpen;

	private SwitchHandler s;
	private DoorHandler d;

	// Use this for initialization
	void Start () {
		d = this.transform.FindChild ("Door").GetComponent<DoorHandler> ();
		s = this.transform.FindChild ("Switch").GetComponent<SwitchHandler> ();
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenDoorSystem () {
		isOpen = true;
		s.SetIsOpen (true);
		d.SetIsOpen (true);
	}

	public void CloseDoorSystem () {
		isOpen = false;
		s.SetIsOpen (false);
		d.SetIsOpen (false);
	}
}
