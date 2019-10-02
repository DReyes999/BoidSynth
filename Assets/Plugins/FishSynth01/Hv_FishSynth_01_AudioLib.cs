/**
 * Copyright (c) 2019 Enzien Audio, Ltd.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions, and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the phrase "powered by heavy",
 *    the heavy logo, and a hyperlink to https://enzienaudio.com, all in a visible
 *    form.
 * 
 *   2.1 If the Application is distributed in a store system (for example,
 *       the Apple "App Store" or "Google Play"), the phrase "powered by heavy"
 *       shall be included in the app description or the copyright text as well as
 *       the in the app itself. The heavy logo will shall be visible in the app
 *       itself as well.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using AOT;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Hv_FishSynth_01_AudioLib))]
public class Hv_FishSynth_01_Editor : Editor {

  [MenuItem("Heavy/FishSynth_01")]
  static void CreateHv_FishSynth_01() {
    GameObject target = Selection.activeGameObject;
    if (target != null) {
      target.AddComponent<Hv_FishSynth_01_AudioLib>();
    }
  }

  private Hv_FishSynth_01_AudioLib _dsp;

  private void OnEnable() {
    _dsp = target as Hv_FishSynth_01_AudioLib;
  }

  public override void OnInspectorGUI() {
    bool isEnabled = _dsp.IsInstantiated();
    if (!isEnabled) {
      EditorGUILayout.LabelField("Press Play!",  EditorStyles.centeredGreyMiniLabel);
    }
    GUILayout.BeginVertical();
    // EVENTS
    GUI.enabled = isEnabled;
    EditorGUILayout.Space();

    // osc1_trigger
    if (GUILayout.Button("osc1_trigger")) {
      _dsp.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc1_trigger);
    }

    // osc2_trigger
    if (GUILayout.Button("osc2_trigger")) {
      _dsp.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc2_trigger);
    }

    // osc3_trigger
    if (GUILayout.Button("osc3_trigger")) {
      _dsp.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc3_trigger);
    }

    // osc4_trigger
    if (GUILayout.Button("osc4_trigger")) {
      _dsp.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc4_trigger);
    }
    // PARAMETERS
    GUI.enabled = true;
    EditorGUILayout.Space();
    EditorGUI.indentLevel++;

    // freq
    GUILayout.BeginHorizontal();
    float freq = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq);
    float newFreq = EditorGUILayout.Slider("freq", freq, 100.0f, 2000.0f);
    if (freq != newFreq) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq, newFreq);
    }
    GUILayout.EndHorizontal();

    // gain
    GUILayout.BeginHorizontal();
    float gain = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Gain);
    float newGain = EditorGUILayout.Slider("gain", gain, 0.0f, 1.0f);
    if (gain != newGain) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Gain, newGain);
    }
    GUILayout.EndHorizontal();

    // osc1Gain
    GUILayout.BeginHorizontal();
    float osc1Gain = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1gain);
    float newOsc1gain = EditorGUILayout.Slider("osc1Gain", osc1Gain, 0.0f, 1.0f);
    if (osc1Gain != newOsc1gain) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1gain, newOsc1gain);
    }
    GUILayout.EndHorizontal();

    // osc1attack
    GUILayout.BeginHorizontal();
    float osc1attack = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1attack);
    float newOsc1attack = EditorGUILayout.Slider("osc1attack", osc1attack, 1.0f, 50.0f);
    if (osc1attack != newOsc1attack) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1attack, newOsc1attack);
    }
    GUILayout.EndHorizontal();

    // osc1releasetime
    GUILayout.BeginHorizontal();
    float osc1releasetime = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1releasetime);
    float newOsc1releasetime = EditorGUILayout.Slider("osc1releasetime", osc1releasetime, 100.0f, 5000.0f);
    if (osc1releasetime != newOsc1releasetime) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc1releasetime, newOsc1releasetime);
    }
    GUILayout.EndHorizontal();

    // osc2Gain
    GUILayout.BeginHorizontal();
    float osc2Gain = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2gain);
    float newOsc2gain = EditorGUILayout.Slider("osc2Gain", osc2Gain, 0.0f, 1.0f);
    if (osc2Gain != newOsc2gain) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2gain, newOsc2gain);
    }
    GUILayout.EndHorizontal();

    // osc2attack
    GUILayout.BeginHorizontal();
    float osc2attack = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2attack);
    float newOsc2attack = EditorGUILayout.Slider("osc2attack", osc2attack, 1.0f, 50.0f);
    if (osc2attack != newOsc2attack) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2attack, newOsc2attack);
    }
    GUILayout.EndHorizontal();

    // osc2releasetime
    GUILayout.BeginHorizontal();
    float osc2releasetime = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2releasetime);
    float newOsc2releasetime = EditorGUILayout.Slider("osc2releasetime", osc2releasetime, 100.0f, 5000.0f);
    if (osc2releasetime != newOsc2releasetime) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc2releasetime, newOsc2releasetime);
    }
    GUILayout.EndHorizontal();

    // osc3Gain
    GUILayout.BeginHorizontal();
    float osc3Gain = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3gain);
    float newOsc3gain = EditorGUILayout.Slider("osc3Gain", osc3Gain, 0.0f, 1.0f);
    if (osc3Gain != newOsc3gain) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3gain, newOsc3gain);
    }
    GUILayout.EndHorizontal();

    // osc3attack
    GUILayout.BeginHorizontal();
    float osc3attack = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3attack);
    float newOsc3attack = EditorGUILayout.Slider("osc3attack", osc3attack, 1.0f, 50.0f);
    if (osc3attack != newOsc3attack) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3attack, newOsc3attack);
    }
    GUILayout.EndHorizontal();

    // osc3releasetime
    GUILayout.BeginHorizontal();
    float osc3releasetime = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3releasetime);
    float newOsc3releasetime = EditorGUILayout.Slider("osc3releasetime", osc3releasetime, 100.0f, 5000.0f);
    if (osc3releasetime != newOsc3releasetime) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc3releasetime, newOsc3releasetime);
    }
    GUILayout.EndHorizontal();

    // osc4Gain
    GUILayout.BeginHorizontal();
    float osc4Gain = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4gain);
    float newOsc4gain = EditorGUILayout.Slider("osc4Gain", osc4Gain, 0.0f, 1.0f);
    if (osc4Gain != newOsc4gain) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4gain, newOsc4gain);
    }
    GUILayout.EndHorizontal();

    // osc4attack
    GUILayout.BeginHorizontal();
    float osc4attack = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4attack);
    float newOsc4attack = EditorGUILayout.Slider("osc4attack", osc4attack, 1.0f, 50.0f);
    if (osc4attack != newOsc4attack) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4attack, newOsc4attack);
    }
    GUILayout.EndHorizontal();

    // osc4releasetime
    GUILayout.BeginHorizontal();
    float osc4releasetime = _dsp.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4releasetime);
    float newOsc4releasetime = EditorGUILayout.Slider("osc4releasetime", osc4releasetime, 100.0f, 5000.0f);
    if (osc4releasetime != newOsc4releasetime) {
      _dsp.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Osc4releasetime, newOsc4releasetime);
    }
    GUILayout.EndHorizontal();

    EditorGUI.indentLevel--;

    

    GUILayout.EndVertical();
  }
}
#endif // UNITY_EDITOR

[RequireComponent (typeof (AudioSource))]
public class Hv_FishSynth_01_AudioLib : MonoBehaviour {
  
  // Events are used to trigger bangs in the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_FishSynth_01_AudioLib script = GetComponent<Hv_FishSynth_01_AudioLib>();
        script.SendEvent(Hv_FishSynth_01_AudioLib.Event.Osc1_trigger);
    }
  */
  public enum Event : uint {
    Osc1_trigger = 0xC2E1350E,
    Osc2_trigger = 0x3A3A1321,
    Osc3_trigger = 0x25AC120E,
    Osc4_trigger = 0x251DBD70,
  }
  
  // Parameters are used to send float messages into the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_FishSynth_01_AudioLib script = GetComponent<Hv_FishSynth_01_AudioLib>();
        // Get and set a parameter
        float freq = script.GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq);
        script.SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter.Freq, freq + 0.1f);
    }
  */
  public enum Parameter : uint {
    Freq = 0x345FC008,
    Gain = 0x811CC33F,
    Osc1gain = 0xEA77904,
    Osc1attack = 0x4F17611,
    Osc1releasetime = 0x7A1341F4,
    Osc2gain = 0x1372FF97,
    Osc2attack = 0x31E9CDBD,
    Osc2releasetime = 0xE724BB24,
    Osc3gain = 0xDA4EF544,
    Osc3attack = 0x31F84A4F,
    Osc3releasetime = 0x7EF352F8,
    Osc4gain = 0xF3B29D6B,
    Osc4attack = 0x1AA0E5E7,
    Osc4releasetime = 0x1724CF1D,
  }
  
  // Delegate method for receiving float messages from the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_FishSynth_01_AudioLib script = GetComponent<Hv_FishSynth_01_AudioLib>();
        script.RegisterSendHook();
        script.FloatReceivedCallback += OnFloatMessage;
    }

    void OnFloatMessage(Hv_FishSynth_01_AudioLib.FloatMessage message) {
        Debug.Log(message.receiverName + ": " + message.value);
    }
  */
  public class FloatMessage {
    public string receiverName;
    public float value;

    public FloatMessage(string name, float x) {
      receiverName = name;
      value = x;
    }
  }
  public delegate void FloatMessageReceived(FloatMessage message);
  public FloatMessageReceived FloatReceivedCallback;
  public float freq = 440.0f;
  public float gain = 0.5f;
  public float osc1Gain = 0.5f;
  public float osc1attack = 10.0f;
  public float osc1releasetime = 500.0f;
  public float osc2Gain = 0.5f;
  public float osc2attack = 10.0f;
  public float osc2releasetime = 500.0f;
  public float osc3Gain = 0.5f;
  public float osc3attack = 10.0f;
  public float osc3releasetime = 500.0f;
  public float osc4Gain = 0.5f;
  public float osc4attack = 10.0f;
  public float osc4releasetime = 500.0f;

  // internal state
  private Hv_FishSynth_01_Context _context;

  public bool IsInstantiated() {
    return (_context != null);
  }

  public void RegisterSendHook() {
    _context.RegisterSendHook();
  }
  
  // see Hv_FishSynth_01_AudioLib.Event for definitions
  public void SendEvent(Hv_FishSynth_01_AudioLib.Event e) {
    if (IsInstantiated()) _context.SendBangToReceiver((uint) e);
  }
  
  // see Hv_FishSynth_01_AudioLib.Parameter for definitions
  public float GetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter param) {
    switch (param) {
      case Parameter.Freq: return freq;
      case Parameter.Gain: return gain;
      case Parameter.Osc1gain: return osc1Gain;
      case Parameter.Osc1attack: return osc1attack;
      case Parameter.Osc1releasetime: return osc1releasetime;
      case Parameter.Osc2gain: return osc2Gain;
      case Parameter.Osc2attack: return osc2attack;
      case Parameter.Osc2releasetime: return osc2releasetime;
      case Parameter.Osc3gain: return osc3Gain;
      case Parameter.Osc3attack: return osc3attack;
      case Parameter.Osc3releasetime: return osc3releasetime;
      case Parameter.Osc4gain: return osc4Gain;
      case Parameter.Osc4attack: return osc4attack;
      case Parameter.Osc4releasetime: return osc4releasetime;
      default: return 0.0f;
    }
  }

  public void SetFloatParameter(Hv_FishSynth_01_AudioLib.Parameter param, float x) {
    switch (param) {
      case Parameter.Freq: {
        x = Mathf.Clamp(x, 100.0f, 2000.0f);
        freq = x;
        break;
      }
      case Parameter.Gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        gain = x;
        break;
      }
      case Parameter.Osc1gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        osc1Gain = x;
        break;
      }
      case Parameter.Osc1attack: {
        x = Mathf.Clamp(x, 1.0f, 50.0f);
        osc1attack = x;
        break;
      }
      case Parameter.Osc1releasetime: {
        x = Mathf.Clamp(x, 100.0f, 5000.0f);
        osc1releasetime = x;
        break;
      }
      case Parameter.Osc2gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        osc2Gain = x;
        break;
      }
      case Parameter.Osc2attack: {
        x = Mathf.Clamp(x, 1.0f, 50.0f);
        osc2attack = x;
        break;
      }
      case Parameter.Osc2releasetime: {
        x = Mathf.Clamp(x, 100.0f, 5000.0f);
        osc2releasetime = x;
        break;
      }
      case Parameter.Osc3gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        osc3Gain = x;
        break;
      }
      case Parameter.Osc3attack: {
        x = Mathf.Clamp(x, 1.0f, 50.0f);
        osc3attack = x;
        break;
      }
      case Parameter.Osc3releasetime: {
        x = Mathf.Clamp(x, 100.0f, 5000.0f);
        osc3releasetime = x;
        break;
      }
      case Parameter.Osc4gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        osc4Gain = x;
        break;
      }
      case Parameter.Osc4attack: {
        x = Mathf.Clamp(x, 1.0f, 50.0f);
        osc4attack = x;
        break;
      }
      case Parameter.Osc4releasetime: {
        x = Mathf.Clamp(x, 100.0f, 5000.0f);
        osc4releasetime = x;
        break;
      }
      default: return;
    }
    if (IsInstantiated()) _context.SendFloatToReceiver((uint) param, x);
  }
  
  public void SendFloatToReceiver(string receiverName, float x) {
    _context.SendFloatToReceiver(StringToHash(receiverName), x);
  }

  public void FillTableWithMonoAudioClip(string tableName, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_FishSynth_01_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableName + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(StringToHash(tableName), buffer);
  }

  public void FillTableWithMonoAudioClip(uint tableHash, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_FishSynth_01_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableHash + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(tableHash, buffer);
  }

  public void FillTableWithFloatBuffer(string tableName, float[] buffer) {
    _context.FillTableWithFloatBuffer(StringToHash(tableName), buffer);
  }

  public void FillTableWithFloatBuffer(uint tableHash, float[] buffer) {
    _context.FillTableWithFloatBuffer(tableHash, buffer);
  }

  public uint StringToHash(string str) {
    return _context.StringToHash(str);
  }

  private void Awake() {
    _context = new Hv_FishSynth_01_Context((double) AudioSettings.outputSampleRate);
    
  }
  
  private void Start() {
    _context.SendFloatToReceiver((uint) Parameter.Freq, freq);
    _context.SendFloatToReceiver((uint) Parameter.Gain, gain);
    _context.SendFloatToReceiver((uint) Parameter.Osc1gain, osc1Gain);
    _context.SendFloatToReceiver((uint) Parameter.Osc1attack, osc1attack);
    _context.SendFloatToReceiver((uint) Parameter.Osc1releasetime, osc1releasetime);
    _context.SendFloatToReceiver((uint) Parameter.Osc2gain, osc2Gain);
    _context.SendFloatToReceiver((uint) Parameter.Osc2attack, osc2attack);
    _context.SendFloatToReceiver((uint) Parameter.Osc2releasetime, osc2releasetime);
    _context.SendFloatToReceiver((uint) Parameter.Osc3gain, osc3Gain);
    _context.SendFloatToReceiver((uint) Parameter.Osc3attack, osc3attack);
    _context.SendFloatToReceiver((uint) Parameter.Osc3releasetime, osc3releasetime);
    _context.SendFloatToReceiver((uint) Parameter.Osc4gain, osc4Gain);
    _context.SendFloatToReceiver((uint) Parameter.Osc4attack, osc4attack);
    _context.SendFloatToReceiver((uint) Parameter.Osc4releasetime, osc4releasetime);
  }
  
  private void Update() {
    // retreive sent messages
    if (_context.IsSendHookRegistered()) {
      Hv_FishSynth_01_AudioLib.FloatMessage tempMessage;
      while ((tempMessage = _context.msgQueue.GetNextMessage()) != null) {
        FloatReceivedCallback(tempMessage);
      }
    }
  }

  private void OnAudioFilterRead(float[] buffer, int numChannels) {
    Assert.AreEqual(numChannels, _context.GetNumOutputChannels()); // invalid channel configuration
    _context.Process(buffer, buffer.Length / numChannels); // process dsp
  }
}

