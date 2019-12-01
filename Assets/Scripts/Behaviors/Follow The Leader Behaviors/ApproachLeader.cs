using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior With Leader/Approach Leader")]
public class ApproachLeader : FlockBehaviorWithLeader 
{
public override Vector2 CalculateLeadMove(FlockAgent_Follower agent, List<Transform> context, FlockWithLeader flock, Leader_Boidsynth07 leader)
	{
		if (leader != null)
		{
			Vector2 leaderPos = leader.transform.position;
			/* First let's face the agent towards the mouse */

			/*  subtract the position of agent from mouse position to get vector pointing from
			the agent to the mouse */

			Vector2 vectorToLeader = new Vector2(
				leaderPos.x - agent.transform.position.x,
				leaderPos.y - agent.transform.position.y
			);

			

			return vectorToLeader;
		}else
		{
			Debug.Log("Leader not found");
			return Vector2.zero;
		}
	}
}
