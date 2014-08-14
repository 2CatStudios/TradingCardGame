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
public class ExternalInformation : MonoBehaviour
{
	
	DebugLog debugLog;
	DeckManager deckManager;
	ServersManager serversManager;
	PreferencesManager preferencesManager;
	
	internal bool startup = true;
	
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
		deckManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<DeckManager>();
		serversManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<ServersManager>();
		preferencesManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<PreferencesManager>();
		
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
		
		if ( !Directory.Exists ( path ))
		{
			
			Directory.CreateDirectory ( path );
		}
		
		if ( !Directory.Exists ( cardsPath ))
		{
			
			Directory.CreateDirectory ( cardsPath );
		}
		
		if ( !File.Exists ( path + "PersonalDeck.xml" ))
		{
			
			debugLog.ReceiveMessage ( "\tPersonal Deck does not Exist" );
			
			WritePersonalDeck ();
			
			debugLog.ReceiveMessage ( "\tPersonal Deck Created" );
		}
		
		ReadPersonalDeck ();
		debugLog.ReceiveMessage ( "\tPersonal Deck Loaded" );
		
		if ( !File.Exists ( path + "Preferences.xml" ))
		{
			
			debugLog.ReceiveMessage ( "\tPreferences File does not Exist" );
			
			WritePreferences ();
			debugLog.ReceiveMessage ( "\tNew Preferences File Created" );
		}
		
		ReadPreferences ();
		debugLog.ReceiveMessage ( "\tPreferences Loaded" );
		
		debugLog.ReceiveMessage ( "\tGame Directories have been Created" );
		
		localDirectories = true;
		debugLog.ReceiveMessage ( "\tLocal Directories Setup Successfully" );
		
