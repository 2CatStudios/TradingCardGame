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

[XmlRoot ( "PersonalDeck" )]
public class PersonalDeck
{
	
	[XmlElement ( "Card" )]
	public List<String> cardIdentifiers = new List<String> ();
	
	[XmlIgnore]
	public List<GameCard> cards = new List<GameCard> ();
}


public class Hand
{
	
	public List<GameCard> cards = new List<GameCard> ();
	public List<int> removeList = new List<int> ();
}


public class Field
{
	
	public List<GameCard> playerCards = new List<GameCard> ();
	public List<GameCard> opponentCards = new List<GameCard> ();
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


public class DeckManager : MonoBehaviour
{

	internal MasterDeck masterDeck = new MasterDeck ();
	internal PersonalDeck personalDeck = new PersonalDeck ();
	
	internal Hand hand = new Hand ();
	internal Field field = new Field ();
	
	
	public void SetupDecks ()
	{
		
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length - 1 )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length - 1 )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length - 1 )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length - 1 )] );

	}
}