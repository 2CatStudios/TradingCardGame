using UnityEditor;
using UnityEngine;
using System.Collections;
//Written by Michael Bethke

[CustomEditor ( typeof ( NetworkManager ))]
public class NetworkSimulation : Editor
{
	
    public override void OnInspectorGUI ()
    {
		
        DrawDefaultInspector();
        
        NetworkManager networkManager = ( NetworkManager ) target;
		
		GUILayout.Space ( 10 );
		GUILayout.Label ( "Simulate Opponent" );
		
		GUILayout.BeginHorizontal ();
        if ( GUILayout.Button ( "Connect", GUILayout.Width ( 200 )))
        {
			
            networkManager.ReceiveConnection ( "SimulatedOpp" );
        }
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if ( GUILayout.Button ( "Disconnect", GUILayout.Width ( 200 )))
		{
			
			networkManager.DisconnectOpponent ();
		}
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
    }
}