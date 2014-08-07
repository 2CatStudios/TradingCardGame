using System;
using UnityEngine;
using System.Net.Sockets;
using System.Collections;
//Written by Michael Bethke
public class NetworkManager : MonoBehaviour
{
	
	internal enum ConnectionType { None, Hosting, Connecting }
	internal ConnectionType connectionType;


	void Start ()
	{
		
		connectionType = ConnectionType.None;
	}


	public bool SetupHost ( string port )
	{
			
		//Network.InitializeServer ( 1, Int32.Parse ( port ), false );
		connectionType = ConnectionType.Hosting;
			
		return true;
	}
		
		
	public bool ShutdownHost ()
	{
			
		//Network.Disconnect ();
		connectionType = ConnectionType.None;
			
		return true;
	}
	
	public void Test ()
	{
		
		UnityEngine.Debug.Log ( "Test" );
	}
}
