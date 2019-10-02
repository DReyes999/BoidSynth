using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject 
{

	public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
	public abstract Vector2 Wander(FlockAgent agent, Vector3 wanderStartingPos);
	
	
}