class Hv_FishSynth_01_Context {

#if UNITY_IOS && !UNITY_EDITOR
  private const string _dllName = "__Internal";
#else
  private const string _dllName = "Hv_FishSynth_01_AudioLib";
#endif

  // Thread-safe message queue
  public class SendMessageQueue {
    private readonly object _msgQueueSync = new object();
    private readonly Queue<Hv_FishSynth_01_AudioLib.FloatMessage> _msgQueue = new Queue<Hv_FishSynth_01_AudioLib.FloatMessage>();

    public Hv_FishSynth_01_AudioLib.FloatMessage GetNextMessage() {
      lock (_msgQueueSync) {
        return (_msgQueue.Count != 0) ? _msgQueue.Dequeue() : null;
      }
    }

    public void AddMessage(string receiverName, float value) {
      Hv_FishSynth_01_AudioLib.FloatMessage msg = new Hv_FishSynth_01_AudioLib.FloatMessage(receiverName, value);
      lock (_msgQueueSync) {
        _msgQueue.Enqueue(msg);
      }
    }
  }

  public readonly SendMessageQueue msgQueue = new SendMessageQueue();
  private readonly GCHandle gch;
  private readonly IntPtr _context; // handle into unmanaged memory
  private SendHook _sendHook = null;

  [DllImport (_dllName)]
  private static extern IntPtr hv_FishSynth_01_new_with_options(double sampleRate, int poolKb, int inQueueKb, int outQueueKb);

