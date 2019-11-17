﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior With Leader/Follow Leader")]
public class FollowLeader : FlockBehaviorWithLeader {

	//TODO: Set up variables to get the mouse position in the world
	//private GameObject leader;
	public Vector2 leaderPos;


	public override Vector2 CalculateLeadMove(FlockAgent agent, List<Transform> context, FlockWithLeader flock, Leader_Boidsynth07 leader)
	{
		if (leader != null)
		{
			leaderPos = leader.transform.position;
			/* First let's face the agent towards the mouse */

			/*  subtract the position of agent from mouse position to get vector pointing from
			the agent to the mouse */

			Vector2 vectorToLeader = new Vector2(
				leaderPos.x - agent.transform.position.x,
				leaderPos.y - agent.transform.position.y
			);

			agent.dist = vectorToLeader.magnitude;

			//Draw a line to mouse position
			Debug.DrawLine(
				agent.transform.position,
				leaderPos,
				Color.white,
				0.0f,
				true
			);

			return vectorToLeader;
		}else
		{
			Debug.Log("Leader not found");
			return Vector2.zero;
		}
	}
}
