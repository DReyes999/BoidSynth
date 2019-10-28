using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {

	public GameObject _samplecubePrefab;
	GameObject[] _sampleCube = new GameObject[512];
	public float _maxScale;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 512; i++)
		{
			GameObject _instanceSampleCube = (GameObject)Instantiate(_samplecubePrefab);
			_instanceSampleCube.transform.position = this.transform.position;
			_instanceSampleCube.transform.parent = this.transform;
			_instanceSampleCube.name = "SampleCube " + i;

			//Position the sample cubes in a circle around the parent object
			this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
			_instanceSampleCube.transform.position = Vector3.forward * 100;

			//Set this sample cube into our array
			_sampleCube[i] = _instanceSampleCube;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//Set the sample cube scale to the samples in audio peer
		for (int i = 0; i < 512; i++)
		{
			if (_sampleCube != null)
			{
				_sampleCube[i].transform.localScale = new Vector3(10,(AudioPeer._samples[i]*_maxScale)+2,10);
			}
			
		}
		
	}
}
