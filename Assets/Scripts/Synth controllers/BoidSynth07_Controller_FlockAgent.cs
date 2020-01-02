using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSynth07_Controller_FlockAgent : MonoBehaviour {

	// Use this for initialization
	Hv_Boidsynth07_AudioLib synth;
	FlockAgent  agent; 
	AngleDetection angles;

	public string scaleType;

	private int[] scaleLow = new int[] {50,52,54,55,57,59,61,62},
	scaleMed = new int[] {62,64,66,67,69,71,73,74},
	scaleHigh = new int[] {74,76,78,79,81,83,85,86},
	scale = new int[8];
	
	[SerializeField]
	private int sector, tempSector = 1;

	[SerializeField]
	private float speed,speedClamped,
	freq, baseFreq, pan, spread, harmonics; 
	float yVel = 0.0f;

	[SerializeField]
	private bool reset = false,
	stoppedMoving = true;

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

		PickScale();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Remap speed from 0-2 to 0-1
		// speed = ExtensionMethods.Remap((float)agent.speed,0,2,0,1);
		// spread = ExtensionMethods.Remap((float)agent.speed, 0,2,0,10);
		// harmonics = ExtensionMethods.Remap((float)agent.speed,0,2,0,3);
		speed = agent.maxSpeed;
		spread = agent.maxSpeed;
		harmonics = agent.maxSpeed;

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
		//set the freq
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

	public void PickScale()
	{
		int scalePicker = Random.Range(1,4);
		switch (scalePicker)
		{
			case 1:
				scale = scaleLow;
				scaleType = "Low";
				break;
			case 2:
				scale = scaleMed;
				scaleType = "Med";
				break;
			case 3:
				scale = scaleHigh;
				scaleType = "High";
				break;
			default:
				scale = scaleMed;
				scaleType = "Med";
				break;
		}
	}

	public void freqAdjust(int sector)
	{
		/** 
		 * This function changes the frequency depending on which part
		 * of a 360 degree angle the boid is facing
		 */
		
		/**The following code will randomize the frequency **/
		/** TODO: Create a 2D array with multiple flocks. each flock takes a part in a 
		 * four part harmony */

		switch (agent.noteMode)
		{
			case 1:
				if (sector == tempSector)
				{
					Debug.Log("nochange");
				}else
				{
					Debug.Log("change!");
					freq = scale[Random.Range(0,scale.Length-1)];
					Debug.Log("freq is now " + freq);
					tempSector = sector;
				}
				break;
			case 2:
				switch (sector)
				{
					case 1:
						freq = scale[7];
						break;
					case 2:
						freq = scale[6];
						break;
					case 3:
						freq = scale[5];
						break;
					case 4:
						freq = scale[4];
						break;
					case 5:
						freq = scale[3];
						break;
					case 6:
						freq = scale[2];
						break;
					case 7:
						freq = scale[1];
						break;
					case 8:
						freq = scale[0];
						break;
					default:
					break;
				}
				break;
			default:
				break;
		}
	}
}

