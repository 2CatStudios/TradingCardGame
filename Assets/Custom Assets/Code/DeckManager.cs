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
	
	public GameCard ToGameCard ()
	{
		
		return new GameCard ( cardVersion, name, "0", "0", new Action (), image );
	}
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
	
	public GameCard () : this ( "", "", "", "", null, null ) {}
	public GameCard ( string _cardIdentifier, string _name, string _hitPoints, string _focus, Action _action, Texture2D _image )
	{

		this.cardIdentifier = _cardIdentifier;
		this.name = _name;
		this.hitPoints = _hitPoints;
		this.focus = _focus;
		this.action = _action;
		this.image = _image;
	}
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
	
	internal Hand hand;
	internal Field field;
	
	internal GameCard heldCard;
	internal int heldFromDeckIndex;
	
	
	public void SetupDecks ()
	{
		
		hand = new Hand ();
		field = new Field ();
		
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length )] );
		hand.cards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length )] );
		
		int setupFieldIndex = 0;
		while ( setupFieldIndex < 3 )
		{
			
			field.opponentCards.Add ( masterDeck.gameCards[UnityEngine.Random.Range ( 0, masterDeck.gameCards.Length )]);
			field.playerCards.Add ( masterDeck.supportCards[1].ToGameCard ());
			setupFieldIndex += 1;
		}
		
		heldCard = null;
	}
}