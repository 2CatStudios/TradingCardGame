using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class DebugLog : MonoBehaviour
{
	
	public bool debugLogActive = false;
	
	internal List<String> debugLog = new List<String> ();
	

	void Start ()
	{
	
		if ( debugLogActive == true )
		{
			
			debugLog.Add ( "Debug Log: Active\n" );
		} else {
			
			//Destroy ( gameObject );
		}
	}
	
	
	public void ReceiveMessage ( string logString )
	{
		
		debugLog.Add ( logString + " (Log)" );
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
}