  [DllImport (_dllName)]
  private static extern int hv_processInlineInterleaved(IntPtr ctx,
      [In] float[] inBuffer, [Out] float[] outBuffer, int numSamples);

  [DllImport (_dllName)]
  private static extern void hv_delete(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern double hv_getSampleRate(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumInputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumOutputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_setSendHook(IntPtr ctx, SendHook sendHook);

  [DllImport (_dllName)]
  private static extern void hv_setPrintHook(IntPtr ctx, PrintHook printHook);

  [DllImport (_dllName)]
  private static extern int hv_setUserData(IntPtr ctx, IntPtr userData);

  [DllImport (_dllName)]
  private static extern IntPtr hv_getUserData(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_sendBangToReceiver(IntPtr ctx, uint receiverHash);

  [DllImport (_dllName)]
  private static extern void hv_sendFloatToReceiver(IntPtr ctx, uint receiverHash, float x);

  [DllImport (_dllName)]
  private static extern uint hv_msg_getTimestamp(IntPtr message);

  [DllImport (_dllName)]
  private static extern bool hv_msg_hasFormat(IntPtr message, string format);

  [DllImport (_dllName)]
  private static extern float hv_msg_getFloat(IntPtr message, int index);

  [DllImport (_dllName)]
  private static extern bool hv_table_setLength(IntPtr ctx, uint tableHash, uint newSampleLength);

  [DllImport (_dllName)]
  private static extern IntPtr hv_table_getBuffer(IntPtr ctx, uint tableHash);

  [DllImport (_dllName)]
  private static extern float hv_samplesToMilliseconds(IntPtr ctx, uint numSamples);

  [DllImport (_dllName)]
  private static extern uint hv_stringToHash(string s);

  private delegate void PrintHook(IntPtr context, string printName, string str, IntPtr message);

  private delegate void SendHook(IntPtr context, string sendName, uint sendHash, IntPtr message);

  public Hv_FishSynth_01_Context(double sampleRate, int poolKb=10, int inQueueKb=15, int outQueueKb=2) {
    gch = GCHandle.Alloc(msgQueue);
    _context = hv_FishSynth_01_new_with_options(sampleRate, poolKb, inQueueKb, outQueueKb);
    hv_setPrintHook(_context, new PrintHook(OnPrint));
    hv_setUserData(_context, GCHandle.ToIntPtr(gch));
  }

  ~Hv_FishSynth_01_Context() {
    hv_delete(_context);
    GC.KeepAlive(_context);
    GC.KeepAlive(_sendHook);
    gch.Free();
  }

  public void RegisterSendHook() {
    // Note: send hook functionality only applies to messages containing a single float value
    if (_sendHook == null) {
      _sendHook = new SendHook(OnMessageSent);
      hv_setSendHook(_context, _sendHook);
    }
  }

  public bool IsSendHookRegistered() {
    return (_sendHook != null);
  }

  public double GetSampleRate() {
    return hv_getSampleRate(_context);
  }

  public int GetNumInputChannels() {
    return hv_getNumInputChannels(_context);
  }

  public int GetNumOutputChannels() {
    return hv_getNumOutputChannels(_context);
  }

  public void SendBangToReceiver(uint receiverHash) {
    hv_sendBangToReceiver(_context, receiverHash);
  }

  public void SendFloatToReceiver(uint receiverHash, float x) {
    hv_sendFloatToReceiver(_context, receiverHash, x);
  }

  public void FillTableWithFloatBuffer(uint tableHash, float[] buffer) {
    if (hv_table_getBuffer(_context, tableHash) != IntPtr.Zero) {
      hv_table_setLength(_context, tableHash, (uint) buffer.Length);
      Marshal.Copy(buffer, 0, hv_table_getBuffer(_context, tableHash), buffer.Length);
    } else {
      Debug.Log(string.Format("Table '{0}' doesn't exist in the patch context.", tableHash));
    }
  }

  public uint StringToHash(string s) {
    return hv_stringToHash(s);
  }

  public int Process(float[] buffer, int numFrames) {
    return hv_processInlineInterleaved(_context, buffer, buffer, numFrames);
  }

  [MonoPInvokeCallback(typeof(PrintHook))]
  private static void OnPrint(IntPtr context, string printName, string str, IntPtr message) {
    float timeInSecs = hv_samplesToMilliseconds(context, hv_msg_getTimestamp(message)) / 1000.0f;
    Debug.Log(string.Format("{0} [{1:0.000}]: {2}", printName, timeInSecs, str));
  }

  [MonoPInvokeCallback(typeof(SendHook))]
  private static void OnMessageSent(IntPtr context, string sendName, uint sendHash, IntPtr message) {
    if (hv_msg_hasFormat(message, "f")) {
      SendMessageQueue msgQueue = (SendMessageQueue) GCHandle.FromIntPtr(hv_getUserData(context)).Target;
      msgQueue.AddMessage(sendName, hv_msg_getFloat(message, 0));
    }
  }
}
