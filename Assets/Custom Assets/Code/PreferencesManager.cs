using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
[XmlRoot ( "Preferences" )]
public class Preferences
{
	
	[XmlElement ( "PlayerName" )]
	public string playerName = "Ember";
	
	[XmlElement ( "AllowChat" )]
	public string allowChat = "True";
}


public class PreferencesManager : MonoBehaviour
{

	internal Preferences preferences = new Preferences ();
	internal bool tempAllowChat = true;
}
