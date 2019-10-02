using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehaviorWithLeader))]
public class CompositeBehaviorWithLeaderEditor : Editor 
{
public override void OnInspectorGUI()
	{
		//create reference to the composite behavior
		CompositeBehaviorWithLeader cb = (CompositeBehaviorWithLeader)target;

		// Create an object to keep track of positions
		//Creates a rect wherever the cursor is
		Rect r = EditorGUILayout.BeginHorizontal();
		//Gives us a baseline cursor to tell where the fields should appear
		r.height = EditorGUIUtility.singleLineHeight;

		//If nothing is in here, put up a warning
		if (cb.behaviors == null || cb.behaviors.Length == 0)
		{
			EditorGUILayout.HelpBox("No Behaviors in array.", MessageType.Warning);
			EditorGUILayout.EndHorizontal();
			r = EditorGUILayout.BeginHorizontal();
			r.height = EditorGUIUtility.singleLineHeight;
		}
		else // If there are behaviors
		{
			//give ourselves a margin on the left
			r.x = 30f;

			/* columns of behaviors and weights  */

			// Create width of behavior to be expandable
			r.width = EditorGUIUtility.currentViewWidth - 95f;
			EditorGUI.LabelField(r, "Behaviors");
			r.x = EditorGUIUtility.currentViewWidth - 65f;
			r.width = 60f;
			EditorGUI.LabelField(r, "Weights");

			//push the cursor down 
			r.y += EditorGUIUtility.singleLineHeight * 1.2f;

			EditorGUI.BeginChangeCheck();

			//For each behavior we have, show it on the line
			for (int i = 0; i < cb.behaviors.Length; i++)
			{
				r.x = 5f;
				r.width = 20f;
				EditorGUI.LabelField(r, i.ToString());
				r.x = 30f;
				r.width = EditorGUIUtility.currentViewWidth - 95f;
				//Show a field for the behavior
				cb.behaviors[i] = (FlockBehaviorWithLeader)EditorGUI.ObjectField(r, cb.behaviors[i], typeof(FlockBehaviorWithLeader), false);

				// same idea for the weights
				r.x = EditorGUIUtility.currentViewWidth - 65f;
				r.width = 60f;
				cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
				r.y += EditorGUIUtility.singleLineHeight * 1.1f;
			}

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(cb);
			}
		}

		// Add some buttons for functionality
		EditorGUILayout.EndHorizontal();
		r.x = 5f;
		r.width = EditorGUIUtility.currentViewWidth - 10f;
		r.y += EditorGUIUtility.singleLineHeight * 0.5f;

		// check if button has been pressed
		if (GUI.Button(r, "Add Behavior"))
		{
			// Add behavior
			AddBehavior(cb);
			//Let's unity know this scriptable object has been changed and needs to be saved
			EditorUtility.SetDirty(cb);
		}

		r.y += EditorGUIUtility.singleLineHeight * 1.5f;
		if (cb.behaviors != null && cb.behaviors.Length > 0)
		{
			if (GUI.Button(r, "Remove Behavior"))
			{
				// remove behavior
				RemoveBehavior(cb);
				EditorUtility.SetDirty(cb);
			}
		}	
	}

	// Add and remove behavior methods
	void AddBehavior (CompositeBehaviorWithLeader cb)
	{
		// get the original size of the array
		int oldCount = (cb.behaviors != null ? cb.behaviors.Length : 0);
		FlockBehaviorWithLeader[] newBehaviors = new FlockBehaviorWithLeader[oldCount + 1];
		float[] newWeights = new float[oldCount + 1];

		for (int i = 0; i < oldCount; i++)
		{
			newBehaviors[i] = cb.behaviors[i];
			newWeights[i] = cb.weights[i];
		}

		newWeights[oldCount] = 1f;
		cb.behaviors = newBehaviors;
		cb.weights = newWeights;
	}

	void RemoveBehavior(CompositeBehaviorWithLeader cb)
	{
		// get the original size of the array
		int oldCount = cb.behaviors.Length;
		if (oldCount ==1)
		{
			cb.behaviors = null;
			cb.weights = null;
			return;
		}

		FlockBehaviorWithLeader[] newBehaviors = new FlockBehaviorWithLeader[oldCount - 1];
		float[] newWeights = new float[oldCount - 1];

		for (int i = 0; i < oldCount -1; i++)
		{
			newBehaviors[i] = cb.behaviors[i];
			newWeights[i] = cb.weights[i];
		}

		cb.behaviors = newBehaviors;
		cb.weights = newWeights;
	}
	
}
