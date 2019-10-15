using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockWithLeader : MonoBehaviour {



	public FlockAgent agentPrefab;
	public SingleAgentWander leaderPrefab;

	SingleAgentWander leader;
	List<FlockAgent> agents = new List<FlockAgent>();
	public FlockBehaviorWithLeader behavior;
	[Range(0,50)]
	public int startingCount = 0;
	const float agentDensity = 0.08f;
	[Range(1.0f, 100.0f)]
	public float driveFactor = 10f;
	[Range(1f,100f)]
	public float maxSpeed = 1f;
	[Range(1f, 10f)]
	public float neighborRadius = 1.5f;
	[Range(0f,1f)]
	public float avoidanceRadiusMultiplier = 0.5f,
				avoidingBoundaryMin = 0.1f,
				avoidingBoundaryMax = 0.9f,
				avoidMultiplierScalar = 0.9f;

	float squareMaxSpeed, squareNeighborRadius, squareAvoidanceRadius;
	public float SquareAvoidanceRadius {get {return squareAvoidanceRadius;}}

	// Use this for initialization
	void Start () 
	{
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighborRadius = neighborRadius * neighborRadius;
		squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
		
		//Imstantiate the leader
		leader = Instantiate(
			leaderPrefab,
			Random.insideUnitCircle * 3,
			Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
			transform
		);

		// Populate the scene with flock agents
		for (int i = 0; i < startingCount; i++)
		{
			Debug.Log("Instantiating Flock Agent");
			FlockAgent newAgent = Instantiate(
				agentPrefab,
				Random.insideUnitCircle * 3,
				Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
				transform
			);
			newAgent.name = "Agent " + i;
			
			
			agents.Add(newAgent);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (FlockAgent agent in agents)
		{
			List<Transform> context = GetNearbyAgents(agent);
			
			//FOR DEMO
			//agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

			// this is where we calculate in which way the flock agent will move
			Vector2 move = behavior.CalculateLeadMove(agent, context, this, leader);
			
			// Moves the agent at the speed we want
			//move *= driveFactor;
			
			//check if we have exceeded our max speed
			if (move.sqrMagnitude > squareMaxSpeed)
			{
				move = move.normalized * maxSpeed;
			}

			
			move = Vector2.SmoothDamp(
				agent.transform.up, 
				move, 
				ref agent.currentVelocity, 
				agent.agentSmoothTime
				);

			//Move the flock agent
			agent.Move(move);
		}	
	}

	List<Transform> GetNearbyAgents(FlockAgent agent)
	{
		List<Transform> context = new List<Transform>();
		
		// Create an array of colliders by getting all of the colliders in a surrounding radius
		Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
		
		foreach (Collider2D c in contextColliders)
		{
			if (c != agent.agentCollider)
			{
				context.Add(c.transform);
			}
		}
		return context;
	}

	Vector2 Sensors(Vector2 move, FlockAgent agent)
	{
		agent.avoiding = false;
		float avoidMultiplier = 0.0f;
		int directionPicker;
		//Debug.Log(agent.name + " screenviewpos = " + agent.screenViewPos);


		if (agent.screenViewPos.x < avoidingBoundaryMin || agent.screenViewPos.x > avoidingBoundaryMax)
		{
			agent.avoiding = true;
			directionPicker = Random.Range(1,2);
			switch(directionPicker)
			{
				case 1:
					avoidMultiplier -= avoidMultiplierScalar;
					break;
				case 2:
					avoidMultiplier += avoidMultiplierScalar;
					break;
				default:
					break;
			}
		}

		if (agent.screenViewPos.y < avoidingBoundaryMin || agent.screenViewPos.y > avoidingBoundaryMax)
		{
			agent.avoiding = true;
			directionPicker = Random.Range(1,2);
			switch(directionPicker)
			{
				case 1:
					avoidMultiplier -= avoidMultiplierScalar;
					break;
				case 2:
					avoidMultiplier += avoidMultiplierScalar;
					break;
				default:
					break;
			}
		}
		
		if (agent.avoiding)
		{	Debug.Log(agent.name + " avoiding");
			move *= avoidMultiplier;
			agent.agentSmoothTime = agent.avoidingSmoothTime;
		
		}else
			agent.agentSmoothTime = agent.normalSmoothTime;

		move = Vector2.SmoothDamp(
				agent.transform.up, 
				move, 
				ref agent.currentVelocity, 
				agent.agentSmoothTime
				);

		return move;
	}
}
