using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAutoFlockAgent : MonoBehaviour 
{
	
	Collider2D agentcollider;
	public Collider2D AgentCollider {get {return agentcollider;}}
	public Vector2 //direction, 
				//mPos, 
				//mPosInWorld, 
				//currentVelocity, 
				wanderTarget;

	private Vector2 direction,
					currentVelocity;
	public Vector3 agentPos,
					centerSensorPos,
					centerSensorScreenPos,
					rightSensorPos,
					rightSensorScreenPos,
					leftSensorPos,
					leftSensorScreenPos,
					screenViewPos;
	public float maxSpeed = 2.0f,
				seekForce = 0.1f,
				avoidingSmoothTime = 0.25f,
				normalSmoothTime = 0.5f,
				lastWanderTarget = 0.0f,
				now,
				sensorAngle;
	private float rayLength = 1.0f,
				agentSmoothTime;
	private bool avoiding = false;

	/****** Screen edge positioning Vars  */
	
	


	void Start()
	{
		//agentcollider = GetComponent<Collider2D>();

		/*  Here we set which direction the agent is facing.*/

		// Below we are choosing a random direction from 360 degrees
		transform.up = Random.insideUnitCircle.normalized;

		// now that we have our direction, set our variable to that direction
		direction = transform.up;

		agentPos = transform.position;

		// Pick a random point from a random range inside of a unit circle
		wanderTarget = Random.insideUnitCircle;
		now = Time.time;

		float a= transform.position.y;
		float b= transform.position.z;

		
	}

	void Update()
	{
		screenViewPos = Camera.main.WorldToViewportPoint(transform.position);
		//centerSensorPos = Camera.main.WorldToViewportPoint(transform.up.normalized);
		
		Move(Seek(transform.position, Wander()));
		
	}

	public void Move(Vector2 directionToPointTowards)
	{
		/* given a vector, the agent will face that target vector
		and will move in that direction */

		transform.up = directionToPointTowards;

		/* move the agent by adding to the x,y values of the position every frame */
		transform.position += (Vector3)directionToPointTowards * maxSpeed * Time.deltaTime;
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
		if (now - lastWanderTarget > Random.Range(3,8))
			{
				Debug.Log("New Target");
				lastWanderTarget = now;
				// TODO: pick a random point inside a defined circle
				// OR: pick a random coordinate within the bounding box
				// TODO make the scalar nor a magic number
				wanderTarget = Random.insideUnitCircle * 5f;
			}
		
		// Draw a line to the random target. for visual aid / debugging

		Debug.DrawLine(
			this.transform.position,
			wanderTarget,
			Color.red,
			0.0f,
			true
		);

		wanderTarget = Sensors(wanderTarget);

		return wanderTarget;
		
	}

	public Vector2 Seek(Vector3 agentPos, Vector3 targetPos)
	{
		/* Seek behavior is a smoothed method for facing a target */

		// Get the target vector. Target = target pos - agent pos
		Vector2 targetVector = new Vector2(
			targetPos.x - agentPos.x,
			targetPos.y - agentPos.y
		).normalized * maxSpeed;

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

	// public void FollowMouse(Vector2 mousePosition)
	// {
	// 	/* First let's face the agent towards the mouse */

	// 	/*  subtract the position of agent from mouse position to get vector pointing from
	// 	the agent to the mouse */
	// 	Vector2 vectorToMouse = new Vector2(
	// 		mousePosition.x - agentPos.x,
	// 		mousePosition.y - agentPos.y
	// 	);

	// 	// set the direction variable to the vector we calculated.
	// 	// Normalized gives the vector a constant length of 1
	// 	direction = vectorToMouse.normalized;
	// 	// This actually faces the direction we set
	// 	transform.up = direction;

	// 	//Draw a line to mouse position
	// 	Debug.DrawLine(
	// 		this.transform.position,
	// 		mPosInWorld,
	// 		Color.white,
	// 		0.0f,
	// 		true
	// 	);

	// 	// Move the agent
	// 	this.transform.position += (Vector3)direction * Time.deltaTime;
	// }

	private Vector2 Sensors(Vector2 directionToPointTowards)
	{
		/* Sensors for obstacle detection */
		//Debug.Log("Sensors are working");
		
        Vector2 sensorStartPos = transform.position;
		Vector2 upOffSet = new Vector2(0,1);

        Vector2 centerRay = transform.up;
        Vector2 leftRay = Quaternion.Euler(0, 0, -20) * transform.up;
        Vector2 rightRay = Quaternion.Euler(0, 0, 20) * transform.up;
		float avoidMultiplier = 0.0f;
		avoiding = false;

		/* Draw the RayCasts */
        RaycastHit2D centerHit = Physics2D.Raycast(sensorStartPos, centerRay, rayLength);
        RaycastHit2D leftHit = Physics2D.Raycast(sensorStartPos, leftRay, rayLength);
        RaycastHit2D rightHit = Physics2D.Raycast(sensorStartPos, rightRay, rayLength);

		// /** Center Sensor **/
		// centerSensorPos = centerRay.normalized;
		// Debug.DrawRay(transform.position, centerSensorPos, Color.yellow);
		// centerSensorScreenPos = Camera.main.WorldToViewportPoint(centerSensorPos);

		// /** Right Sensor **/
		// rightSensorPos = rightRay.normalized;
		// Debug.DrawRay(transform.position, rightSensorPos, Color.yellow);
		// rightSensorScreenPos = Camera.main.WorldToViewportPoint(rightSensorPos);

		// /** Left Sensor **/
		// leftSensorPos = leftRay.normalized;
		// Debug.DrawRay(transform.position, leftSensorPos, Color.yellow);
		// leftSensorScreenPos = Camera.main.WorldToViewportPoint(leftSensorPos);

		// if (leftSensorScreenPos.x < 0 || leftSensorScreenPos.x > 1)
		// {
		// 	Debug.DrawLine(sensorStartPos,leftSensorPos, Color.red);
		// 	Debug.Log("Did Hit");
		// 	avoiding = true;
		// 	avoidMultiplier += 0.5f;
		// }
		// if (leftSensorScreenPos.y < 0 || leftSensorScreenPos.y > 1)
		// {
		// 	Debug.DrawLine(sensorStartPos,leftSensorPos, Color.red);
		// 	Debug.Log("Did Hit");
		// 	avoiding = true;
		// 	avoidMultiplier += 0.5f;
		// }

		// if (rightSensorScreenPos.x < 0 || rightSensorScreenPos.x > 1)
		// {
		// 	Debug.DrawLine(sensorStartPos,rightSensorPos, Color.red);
		// 	Debug.Log("Did Hit");
		// 	avoiding = true;
		// 	avoidMultiplier -= 0.5f;
		// }
		// if (rightSensorScreenPos.y < 0 || rightSensorScreenPos.y > 1)
		// {
		// 	Debug.DrawLine(sensorStartPos,rightSensorPos, Color.red);
		// 	Debug.Log("Did Hit");
		// 	avoiding = true;
		// 	avoidMultiplier -= 0.5f;
		// }




		/*Draw the debug rays */
        // Debug.DrawRay(sensorStartPos, centerRay.normalized, Color.yellow);
        // Debug.DrawRay(sensorStartPos, leftRay.normalized, Color.yellow);
        // Debug.DrawRay(sensorStartPos, rightRay.normalized, Color.yellow);

		/* Center Ray checking for Obstacles */
		if (centerHit)
        {
            // if (centerHit.collider.CompareTag("Obstacle"))
			// {
			// 	Debug.Log("Did Hit");
			// 	Debug.DrawLine(transform.position,centerHit.point, Color.red);
			// 	avoiding = true;
			// }
			
		}
		else
		{
			//Debug.Log("Did not Hit");
			//Debug.DrawRay(transform.position, centerRay.normalized, Color.white);
			centerSensorPos = centerRay.normalized;
			Debug.DrawRay(transform.position, centerSensorPos, Color.yellow);
		}

		/* left Ray checking for Obstacles */
		if (leftHit)
        {
            if (leftHit.collider.CompareTag("Obstacle"))
			{
				Debug.DrawLine(sensorStartPos,leftHit.point, Color.red);
				Debug.Log("Did Hit");
				avoiding = true;
				avoidMultiplier += 0.5f;
			}
			else
			{
				Debug.DrawRay(transform.position, leftRay.normalized, Color.white);
				//Debug.Log("Did not Hit");
			}
		}

		/* Right Ray checking for Obstacles */
		if (rightHit)
        {
            if (!rightHit.collider.CompareTag("Background"))
			{
				Debug.DrawLine(sensorStartPos, rightHit.point, Color.red);
				Debug.Log("Did Hit");
				avoiding = true;
				avoidMultiplier -= 0.5f;
			}
			else
			{
				Debug.DrawRay(transform.position, rightRay.normalized, Color.white);
				//Debug.Log("Background");
			}
		}
		
		//TODO: MAKE THE BOUNDARIES NOT MAGIC NUMBERS
		if (screenViewPos.x < 0.1 || screenViewPos.x > 0.9)
		{
			avoiding = true;
			Debug.Log("Avoiding horizontal edges");
			avoidMultiplier += 0.5f;
		}

		if (screenViewPos.y < 0.1 || screenViewPos.y > 0.9)
		{
			avoiding = true;
			Debug.Log("Avoiding vertical edges");
			avoidMultiplier -= 0.5f;
		}
		
		if (avoiding)
		{
			directionToPointTowards *= avoidMultiplier;
		}

		return directionToPointTowards;

	}
}
