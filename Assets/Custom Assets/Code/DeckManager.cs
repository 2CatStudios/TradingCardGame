using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
//Written by Michael Bethke
[XmlRoot ( "MasterDeck" )]
public class MasterDeck
{
	
	[XmlAttribute ( "version" )]
	public String deckVersion;
	
	[XmlElement ( "SupportCard" )]
	public SupportCard[] supportCards;

	[XmlElement ( "GameCard" )]
	public GameCard[] gameCards;
}


public class SupportCard
{
	
	[XmlAttribute ( "version" )]
	public String cardVersion;
	
	public String name;
	
	[XmlIgnore]
	public Texture2D image;
}


public class GameCard
{

	[XmlAttribute ( "identifier" )]
	public String cardIdentifier;
	
	public String name;
	public String hitPoints;
	public String focus;
	
	[XmlElement("Action")]
	public Action action;
	
	[XmlIgnore]
	public Texture2D image;
}


public class Action
{
	
	public String name;
	public String baseEffect;
	public String chance;
	
	[XmlAttribute ( "operation" )]
	public String operation;
}

[XmlRoot ( "PersonalDeck" )]
public class PersonalDeck
{
	
	[XmlElement ( "Card" )]
	public List<String> cardIdentifiers = new List<String> ();
}


public class DeckManager : MonoBehaviour
{

	internal MasterDeck masterDeck = new MasterDeck ();
	internal PersonalDeck personalDeck = new PersonalDeck ();
}