using System;
using UnityEngine;
using System.Net.Sockets;
using System.Collections;
//Written by Michael Bethke
public class NetworkManager : MonoBehaviour
{
	
	internal enum ConnectionType { None, Hosting, Connecting, Connected, Playing }
	internal ConnectionType connectionType;
	
	internal bool hosting = false;
	
	internal bool info = false;
	internal string infoString = "";
	
	internal bool options = false;

	internal bool opponentConnected = false;
	internal string opponentName;
	

	void Start ()
	{
		
		connectionType = ConnectionType.None;
	}


	public bool SetupHost ( string port )
	{
		
		UnityEngine.Debug.Log ( "\tSetting Up Server on " + port );
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		UnityEngine.Debug.Log ( "\tConnection Type Set to Hosting" );
		
		return true;
	}
		
		
	public bool ShutdownHost ()
	{
		
		opponentName = null;
		
		info = false;
		infoString = null;
		
		hosting = false;
		connectionType = ConnectionType.None;
		
		UnityEngine.Debug.Log ( "\tConnection Type Set to None" );
		
		return true;
	}
	

	public void ReceiveConnection ( string receivedOpponentName )
	{
		
		opponentName = receivedOpponentName;
		connectionType = ConnectionType.Connected;
		
		UnityEngine.Debug.Log ( "Connection Received" );
		UnityEngine.Debug.Log ( "\t" + opponentName );
		
		UnityEngine.Debug.Log ( "\tConnection Type Set to Connected" );
	}
	
	
	public void DisconnectOpponent ()
	{
		
		UnityEngine.Debug.Log ( "Opponent Disconnected" );
		
		opponentName = null;
		
		infoString = "Your opponent has disconnected.";
		info = true;
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		
		UnityEngine.Debug.Log ( "\tConnection Type Reset" );
	}
}
