using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
public class DeckManager : MonoBehaviour
{

	[XmlRoot("MasterDeck")]
	public class SongCollection
	{
		
		[XmlAttribute ( "version" )]
		public String deckVersion;
	
		[XmlElement("Card")]
		public Card[] cards;
	}


	public class Card
	{
	
		[XmlAttribute]
		public String cardIdentifier;
	
		public String name;
		public String health;
		public String element;
		public String focus;
		[XmlElement("attack")]
	         public Attack[] attacks;
	}


	public class Attack
	{
		
		public String name;
		public String baseDamage;
		public String chance;
		public String strengthModifier;
		public String weaknessModifier;
		
		//[XmlElement]
		//public SideEffect sideEffect;
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
				<chance operation='+'>5</chance>
				<strengthModifier operation='*'>1.5</strengthModifier>
				<weaknessModifier operation='*'>0.5</weaknessModifier>
				<SideEffect>
					<name>Wet</name>
					<description>Being wet adds weakness to electricity-based attacks.</description>
					<applyTo>weakness</applyTo>
					<do operation='+'>electricity</do>
					<duration>2</duration>
				</sideEffect>
				<coolDown>3</coolDown>
			</attack>
			<defence>
				<name>Harden</name>
				<baseDefence>4</baseDefence>
				<chance operation='-'>10</chance>
				<strengthModifier operation='*'>1.2</strengthModifier>
				<weaknessModifier operation='*'>0.9</weaknessModifier>
				<SideEffect>
					<name>Tired</name>
					<description>Being tired decreases this card's focus.</description>
					<applyTo>focus</applyTo>
					<do operation='-'>5</do>
					<duration>2</duration>
				</sideEffect>
				<coolDown>0</coolDown>
			</defence>
			<imageIdentifier>1AH0</imageIdentifier>
		</Card>
*/