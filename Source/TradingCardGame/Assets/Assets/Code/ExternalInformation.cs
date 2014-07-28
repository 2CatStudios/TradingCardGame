using System;
using System.IO;
using System.Net;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke

[XmlRoot("Servers")]
public class OfficalServerList
{
	
	[XmlElement("Server")]
	public Server[] officialServers;
}

public class Server
{
	
	[XmlAttribute]
	public string featured;
	
	public string name;
	public string host;
	public string description;
	public string ipaddress;
	public string port;
}

public class ExternalInformation : MonoBehaviour
{
	
	internal OfficalServerList officalServerList;
	
	string macPath = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string windowsPath = Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ) + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "TradingCardGame" + Path.DirectorySeparatorChar;
	string path;
	string supportPath;
	
	void Start ()
	{
		
		if ( Environment.OSVersion.ToString ().Substring ( 0, 4 ) == "Unix" )
			path = macPath;
		else
			path = windowsPath;
		
		supportPath = path + "Support" + Path.DirectorySeparatorChar;
		
		if ( !Directory.Exists ( supportPath ))
			Directory.CreateDirectory ( supportPath );
		
		if ( File.Exists ( supportPath + "Servers.xml" ))
			File.Delete ( supportPath + "Servers.xml" );
			
		Uri url = new Uri ( "https://raw.githubusercontent.com/2CatStudios/TradingCardGame/master/Online/Servers.xml" );
		using ( WebClient client = new WebClient ())
		{
				
			client.DownloadFile ( url, supportPath + "Servers.xml" );
		}
	
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( supportPath + "Servers.xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
		
		officalServerList = xml.DeserializeXml<OfficalServerList>();
	}
}
