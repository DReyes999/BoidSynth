using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader_Boidsynth07 : MonoBehaviour {
	private Vector2 direction,
					currentVelocity,
					wanderTarget,
					previousWanderTarget;

	private Camera cam;
					
	private Vector3 agentPos, screenViewPos;
	public float maxSpeed = 3.0f,

				avoidingBoundaryMin = 0.1f,
				avoidingBoundaryMax = 0.9f,
				wanderLengthScalar = 5.0f,
				wanderTargetTimeRangeMin = 10,
				wanderTargetTimeRangeMax = 13,
				approachDistance = 2,
				lerpSpeed = 0.5f,
				freq;
				
	public double speed;


	[SerializeField]
	private float agentSmoothTime = 1.0f, 
				avoidingSmoothTime = 1.0f,
				normalSmoothTime = 3.0f,
				avoidMultiplier = 0.0f,
				avoidMultiplierScalar = 0.5f,
				wanderTargetDist,
				previousWanderTargetDist,
				speedClamped,
				lastWanderTarget = 0.0f,
				now;
	
	public bool approaching = false,
				avoiding = false;

	void Start()
	{
		cam = Camera.main;

		// Below we are choosing a random direction from 360 degrees
		transform.up = Random.insideUnitCircle.normalized;

		// now that we have our direction, set our variable to that direction
		direction = transform.up;
		agentPos = transform.position;

		/** Here we set our first wander target **/
		wanderTarget = Random.insideUnitCircle;
		previousWanderTarget = Vector2.zero;
		now = Time.time;
	}

	void Update()
	{
		/* Updates the screen position of the agent */
		screenViewPos = Camera.main.WorldToViewportPoint(transform.position);

		agentPos = this.transform.position;
		this.speedClamped = Mathf.Clamp((maxSpeed*(previousWanderTargetDist / 2)), 0,2);
		this.speed = System.Math.Round(speedClamped,2);
	}

	void FixedUpdate()
	{
		Wander();

		previousWanderTarget = Vector2.Lerp(previousWanderTarget,Wander(), lerpSpeed * Time.deltaTime );
		previousWanderTargetDist = GetTargetVector(agentPos,previousWanderTarget).magnitude;


		if (this.wanderTargetDist < approachDistance)
		{
			approaching = true;
			Move(Seek(agentPos,wanderTarget));
		}
		else
		{
			approaching = false;
			Move(Seek(agentPos,previousWanderTarget));
		}
		Debug.DrawLine(agentPos,wanderTarget,Color.red);
	}

	public void Move(Vector2 velocity)
	{
		/* given a vector, the agent will face that target vector
		and will move in that direction */
		this.transform.up = velocity;

		/* move the agent by adding to the x,y values of the position every frame */
		this.transform.position += (Vector3)velocity * (float)this.speed * Time.deltaTime;
	}

	public Vector2 Wander()
	{
		/* This function picks a random point on the map and returns the vector. When fed into the
		move or seek function, the agent will move in the direction fo that 
		random point */

		// Set 'now' to the current time
		now = Time.time;

		// If the difference between now and the last time a random point was chosen
		// exceeds some amount, pick a new random target
		if (now - lastWanderTarget > Random.Range(wanderTargetTimeRangeMin,wanderTargetTimeRangeMax))
			{
				
				lastWanderTarget = now;
				
				previousWanderTarget = wanderTarget;
				//wanderTarget = Random.insideUnitCircle * wanderLengthScalar;
				wanderTarget = SpawnWanderTarget();
			}

		// Get the distance from the boid to the target
		Vector2 targetVector = new Vector2(
			wanderTarget.x - agentPos.x,
			wanderTarget.y - agentPos.y
		);

		this.wanderTargetDist = targetVector.magnitude;

		return wanderTarget;
		
	}

	public Vector2 Seek(Vector3 agentPos, Vector3 targetPos)
	{
		/* Seek behavior is a smoothed method for facing a target */
		// Get the target vector. Target = target pos - agent pos
		Vector2 targetVector = new Vector2(
			targetPos.x - agentPos.x,
			targetPos.y - agentPos.y
		);

		Sensors2();

		/** If the agent is in an avoiding state, make it turn faster */
		if (avoiding)
			agentSmoothTime = avoidingSmoothTime;
		else
			agentSmoothTime = normalSmoothTime;

		targetVector = Vector2.SmoothDamp(
			transform.up,
			targetVector,
			ref currentVelocity,
			agentSmoothTime
		);

		return targetVector;
	}

	private Vector2 GetTargetVector(Vector2 currentPos, Vector2 targetPos)
	{
		Vector2 targetVector = new Vector2(
			targetPos.x - currentPos.x,
			targetPos.y - currentPos.y
		);

		return targetVector;
	}

	private Vector2 Sensors(Vector2 directionToPointTowards)
	{
		/* This function detects how close the agent is to the edge of the screen.
		If the agent is too close to the horizontal or vertical edges, the avoid multiplier
		will be pplied to the random wander target in order to turn the agent away from the edge*/

		avoiding = false;
		avoidMultiplier = 0.0f;
		

		if (screenViewPos.x < avoidingBoundaryMin || screenViewPos.x > avoidingBoundaryMax)
		{
			avoiding = true;
			avoidMultiplier += avoidMultiplierScalar;
		}

		if (screenViewPos.y < avoidingBoundaryMin || screenViewPos.y > avoidingBoundaryMax)
		{
			avoiding = true;
			avoidMultiplier -= avoidMultiplierScalar;
		}
		
		if (avoiding)
		{
			directionToPointTowards *= avoidMultiplier;
		}

		directionToPointTowards = Vector2.SmoothDamp(
			transform.up,
			directionToPointTowards,
			ref currentVelocity,
			avoidingSmoothTime
		);

		return directionToPointTowards;

	}

	void Sensors2()
	{
		/* This function detects how close the agent is to the edge of the screen.
		If the agent is too close to the horizontal or vertical edges, the avoid multiplier
		will be pplied to the random wander target in order to turn the agent away from the edge*/

		avoiding = false;
		avoidMultiplier = 0.0f;
		

		if (screenViewPos.x < avoidingBoundaryMin || screenViewPos.x > avoidingBoundaryMax)
		{
			avoiding = true;
			//avoidMultiplier += avoidMultiplierScalar;
		}

		if (screenViewPos.y < avoidingBoundaryMin || screenViewPos.y > avoidingBoundaryMax)
		{
			avoiding = true;
			//avoidMultiplier -= avoidMultiplierScalar;
		}
	}

	Vector2 SpawnWanderTarget()
	{
		Vector3 randPos = new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height),-7);
		Vector2 spawnPosition = new Vector2();
		Debug.Log(randPos);
		randPos.z = -7;
		spawnPosition = Camera.main.ScreenToWorldPoint(randPos);
		Debug.Log(spawnPosition);
			return spawnPosition;
	}
}
