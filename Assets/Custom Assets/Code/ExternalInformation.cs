using System;
using System.IO;
using System.Xml;
using System.Net;
using UnityEngine;
using System.Linq;
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
	
	internal OfficialServerList officialServerList = new OfficialServerList ();
	internal SavedServerList savedServerList = new SavedServerList ();
	
	string macPath = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string windowsPath = Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ) + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string path;
	string supportPath;
	
	void Start ()
	{
		
		if ( Environment.OSVersion.ToString ().Substring ( 0, 4 ) == "Unix" )
		{
			
			path = macPath;
			UnityEngine.Debug.Log ( "Client running on Mac OS" );
		} else {
			
			path = windowsPath;
			UnityEngine.Debug.Log ( "Client running on Windows OS" );
		}
		
		UnityEngine.Debug.Log ( "Application Size: " + Screen.width + " x " + Screen.height );
		UnityEngine.Debug.Log ( "Screen Size: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height );
		UnityEngine.Debug.Log ( "Refresh Rate (If Obtainable): " + Screen.currentResolution.refreshRate );
		
		UnityEngine.Debug.Log ( "\nInitializing Local Directories" );
		if ( SetupLocalDirectories ())
		{
			
			UnityEngine.Debug.Log ( "\tLocal Directories Setup Successfully" );
		} else {
			
			UnityEngine.Debug.LogError ( "\tUnable to Setup Local Directories" );
		}
		
		UnityEngine.Debug.Log ( "\nLoading Saved Servers" );
		if ( FetchSavedServers ())
		{
			
			UnityEngine.Debug.Log ( "\tSaved Servers Index Loaded Successfully" );
		} else {
			
			UnityEngine.Debug.LogError ( "\tUnable to Load Saved Servers" );
		}
		
		UnityEngine.Debug.Log ( "\nInitialize MasterDeck" );
		if ( InitializeMasterDeck ())
		{
			
			UnityEngine.Debug.Log ( "\tMasterDeck Updated" );
		} else {
			
			UnityEngine.Debug.LogError ( "\tUnable to Update MasterDeck" );
		}
		
		UnityEngine.Debug.Log ( "\nDownloading Official Server Index" );
		if ( FetchOfficialServers ())
		{
			
			UnityEngine.Debug.Log ( "\tOfficial Server Index Loaded Successfully" );
		} else {
			
			UnityEngine.Debug.LogError ( "\tUnable to Download Official Server Index" );
		}
		
		UnityEngine.Debug.Log ( "\nStartup Completed" );
	}
	
	
	bool SetupLocalDirectories ()
	{
		
		supportPath = path + "Support" + Path.DirectorySeparatorChar;
		
		if ( !Directory.Exists ( supportPath ))
		{
			
			Directory.CreateDirectory ( supportPath );
			UnityEngine.Debug.Log ( "\tSupport Path has been created" );
		} else {
			
			UnityEngine.Debug.Log ( "\tSupport Path exists" );
		}
		
		return true;
	}
	
	
	bool FetchSavedServers ()
	{
		
		if ( File.Exists ( supportPath + "SavedServers.xml" ))
		{
			
			try
			{
				
				UnityEngine.Debug.Log ( "\tReading Saved Servers" );
				
				System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "SavedServers.xml" );
				string xml = streamReader.ReadToEnd();
				streamReader.Close();
				
				UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
				savedServerList = xml.DeserializeXml<SavedServerList>();
				
				UnityEngine.Debug.Log ( "\t\tDeserialized" );
			} catch ( Exception e )
			{
					
				UnityEngine.Debug.LogError ( "\tERROR: " + e );
			}
		} else {
			
			UnityEngine.Debug.LogWarning ( "\tNo Saved Servers Found" );
		}
		
		return true;
	}
	
	
	internal bool SaveServer ( string ip, string port, string name )
	{
		
		try {
		
			savedServerList.savedServers.Clear ();
			UnityEngine.Debug.Log ( "\tSaved Servers Cleared from Memory" );
		
			if ( File.Exists ( supportPath + "SavedServers.xml" ))
			{
				
				UnityEngine.Debug.Log ( "\tReading Current Saved Servers to Memory" );
				
				System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "SavedServers.xml" );
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
			StreamWriter writer = new StreamWriter ( supportPath + "SavedServers.xml" );
			serializer.Serialize ( writer.BaseStream, savedServerList );
			
			UnityEngine.Debug.Log ( "\tUpdated Saved Servers List Written to Disk" );

		} catch ( Exception e )
		{
			
			UnityEngine.Debug.LogError ( "\tERROR: " + e );
			return false;
		}
		
		return true;
	}
	
	
	internal bool RemoveSavedServer ( int index )
	{
		
		XmlDocument doc = new XmlDocument();
		
		UnityEngine.Debug.Log ( "\tReading Saved Server List into Memory" );
		
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "SavedServers.xml" );
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
			
			doc.Save ( supportPath + "SavedServers.xml" );
			
			UnityEngine.Debug.Log ( "\tLoading Updated Saved Servers" );
			if ( FetchSavedServers ())
			{
			
				UnityEngine.Debug.Log ( "\tSaved Servers Index Loaded Successfully" );
			} else {
			
				UnityEngine.Debug.LogError ( "\tUnable to Load Saved Servers" );
			}
		} else {
			
			UnityEngine.Debug.LogError ( "\t\tUnable to Locate Node" );
			
			return false;
		}
		
		return true;
	}
	
	
	bool InitializeMasterDeck ()
	{
		
		try
		{
				
			if ( !File.Exists ( supportPath + "MasterDeck.xml" ))
			{
				
				UnityEngine.Debug.Log ( "\tLocal MasterDeck does not Exist" );
				
				Uri url = new Uri ( "http://2catstudios.github.io/TradingCardGame/MasterDeck.xml" );
				using ( WebClient wClient = new WebClient ())
				{
					
					wClient.DownloadFile ( url, supportPath + "MasterDeck.xml" );
				}
				
				UnityEngine.Debug.Log ( "\tMasterDeck Downloaded Successfully" );
				
			} else if ( false == true ) {
				
				//Confirm Deck Version
				
				UnityEngine.Debug.Log ( "\tLocal MasterDeck exists. Checking Version" );
				
			}
		} catch ( Exception e ) {
		
			UnityEngine.Debug.LogError ( "\tERROR: " + e );
			return false;
		}
		
		/*System.IO.StreamReader deckReader = new System.IO.StreamReader ( supportPath + "MasterDeck.xml" );
		string deckXML = deckReader.ReadToEnd();
		deckReader.Close();*/
	
		UnityEngine.Debug.Log ( "\t\tRead into Memory" );
		
		//officialServerList = xml.DeserializeXml<OfficialServerList>();
		
		UnityEngine.Debug.Log ( "\t\tDeserialized" );
	
		//preferences = deckXML.DeserializeXml<Preferences> ();
		
		return true;
	}
	
	
	bool FetchOfficialServers ()
	{
		
		if ( File.Exists ( supportPath + "OfficialServers.xml" ))
		{
			
			File.Delete ( supportPath + "OfficialServers.xml" );
			UnityEngine.Debug.Log ( "\tOld Official Server List Deleted" );
		}
		
		try {
			
			Uri url = new Uri ( "http://2catstudios.github.io/TradingCardGame/OfficialServers.xml" );
			using ( WebClient client = new WebClient ())
			{
					
				client.DownloadFile ( url, supportPath + "OfficialServers.xml" );
			}
			UnityEngine.Debug.Log ( "\tOfficial Server Log Downloaded" );
	    	
			System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "OfficialServers.xml" );
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			
			UnityEngine.Debug.Log ( "\t\tRead into Memory" );
			
			officialServerList = xml.DeserializeXml<OfficialServerList>();
			
			UnityEngine.Debug.Log ( "\t\tDeserialized" );
		} catch ( Exception e ) {
			
			UnityEngine.Debug.LogError ( "\tERROR: " + e );
			return false;
		}
		
		return true;
	}
}
