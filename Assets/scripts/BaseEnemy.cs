using UnityEngine;
using System.Collections;
using System;

public class BaseEnemy : MonoBehaviour {
	private GameObject checkpointTarget;
	public float speed;
	public float hp;
	public GameObject currentTarget;
	
	private Vector3 pointLeftPath;
	private GameObject storedTarget;
	private GameObject waveMaster;
	private GameObject[] checkpointList;
	private bool attackingTowers;
	private int currentCheckpointNum;
	private bool offPath;
	private Vector3 leftPathAt;
	// Use this for initialization
	void Start () {
		hp = 100f;
		waveMaster = GameObject.Find ("WaveMaster");
		checkpointList = new GameObject[13];
		for(int i=0; i < 13; i++)
		{
			checkpointList[i] = GameObject.Find ("Checkpoint "+ (i+1));
		}
		checkpointTarget = checkpointList[0];
		currentTarget = checkpointTarget;
		currentCheckpointNum = 0;
		speed = 3f;
		attackingTowers = false;
		offPath = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		StepToTarget(currentTarget);
		checkIfDead();
	}
	
	// Advances one step toward obj.  Must be called in update
	public void StepToTarget(GameObject obj) {
		// If you are not attacking towers AND not on the path, you are on the path, so check if at current target (next checkpoint)
		if(!attackingTowers && !offPath) {
			rigidbody.position = Vector3.MoveTowards(rigidbody.position, obj.transform.position, speed*Time.deltaTime);
			if(atTarget (currentTarget)) {
				if(currentCheckpointNum == checkpointList.Length - 1) {
					Destroy (gameObject);
					waveMaster.BroadcastMessage("enemyFinished");
				}
				else
					advanceCheckpoint();
			}
		}
		// If you are attacking towers, save your spot if you're on the path and move towards it than attack
		else if(attackingTowers){
			// If you are still on the path, save your position before leaving
			if(!offPath) {
				leftPathAt = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				offPath = !offPath;
			}
			rigidbody.position = Vector3.MoveTowards(rigidbody.position, obj.transform.position, speed*Time.deltaTime);
			if(atTarget (currentTarget) ) {
				attack(currentTarget);
			}
		}
		// If you are not attacking towers and you're off the path, move back to your saved spot
		else if(!attackingTowers && offPath) {
			rigidbody.position = Vector3.MoveTowards(rigidbody.position, pointLeftPath, speed*Time.deltaTime);
			// If you're at the spot you left the path
			if(transform.position == pointLeftPath) {
				offPath = !offPath;
			}
		}
	}
	
	void advanceCheckpoint() {
		currentCheckpointNum++;
		checkpointTarget = checkpointList[currentCheckpointNum];
		currentTarget = checkpointTarget;
	}
	
	bool atTarget(GameObject obj) {
		if (Math.Abs (transform.position.x - obj.transform.position.x) <=.1 && 
			Math.Abs (transform.position.z - obj.transform.position.z) <=.1) {
			transform.position.Set(obj.transform.position.x,transform.position.y,obj.transform.position.z);
			return true;
		}
		else
			return false;
	}

	public void hit(object[] args) {
		float dmg = (float) args[0];
		hp = hp - dmg;
		storedTarget = currentTarget;
		currentTarget = (GameObject) args[1];
		if(!attackingTowers)
			attackingTowers = !attackingTowers;
		checkIfDead();
	}
	
	public void attack(GameObject target) {
		//Send target a message with amount of damage done
		//Check if you killed the target
		//If target = dead
		//	currentTarget = storedTarget
		//	attackingTowers = false
		
	}
	void checkIfDead() {
		if(hp <= 0f) {
			Destroy (gameObject);
			waveMaster.BroadcastMessage("enemyKilled");
		}
	}
		
}
