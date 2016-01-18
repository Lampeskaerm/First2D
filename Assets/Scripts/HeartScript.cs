using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HeartScript : MonoBehaviour {
	public Sprite full, empty, half;

	private int noOfHearts;
	private Image[] hearts;

	// Use this for initialization
	void Start () {
		hearts = GetComponentsInChildren<Image> ();
		noOfHearts = hearts.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHearts (float health) {
		int wholeIntHP = (int)Math.Floor (health);
		bool hpIsDecimal = (health % 1 != 0);
		for (int i = 0; i < noOfHearts; i++) {
			Image h = hearts [i];
			if (h != null) {
				if (i < wholeIntHP)
					h.sprite = full;
				else if (hpIsDecimal && (i == (wholeIntHP)))
					h.sprite = half;
				else
					h.sprite = empty;
			}
		}
	}
}