		Thread loadSavedServersThread = new Thread (() => FetchSavedServers ( true ));
		loadSavedServersThread.Start ();
	}
	
	
	void FetchSavedServers ( bool delay )
	{
		
		debugLog.ReceiveMessage ( "\nLoading Saved Servers" );
		
		if ( delay == true )
		{
			
			Thread.Sleep ( 500 );
		}
		
		if ( File.Exists ( path + "SavedServers.xml" ))
		{
			
			try
			{
				
				debugLog.ReceiveMessage ( "\tReading Saved Servers" );
				
				StreamReader streamReaderFS = new StreamReader ( path + "SavedServers.xml" );
				string xmlFS = streamReaderFS.ReadToEnd();
				streamReaderFS.Close();
				
				debugLog.ReceiveMessage ( "\t\tRead into Memory" );
		
				serversManager.savedServerList = xmlFS.DeserializeXml<SavedServerList>();
				
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
		
			serversManager.savedServerList.savedServers.Clear ();
			UnityEngine.Debug.Log ( "\tSaved Servers Cleared from Memory" );
		
			if ( File.Exists ( path + "SavedServers.xml" ))
			{
				
				UnityEngine.Debug.Log ( "\tReading Current Saved Servers to Memory" );
				
				StreamReader streamReaderS = new StreamReader ( path + "SavedServers.xml" );
				string xmlS = streamReaderS.ReadToEnd();
				streamReaderS.Close();
				
				UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
				serversManager.savedServerList = xmlS.DeserializeXml<SavedServerList>();
				
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
			
			newServer.index = serversManager.savedServerList.savedServers.Count + 1;
			UnityEngine.Debug.Log ( "\t\t" + newServer.index );
			
			serversManager.savedServerList.savedServers.Add ( newServer );
			
			UnityEngine.Debug.Log ( "\tNew Server Added to Memory" );
			
			XmlSerializer serializerS = new XmlSerializer ( serversManager.savedServerList.GetType ());
			using ( StreamWriter writerS = new StreamWriter ( path + "SavedServers.xml" ))
			{
				
				serializerS.Serialize ( writerS.BaseStream, serversManager.savedServerList );
			
				UnityEngine.Debug.Log ( "\tUpdated Saved Servers List Written to Disk" );
			}

		} catch ( Exception e )
		{
			
			UnityEngine.Debug.LogError ( "\tERROR: " + e );
		}
	}
	
	
	internal void RemoveSavedServer ( int index )
	{
		
		XmlDocument doc = new XmlDocument();
		
		UnityEngine.Debug.Log ( "\tReading Saved Server List into Memory" );
		
		StreamReader streamReaderRS = new StreamReader ( path + "SavedServers.xml" );
		string xmlRS = streamReaderRS.ReadToEnd();
		streamReaderRS.Close();
		
		UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
		doc.LoadXml ( xmlRS );
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
		
		debugLog.ReceiveMessage ( "\nDownloading Official Server Index" );
		
		Thread.Sleep ( 1000 );

		try {
			
			using ( StreamReader streamReader = new StreamReader ( WebRequest.Create ( "http://2catstudios.github.io/TradingCardGame/OfficialServers.xml" ).GetResponse ().GetResponseStream ()))
			{
				
				string xml = streamReader.ReadToEnd ();
			
				debugLog.ReceiveMessage ( "\t\tRead into Memory" );
			
				serversManager.officialServerList = xml.DeserializeXml<OfficialServerList>();
			
				debugLog.ReceiveMessage ( "\t\tDeserialized" );
			}
		} catch ( Exception e ) {
			
			debugLog.ReceiveMessage ( "\tERROR: " + e );
		}
		
		officialServers = true;
		
		Thread downloadMasterDeckThread = new Thread ( new ThreadStart ( DownloadMasterDeck ));
		downloadMasterDeckThread.Start ();
	}
	
	
	void DownloadMasterDeck ()
	{
		
		debugLog.ReceiveMessage ( "\nDownloading MasterDeck" );
		
		Thread.Sleep ( 1000 );
		
		try
		{
			
			StreamReader reader = new StreamReader ( WebRequest.Create ( "http://2catstudios.github.io/TradingCardGame/MasterDeck.xml" ).GetResponse ().GetResponseStream ());
			string deckXML = reader.ReadToEnd ();
			
			debugLog.ReceiveMessage ( "\t\tDownloaded and Read into Memory" );
			
			deckManager.masterDeck = deckXML.DeserializeXml<MasterDeck>();
			debugLog.ReceiveMessage ( "\t\tDeserialized" );
			
		} catch ( Exception e ) {
			
			debugLog.ReceiveMessage ( "\tERROR: " + e );
		}
		
		masterDeck = true;
		
	    Thread initializeCardsThread = new Thread ( new ThreadStart ( InitializeCards ));
		initializeCardsThread.Start ();
	}
	
	
	void InitializeCards ()
	{
		
		debugLog.ReceiveMessage ( "\nInitialize Cards" );
		
		Thread.Sleep ( 1000 );
		
		debugLog.ReceiveMessage ( "\tVerifying " + deckManager.masterDeck.cards.Length + " Cards" );
		
		int index = 0;
		while ( index < deckManager.masterDeck.cards.Length )
		{
			
			debugLog.ReceiveMessage ( "\t\tVerifying " + index + " of " + ( deckManager.masterDeck.cards.Length - 1 ));
			if ( !File.Exists ( cardsPath + deckManager.masterDeck.cards[index].cardIdentifier + ".png" ))
			{
				
				debugLog.ReceiveMessage ( "\t\t\tCard Does not Exist - Attempting Download" );
				
				try
				{
				
					using ( WebClient webClient = new WebClient ())
					{

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
	
	
	public void WritePersonalDeck ()
	{
		
		XmlSerializer serializerPD = new XmlSerializer ( deckManager.personalDeck.GetType ());
			
		using ( StreamWriter writerPD = new StreamWriter ( path + "PersonalDeck.xml" ))
		{
				
			serializerPD.Serialize ( writerPD.BaseStream, deckManager.personalDeck );
		}
	}
	
	
	public void ReadPersonalDeck ()
	{
		
		using ( StreamReader streamReaderPD = new StreamReader ( path + "PersonalDeck.xml" ))
		{
			
			string xmlPD = streamReaderPD.ReadToEnd();

			deckManager.personalDeck = xmlPD.DeserializeXml<PersonalDeck>();
		}
	}
	
	
	public void WritePreferences ()
	{
		
		XmlSerializer serializerP = new XmlSerializer ( preferencesManager.preferences.GetType ());
		
		using ( StreamWriter writerP = new StreamWriter ( path + "Preferences.xml" ))
		{
			
			serializerP.Serialize ( writerP.BaseStream, preferencesManager.preferences );
		}
	}
	
	
	public void ReadPreferences ()
	{
		
		using ( StreamReader streamReaderP = new StreamReader ( path + "Preferences.xml" ))
		{
			
			string xmlP = streamReaderP.ReadToEnd();
			
			preferencesManager.preferences = xmlP.DeserializeXml<Preferences>();
		}
	}
}