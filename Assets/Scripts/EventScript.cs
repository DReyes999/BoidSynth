using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class _UnityEventFloat:UnityEvent<float> {}

public class EventScript : MonoBehaviour 
{
	public static EventScript Instance {get; private set;} 

	[Header("Here is a cool event. Drag anything here")]
	public UnityEvent inSector1;
	public UnityEvent inSector2;
	public UnityEvent inSector3;
	public UnityEvent inSector4;

	public _UnityEventFloat UpdateAngle;

	void Awake()
	{
		if(Instance != null)
             GameObject.Destroy(Instance);
         else
             Instance = this;
         
        
		DontDestroyOnLoad(this);
	}

	
}
