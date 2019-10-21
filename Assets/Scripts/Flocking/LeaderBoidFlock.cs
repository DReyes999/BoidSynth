using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoidFlock : MonoBehaviour {

	public SingleAgentWander agentPrefab;
	//public SingleAgentWander leaderPrefab;
	List<FlockAgent> agents = new List<FlockAgent>();
	
	[Range(1,20)]
	public int startingCount = 1;
	const float agentDensity = 0.08f;

	// Use this for initialization
	void Start () 
	{



		// Populate the scene with flock agents
		for (int i = 0; i < startingCount; i++)
		{
			//Debug.Log("Instantiating Flock Agent");
			SingleAgentWander newAgent = Instantiate(
				agentPrefab,
				Random.insideUnitCircle * 3,
				Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
				transform
			);
			//newAgent.name = "Agent " + i;
			
			
			//agents.Add(newAgent);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

}
