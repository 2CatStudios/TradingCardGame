using System;
using System.Net;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class Opponent
{
	
	public bool connected = false;
	
	public string name;
	public string ipAddress = "192.168.1.197";
	
	public List<GameCard> cards = new List<GameCard> ();
}


class ConnectionInfo
{
	
	public Socket Socket;
	public byte[] Buffer;
}


public class StateObject
{
	
    public Socket workSocket = null;
    public const int bufferSize = 256;
    public byte[] buffer = new byte[bufferSize];
    public StringBuilder stringBuilder = new StringBuilder ();
}


public class NetworkManager : MonoBehaviour
{
	
	DebugLog debugLog;
	PreferencesManager preferencesManager;
	ExternalInformation externalInformation;
	DeckManager deckManager;
	UserInterface userInterface;
	
	private Socket listenSocket;
	
	internal enum ConnectionType { None, Hosting, Connecting, Connected, Playing }
	internal ConnectionType connectionType = ConnectionType.None;
	private ConnectionInfo currentConnection = null;
	
	internal bool hosting = false;
	internal string activePort = "52531";
	
	internal bool connecting = false;
	
	internal bool info = false;
	internal string infoString = "";
	
	
	internal Opponent opponent = new Opponent ();
	
	internal List<String> chatMessages = new List<String> ();
	
	private static String response = String.Empty;
	
	internal bool options = false;
	
	
	public void Awake ()
	{
		
		EditorApplication.playmodeStateChanged += PlaymodeCallback;
	}
	
	public void PlaymodeCallback ()
	{
		
		if ( EditorApplication.isPlayingOrWillChangePlaymode == false )
		{
				
			if ( currentConnection != null )
			{
			
				currentConnection.Socket.Close ();
				currentConnection = null;
			}
			
			if ( listenSocket != null )
			{
				
				LingerOption lingerOption = new LingerOption ( false, 0 );
				listenSocket.SetSocketOption ( SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption );
				listenSocket.Close ();
				listenSocket = null;
			}
		}
	}
	

	void Start ()
	{
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
		preferencesManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<PreferencesManager>();
		externalInformation = GameObject.FindGameObjectWithTag ( "ExternalInformation" ).GetComponent<ExternalInformation> ();
		deckManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<DeckManager>();
		userInterface = GameObject.FindGameObjectWithTag ( "UserInterface" ).GetComponent<UserInterface>();
	}
	
	
	private void AcceptCallback ( IAsyncResult result )
	{
		
		ConnectionInfo newConnection = new ConnectionInfo();
		
		try
		{
			
			Socket s = ( Socket ) result.AsyncState;
			newConnection.Socket = s.EndAccept ( result );
			newConnection.Buffer = new byte[255];
			
			lock ( newConnection ) currentConnection = newConnection;
			
			// Start Receive and a new Accept
			newConnection.Socket.BeginReceive ( newConnection.Buffer, 0, newConnection.Buffer.Length, SocketFlags.None, new AsyncCallback ( ReceiveCallback ), newConnection );
			listenSocket.BeginAccept ( new AsyncCallback ( AcceptCallback ), result.AsyncState );
			
		} catch ( SocketException exc )
		{
			
			CloseConnection ( newConnection );
			UnityEngine.Debug.LogError ( "Socket Exception: " + exc.SocketErrorCode );
		} catch ( Exception exc )
		{
			
			CloseConnection ( newConnection );
			UnityEngine.Debug.LogError ( "Exception: " + exc );
		}
	}
	
	
	private void ReceiveCallback ( IAsyncResult result )
	{
		
		ConnectionInfo connection = ( ConnectionInfo ) result.AsyncState;
		
		try
		{
			
			int bytesRead = connection.Socket.EndReceive ( result );
			if ( bytesRead != 0 )
			{
				
				lock ( currentConnection )
				{
						
					if ( connection != currentConnection )
					{
							
						currentConnection.Socket.Send ( connection.Buffer, bytesRead, SocketFlags.None );
					}
				}
			
				connection.Socket.BeginReceive ( connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback ( ReceiveCallback ), connection );
			} else
			{
				
				CloseConnection ( connection );
			}
		} catch ( SocketException exc )
		{
			
			CloseConnection ( connection );
			UnityEngine.Debug.LogError ( "Socket Exception: " + exc.SocketErrorCode );
		} catch ( Exception exc )
		{
			
			CloseConnection ( connection );
			UnityEngine.Debug.LogError ( "Exception: " + exc );
		}
	}
	
	
	private void CloseConnection ( ConnectionInfo connection )
	{
		
		connection.Socket.Close ();
		lock ( connection ) connection = null;
		
		UnityEngine.Debug.Log ( "Socket Closed" );
	}


