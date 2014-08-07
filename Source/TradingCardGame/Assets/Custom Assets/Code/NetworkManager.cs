using System;
using UnityEngine;
using System.Net.Sockets;
using System.Collections;
//Written by Michael Bethke
public class NetworkManager : MonoBehaviour
{
	
	internal enum ConnectionType { None, WaitingForConnection, Hosting, Connecting, Connected }
	internal ConnectionType connectionType;
	
	internal string opponentName;


	void Start ()
	{
		
		connectionType = ConnectionType.None;
	}


	public void SetupHost ( string port )
	{

		connectionType = ConnectionType.WaitingForConnection;
	}
		
		
	public void ShutdownHost ()
	{
			
		connectionType = ConnectionType.None;
	}
	

	public void RecieveConnection ( string receivedOpponentName )
	{
		
		opponentName = receivedOpponentName;
		connectionType = ConnectionType.Hosting;
	}
}
