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

[CustomEditor(typeof(Hv_BoidSynth06_Sine_AudioLib))]
public class Hv_BoidSynth06_Sine_Editor : Editor {

  [MenuItem("Heavy/BoidSynth06_Sine")]
  static void CreateHv_BoidSynth06_Sine() {
    GameObject target = Selection.activeGameObject;
    if (target != null) {
      target.AddComponent<Hv_BoidSynth06_Sine_AudioLib>();
    }
  }

  private Hv_BoidSynth06_Sine_AudioLib _dsp;

  private void OnEnable() {
    _dsp = target as Hv_BoidSynth06_Sine_AudioLib;
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

    // OnOff_trigger
    if (GUILayout.Button("OnOff_trigger")) {
      _dsp.SendEvent(Hv_BoidSynth06_Sine_AudioLib.Event.Onoff_trigger);
    }

    // noteOn_trigger
    if (GUILayout.Button("noteOn_trigger")) {
      _dsp.SendEvent(Hv_BoidSynth06_Sine_AudioLib.Event.Noteon_trigger);
    }
    // PARAMETERS
    GUI.enabled = true;
    EditorGUILayout.Space();
    EditorGUI.indentLevel++;

    // freq
    GUILayout.BeginHorizontal();
    float freq = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq);
    float newFreq = EditorGUILayout.Slider("freq", freq, 50.0f, 2000.0f);
    if (freq != newFreq) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq, newFreq);
    }
    GUILayout.EndHorizontal();

    // gain
    GUILayout.BeginHorizontal();
    float gain = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gain);
    float newGain = EditorGUILayout.Slider("gain", gain, 0.0f, 1.0f);
    if (gain != newGain) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gain, newGain);
    }
    GUILayout.EndHorizontal();

    // gainSmoothTime
    GUILayout.BeginHorizontal();
    float gainSmoothTime = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gainsmoothtime);
    float newGainsmoothtime = EditorGUILayout.Slider("gainSmoothTime", gainSmoothTime, 1.0f, 1000.0f);
    if (gainSmoothTime != newGainsmoothtime) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Gainsmoothtime, newGainsmoothtime);
    }
    GUILayout.EndHorizontal();

    // mstrAttackRelease
    GUILayout.BeginHorizontal();
    float mstrAttackRelease = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Mstrattackrelease);
    float newMstrattackrelease = EditorGUILayout.Slider("mstrAttackRelease", mstrAttackRelease, 1.0f, 1000.0f);
    if (mstrAttackRelease != newMstrattackrelease) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Mstrattackrelease, newMstrattackrelease);
    }
    GUILayout.EndHorizontal();

    // pan
    GUILayout.BeginHorizontal();
    float pan = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Pan);
    float newPan = EditorGUILayout.Slider("pan", pan, 0.0f, 100.0f);
    if (pan != newPan) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Pan, newPan);
    }
    GUILayout.EndHorizontal();

    // phase
    GUILayout.BeginHorizontal();
    float phase = _dsp.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Phase);
    float newPhase = EditorGUILayout.Slider("phase", phase, 0.0f, 1.0f);
    if (phase != newPhase) {
      _dsp.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Phase, newPhase);
    }
    GUILayout.EndHorizontal();

    EditorGUI.indentLevel--;

    

    GUILayout.EndVertical();
  }
}
#endif // UNITY_EDITOR

[RequireComponent (typeof (AudioSource))]
public class Hv_BoidSynth06_Sine_AudioLib : MonoBehaviour {
  
