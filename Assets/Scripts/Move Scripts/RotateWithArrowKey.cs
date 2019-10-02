using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithArrowKey : MonoBehaviour {

	// Use this for initialization
	public float speed = 50.0f;
	void Awake () 
	{
		speed = 100.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var move = new Vector3(0, Input.GetAxis("Vertical"), 0);
        transform.position += move * 5 * Time.deltaTime;
		transform.Rotate(0, 0, -Input.GetAxis("Horizontal")*speed*Time.deltaTime);
		
        
	}
}
