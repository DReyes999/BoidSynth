using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior With Leader/Avoidance With Leader")]
public class AvoidanceWithLeader : FlockBehaviorWithLeader{

	public override Vector2 CalculateLeadMove(FlockAgent agent, 
											List<Transform> context, 
											FlockWithLeader flock,
											Leader_Boidsynth07 leader)
	{
		/* If we don't find any neighbors then make no adjustments. 
		That means to throw a vector with no magnitude. */
		if (context.Count == 0)
		{
			return Vector2.zero;
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

	public Vector2 Seek(FlockAgent agent, Vector2 agentPos, Vector3 targetPos)
	{
		/* Seek behavior is a smoothed method for facing a target */

		// Get the target vector. Target = target pos - agent pos
		Vector2 targetVector = new Vector2(
			targetPos.x - agentPos.x,
			targetPos.y - agentPos.y
		);


	
		targetVector = Vector2.SmoothDamp(
			agent.transform.up,
			targetVector,
			ref agent.currentVelocity,
			agent.agentSmoothTime
		);

		return targetVector;
	}
}
