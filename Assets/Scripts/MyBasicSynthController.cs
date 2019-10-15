using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MyBasicSynthController : MonoBehaviour {

	// Use this for initialization
	public Hv_FishSynth_01_AudioLib synth;
	FlockAgent agent; 

	public float gainTranslated, freqTranslated;

	void awake()
	{

	}

	void Start () 
	{
		agent = this.GetComponentInParent<FlockAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LateUpdate()
	{
		
		freqTranslated = Maths.scale(0,1,100,2000,agent.screenViewPos.y);
		gainTranslated = agent.alphaAmount / 2;
		synth.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq, freqTranslated);
		synth.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Gain, gainTranslated);
	}

	public void SetFreq(float sliderValue)
	{
		/* Sets the frequency parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq, sliderValue);
	}

	public void SetGain(float sliderValue)
	{
		/* Sets the gain parameter of the synth to the incoming parameter */
        synth.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Gain, sliderValue);
	}

	public void TriggerNote(int osc)
	{
		switch (osc)
		{
			case 1:
				synth.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc1_trigger);
				//Debug.Log("Triggernote 1");
				break;
			case 2:
				synth.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc2_trigger);
				//Debug.Log("Triggernote 2");
				break;
			case 3:
				synth.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc3_trigger);
				//Debug.Log("Triggernote 3");
				break;
			case 4:
				synth.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc4_trigger);
				//Debug.Log("Triggernote 4");
				break;
			default:
				break;
		}
	}
}
