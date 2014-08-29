using UnityEditor;
using UnityEngine;
using System.Collections;
//Written by Michael Bethke

[CustomEditor ( typeof ( NetworkManager ))]
public class NetworkSimulation : Editor
{
	
	string message = "";
	
	
    public override void OnInspectorGUI ()
    {
		
        DrawDefaultInspector();
        
        NetworkManager networkManager = ( NetworkManager ) target;
		
		GUILayout.Space ( 10 );
		GUILayout.Label ( "Simulate Opponent" );
		
		GUILayout.BeginHorizontal ();
        if ( GUILayout.Button ( "Connect", GUILayout.Width ( 200 )))
        {
			
            networkManager.ReceiveConnection ( "SimulatedOpp", "192.168.1.197" );
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
		
		GUILayout.Space ( 10 );
		
        if ( GUILayout.Button ( "Send ChatMessage", GUILayout.Width ( 200 )))
        {
			
            networkManager.ReceiveChatMessage ( "\t" + /* preferencesManager.preferences.playerName */ "SimulatedOpp" + " [" + System.DateTime.Now.ToString ( "HH:mm" ) + "]\n" + message );
        }
		
		message = EditorGUILayout.TextField ( message );
    }
}