using System;
using UnityEngine;

public class TextBehaviour : MonoBehaviour 
{
	void Start ()
	{
		//transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0,1,0));
		Debug.Log (transform.position.ToString());
	}
}

