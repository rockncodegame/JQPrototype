using UnityEngine;
using System.Collections;

public class BossEnemyController : MonoBehaviour
{	//set up variables and get controller
	public float health;
	public BossWolfAI BossWolf;
	public float speed;
	public Vector3 playerPosition;
	public bool isRotated;
	public float dropItem;
	public GameObject HPdrop;
	public GameObject spark;
	public GameObject p;
	public float shockTime;
	public float Shock;
	public float inv;
	public float nextReactHit;
	Animator anim;
	int HitHash = Animator.StringToHash("Hit");
	//	Vector3 movement = Vector3.zero;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		//access to controller and other connected scripts
		BossWolf = GetComponent<BossWolfAI>();
		isRotated = false;
		//controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update (){

		//Flipping to face player
		p = GameObject.Find ("Player");
		if (p != null) {
			playerPosition = p.transform.position;
			if (playerPosition.x > transform.position.x && isRotated == false) {
					transform.Rotate (0, 180, 0); 
					isRotated = true;
			}
			if (playerPosition.x < transform.position.x && isRotated == true) {
					transform.Rotate (0, 180, 0); 
					isRotated = false;
			}
		}
	}
	
	
	public void GetHit(float dmg){
		if (Time.time >= inv){
			if(BossWolf.CurrentState != BossWolfAI.States.OverLook){
				health -= dmg;
				Instantiate(spark, transform.position, transform.rotation);
				
				if (health > 0 && Time.time >= nextReactHit) {
					anim.SetTrigger (HitHash);
					nextReactHit = Time.time + 1.5f;
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider c){
		Vector3 PlayerPos = c.transform.position;
		if (c.gameObject.tag == "Player") {
			if(PlayerPos.x < transform.position.x){
				c.attachedRigidbody.AddForce(new Vector3(1 * -1, 1, 0) * 100);
			}
			else if(PlayerPos.x > transform.position.x){
				c.attachedRigidbody.AddForce(new Vector3(1 * 1, 1, 0) * 100);
			}
			if(PlayerPos.y < transform.position.y){
				c.attachedRigidbody.AddForce(new Vector3(1 * -1, 1, 0) * 100);
			}
			else if(PlayerPos.y > transform.position.y){
				c.attachedRigidbody.AddForce(new Vector3(1 * 1, 1, 0) * 100);
			}
		}
		
	}
	
}

