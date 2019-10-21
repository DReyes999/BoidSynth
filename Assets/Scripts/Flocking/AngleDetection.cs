using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDetection : MonoBehaviour {

	//public EventScript eventScript = Find

	Quaternion q;
	Vector3 vector;
	public int sector {get; private set;}
	public float fadeTime = 0.1f,
	phase = 0;
	Boidsynth06Controller synthController;

	void Awake()
	{
		
	}

	void Start () 
	{
		synthController = GetComponent<Boidsynth06Controller>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		q = transform.rotation;
		vector = q.eulerAngles;
		phase = ExtensionMethods.Remap(vector.z,0,360,0,1);


		if (vector.z >= 0 && vector.z < 90)
		{
			this.sector = 1;
			//synthController.freq /= 2;
		}else if (vector.z >= 90 && vector.z < 180)
		{
			this.sector = 2;
			//synthController.freq *= 2;
		}else if (vector.z >= 180 && vector.z < 270)
		{
			this.sector = 3;
			//synthController.freq *= 3;
		}else if (vector.z >= 270 && vector.z < 360)
		{
			this.sector = 4;
			//synthController.freq /= 3;
		}
	}
}
