﻿using UnityEngine;
using System.Collections;

public class BulletAction : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public Vector3 playerPosition;
	public PlayerStats pStats;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		//set bullet to auto destroy after a time

		Destroy (gameObject, 1f);
		playerPosition = (GameObject.Find ("Player").transform.position);


	}
	

	// Update is called once per frame
	void Update () {
		float speed = 7 * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
	}
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<PlayerStats>().GetHit (1);
			Destroy (gameObject);
			//c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
		}
		

	}
}