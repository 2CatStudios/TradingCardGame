using System;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class Opponent
{
	
	public bool connected = false;
	
	public string name;
	public string ipAddress;
	
	public List<GameCard> cards = new List<GameCard> ();
}


public class NetworkManager : MonoBehaviour
{
	
	DebugLog debugLog;
	PreferencesManager preferencesManager;
	DeckManager deckManager;
	UserInterface userInterface;
	
	internal enum ConnectionType { None, Hosting, Connecting, Connected, Playing }
	internal ConnectionType connectionType;
	
	internal bool hosting = false;
	internal string activePort = "52531";
	
	internal bool info = false;
	internal string infoString = "";
	
	
	internal Opponent opponent = new Opponent ();
	
	internal List<String> chatMessages = new List<String> ();
	
	internal bool options = false;
	

	void Start ()
	{
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
		preferencesManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<PreferencesManager>();
		deckManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<DeckManager>();
		userInterface = GameObject.FindGameObjectWithTag ( "UserInterface" ).GetComponent<UserInterface>();
		
		connectionType = ConnectionType.None;
	}


	public bool SetupHost ()
	{
		
		debugLog.ReceiveMessage ( "\tSetting Up Server on " + activePort );
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		debugLog.ReceiveMessage ( "\tConnection Type Set to Hosting" );
		
		return true;
	}
		
		
	public bool ShutdownHost ()
	{
		
		opponent.name = null;
		opponent.cards.Clear ();
		
		info = false;
		infoString = null;
		
		hosting = false;
		connectionType = ConnectionType.None;
		
		debugLog.ReceiveMessage ( "\tConnection Type Set to None" );
		
		return true;
	}
	

	public void ReceiveConnection ( string receivedOpponentName )
	{
		
		if ( hosting == true )
		{
			
			if ( connectionType == ConnectionType.Hosting )
			{
				
				opponent.name = receivedOpponentName;
				opponent.ipAddress = ""; //ASSIGN IP
				opponent.cards = new List<GameCard> ();
				
				opponent.cards.Add ( deckManager.masterDeck.gameCards[0] );
				
				connectionType = ConnectionType.Connected;
				
				debugLog.ReceiveMessage ( "\nConnection Received" );
				debugLog.ReceiveMessage ( "\t" + opponent.name );
				
				debugLog.ReceiveMessage ( "\tConnection Type Set to Connected" );
			}
		}
	}
	
	
	public void DisconnectOpponent ()
	{
		
		debugLog.ReceiveMessage ( "\nOpponent Disconnected" );
		
		opponent.name = null;
		
		infoString = "Your opponent has disconnected.";
		info = true;
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		
		debugLog.ReceiveMessage ( "\tConnection Type Reset" );
	}
	
	
	public bool BootOpponent ()
	{
		
		debugLog.ReceiveMessage ( "\tSending Disconnect Instruct" );
		
		
		return true;
	}
	
	
	public void ReceiveChatMessage ( string receivedMessage )
	{
		
		chatMessages.Add ( receivedMessage );
		userInterface.chatWindowScrollView.y = Mathf.Infinity;
	}
	
	
	public void SendChatMessage ( string messageToSend )
	{
		
		if ( String.IsNullOrEmpty ( messageToSend.Trim ()) == false && messageToSend != "Chat Message" )
		{
			
			UdpClient udpClient = new UdpClient( opponent.ipAddress, Int32.Parse ( activePort ));
			Byte[] sendBytes = Encoding.Unicode.GetBytes ( "[TCG]" + "\t" + preferencesManager.preferences.playerName + " [" + System.DateTime.Now.ToString ( "HH:mm" ) + "]\n" + messageToSend );
			try
			{
			
		  		udpClient.Send ( sendBytes, sendBytes.Length );
			} catch ( Exception e )
			{
				
				UnityEngine.Debug.Log ( e.ToString ());
			}
		}
	}
}
