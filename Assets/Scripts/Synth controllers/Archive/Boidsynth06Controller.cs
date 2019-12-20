using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boidsynth06Controller : MonoBehaviour {



	// Use this for initialization
	Hv_BoidSynth06_Sine_AudioLib synth;
	SingleAgentWander agent; 
	AngleDetection angles;
	
	[SerializeField]
	private int sector;
	

	[SerializeField]
	private float speed,speedClamped,
	freq, baseFreq, phase, phaseSmoothed,pan; 
	float yVel = 0.0f;
	double phaseRounded;

	[SerializeField]
	private bool reset = false,
	stoppedMoving = true;

	void awake()
	{

	}
	void Awake()
	{
		synth = this.GetComponentInParent<Hv_BoidSynth06_Sine_AudioLib>();
		agent = this.GetComponentInParent<SingleAgentWander>();
		angles = this.GetComponentInParent<AngleDetection>();
		if (synth != null)
		{
			Debug.Log("Synth Okay");
			NoteToggle();
			SetGainSmoothTime(500);
			SetAttackRelease(1000);
		}else{
			Debug.Log("Where synth?");
		}
		
	}

	void Start () 
	{
		
		baseFreq = 440;
		if (synth != null)
		{
			Debug.Log("Synth Okay");
			SetFreq(baseFreq);
		}else{
			Debug.Log("Where synth?");
		}
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = ExtensionMethods.Remap((float)agent.speed,0,2,0,1);
		if (speed < 0.1)
		{
			reset = false;
			if (!stoppedMoving)
			{
				NoteToggle();
			}
			stoppedMoving = true;
			SetFreq(freq);
			
		}else{
			if (stoppedMoving)
			{
				NoteToggle();
			}
			stoppedMoving = false;
			
		}
		//phaseRounded = System.Math.Round((double)angles.phase,3);
		//phaseSmoothed = Mathf.SmoothDamp((float)phaseRounded,angles.phase,ref yVel,0.001f);
		sector = angles.sector;
		pan = Mathf.Round(agent.screenViewPos.x * 100);
		//SetPhase(phaseSmoothed);
		
	}

	void LateUpdate()
	{
		freqAdjust(sector);
		SetGain(speed);
		SetPan(pan);
		
		// freqTranslated = Maths.scale(0,1,100,2000,agent.screenViewPos.y);
		// //gainTranslated = agent.alphaAmount / 2;
		// synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq, freqTranslated);
		// synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gain, gainTranslated);
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
			synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq, freq);
			reset = true;
		} 

	}

	public void SetGain(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gain, sliderValue);
	}

	public void SetGainSmoothTime(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gainsmoothtime, sliderValue);
	}

	public void SetAttackRelease(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Mstrattackrelease, sliderValue);
	}

	public void SetPhase(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Phase, sliderValue);
	}

	public void SetPan(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Pan, sliderValue);
	}

	public void NoteToggle()
	{
		synth.SendEvent(Hv_BoidSynth06_Sine_AudioLib.Event.Noteon_trigger);
	}

	public void freqAdjust(int sector)
	{
		switch (sector)
		{
			case 1:
				freq = baseFreq;
				break;
			case 2:
				freq = baseFreq * 2;
				break;
			case 3:
				freq = baseFreq / 2;
				break;
			case 4:
				freq = baseFreq * 3;
				break;
			default:
			break;
		}
	}


}