	public bool SetupHost ()
	{
		
		debugLog.ReceiveMessage ( "\tSetting Up Server on " + activePort );
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		
		debugLog.ReceiveMessage ( "\tCreating Socket" );
		IPEndPoint endpoint = new IPEndPoint( Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], Convert.ToInt32 ( activePort ));

		listenSocket = new Socket ( endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp );
		listenSocket.Bind ( endpoint );
		listenSocket.Listen (( int ) SocketOptionName.MaxConnections );

		debugLog.ReceiveMessage ( "\t\tSocket Created and Bound" );
		
		return true;
	}
	

	public void ReceiveConnection ( float receivedOpponentVersion, string receivedOpponentName )
	{
		
		if ( hosting == true )
		{
			
			if ( connectionType == ConnectionType.Hosting )
			{
				
				if ( receivedOpponentVersion == externalInformation.runningVersion )
				{
				
					opponent.name = receivedOpponentName;
					//opponent.ipAddress = receivedIPAddress; //ASSIGN IP
					opponent.cards = new List<GameCard> ();
					
					opponent.cards.Add ( deckManager.masterDeck.gameCards[0] );
					
					connectionType = ConnectionType.Connected;
					
					debugLog.ReceiveMessage ( "\nConnection Received" );
					debugLog.ReceiveMessage ( "\t" + opponent.name );
					
					debugLog.ReceiveMessage ( "\tConnection Type Set to Connected" );
				} else {
					
					//Send Incompatible Versions Warning
				}
			}
		}
	}
	
	
	public bool ShutdownHost ()
	{
		
		chatMessages = new List<String> ();
		
		opponent.name = null;
		opponent.cards.Clear ();
		
		info = false;
		infoString = null;
		
		hosting = false;
		connectionType = ConnectionType.None;
		
		if ( currentConnection != null )
		{
			
			currentConnection.Socket.Close ();
			currentConnection = null;
		}
		
		LingerOption lingerOption = new LingerOption ( false, 0 );
		listenSocket.SetSocketOption ( SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption );
		listenSocket.Close ();
		listenSocket = null;
		
		debugLog.ReceiveMessage ( "\tConnection Type Set to None" );
		
		return true;
	}
	
	
	public void AttemptConnection ( string ipAddressString, string portString )
	{
		
		debugLog.ReceiveMessage ( "\tAttempting Connection to " + ipAddressString + " on " + portString );
		
		connecting = true;
		connectionType = ConnectionType.Connecting;
		debugLog.ReceiveMessage ( "\tConnection Type Set to Connecting" );
		
		try
		{
			
			IPAddress ipAddress = Dns.GetHostEntry ( ipAddressString ).AddressList[0];
			IPEndPoint remoteEndPoint = new IPEndPoint ( ipAddress, Convert.ToInt32 ( portString ));
			
			Socket client = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			
			client.BeginConnect ( remoteEndPoint, new AsyncCallback ( ConnectCallback ), client );
			
			Send ( client, "This is a test<EOF>" );
			
			Receive ( client );
			
			UnityEngine.Debug.Log ( "Response received : " + response );

			client.Shutdown ( SocketShutdown.Both );
			client.Close ();
		} catch ( Exception connectionError )
		{
			
			UnityEngine.Debug.LogError ( connectionError );
		}
	}
	
	
	private static void ConnectCallback ( IAsyncResult callback )
	{
		
		try
		{
			
		    Socket client = ( Socket ) callback.AsyncState;
		    client.EndConnect ( callback );
    	
		    UnityEngine.Debug.Log ( "Socket connected to " + client.RemoteEndPoint.ToString ());
		} catch ( Exception connectionError )
		{
			
		    UnityEngine.Debug.LogError ( connectionError );
		}
	}
	
	
	private static void Receive ( Socket client )
	{
		try
		{
			
    		StateObject state = new StateObject ();
			state.workSocket = client;
			
			client.BeginReceive ( state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback ( ClientReceiveCallback ), state );
		} catch ( Exception error )
		{
			
    		UnityEngine.Debug.LogError ( error );
		}
	}
	
	
	private static void ClientReceiveCallback ( IAsyncResult callback )
	{
		try
		{

		    StateObject state = ( StateObject ) callback.AsyncState;
		    Socket client = state.workSocket;
    	
		    int bytesRead = client.EndReceive ( callback );
    	
		    if ( bytesRead > 0 )
			{
				
				state.stringBuilder.Append ( Encoding.ASCII.GetString ( state.buffer, 0, bytesRead ));
    	
		        client.BeginReceive ( state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback ( ClientReceiveCallback ), state );
		    } else {
				
		        if ( state.stringBuilder.Length > 1 )
				{
					
		            response = state.stringBuilder.ToString ();
		        }
			}
		} catch ( Exception error )
		{
			
			UnityEngine.Debug.LogError ( error );
		}
	}
	
	
	private static void Send ( Socket client, String data )
	{
	
		byte[] byteData = Encoding.ASCII.GetBytes(data);
		client.BeginSend ( byteData, 0, byteData.Length, 0, new AsyncCallback ( ClientSendCallback ), client );
	}
	
	
	private static void ClientSendCallback ( IAsyncResult callback )
	{
	
		try
		{
		
			Socket client = ( Socket ) callback.AsyncState;
			int bytesSent = client.EndSend ( callback );
			UnityEngine.Debug.Log ( "Sent " + bytesSent + " to server." );
		
		} catch ( Exception callbackError )
		{
			
	    	UnityEngine.Debug.LogError ( callbackError );
		}
	}
	
	
	public void DisconnectOpponent ()
	{
		
		if ( connectionType == ConnectionType.Connected || connectionType == ConnectionType.Playing )
		{
		
			debugLog.ReceiveMessage ( "\nOpponent Disconnected" );
			
			opponent.name = null;
			
			infoString = "Your opponent has disconnected.";
			info = true;
			
			hosting = true;
			connectionType = ConnectionType.Hosting;
			
			debugLog.ReceiveMessage ( "\tConnection Type Reset" );
		}
	}
	
	
	public bool BootOpponent ()
	{
		
		debugLog.ReceiveMessage ( "\tSending Disconnect Instruct" );
		
		return true;
	}
	
	
	public void ReceiveChatMessage ( string receivedMessage )
	{
		
		debugLog.ReceiveMessage ( "\tReceived Message!" );
		
		if ( receivedMessage.Substring ( 0, 17 ) == "[2CatStudios_TCG]" )
		{
			
			receivedMessage = receivedMessage.Remove ( 0, 17 );
			
			chatMessages.Add ( receivedMessage );
			userInterface.chatWindowScrollView.y = Mathf.Infinity;
		} else {
			
			debugLog.ReceiveMessage ( "Recieved Unknown Message: '" + receivedMessage + "'" );
			debugLog.ReceiveMessage ( "'" + receivedMessage.Substring ( 0, 17 ) + "' != '" + "[2CatStudios_TCG]" + "'" );
		}
	}
	
	
	public void SendChatMessage ( string messageToSend )
	{
		
		if ( String.IsNullOrEmpty ( messageToSend.Trim ()) == false && messageToSend != "Chat Message" )
		{
			
			string message = "[2CatStudios_TCG]" + preferencesManager.preferences.playerName + " [" + System.DateTime.Now.ToString ( "HH:mm" ) + "] " + messageToSend;
			UdpClient udpClient = new UdpClient( opponent.ipAddress, Int32.Parse ( activePort ));
			Byte[] sendBytes = Encoding.Unicode.GetBytes ( message );
			try
			{
			
		  		udpClient.Send ( sendBytes, sendBytes.Length );
				chatMessages.Add ( message.Remove ( 0, 17 ));
			} catch ( Exception e )
			{
				
				UnityEngine.Debug.Log ( e.ToString ());
				chatMessages.Add ( "Unable to Send ChatMessage!" );
			}
		}
	}
	
	
	public void BrodcastChatMessage ( string messageToSend )
	{
		
		string message = "[2CatStudios_TCG]" + messageToSend;
		UdpClient udpClient = new UdpClient( opponent.ipAddress, Int32.Parse ( activePort ));
		Byte[] sendBytes = Encoding.Unicode.GetBytes ( message );
		try
		{
		
	  		udpClient.Send ( sendBytes, sendBytes.Length );
			chatMessages.Add ( message.Remove ( 0, 17 ));
		} catch ( Exception e )
		{
			
			UnityEngine.Debug.Log ( e.ToString ());
			chatMessages.Add ( "Unable to Send ChatMessage!" );
		}
	}
	
	
	internal void EndTurn ()
	{
		
		
	}
}
