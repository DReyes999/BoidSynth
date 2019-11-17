using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDetection : MonoBehaviour {

	//public EventScript eventScript = Find

	Quaternion q;
	Vector3 vector;
	public int sector {get; private set;}

	// Update is called once per frame
	void Update () 
	{
		GetAngle();
	}

	void GetAngle()
	{
		q = transform.rotation;
		vector = q.eulerAngles;
		
		if (vector.z >= 0 && vector.z < 45)
		{
			this.sector = 1;
			//synthController.freq /= 2;
		}else if (vector.z >= 45 && vector.z < 90)
		{
			this.sector = 2;
			//synthController.freq *= 2;
		}else if (vector.z >= 90 && vector.z < 135)
		{
			this.sector = 3;
			//synthController.freq *= 3;
		}else if (vector.z >= 135 && vector.z < 180)
		{
			this.sector = 4;
			//synthController.freq /= 3;
		}else if (vector.z >= 180 && vector.z < 225)
		{
			this.sector = 5;
			//synthController.freq /= 3;
		}else if (vector.z >= 225 && vector.z < 270)
		{
			this.sector = 6;
			//synthController.freq /= 3;
		}else if (vector.z >= 270 && vector.z < 315)
		{
			this.sector = 7;
			//synthController.freq /= 3;
		}else if (vector.z >= 315 && vector.z < 360)
		{
			this.sector = 8;
			//synthController.freq /= 3;
		}
	}
}
