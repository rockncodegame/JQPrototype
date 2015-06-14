﻿using UnityEngine;
using System.Collections;

public class Bldg : MonoBehaviour {
	public GameObject front;
	bool fade;
	float rate;
	Color temp;
	// Use this for initialization
	void Start () {
		fade = false;
		temp = front.renderer.material.color;
		rate = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (fade) {
			temp.a -= rate;
		}
		else {
			temp.a += rate;
		}

		if (temp.a <= 0)
			temp.a = 0;

		if (temp.a >= 1)
			temp.a = 1;

		front.renderer.material.color = temp;
	}

	void OnTriggerEnter (Collider c) {
		if (c.gameObject.tag == "Player")
			fade = true;
	}

	void OnTriggerExit (Collider c) {
		if (c.gameObject.tag == "Player")
			fade = false;
	}
}
