using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
	public FlockBehavior[] behaviors;
	public float[] weights;

	public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
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
			Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

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

	public override Vector2 Wander(FlockAgent agent, Vector3 wanderStartingPos){
		return Vector2.zero;
	}
}
