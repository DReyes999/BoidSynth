using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundsScript2 : MonoBehaviour {

	
	public Vector2 _BoundsMin;
	public Vector2 _BoundsMax;
	private BoxCollider2D _BoxCol;

	void Awake()
	{
		_BoxCol = GetComponent<BoxCollider2D>();

		
	}

	void Update()
	{
		_BoundsMin = _BoxCol.bounds.max;
		_BoundsMax = _BoxCol.bounds.min;
	}

	public Vector2 GetBoundsMin()
	{

		return _BoxCol.bounds.min;
	}

	public Vector2 GetBoundsMax()
	{

		return _BoxCol.bounds.max;
	}
}
