using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
//Written by Michael Bethke

[CustomEditor ( typeof ( NetworkManager ))]
public class NetworkSimulation : Editor
{
	
	string message = "";
	string version = "0.0";
	
	
    public override void OnInspectorGUI ()
    {
		
        DrawDefaultInspector();
        
        NetworkManager networkManager = ( NetworkManager ) target;
		
		GUILayout.Space ( 10 );
		GUILayout.Label ( "Simulate Opponent" );
		GUILayout.Space ( 10 );
		
		GUILayout.Label ( "Version" );
		GUILayout.BeginHorizontal ();
		version = EditorGUILayout.TextField ( version );
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
        if ( GUILayout.Button ( "Connect", GUILayout.Width ( 200 )))
        {
			
            networkManager.ReceiveConnection ( float.Parse ( version ), "SimulatedOpp" );
        }
		
		if ( GUILayout.Button ( "Disconnect", GUILayout.Width ( 200 )))
		{
			
			networkManager.DisconnectOpponent ();
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space ( 10 );
		
		GUILayout.BeginHorizontal ();
		message = EditorGUILayout.TextField ( message );
		
        if ( GUILayout.Button ( "Send ChatMessage" ))
        {
			
            networkManager.ReceiveChatMessage ( "[2CatStudios_TCG]" + "\t" + "SimulatedOpp" + " [" + System.DateTime.Now.ToString ( "HH:mm" ) + "]\n" + message );
		}
    	GUILayout.EndHorizontal ();
	}
}