  // Events are used to trigger bangs in the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_BoidSynth06_Sine_AudioLib script = GetComponent<Hv_BoidSynth06_Sine_AudioLib>();
        script.SendEvent(Hv_BoidSynth06_Sine_AudioLib.Event.Onoff_trigger);
    }
  */
  public enum Event : uint {
    Onoff_trigger = 0x11EA5CF8,
    Noteon_trigger = 0xEC4B8CAA,
  }
  
  // Parameters are used to send float messages into the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_BoidSynth06_Sine_AudioLib script = GetComponent<Hv_BoidSynth06_Sine_AudioLib>();
        // Get and set a parameter
        float freq = script.GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq);
        script.SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter.Freq, freq + 0.1f);
    }
  */
  public enum Parameter : uint {
    Freq = 0x345FC008,
    Gain = 0x811CC33F,
    Gainsmoothtime = 0x90CC4C6B,
    Mstrattackrelease = 0x578460C,
    Pan = 0x35FCD07D,
    Phase = 0x67F7D820,
  }
  
  // Delegate method for receiving float messages from the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_BoidSynth06_Sine_AudioLib script = GetComponent<Hv_BoidSynth06_Sine_AudioLib>();
        script.RegisterSendHook();
        script.FloatReceivedCallback += OnFloatMessage;
    }

    void OnFloatMessage(Hv_BoidSynth06_Sine_AudioLib.FloatMessage message) {
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
  public float gainSmoothTime = 50.0f;
  public float mstrAttackRelease = 500.0f;
  public float pan = 50.0f;
  public float phase = 0.0f;

  // internal state
  private Hv_BoidSynth06_Sine_Context _context;

  public bool IsInstantiated() {
    return (_context != null);
  }

  public void RegisterSendHook() {
    _context.RegisterSendHook();
  }
  
  // see Hv_BoidSynth06_Sine_AudioLib.Event for definitions
  public void SendEvent(Hv_BoidSynth06_Sine_AudioLib.Event e) {
    if (IsInstantiated()) _context.SendBangToReceiver((uint) e);
  }
  
  // see Hv_BoidSynth06_Sine_AudioLib.Parameter for definitions
  public float GetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter param) {
    switch (param) {
      case Parameter.Freq: return freq;
      case Parameter.Gain: return gain;
      case Parameter.Gainsmoothtime: return gainSmoothTime;
      case Parameter.Mstrattackrelease: return mstrAttackRelease;
      case Parameter.Pan: return pan;
      case Parameter.Phase: return phase;
      default: return 0.0f;
    }
  }

  public void SetFloatParameter(Hv_BoidSynth06_Sine_AudioLib.Parameter param, float x) {
    switch (param) {
      case Parameter.Freq: {
        x = Mathf.Clamp(x, 50.0f, 2000.0f);
        freq = x;
        break;
      }
      case Parameter.Gain: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        gain = x;
        break;
      }
      case Parameter.Gainsmoothtime: {
        x = Mathf.Clamp(x, 1.0f, 1000.0f);
        gainSmoothTime = x;
        break;
      }
      case Parameter.Mstrattackrelease: {
        x = Mathf.Clamp(x, 1.0f, 1000.0f);
        mstrAttackRelease = x;
        break;
      }
      case Parameter.Pan: {
        x = Mathf.Clamp(x, 0.0f, 100.0f);
        pan = x;
        break;
      }
      case Parameter.Phase: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        phase = x;
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
      Debug.LogWarning("Hv_BoidSynth06_Sine_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableName + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(StringToHash(tableName), buffer);
  }

  public void FillTableWithMonoAudioClip(uint tableHash, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_BoidSynth06_Sine_AudioLib: Only loading first channel of '" +
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
    _context = new Hv_BoidSynth06_Sine_Context((double) AudioSettings.outputSampleRate);
    
  }
  
  private void Start() {
    _context.SendFloatToReceiver((uint) Parameter.Freq, freq);
    _context.SendFloatToReceiver((uint) Parameter.Gain, gain);
    _context.SendFloatToReceiver((uint) Parameter.Gainsmoothtime, gainSmoothTime);
    _context.SendFloatToReceiver((uint) Parameter.Mstrattackrelease, mstrAttackRelease);
    _context.SendFloatToReceiver((uint) Parameter.Pan, pan);
    _context.SendFloatToReceiver((uint) Parameter.Phase, phase);
  }
  
  private void Update() {
    // retreive sent messages
    if (_context.IsSendHookRegistered()) {
      Hv_BoidSynth06_Sine_AudioLib.FloatMessage tempMessage;
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

class Hv_BoidSynth06_Sine_Context {

#if UNITY_IOS && !UNITY_EDITOR
  private const string _dllName = "__Internal";
#else
  private const string _dllName = "Hv_BoidSynth06_Sine_AudioLib";
#endif

  // Thread-safe message queue
  public class SendMessageQueue {
    private readonly object _msgQueueSync = new object();
    private readonly Queue<Hv_BoidSynth06_Sine_AudioLib.FloatMessage> _msgQueue = new Queue<Hv_BoidSynth06_Sine_AudioLib.FloatMessage>();

    public Hv_BoidSynth06_Sine_AudioLib.FloatMessage GetNextMessage() {
      lock (_msgQueueSync) {
        return (_msgQueue.Count != 0) ? _msgQueue.Dequeue() : null;
      }
    }

    public void AddMessage(string receiverName, float value) {
      Hv_BoidSynth06_Sine_AudioLib.FloatMessage msg = new Hv_BoidSynth06_Sine_AudioLib.FloatMessage(receiverName, value);
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
  private static extern IntPtr hv_BoidSynth06_Sine_new_with_options(double sampleRate, int poolKb, int inQueueKb, int outQueueKb);

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

  public Hv_BoidSynth06_Sine_Context(double sampleRate, int poolKb=10, int inQueueKb=6, int outQueueKb=2) {
    gch = GCHandle.Alloc(msgQueue);
    _context = hv_BoidSynth06_Sine_new_with_options(sampleRate, poolKb, inQueueKb, outQueueKb);
    hv_setPrintHook(_context, new PrintHook(OnPrint));
    hv_setUserData(_context, GCHandle.ToIntPtr(gch));
  }

  ~Hv_BoidSynth06_Sine_Context() {
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
