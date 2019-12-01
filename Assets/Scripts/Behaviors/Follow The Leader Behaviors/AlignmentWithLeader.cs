using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior With Leader/Alignment With Leader")]
public class AlignmentWithLeader : FlockBehaviorWithLeader 
{
	public override Vector2 CalculateLeadMove(FlockAgent_Follower agent, 
											List<Transform> context, 
											FlockWithLeader flock,
											Leader_Boidsynth07 leader)
	{
		/* If no neighbors maintain current alignment (heading)
		In this case, if no neighbors are found, the agent will move forward
		on its current heading. */
		if (leader != null)
		{
			return Vector2.zero;
		}

		// get the average alignment of neighbors and face that direction
		Vector2 alignmentMove = Vector2.zero;

		foreach (Transform item in context)
		{
			alignmentMove += (Vector2)item.transform.up;
		}
		alignmentMove /= context.Count;

		// alignmentMove = Vector2.SmoothDamp(
		// 		agent.transform.up, 
		// 		alignmentMove, 
		// 		ref agent.currentVelocity, 
		// 		agent.agentSmoothTime
		// 		);

		return alignmentMove;
		//return Seek(agent, agent.transform.position,alignmentMove);
	}
	
}
