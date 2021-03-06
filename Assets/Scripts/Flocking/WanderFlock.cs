﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFlock : MonoBehaviour {

	public Leader_Boidsynth07 agentPrefab;
	//public SingleAgentWander leaderPrefab;
	// List<FlockAgent> agents = new List<FlockAgent>();
	// public FlockBehavior behavior;
	[Range(1,20)]
	public int startingCount = 1;
	private double volumeScalar = 1;
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
				avoidMultiplierScalar = 0.9f,
				agentSmoothTime = 0.2f;

	float squareMaxSpeed, squareNeighborRadius, squareAvoidanceRadius;
	public float SquareAvoidanceRadius {get {return squareAvoidanceRadius;}}

	// Use this for initialization
	void Start () 
	{
		// squareMaxSpeed = maxSpeed * maxSpeed;
		// squareNeighborRadius = neighborRadius * neighborRadius;
		// squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
		volumeScalar = 1/startingCount;
		
		// Populate the scene with flock agents
		for (int i = 0; i < startingCount; i++)
		{
			Leader_Boidsynth07 newAgent = Instantiate(
				agentPrefab,
				Random.insideUnitCircle * 3,
				Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
				transform
			);
			newAgent.name = "Agent " + i;
			newAgent.GetComponent<AudioSource>().volume = 1.0f / startingCount; 
			
			// agents.Add(newAgent);
		}

		// foreach (FlockAgent agent in agents)
		// {
		// 	Debug.Log(1/startingCount);
		// 	agent.audioSource.volume = 1/startingCount;
		// }
	}
	
	void Update () 
	{
		
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
		Debug.Log("avoiding = false");
		float avoidMultiplier = 0.0f;

		if (agent.screenViewPos.x < avoidingBoundaryMin || agent.screenViewPos.x > avoidingBoundaryMax)
			agent.avoiding = true;
		
		if (agent.avoiding)
		{	Debug.Log(agent.name + " avoiding");
			move *= avoidMultiplier;
			agent.agentSmoothTime = agent.avoidingSmoothTime;
		
		}else
			agent.agentSmoothTime = agent.normalSmoothTime;

		// move = Vector2.SmoothDamp(
		// 		agent.transform.up, 
		// 		move, 
		// 		ref agent.currentVelocity, 
		// 		agent.agentSmoothTime
		// 		);

		return move;
	}
}
