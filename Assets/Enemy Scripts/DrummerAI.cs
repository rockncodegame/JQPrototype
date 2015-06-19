using UnityEngine;
using System.Collections;

public class DrummerAI : MonoBehaviour
{
	//creating variables
	public float attackTime;
	public float distance;
	private float speed;
	public Vector3 playerPosition;
	public Vector3 spawnPoint;
	public float beat;
	public Sprite DrummerSprite;
	public string cState;

	public Vector3 nextTarget;
	public float nextMove;
	public float idleTime;
	public GameObject Bullet;
	//attack variables
	double nextBlast=0;
	double delay = 2;
	double attacked = 0;

	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");

	//starting state system
	enum States
	{
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	States CurrentState = States.Idle;

	//creating holders for outside scripts
	public GameObject Player; 
	// Use this for initialization
	void Start ()
	{
		//grabbing outside scripts and variables
		spawnPoint = transform.position;
		spawnPoint.y = playerPosition.y;
		anim = GetComponent<Animator> ();
		GetComponent<EnemyController>().health = 5;
		//setting sprite
		//GetComponent<SpriteRenderer>().sprite = DrummerSprite;
		attackTime = 0;
		InvokeRepeating ("BeatTime", 2,1);

	}
	
	// Update is called once per frame
	void Update ()
	{ 	

		// getting player position and distance between
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance (playerPosition, transform.position);
		//switching states if  conditions met
		if((beat == 2) && distance > 2){
			changeState(States.Advance);
			attacked = 0;
			nextTarget = playerPosition; 
			nextTarget.z = (nextTarget.z + Random.Range(-2,2));
		}
		 
		if((beat == 10)){
			changeState(States.Retreat);
			attacked = 0;
			nextTarget = playerPosition; 
		}

		if (CurrentState == States.Advance)
			{
				Advance();
			}

			else if (CurrentState == States.Retreat)
			{
				Retreat();
			}
			else if (CurrentState == States.Attack)
			{
				Attack();
			}
			
			else if (CurrentState == States.Idle)
			{
				Idle();
			}
		
	}

	

	void BeatTime(){
		//self kept beat
		if(beat == 16){
			beat = 0;
			}
			beat++;
		}


	
	void Retreat(){
		speed = 4 * Time.deltaTime;
		idleTime = Time.time + 3;
		transform.position = Vector3.MoveTowards (transform.position, spawnPoint, speed);
		//moves back to spawn point
		if (transform.position.x >= (spawnPoint.x -2) && transform.position.x <= (spawnPoint.x +2)){ 
			changeState(States.Idle);

			}
		}

	void Advance(){
		speed = 5 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, nextTarget, speed);
		if (distance <= 5) {
			changeState(States.Attack);
			}

	}

	void Idle(){
		attacked = 0;
	}
	
	void Attack(){
		//turn off and on scripts
		if(Time.time > nextBlast){
			//animate
			anim.SetTrigger (AttackHash);
			// create bullet
			nextBlast = Time.time + delay;
			Instantiate(Bullet, transform.position, transform.rotation);
			Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
			attacked++;
		}
		if (attacked >= 1) {
			idleTime = Time.time + 3;
			changeState(States.Idle);
		}
	}
	
	void changeState(States newState)
	{	//states changer machine
		if (CurrentState == newState) {
			return;
		}
		
		switch(newState)
		{
		case States.Idle:
		{
			Idle();
			CurrentState = newState;
			break;
		}
		case States.Retreat:
		{
			Retreat();
			CurrentState = newState;
			break;
		}
		case States.Advance:
		{

			Advance();
			CurrentState = newState;
			break;
		}
		case States.Attack:
		{
			Attack();
			CurrentState = newState;
			break;
		}
		default:
		{ return;
		}
		}
		
	}
	
}