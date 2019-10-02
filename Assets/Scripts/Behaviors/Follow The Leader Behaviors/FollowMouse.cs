using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Follow Mouse")]
public class FollowMouse : FlockBehavior 
{

	//TODO: Set up variables to get the mouse position in the world
	public Vector2 mPos,
					mPosInWorld;

	public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		/* First let's face the agent towards the mouse */

		/*  subtract the position of agent from mouse position to get vector pointing from
		the agent to the mouse */

		Vector2 vectorToMouse = new Vector2(
			mousePosition.x - agent.transform.position.x,
			mousePosition.y - agent.transform.position.y
		);

		// set the direction variable to the vector we calculated.
		// Normalized gives the vector a constant length of 1

		// direction = vectorToMouse.normalized;

		// This actually faces the direction we set

		// transform.up = direction;

		//Draw a line to mouse position
		Debug.DrawLine(
			agent.transform.position,
			mousePosition,
			Color.white,
			0.0f,
			true
		);

		return vectorToMouse;
	}

	public override Vector2 Wander(FlockAgent agent, Vector3 wanderStartingPos)
	{
		return Vector2.zero;
	}

}
