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

[XmlRoot("OfficialServers")]
public class OfficialServerList
{
	
	[XmlElement("Server")]
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

[XmlRoot("SavedServers")]
public class SavedServerList
{
	
	[XmlElement("Server")]
	public SavedServer[] savedServers;
}

public class SavedServer
{
	
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
		
		SetupLocalDirectories ();
		UnityEngine.Debug.Log ( "Local directories setup" );
		
		FetchOfficialServers ();
		UnityEngine.Debug.Log ( "Offical servers loaded" );
		
		FetchSavedServers ();
		UnityEngine.Debug.Log ( "Saved servers loaded" );
	}
	
	
	void SetupLocalDirectories ()
	{
		
		if ( Environment.OSVersion.ToString ().Substring ( 0, 4 ) == "Unix" )
			path = macPath;
		else
			path = windowsPath;
		
		supportPath = path + "Support" + Path.DirectorySeparatorChar;
		
		if ( !Directory.Exists ( supportPath ))
			Directory.CreateDirectory ( supportPath );
	}
	
	
	void FetchOfficialServers ()
	{
		
		if ( File.Exists ( supportPath + "OfficialServers.xml" ))
			File.Delete ( supportPath + "OfficialServers.xml" );
			
		Uri url = new Uri ( "https://raw.githubusercontent.com/2CatStudios/TradingCardGame/master/Online/Servers.xml" );
		using ( WebClient client = new WebClient ())
		{
				
			client.DownloadFile ( url, supportPath + "OfficialServers.xml" );
		}
	
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "OfficialServers.xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
		
		officialServerList = xml.DeserializeXml<OfficialServerList>();
	}
	
	
	void FetchSavedServers ()
	{
		
		if ( File.Exists ( supportPath + "SavedServers.xml" ))
		{
			
			try
			{
				
				System.IO.StreamReader savedServerReader = new System.IO.StreamReader ( supportPath + "SavedServers.xml" );
				string savedServerXML = savedServerReader.ReadToEnd();
				savedServerReader.Close();
		
				savedServerList = savedServerXML.DeserializeXml<SavedServerList> ();
			} catch ( Exception e )
			{
					
				UnityEngine.Debug.Log ( e );
			}
		} else {
			
			UnityEngine.Debug.Log ( "SavedServers.xml doesn't exist!" );
		}
	}
	
	
	internal void SaveServer ( string ip, string port, string name )
	{
			
		List<SavedServer> serverList = new List<SavedServer>();
		
		if ( File.Exists ( supportPath + "SavedServers.xml" ))
		{
			
			System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "SavedServers.xml" );
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
		
			savedServerList = xml.DeserializeXml<SavedServerList>();
			serverList = savedServerList.savedServers.ToList ();
		}
		
		SavedServer serverToAdd = new SavedServer ();
		serverToAdd.ipaddress = ip;
		serverToAdd.port = port;
		serverToAdd.name = name;
		
		
		serverList.Add ( serverToAdd );
		
		XmlSerializer serializer = new XmlSerializer ( typeof ( SavedServerList ) );
		StreamWriter writer = new StreamWriter ( supportPath + "SavedServers.xml" );
		serializer.Serialize ( writer.BaseStream, serverList );
		
		/*System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer ( typeof ( Book ));
		System.IO.StreamWriter file = new System.IO.StreamWriter ( @"c:\temp\SerializationOverview.xml" );
		writer.Serialize ( file, overview );
		file.Close();*/
		
		UnityEngine.Debug.Log ( "Saved without fault" );
	}
}
