using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boidsynth07Controller_Follower : MonoBehaviour {

	// Use this for initialization
	Hv_Boidsynth07_AudioLib synth;
	FlockAgent agent; 
	AngleDetection angles;
	
	[SerializeField]
	private int sector;

	[SerializeField]
	private float speed,speedClamped,
	freq, baseFreq, pan, spread, harmonics; 
	float yVel = 0.0f;

	[SerializeField]
	private bool reset = false,
	stoppedMoving = true;

	void awake()
	{

	}
	void Awake()
	{
		synth = this.GetComponentInParent<Hv_Boidsynth07_AudioLib>();
		agent = this.GetComponentInParent<FlockAgent>();
		angles = this.GetComponentInParent<AngleDetection>();
		
	}

	void Start () 
	{
		
		baseFreq = 69;
		freq = baseFreq;
		SetFreq(freq);

		if (synth != null)
		{
			SetGainSmoothTime(500);
			SetAttackRelease(1000);
			NoteToggle();
		}else{
			Debug.Log("Where synth?");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Remap speed from 0-2 to 0-1
		speed = ExtensionMethods.Remap((float)agent.speed,0,2,0,1);
		spread = ExtensionMethods.Remap((float)agent.speed, 0,2,0,10);
		harmonics = ExtensionMethods.Remap((float)agent.speed,0,2,0,3);

		if (speed < 0.1)
		{
			reset = false;
			if (!stoppedMoving)
			{
				//Toggle the synth off when the boid has stopped moving
				NoteToggle();
			}
			stoppedMoving = true;
			// When the boid has stopped moving, set the frequency to 'freq'
			//SetFreq(freq);
			
		}else
		{
			if (stoppedMoving)
			{
				NoteToggle();
			}
			stoppedMoving = false;
		}
		
		sector = angles.sector;
		pan = Mathf.Round(agent.screenViewPos.x * 100);	
	}

	void LateUpdate()
	{
		//Set the frequency variable to a frequency based on sector
		freqAdjust(sector);
		//Adjust the gain according to our speed
		SetGain(speed);
		//Adjust the pan according to our screen view position
		SetPan(pan);
		//Adjust the spread according to speed
		SetSpread(spread);
		//Add harmonics according to speed
		SetHarmonics(harmonics);
		synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Frequency, freq);

	}

	public void SetFreq(float freq)
	{
		StartCoroutine(eSetFreq());
	}

	IEnumerator eSetFreq()
	{
		yield return new WaitForSeconds(0.5f);

		if (!reset)
		{
			/* Sets the frequency parameter of the synth to the incoming parameter */
			synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Frequency, freq);
			reset = true;
		} 

	}

	public void SetGain(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Gain, sliderValue);
	}

	public void SetSpread(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Spread, sliderValue);
	}

	public void SetHarmonics(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Harmonics, sliderValue);
	}

	public void SetGainSmoothTime(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Gainsmoothtime, sliderValue);
	}

	public void SetAttackRelease(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Mstrattackrelease, sliderValue);
	}

	public void SetPhase(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        //synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Phase, sliderValue);
	}

	public void SetPan(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_Boidsynth07_AudioLib.Parameter.Pan, sliderValue);
	}

	public void NoteToggle()
	{
		synth.SendEvent(Hv_Boidsynth07_AudioLib.Event.Noteon_trigger);
	}

	public void freqAdjust(int sector)
	{
		/** 
		 * This function changes the frequency depending on which part
		 * of a 360 degree angle the boid is facing
		 */

		switch (sector)
		{
			case 1:
				freq = 74;
				break;
			case 2:
				freq = 73;
				break;
			case 3:
				freq = 71;
				break;
			case 4:
				freq = 69;
				break;
			case 5:
				freq = 67;
				break;
			case 6:
				freq = 66;
				break;
			case 7:
				freq = 64;
				break;
			case 8:
				freq = 62;
				break;
			default:
			break;
		}
	}


}

