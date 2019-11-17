using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviorWithLeader : ScriptableObject{

	public abstract Vector2 CalculateLeadMove(
							FlockAgent agent, 
							List<Transform> context, 
							FlockWithLeader flock, 
							Leader_Boidsynth07 leader);
}
