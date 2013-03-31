using UnityEngine;
using System.Collections;
using System;

public class BaseEnemy : MonoBehaviour {
	public GameObject CurrentCheckpointTarget;
	private GameObject[] checkpointList;
	private int currentCheckpointNum;
	public float hp;
	private GameObject waveMaster;
	private GameObject target;
	
	// Use this for initialization
	void Start () {
		hp = 100f;
		waveMaster = GameObject.Find ("WaveMaster");
		checkpointList = new GameObject[10];
		for(int i=0; i < 10; i++)
		{
			checkpointList[i] = GameObject.Find ("Checkpoint "+ (i+1));
		}
		CurrentCheckpointTarget = checkpointList[0];
		currentCheckpointNum = 0;
		Move ();
	}
	
	// Update is called once per frame
	void Update () {
		if(checkAtCheckpoint())
			advanceCheckpoint();
		checkIfDead();
			
	}
	void Move() {
		/*rigidbody.velocity.Set(0,0,0);
		float myX, myZ, tX, tZ;
		myX = transform.position.x;
		myZ = transform.position.z;
		tX = CurrentCheckpointTarget.transform.position.x;
		tZ = CurrentCheckpointTarget.transform.position.z;
		transform.LookAt(CurrentCheckpointTarget.transform.position);
		//Vector3 velocity = Vector3.MoveTowards(transform.position, CurrentCheckpointTarget.transform.position, Time.deltaTime);
		rigidbody.velocity = new Vector3((tX-myX)/(Math.Abs (tX-myX)+.00001f), 0, (tZ-myZ)/(Math.Abs(tZ-myZ)+.00001f));
		
		if((transform.position.x - CurrentCheckpointTarget.transform.position.x) > .1)
			xVel = -1f;
		else if((transform.position.x - CurrentCheckpointTarget.transform.position.x) < -.1)
			xVel = 1f;
		else
			xVel = 0f;
		if((transform.position.z - CurrentCheckpointTarget.transform.position.z) > .1)
			zVel = -1f;
		else if((transform.position.z - CurrentCheckpointTarget.transform.position.z) < -.1)
			zVel = 1f;
		else
			zVel = 0f;
		Vector3 velocity = new Vector3(xVel,0,zVel);
		rigidbody.velocity = velocity;
		*/
	}
	
	void advanceCheckpoint() {
		currentCheckpointNum++;
		CurrentCheckpointTarget = checkpointList[currentCheckpointNum];
		Move ();
	}
	
	bool checkAtCheckpoint() {
		if (Math.Abs (transform.position.x - CurrentCheckpointTarget.transform.position.x) <=.1 && Math.Abs (transform.position.z - CurrentCheckpointTarget.transform.position.z) <=.1)
		{
			rigidbody.velocity = new Vector3(0f,0f,0f);
			transform.position.Set(CurrentCheckpointTarget.transform.position.x,transform.position.y,CurrentCheckpointTarget.transform.position.z);
			return true;
		}
		else
			return false;
	}
	
	public void hit(float dmg, GameObject source) {
		hp = hp - dmg;
		target = source;
		checkIfDead();
	}
	
	void checkIfDead() {
		if(hp <= 0f) {
			Destroy (gameObject);
			waveMaster.BroadcastMessage("enemyKilled");
		}
	}
		
}
