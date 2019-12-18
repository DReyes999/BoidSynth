using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Screen")]
public class StayInScreen : FlockBehavior {

	private bool avoiding = false;

	public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		avoiding = false;
		float avoidMultiplier = 0.0f;

		Vector2 direction = agent.transform.up;

		if (agent.screenViewPos.x < flock.avoidingBoundaryMin || agent.screenViewPos.x > flock.avoidingBoundaryMax)
		{
			avoiding = true;
			avoidMultiplier -= 0.5f;
		}

		if (agent.screenViewPos.y < flock.avoidingBoundaryMin || agent.screenViewPos.y > flock.avoidingBoundaryMax)
		{
			avoiding = true;
			avoidMultiplier -= 0.5f;
		}
		
		if (avoiding)
		{
			direction *= avoidMultiplier;
		}

		// direction = Vector2.SmoothDamp(
		// 		agent.transform.up, 
		// 		direction *= avoidMultiplier, 
		// 		ref agent.currentVelocity, 
		// 		agent.agentSmoothTime
		// 		);

		return direction;
	}

	public override Vector2 Wander(FlockAgent agent, Vector3 wanderStartingPos){
		return Vector2.zero;
	}
}
