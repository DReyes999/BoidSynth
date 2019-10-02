using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior 
{

	public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		/* If we don't find any neighbors then make no adjustments. 
		That means to throw a vector with no magnitude. */
		if (context.Count == 0)
		{
			return Wander(agent, agent.transform.position);
		}

		// Add all points together and average. (trying to find a point in the middle of the group)
		Vector2 avoidanceMove = Vector2.zero;

		// How many agents are in our avoidance radius
		int nAvoid = 0;

		//go through each transform
		foreach (Transform item in context)
		{
			// calculate if the transform is within our avoidance radius
			if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
			{
				nAvoid++;
				avoidanceMove += (Vector2)(agent.transform.position - item.position);	
			}
			
		}
		if (nAvoid > 0)
			avoidanceMove /= nAvoid;

		return avoidanceMove;
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

	public override Vector2 Wander(FlockAgent agent, Vector3 wanderStartingPos)
	{
		/* This function picks a random point on the map and returns the vector. When fed into the
		move or seek function, the agent will move in the direction fo that 
		random point */

		// Set 'now' to the current time
		agent.now = Time.time;

		// If the difference between now and the last time a random point was chosen
		// exceeds some amounte, pick a new random target
		if (agent.now - agent.lastWanderTarget > Random.Range(3,8))
			{
				Debug.Log("New Target");
				agent.lastWanderTarget = agent.now;
				// TODO: pick a random point inside a defined circle
				// OR: pick a random coordinate within the bounding box
				// TODO make the scalar nor a magic number
				agent.wanderTarget = Random.insideUnitCircle * agent.wanderLengthScalar;
			}
		
		// Draw a line to the random target. for visual aid / debugging
		Debug.DrawLine(
			wanderStartingPos,
			agent.wanderTarget,
			Color.red,
			0.0f,
			true
		);
		return Seek(agent, wanderStartingPos, agent.wanderTarget);
	}
}
