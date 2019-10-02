using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

	public GameObject Inner, Outer;

	public Vector2 InnerMax, InnerMin, OuterMax, OuterMin, InnerDiff;

	private Vector3 DiffMax, DiffMin, InnerPos, OuterPos;

	public Vector2 innerCoords;

	public float outerXLength, outerYLength;


	

	// Use this for initialization
	
	void Awake()
	{
		InnerMax = Inner.GetComponent<BoxCollider2D>().bounds.max;
		InnerMin = Inner.GetComponent<BoxCollider2D>().bounds.min;
		OuterMax = Outer.GetComponent<BoxCollider2D>().bounds.max;
		OuterMin = Outer.GetComponent<BoxCollider2D>().bounds.min;

		
		// Get the length of x and y of outer
		outerXLength = OuterMax.x - OuterMin.x;
		outerYLength = OuterMax.y - OuterMin.y;

		InnerDiff = InnerMax;

		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		InnerMax = Inner.GetComponent<BoxCollider2D>().bounds.max;
		InnerMin = Inner.GetComponent<BoxCollider2D>().bounds.min;

		DiffMax = InnerMax - OuterMax;
		DiffMin = InnerMin - OuterMin;

		InnerPos = Inner.transform.position;

		// outerXLength = OuterMax.x - OuterMin.x;
		// outerYLength = OuterMax.y - OuterMin.y;

	}

	void LateUpdate()
	{
		if (InnerMax.x > OuterMax.x)
		{
			InnerPos.x -= DiffMax.x;
			Inner.transform.position = InnerPos;
		}
		if (InnerMax.y > OuterMax.y)
		{
			InnerPos.y -= DiffMax.y;
			Inner.transform.position = InnerPos;
		}

		if (InnerMin.x < OuterMin.x)
		{
			InnerPos.x -= DiffMin.x;
			Inner.transform.position = InnerPos;
		}
		if (InnerMin.y < OuterMin.y)
		{
			InnerPos.y -= DiffMin.y;
			Inner.transform.position = InnerPos;
		}

		innerCoords.x = Maths.scale(0,(outerXLength-(InnerDiff.x*2)),0,100,(InnerMin.x + Mathf.Abs(OuterMax.x)));
		innerCoords.y = Maths.scale(0,(outerYLength-(InnerDiff.y*2)),0,100,(InnerMin.y + Mathf.Abs(OuterMax.y)));
	}
}
