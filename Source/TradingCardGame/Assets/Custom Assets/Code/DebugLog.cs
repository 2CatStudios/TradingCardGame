using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class DebugLog : MonoBehaviour
{
	
	public bool debugLogActive = false;
	public GUISkin guiskin;
	
	Vector2 scrollPosition;
	
	internal List<String> debugLog = new List<String> ();
	

	void Start ()
	{
	
		if ( debugLogActive == true )
		{
			
			debugLog.Add ( "Debug Log: Active\n" );
		} else {
			
			Destroy ( gameObject );
		}
	}
	
	
	void OnEnable ()
	{
		
		Application.RegisterLogCallback ( HandleLog );
	}
	
	
	void OnDisable ()
	{
		
		Application.RegisterLogCallback ( null );
	}
	
	
	void HandleLog ( string logString, string stackTrace, LogType type )
	{
		
		debugLog.Add ( logString + " (" + type + ")" );
	}
	
	
	void OnGUI ()
	{
		
		GUI.skin = guiskin;
		GUILayout.BeginArea ( new Rect ( 0, 0, Screen.width, Screen.height ));
		scrollPosition = GUILayout.BeginScrollView ( scrollPosition, false, false, GUILayout.Width ( Screen.width ), GUILayout.Height ( Screen.height ));
		GUILayout.FlexibleSpace ();
		
		for ( int index = 0; index < debugLog.Count; index += 1 )
		{
			
			GUILayout.Label ( debugLog[index] );
		}
		
		scrollPosition.y = Mathf.Infinity;
			
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
	}
}