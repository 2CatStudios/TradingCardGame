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
		
        if ( GUILayout.Button ( "Simulate Connection" ))
        {
			
            networkManager.RecieveConnection ( "Simulated Opponent" );
        }
    }
}