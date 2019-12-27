using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is meant to replace wander behavior for a single agent.
It will include flocking behavior if the agent detects other agents nearby
 */

public class FlockAgent : MonoBehaviour {

	public Collider2D agentCollider;
	public AudioSource audioSource;
	[Range(1f,100f)]
	public float maxSpeed = 1.0f;
	[Range(0f,1f)]
	public float avoidanceRadiusMultiplier = 0.5f;
	
	/*** Variables needed for alpha fade */
	Color tmpColor;
	SpriteRenderer sprite;
	
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
				wanderLengthScalar = 5.0f,
				agentSmoothTime = 0.25f,
				speed,
				dist,
				alphaAmount,
				ReMappedValue,
				colorVelocity;

	public bool avoiding = false;

	void Start () 
	{
		agentCollider = GetComponent<Collider2D>();	
		// this.audioSource = this.GetComponent<AudioSource>();
		// if (audioSource != null)
		// 	Debug.Log("audiosource okay!");
		// else	
		// 	Debug.Log("audiosource not okay!");
		// sprite = GetComponentInChildren<SpriteRenderer>();
		// tmpColor = sprite.color;

		// Below we are choosing a random direction from 360 degrees
		transform.up = Random.insideUnitCircle.normalized;
		// now that we have our direction, set our variable to that direction
		direction = transform.up;
		agentPos = transform.position;

		/** Here we set our first wander target **/
		// wanderTarget = Random.insideUnitCircle;
		// now = Time.time;
	}
	
	
	void Update () 
	{
		this.screenViewPos = Camera.main.WorldToViewportPoint(transform.position);
		speed = Mathf.Clamp((maxSpeed*(dist / 2)), 0,2);
		//Changecolor();
		
	}

	public void Move(Vector2 velocity)
	{
		transform.up = velocity;
		transform.position += (Vector3)velocity * Time.deltaTime;

	}

	void Changecolor()
	{
		ReMappedValue = ExtensionMethods.Remap(speed,0,2,0,1);
		alphaAmount = Mathf.SmoothDamp(tmpColor.a,ReMappedValue, ref colorVelocity, 0.5f);
		if (alphaAmount < 0.08)
			alphaAmount = 0;
		tmpColor.a = alphaAmount;
		sprite.color = tmpColor;
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
