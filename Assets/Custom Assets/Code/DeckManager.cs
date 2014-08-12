using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
public class DeckManager : MonoBehaviour
{

	[XmlRoot("MasterDeck")]
	public class MasterDeck
	{
		
		[XmlAttribute ( "version" )]
		public String deckVersion;
	
		[XmlElement("Card")]
		public Card[] cards;
	}


	public class Card
	{
	
		[XmlAttribute ( "version" )]
		public String cardVersion;
		
		public String element;
		public String hitPoints;
		public String focus;
		
		[XmlElement("Action")]
		public Action action;
	}
}

/*

<MasterDeck version='1A'>
	<Card identifier='1A/H/0'>
		<name>Storm Cloud</name>
		<health>100</health>
		<element>Air</element>
		<focus>70</focus>
		<attack>
			<name>Rain</name>
			<baseDamage>4</baseDamage>
			<chance operation='add'>5</chance>
			<strengthModifier>1.5</strengthModifier>
			<weaknessModifier>0.5</weaknessModifier>
			<sideEffect>
				<name>Wet</name>
				<description>Being wet adds weakness to electricity-based attacks.</description>
				<applyTo>weakness</applyTo>
				<do operation='add'>electricity</do>
				<duration>2</duration>
			</sideEffect>
			<coolDown>3</coolDown>
		</attack>
		<defence>
			<name>Harden</name>
			<baseDefence>4</baseDefence>
			<chance operation='subtract'>10</chance>
			<strengthModifier>1.2</strengthModifier>
			<weaknessModifier>0.9</weaknessModifier>
			<sideEffect>
				<name>Tired</name>
				<description>Being tired decreases this card's focus.</description>
				<applyTo>focus</applyTo>
				<do operation='subtract'>5</do>
				<duration>2</duration>
			</sideEffect>
			<coolDown>0</coolDown>
		</defence>
		<imageIdentifier>1AH0</imageIdentifier>
	</Card>
</MasterDeck>

Card (unlimited)
	Name							[The title of the card]
	Health							[The amount of damage this card can take]
	Element 						[The type of damage this card deals]
	Focus 							[The base chance of an action to succeed]
	Attack (unlimited) 				[An action to inflict damage]
		Name 			 		[The title of the action]
		BaseDamage 		 		[The amount of damage that will be done without modifiers]
		Chance 			 		[The likelyhood that this move will succeed (applies to Card's focus)]
		StrengthModifier 		[The amount of damage that will be done to a card of a weaker type]
		WeaknessModifier 		[The amount of damage that will be done to a card of a stronger type]
		SideEffect (unlimited) 	[An effect that will be placed on the card that is attacked]
			Name			[The title of the side effect]
			Description		[An explination of what the side effect does]
			ApplyTo			[What the side effect affects]
			Do				[What the side effect does]
			Duration		[The length of time that the side effect applies]
		CoolDown				[The amount of time that must pass before this move can be used again]
	Defence (unlimited)				[An action to reduce incoming damage]
		Name					[The title of the action]
		BaseDefence				[The amount of damage that will be negated]
		Chance					[The likelyhood that this move will succeed (applies to Card's focus)]
		StrengthModifier		[The amount of damage that will be negated from a weaker type]
		WeaknessModifier		[The amount of damage that will be negated from a stronger type]
		SideEffect (unlimited)	[An effect that will be placed on the card that is attacked]
			Name			[The title of the side effect]
			Description		[An explination of what the side effect does]
			ApplyTo			[What the side effect affects]
			Do				[What the side effect does]
			Duration		[The length of time that the side effect applies]
		CoolDown				[The amount of time that must pass before this move can be used again]
	ImageIdentifier	
*/