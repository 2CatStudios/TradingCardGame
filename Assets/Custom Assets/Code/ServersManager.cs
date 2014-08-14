using UnityEngine;
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



public class ServersManager : MonoBehaviour
{

	internal OfficialServerList officialServerList = new OfficialServerList ();
	internal SavedServerList savedServerList = new SavedServerList ();
}
