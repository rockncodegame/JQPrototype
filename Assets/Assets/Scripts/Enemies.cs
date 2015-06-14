﻿using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {
	public float health, damage, stunTime;
	public GameObject p;
	public MoveTest pMove;
	public PlayerStats pStats;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pMove = p.GetComponent<MoveTest> ();
		pStats = p.GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if health is below 0 destroy the object
		if (health <= 0) {
			health = 0;
			Destroy(gameObject);
		}
		//enemies move towards the player for now for testing purposes
		//move away when player is stunned
		if (pMove.stun < Time.time)
			transform.position = Vector3.MoveTowards (transform.position, p.transform.position, 3 * Time.deltaTime);
		//else
			//transform.position = Vector3.MoveTowards (transform.position, new Vector3 (p.transform.position.x, transform.position.y, transform.position.z), -5 * Time.deltaTime);
	}

	void OnTriggerEnter (Collider c){
		if (c.gameObject.tag == "Player") {
			if (transform.position.x <= p.transform.position.x)
				pMove.movement = new Vector3(130.0f, 150.0f, 0.0f) * Time.deltaTime;
			else
				pMove.movement =  new Vector3(-130.0f, 150.0f, 0.0f) * Time.deltaTime;
			
			pMove.stun = Time.time + .5f;
			pStats.GetHit(1);
		}
	}

	//called whenever the player attacks hit the enemy
	public void GetHit(float dmg){
		health -= dmg;
	}
}
