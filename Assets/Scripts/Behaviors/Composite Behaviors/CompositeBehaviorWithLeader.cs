using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior/Composite With Leader")]
public class CompositeBehaviorWithLeader : FlockBehaviorWithLeader {

	public FlockBehaviorWithLeader[] behaviors;
	public float[] weights;

	public override Vector2 CalculateLeadMove(
							FlockAgent agent, 
							List<Transform> context, 
							FlockWithLeader flock, 
							SingleAgentWander leader)
	{
// Check if the amount of weights and behaviors is the same
		if (weights.Length != behaviors.Length)
		{
			// Throw and error and don't move
			Debug.LogError("Data mismatch in " + name, this);
			return Vector2.zero;
		}

		// Set up Move
		Vector2 move = Vector2.zero;

		// Iterate through the behaviors
		for (int i = 0; i < behaviors.Length; i++)
		{
			Vector2 partialMove = behaviors[i].CalculateLeadMove(agent, context, flock, leader) * weights[i];

			// if movement is not zero
			if (partialMove != Vector2.zero)
			{
				if (partialMove.sqrMagnitude > weights[i] * weights[i])
				{
					// normalize to a magnitude of 1 and multiply it by the weights
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove;
			}
		}

		return move;
	}


}
