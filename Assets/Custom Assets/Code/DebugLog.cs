using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class DebugLog : MonoBehaviour
{
	
	UserInterface userInterface;
	
	public bool debugLogActive = false;
	
	internal List<String> debugLog = new List<String> ();
	

	void Start ()
	{
		
		userInterface = GameObject.FindGameObjectWithTag ( "UserInterface" ).GetComponent<UserInterface>();
	
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
		userInterface.debugScrollPosition.y = Mathf.Infinity;
	}
}