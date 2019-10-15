using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class FlockAgentRotationDetection : MonoBehaviour {

	
	//public EventScript eventScript = Find

	Quaternion q;
	Vector3 vector;
	int sector;
	public SpriteRenderer sprite;
	public Color spriteColor;

	public float fadeTime = 0.1f;
	MyBasicSynthController synthController;

	private bool sec1Played = false,
				sec2Played = false,
				sec3Played = false,
				sec4Played = false;
	void Awake()
	{
		
	}

	void Start () 
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
		synthController = GetComponent<MyBasicSynthController>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		q = transform.rotation;
		vector = q.eulerAngles;

		spriteColor = sprite.color;

		if (vector.z >= 0 && vector.z < 90)
		{
			sector = 1;
		}else if (vector.z >= 90 && vector.z < 180)
		{
			sector = 2;
		}else if (vector.z >= 180 && vector.z < 270)
		{
			sector = 3;
		}else if (vector.z >= 270 && vector.z < 360)
		{
			sector = 4;
		}

		TriggerNotesAnims(sector);

		
	}

	public void TriggerNotesAnims(int sector)
	{
		switch (sector)
		{
			case 1:
				sprite.color = CrossFadeColors(Color.white);
				if (!sec1Played)
				{
					synthController.TriggerNote(1);
					sec1Played = true;
					sec2Played = false;
					sec3Played = false;
					sec4Played = false;
					
				}else
				{
					return;
				}
				break;
			case 2:
				sprite.color = CrossFadeColors(Color.yellow);
				if (!sec2Played)
				{
					synthController.TriggerNote(2);
					sec2Played = true;
					sec3Played = false;
					sec4Played = false;
					sec1Played = false;
				}else
				{
					return;
				}
				break;
				
			case 3:
				sprite.color = CrossFadeColors(Color.magenta);
				if (!sec3Played)
				{
					synthController.TriggerNote(3);
					sec3Played = true;
					sec4Played = false;
					sec2Played = false;
					sec1Played = false;
				}else
				{
					return;
				}
				break;
			case 4:
				sprite.color = CrossFadeColors(Color.green);
				if (!sec4Played)
				{
					synthController.TriggerNote(4);
					sec4Played = true;
					sec3Played = false;
					sec2Played = false;
					sec1Played = false;
				}else
				{
					return;
				}
				break;

			default:
				break;
		}

		
	}

	public Color CrossFadeColors(Color targetColor)
	{
		Color newColor;
		newColor = Color.Lerp(spriteColor,targetColor,fadeTime);
		return newColor;
	}
}
