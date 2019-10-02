using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is meant to replace wander behavior for a single agent.
It will include flocking behavior if the agent detects other agents nearby */

public class FlockAgent : MonoBehaviour {

	public Collider2D agentCollider;
	//public FlockBehavior behavior;
	[Range(1f,100f)]
	public float maxSpeed = 1.0f;
	
	[Range(0f,1f)]
	public float avoidanceRadiusMultiplier = 0.5f;


	/*** Variables needed for single agent behavior */

	public Vector2 direction,
					currentVelocity,
					wanderTarget;

	private Vector3 agentPos;				
	public Vector3 screenViewPos;

	public float seekForce = 0.1f,
				avoidingSmoothTime = 0.1f,
				normalSmoothTime = 0.25f,
				lastWanderTarget = 0.0f,
				now,
				avoidingBoundaryMin = 0.1f,
				avoidingBoundaryMax = 0.9f,
				wanderLengthScalar = 5.0f,
				agentSmoothTime = 0.25f;

	public bool avoiding = false;

	void Start () 
	{
		agentCollider = GetComponent<Collider2D>();	

		// Below we are choosing a random direction from 360 degrees
		transform.up = Random.insideUnitCircle.normalized;

		// now that we have our direction, set our variable to that direction
		direction = transform.up;

		agentPos = transform.position;

		/** Here we set our first wander target **/
		wanderTarget = Random.insideUnitCircle;
		now = Time.time;
	}
	
	
	void Update () 
	{
		screenViewPos = Camera.main.WorldToViewportPoint(transform.position);
		
	}

	// void OnDrawGizmosSelected()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position, neighborRadius);
    // }



	public void Move(Vector2 velocity)
	{
		transform.up = velocity;
		transform.position += (Vector3)velocity * Time.deltaTime;

	}

	public Vector2 Seek(FlockAgent agent, Vector3 startingPos, Vector3 targetPos)
	{
		/* Seek behavior is a smoothed method for facing a target */

		// Get the target vector. Target = target pos - agent pos
		Vector2 targetVector = new Vector2(
			targetPos.x - startingPos.x,
			targetPos.y - startingPos.y
		).normalized * agent.maxSpeed;

		/** If the agent is in an avoiding state, make it turn faster */
		// if (agent.avoiding)
		// 	agent.agentSmoothTime = agent.avoidingSmoothTime;
		// else
		// 	agent.agentSmoothTime = agent.normalSmoothTime;
	
		targetVector = Vector2.SmoothDamp(
			agent.transform.up,
			targetVector,
			ref agent.currentVelocity,
			agent.agentSmoothTime
		);
		return targetVector;
	}

	
}
