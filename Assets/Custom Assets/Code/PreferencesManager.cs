using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
[XmlRoot ( "Preferences" )]
public class Preferences
{
	
	[XmlElement ( "PlayerName" )]
	public string playerName = "Ember";
}


public class PreferencesManager : MonoBehaviour
{

	internal Preferences preferences = new Preferences ();
}
