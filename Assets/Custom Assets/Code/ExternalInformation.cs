using System;
using System.IO;
using System.Xml;
using System.Net;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
//Written by Michael Bethke

[XmlRoot ( "OfficialServers" )]
public class OfficialServerList
{
	
	[XmlElement ( "OfficialServer" )]
	public OfficialServer[] officialServers;
}

public class OfficialServer
{
	
	[XmlAttribute]
	public string featured;
	
	public string name;
	public string host;
	public string description;
	public string ipaddress;
	public string port;
}


[XmlRoot ( "SavedServers" )]
public class SavedServerList
{
	
	[XmlElement ( "Server" )]
	public List<SavedServer> savedServers = new List<SavedServer> ();
}

public class SavedServer
{
	[XmlAttribute]
	public int index;
		
	public string name;
	public string ipaddress;
	public string port;
}


public class ExternalInformation : MonoBehaviour
{
	
	DebugLog debugLog;
	DeckManager deckManager;
	
	internal bool startup = true;
	
	internal OfficialServerList officialServerList = new OfficialServerList ();
	internal SavedServerList savedServerList = new SavedServerList ();
	
	string macPath = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string windowsPath = Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ) + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string path;
	string cardsPath;
	
	bool localDirectories = false;
	bool savedServers = false;
	bool officialServers = false;
	bool masterDeck = false;
	bool cards = false;
	
	void Start ()
	{
		
		Screen.SetResolution ( 772, 360, false );
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
		deckManager = GameObject.FindGameObjectWithTag ( "DeckManager" ).GetComponent<DeckManager>();
		
		debugLog.debugLog.Add ( "Startup Process Running\n\n" );
		
		if ( Environment.OSVersion.ToString ().Substring ( 0, 4 ) == "Unix" )
		{
			
			path = macPath;
			UnityEngine.Debug.Log ( "Client running on Mac OS" );
		} else {
			
			path = windowsPath;
			UnityEngine.Debug.Log ( "Client running on Windows OS" );
		}
		
		cardsPath = path + "Cards" + Path.DirectorySeparatorChar;
		
		UnityEngine.Debug.Log ( "Application Size: " + Screen.width + " x " + Screen.height );
		UnityEngine.Debug.Log ( "Screen Size: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height );
		UnityEngine.Debug.Log ( "Refresh Rate (0 If not Obtainable): " + Screen.currentResolution.refreshRate );
		
		
		Thread setupLocalDirectoriesThread = new Thread ( new ThreadStart ( SetupLocalDirectories ));
		setupLocalDirectoriesThread.Start ();
		
		StartCoroutine ( AwaitStartup ());
	}
	
	
	IEnumerator AwaitStartup ()
	{
		
		while ( true )
		{
			
			if ( localDirectories == true & savedServers == true & officialServers == true & masterDeck == true && cards == true )
			{
			
				UnityEngine.Debug.Log ( "\nStartup Completed" );
	
				startup = false;
				yield break;
			}
			
			yield return null;
		}
	}
	
	
	void SetupLocalDirectories ()
	{
		
		debugLog.ReceiveMessage ( "\nInitializing Local Directories" );
		
		Thread.Sleep ( 500 );
		
		if ( !Directory.Exists ( cardsPath ))
		{
			
			Directory.CreateDirectory ( cardsPath );
		}
		
		if ( !Directory.Exists ( path ))
		{
			
			Directory.CreateDirectory ( path );
		}
		
		debugLog.ReceiveMessage ( "\tGame Directories have been Created" );
		
		localDirectories = true;
		debugLog.ReceiveMessage ( "\tLocal Directories Setup Successfully" );
		
		Thread loadSavedServersThread = new Thread (() => FetchSavedServers ( true ));
		loadSavedServersThread.Start ();
	}
	
	
	void FetchSavedServers ( bool delay )
	{
		
		if ( delay == true )
		{
			
			Thread.Sleep ( 500 );
		}
		
		debugLog.ReceiveMessage ( "\nLoading Saved Servers" );
		
		if ( File.Exists ( path + "SavedServers.xml" ))
		{
			
			try
			{
				
				debugLog.ReceiveMessage ( "\tReading Saved Servers" );
				
				System.IO.StreamReader streamReader = new System.IO.StreamReader ( path + "SavedServers.xml" );
				string xml = streamReader.ReadToEnd();
				streamReader.Close();
				
				debugLog.ReceiveMessage ( "\t\tRead into Memory" );
		
				savedServerList = xml.DeserializeXml<SavedServerList>();
				
				debugLog.ReceiveMessage ( "\t\tDeserialized" );
			} catch ( Exception e )
			{
					
				debugLog.ReceiveMessage ( "\tERROR: " + e );
			}
		} else {
			
			debugLog.ReceiveMessage ( "\tNo Saved Servers Found" );
		}
		
		savedServers = true;
		
		Thread downloadingOfficialServerIndexThread = new Thread ( new ThreadStart ( FetchOfficialServers ));
		downloadingOfficialServerIndexThread.Start ();
	}
	
	
	internal void SaveServer ( string ip, string port, string name )
	{
		
		try {
		
			savedServerList.savedServers.Clear ();
			UnityEngine.Debug.Log ( "\tSaved Servers Cleared from Memory" );
		
			if ( File.Exists ( path + "SavedServers.xml" ))
			{
				
				UnityEngine.Debug.Log ( "\tReading Current Saved Servers to Memory" );
				
				System.IO.StreamReader streamReader = new System.IO.StreamReader ( path + "SavedServers.xml" );
				string xml = streamReader.ReadToEnd();
				streamReader.Close();
				
				UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
				savedServerList = xml.DeserializeXml<SavedServerList>();
				
				UnityEngine.Debug.Log ( "\t\tDeserialized" );
			}
			
			UnityEngine.Debug.Log ( "\tCreating New Server Entry" );
		
			SavedServer newServer = new SavedServer ();
			newServer.ipaddress = ip;
			UnityEngine.Debug.Log ( "\t\t" + newServer.ipaddress );
			
			newServer.port = port;
			UnityEngine.Debug.Log ( "\t\t" + newServer.port );
			
			newServer.name = name;
			UnityEngine.Debug.Log ( "\t\t" + newServer.name );
			
			newServer.index = savedServerList.savedServers.Count + 1;
			UnityEngine.Debug.Log ( "\t\t" + newServer.index );
			
			savedServerList.savedServers.Add ( newServer );
			
			UnityEngine.Debug.Log ( "\tNew Server Added to Memory" );
			
			XmlSerializer serializer = new XmlSerializer ( savedServerList.GetType ());
			StreamWriter writer = new StreamWriter ( path + "SavedServers.xml" );
			serializer.Serialize ( writer.BaseStream, savedServerList );
			
			UnityEngine.Debug.Log ( "\tUpdated Saved Servers List Written to Disk" );

		} catch ( Exception e )
		{
			
			UnityEngine.Debug.LogError ( "\tERROR: " + e );
		}
	}
	
	
	internal void RemoveSavedServer ( int index )
	{
		
		XmlDocument doc = new XmlDocument();
		
		UnityEngine.Debug.Log ( "\tReading Saved Server List into Memory" );
		
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( path + "SavedServers.xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
		
		UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
		doc.LoadXml ( xml );
		XmlNode node = doc.SelectSingleNode ( "/SavedServers/Server[@index='" + index + "']" );
		
		UnityEngine.Debug.Log ( "\tSearching for Node" );
		
		if ( node != null )
		{
			
			UnityEngine.Debug.Log ( "\t\tNode Located" );
			
			XmlNode parent = node.ParentNode;
			parent.RemoveChild ( node );
			
			UnityEngine.Debug.Log ( "\t\tServer Removed" );
			
			doc.Save ( path + "SavedServers.xml" );
			
			UnityEngine.Debug.Log ( "\tLoading Updated Saved Servers" );
			FetchSavedServers ( false );
		} else {
			
			UnityEngine.Debug.LogError ( "\t\tUnable to Locate Node" );
		}
	}
	
	
	void FetchOfficialServers ()
	{
		
		Thread.Sleep ( 1000 );
		
		debugLog.ReceiveMessage ( "\nDownloading Official Server Index" );

		try {
			
			StreamReader streamReader = new StreamReader ( WebRequest.Create ( "http://2catstudios.github.io/TradingCardGame/OfficialServers.xml" ).GetResponse ().GetResponseStream ());
			string xml = streamReader.ReadToEnd ();
			
			debugLog.ReceiveMessage ( "\t\tRead into Memory" );
			
			officialServerList = xml.DeserializeXml<OfficialServerList>();
			
			debugLog.ReceiveMessage ( "\t\tDeserialized" );
		} catch ( Exception e ) {
			
			debugLog.ReceiveMessage ( "\tERROR: " + e );
		}
		
		officialServers = true;
		
		Thread downloadMasterDeckThread = new Thread ( new ThreadStart ( DownloadMasterDeck ));
		downloadMasterDeckThread.Start ();
	}
	
	
	void DownloadMasterDeck ()
	{
		
		Thread.Sleep ( 1000 );
		
		debugLog.ReceiveMessage ( "\nDownloading MasterDeck" );
		
		try
		{
			
			StreamReader reader = new StreamReader ( WebRequest.Create ( "http://2catstudios.github.io/TradingCardGame/MasterDeck.xml" ).GetResponse ().GetResponseStream ());
			string deckXML = reader.ReadToEnd ();
			
			debugLog.ReceiveMessage ( "\t\tDownloaded and Read into Memory" );
			deckManager.masterDeck = deckXML.DeserializeXml<MasterDeck>();
			UnityEngine.Debug.Log ( "\t\tDeserialized" );
			
		} catch ( Exception e ) {
			
			debugLog.ReceiveMessage ( "\tERROR: " + e );
		}
		
		masterDeck = true;
		
	    Thread initializeCardsThread = new Thread ( new ThreadStart ( InitializeCards ));
		initializeCardsThread.Start ();
	}
	
	
	void InitializeCards ()
	{
		
		Thread.Sleep ( 2000 );
		
		debugLog.ReceiveMessage ( "\nInitialize Cards" );
		debugLog.ReceiveMessage ( "\tVerifying " + deckManager.masterDeck.cards.Length + " Cards" );
		
		int index = 0;
		while ( index < deckManager.masterDeck.cards.Length )
		{
			
			debugLog.ReceiveMessage ( "\t\tVerifying " + index + " of " + deckManager.masterDeck.cards.Length );
			if ( !File.Exists ( cardsPath + deckManager.masterDeck.cards[index].cardIdentifier + ".png" ))
			{
				
				debugLog.ReceiveMessage ( "\t\t\tCard Does not Exist" );
				
				try
				{
				
					using ( WebClient webClient = new WebClient ())
					{
						
						debugLog.ReceiveMessage ( "\t\t\tDownloading Card" );
						webClient.DownloadFile (( "http://2catstudios.github.io/TradingCardGame/Cards/" + deckManager.masterDeck.cards[index].cardIdentifier + ".png" ), ( cardsPath + deckManager.masterDeck.cards[index].cardIdentifier + ".png" ));
						debugLog.ReceiveMessage ( "\t\t\t" + deckManager.masterDeck.cards[index].cardIdentifier + " Download Successfully" );
					}
				} catch ( Exception e )
				{
					
					debugLog.ReceiveMessage ( "\t\tERROR: " + e );
				}
			} else {
				
				debugLog.ReceiveMessage ( "\t\t\tCard Exists" );
			}
			
			index += 1;
		}
		
		debugLog.ReceiveMessage ( "\tAll Cards Found/Downloaded" );
		
		cards = true;
	}
}