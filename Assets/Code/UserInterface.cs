using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	DebugLog debugLog;
	DeckManager deckManager;
	ServersManager serversManager;
	PreferencesManager preferencesManager;
	ExternalInformation externalInformation;
	NetworkManager networkManager;
	LoadingImage loadingImage;
	
	public GUISkin guiSkin;
	public GUISkin emptySkin;

	internal Rect homePaneRect;
	Rect controlWindowRect;
	Rect gameWindowRect;
	Rect chatWindowRect;
	
	
	GUIStyle labelLeftLargeStyle;
	GUIStyle labelLeftMediumStyle;
	GUIStyle labelLeftSmallStyle;
	
	GUIStyle labelCenterLargeStyle;
	GUIStyle labelCenterMediumStyle;
	GUIStyle labelCenterSmallStyle;
	
	
	GUIStyle buttonLeftLargeStyle;
	GUIStyle buttonLeftMediumStyle;
	GUIStyle buttonLeftSmallStyle;
	
	GUIStyle buttonCenterLargeStyle;
	GUIStyle buttonCenterMediumStyle;
	GUIStyle buttonCenterSmallStyle;
	
	
	GUIStyle textFieldStyle;
	GUIStyle toggleStyle;
	GUIStyle windowStyle;
	
	GUIStyle emptyStyle;
	
	
	GUIStyle hiddenCenterLargeStyle;
	GUIStyle hiddenCenterMediumStyle;
	GUIStyle hiddenCenterSmallStyle;
	
	GUIStyle hiddenLeftLargeStyle;
	GUIStyle hiddenLeftMediumStyle;
	GUIStyle hiddenLeftSmallStyle;
	
	
	GUIStyle cardBlueStyle;
	GUIStyle cardRedStyle;
	GUIStyle cardPurpleStyle;
	GUIStyle fieldCardStyle;
	
	Color guicolor;
	

	bool startMenu = false;
	
	
	bool play = false;
	bool hostSection = false;
	
	bool directConnectSection = false;
	string directIP = "192.168.1.1";
	string directPort = "52531";
	string directName = "Server Name";
	
	bool officialConnectSection = false;
	bool savedServerSection = false;
	
	bool options = false;

	
	internal Vector2 debugScrollPosition;
	Vector2 startupWindowScrollPosition;
	Vector2 optionsWindowScrollView;
	Vector2 playWindowScrollView;
	Vector2 gameHandScrollView;
	internal Vector2 chatWindowScrollView;
	
	
	Rect bottomRect;
	Rect bottomActiveZone;
	private float bottomRectVelocity = 0.0F;
	private float chatRectVelocity = 0.0F;
	
	
	bool fadeIN = false;
	bool fadeOUT = false;
	
	
	string chatMessage = "Chat Message";
	
	
	public static string RemoveChar ( string uneditedString )
	{
			
		return Regex.Replace ( uneditedString, "[^0-9.]", "" );
	}
	
	
	void Start ()
	{
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
		deckManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<DeckManager>();
		serversManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<ServersManager>();
		preferencesManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<PreferencesManager>();
		externalInformation = GameObject.FindGameObjectWithTag ( "ExternalInformation" ).GetComponent<ExternalInformation>();
		networkManager = GameObject.FindGameObjectWithTag ( "NetworkManager" ).GetComponent<NetworkManager>();
		loadingImage = gameObject.GetComponent<LoadingImage>();
		
	
		homePaneRect = new Rect ( 0, 0, Screen.width, Screen.height );
		controlWindowRect = new Rect ( Screen.width/2 - 386, 204, 772, 360 );
		gameWindowRect = new Rect ( 0, 0, Screen.width, Screen.height );
		chatWindowRect = new Rect ( Screen.width - 300, 150, 305, Screen.height/2 );
		
		bottomRect = new Rect ( 10, Screen.height - 300, Screen.width - 20, 300 );
		bottomActiveZone = new Rect ( 0, 0, Screen.width, 280 );
		
		
		labelLeftLargeStyle = new GUIStyle ();
		labelLeftLargeStyle.fontSize = 48;
		labelLeftLargeStyle.alignment = TextAnchor.MiddleLeft;
		labelLeftLargeStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelLeftLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelLeftMediumStyle = new GUIStyle ();
		labelLeftMediumStyle.fontSize = 24;
		labelLeftMediumStyle.alignment = TextAnchor.MiddleLeft;
		labelLeftMediumStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelLeftMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelLeftSmallStyle = new GUIStyle ();
		labelLeftSmallStyle.fontSize = 16;
		labelLeftSmallStyle.wordWrap = true;
		labelLeftSmallStyle.alignment = TextAnchor.MiddleLeft;
		labelLeftSmallStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelLeftSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		labelCenterLargeStyle = new GUIStyle ();
		labelCenterLargeStyle.fontSize = 48;
		labelCenterLargeStyle.alignment = TextAnchor.MiddleCenter;
		labelCenterLargeStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelCenterLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelCenterMediumStyle = new GUIStyle ();
		labelCenterMediumStyle.fontSize = 24;
		labelCenterMediumStyle.alignment = TextAnchor.MiddleCenter;
		labelCenterMediumStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelCenterMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelCenterSmallStyle = new GUIStyle ();
		labelCenterSmallStyle.fontSize = 16;
		labelCenterSmallStyle.alignment = TextAnchor.MiddleCenter;
		labelCenterSmallStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelCenterSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		buttonLeftLargeStyle = new GUIStyle ();
		buttonLeftLargeStyle.fontSize = 48;
		buttonLeftLargeStyle.alignment = TextAnchor.MiddleLeft;
		buttonLeftLargeStyle.normal.background = guiSkin.button.normal.background;
		buttonLeftLargeStyle.hover.background = guiSkin.button.hover.background;
		buttonLeftLargeStyle.active.background = guiSkin.button.active.background;
		buttonLeftLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonLeftMediumStyle = new GUIStyle ();
		buttonLeftMediumStyle.fontSize = 24;
		buttonLeftMediumStyle.alignment = TextAnchor.MiddleLeft;
		buttonLeftMediumStyle.normal.background = guiSkin.button.normal.background;
		buttonLeftMediumStyle.hover.background = guiSkin.button.hover.background;
		buttonLeftMediumStyle.active.background = guiSkin.button.active.background;
		buttonLeftMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonLeftSmallStyle = new GUIStyle ();
		buttonLeftSmallStyle.fontSize = 16;
		buttonLeftSmallStyle.alignment = TextAnchor.MiddleLeft;
		buttonLeftSmallStyle.normal.background = guiSkin.button.normal.background;
		buttonLeftSmallStyle.hover.background = guiSkin.button.hover.background;
		buttonLeftSmallStyle.active.background = guiSkin.button.active.background;
		buttonLeftSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		buttonCenterLargeStyle = new GUIStyle ();
		buttonCenterLargeStyle.fontSize = 48;
		buttonCenterLargeStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterLargeStyle.normal.background = guiSkin.button.normal.background;
		buttonCenterLargeStyle.hover.background = guiSkin.button.hover.background;
		buttonCenterLargeStyle.active.background = guiSkin.button.active.background;
		buttonCenterLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonCenterMediumStyle = new GUIStyle ();
		buttonCenterMediumStyle.fontSize = 24;
		buttonCenterMediumStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterMediumStyle.normal.background = guiSkin.button.normal.background;
		buttonCenterMediumStyle.hover.background = guiSkin.button.hover.background;
		buttonCenterMediumStyle.active.background = guiSkin.button.active.background;
		buttonCenterMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonCenterSmallStyle = new GUIStyle ();
		buttonCenterSmallStyle.fontSize = 16;
		buttonCenterSmallStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterSmallStyle.normal.background = guiSkin.button.normal.background;
		buttonCenterSmallStyle.hover.background = guiSkin.button.hover.background;
		buttonCenterSmallStyle.active.background = guiSkin.button.active.background;
		buttonCenterSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		textFieldStyle = new GUIStyle ();
		textFieldStyle.font = guiSkin.font;
		textFieldStyle.font.material.color = Color.black;
		textFieldStyle.border = new RectOffset ( 4, 4, 4, 4 );
		textFieldStyle.padding = new RectOffset ( 3, 3, 3, 3 );
		textFieldStyle.margin = new RectOffset ( 5, 5, 5, 5 );
		textFieldStyle.normal.background = guiSkin.textField.normal.background;
		textFieldStyle.hover.background = guiSkin.textField.hover.background;
		
		toggleStyle = new GUIStyle ();
		toggleStyle.normal.background = guiSkin.customStyles[1].active.background;
		toggleStyle.hover.background = guiSkin.customStyles[1].normal.background;
		toggleStyle.onNormal.background = guiSkin.customStyles[0].active.background;
		toggleStyle.onHover.background = guiSkin.customStyles[0].normal.background;
		toggleStyle.border = new RectOffset ( 5, 5, 5, 5 );
		toggleStyle.padding = new RectOffset ( 2, 2, 2, 2 );
		toggleStyle.margin = new RectOffset ( 5, 5, 5, 5 );
		toggleStyle.overflow = new RectOffset ( 0, 0, 2, 0 );
		toggleStyle.fixedWidth = 24;
		toggleStyle.fixedHeight = 24;
		
		windowStyle = new GUIStyle ();
		windowStyle.border = new RectOffset ( 6, 6, 6, 4 );
		windowStyle.normal.background = guiSkin.customStyles[0].hover.background;
		windowStyle.onNormal.background = guiSkin.customStyles[0].hover.background;
		
		emptyStyle = new GUIStyle ();
		emptyStyle.fontSize = 48;
		emptyStyle.alignment = TextAnchor.MiddleLeft;
		emptyStyle.border = new RectOffset ( 6, 6, 6, 4 );
		emptyStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		emptyStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		hiddenCenterLargeStyle = new GUIStyle ();
		hiddenCenterLargeStyle.fontSize = 48;
		hiddenCenterLargeStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterLargeStyle.hover.background = guiSkin.button.normal.background;
		hiddenCenterLargeStyle.active.background = guiSkin.button.active.background;
		hiddenCenterLargeStyle.onNormal.background = guiSkin.button.active.background;
		hiddenCenterLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenCenterMediumStyle = new GUIStyle ();
		hiddenCenterMediumStyle.fontSize = 24;
		hiddenCenterMediumStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterMediumStyle.hover.background = guiSkin.button.normal.background;
		hiddenCenterMediumStyle.active.background = guiSkin.button.active.background;
		hiddenCenterMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenCenterSmallStyle = new GUIStyle ();
		hiddenCenterSmallStyle.fontSize = 16;
		hiddenCenterSmallStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterSmallStyle.hover.background = guiSkin.button.normal.background;
		hiddenCenterSmallStyle.active.background = guiSkin.button.active.background;
		hiddenCenterSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		hiddenLeftLargeStyle = new GUIStyle ();
		hiddenLeftLargeStyle.fontSize = 48;
		hiddenLeftLargeStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftLargeStyle.hover.background = guiSkin.button.normal.background;
		hiddenLeftLargeStyle.active.background = guiSkin.button.active.background;
		hiddenLeftLargeStyle.onNormal.background = guiSkin.button.active.background;
		hiddenLeftLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenLeftMediumStyle = new GUIStyle ();
		hiddenLeftMediumStyle.fontSize = 24;
		hiddenLeftMediumStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftMediumStyle.hover.background = guiSkin.button.normal.background;
		hiddenLeftMediumStyle.active.background = guiSkin.button.active.background;
		hiddenLeftMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenLeftSmallStyle = new GUIStyle ();
		hiddenLeftSmallStyle.fontSize = 16;
		hiddenLeftSmallStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftSmallStyle.hover.background = guiSkin.button.normal.background;
		hiddenLeftSmallStyle.active.background = guiSkin.button.active.background;
		hiddenLeftSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		cardBlueStyle = new GUIStyle ();
		cardBlueStyle.fontSize = 48;
		cardBlueStyle.alignment = TextAnchor.MiddleLeft;
		cardBlueStyle.hover.background = guiSkin.customStyles[0].active.background;
		cardBlueStyle.active.background = guiSkin.customStyles[0].hover.background;
		cardBlueStyle.border = new RectOffset ( 6, 6, 6, 4 );
		cardBlueStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		cardBlueStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		cardRedStyle = new GUIStyle ();
		cardRedStyle.fontSize = 48;
		cardRedStyle.alignment = TextAnchor.MiddleLeft;
		cardRedStyle.hover.background = guiSkin.customStyles[1].active.background;
		cardRedStyle.active.background = guiSkin.customStyles[1].hover.background;
		cardRedStyle.border = new RectOffset ( 6, 6, 6, 4 );
		cardRedStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		cardRedStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		cardPurpleStyle = new GUIStyle ();
		cardPurpleStyle.fontSize = 48;
		cardPurpleStyle.alignment = TextAnchor.MiddleLeft;
		cardPurpleStyle.hover.background = guiSkin.customStyles[2].active.background;
		cardPurpleStyle.active.background = guiSkin.customStyles[2].hover.background;
		cardPurpleStyle.border = new RectOffset ( 6, 6, 6, 4 );
		cardPurpleStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		cardPurpleStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		fieldCardStyle = cardBlueStyle;
		
		
		guicolor = new Color ( 1, 1, 1, 0 );
	}
	
	
	void Update ()
	{
		
		if ( Input.GetKeyDown ( KeyCode.Escape ))
		{
			
			if ( networkManager.connectionType == NetworkManager.ConnectionType.Playing && networkManager.hosting == true )
			{
				
				if ( deckManager.heldCard != null )
				{
					
					deckManager.hand.cards.Insert ( deckManager.heldFromDeckIndex, deckManager.heldCard );
					deckManager.heldCard = null;
					
				}
			}
		}
		

		if ( fadeIN == true )
		{
			
			if ( guicolor.a < 0.99f )
			{

				guicolor.a = Mathf.SmoothDamp ( guicolor.a, 1.0f, ref guicolor.a, 0.05f );
			} else {

				fadeIN = false;	
			}
		}
		
		if ( fadeOUT == true )
		{
			
			if ( guicolor.a > 0.01f )
			{
			
				guicolor.a = Mathf.SmoothDamp ( guicolor.a, 0.0f, ref guicolor.a, 0.05f );
			} else {

				play = false;
				options = false;
				fadeOUT = false;
				hostSection = false;
				directConnectSection = false;
				officialConnectSection = false;
				savedServerSection = false;
			}
		}
		
		
		if ( bottomActiveZone.Contains ( Input.mousePosition ))
		{
			
			if ( bottomRect.y != Screen.height - 275 )
			{
				
				float bottomRectYUp = Mathf.SmoothDamp ( bottomRect.y, Screen.height - 275, ref bottomRectVelocity, 0.05f );
				float chatRectXOut = Mathf.SmoothDamp ( chatWindowRect.x, Screen.width - 300, ref chatRectVelocity, 0.05f );
				bottomRect = new Rect ( 10, bottomRectYUp, Screen.width - 20, 300 );
				chatWindowRect = new Rect ( chatRectXOut, 150, 305, Screen.height/2 );
			}
			
			fieldCardStyle = emptyStyle;
			
		} else {
			
			if ( bottomRect.y != Screen.height - 100 )
			{
				
				float bottomRectYDown = Mathf.SmoothDamp ( bottomRect.y, Screen.height - 100, ref bottomRectVelocity, 0.05f );
				float chatRectXOut = Mathf.SmoothDamp ( chatWindowRect.x, Screen.width - 10, ref chatRectVelocity, 0.05f );
				bottomRect = new Rect ( 10, bottomRectYDown, Screen.width - 20, 300 );
				chatWindowRect = new Rect ( chatRectXOut, 150, 305, Screen.height/2 );
			}
			
			fieldCardStyle = cardBlueStyle;
		}
	}
	
	
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		
		if ( debugLog.debugLogActive == true )
		{
			
			GUILayout.BeginArea ( new Rect ( 0, 0, Screen.width, Screen.height ));
			debugScrollPosition = GUILayout.BeginScrollView ( debugScrollPosition, false, false, GUILayout.Width ( Screen.width ), GUILayout.Height ( Screen.height ));
			GUILayout.FlexibleSpace ();
			
			for ( int index = 0; index < debugLog.debugLog.Count; index += 1 )
			{
				
				GUILayout.Label ( debugLog.debugLog[index] );
			}
				
			GUILayout.EndScrollView ();
			GUILayout.EndArea ();
		}
		
		
#region BaseMenu
		
		if ( startMenu == false )
		{
			
			GUILayout.Window ( 0, new Rect ( Screen.width/2 - 386, 204, 772, 360 ), StartupWindow, "", windowStyle );
			
		} else {
		
			if ( networkManager.connectionType != NetworkManager.ConnectionType.Playing )
			{
			
				GUILayout.BeginArea ( homePaneRect );
				GUILayout.BeginVertical ();
				
				GUILayout.Space ( 5 );
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Tradingcard Game", labelLeftLargeStyle );
				GUILayout.FlexibleSpace ();
				
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Because OSX 10.10 Doesn't Run Games", labelLeftMediumStyle );
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
				GUILayout.Space ( 50 );
				
				if ( networkManager.connectionType == NetworkManager.ConnectionType.None )
				{
				
					GUILayout.BeginHorizontal ();
					GUILayout.FlexibleSpace ();
				
					if ( GUILayout.Button ( "Play", buttonCenterLargeStyle, GUILayout.Width ( 350 )) && fadeOUT == false && fadeIN == false )
					{
						
						if ( play == false )
						{
							
							play = true;
							options = false;
							fadeIN = true;
							GUI.FocusControl ( "" );
							GUI.FocusWindow ( 2 );
						} else {
							
							fadeOUT = true;
							fadeIN = false;
						}
					}
					
					if ( GUILayout.Button ( "Options", buttonCenterLargeStyle, GUILayout.Width ( 350 )) && fadeOUT == false && fadeIN == false )
					{
						
						if ( options == false )
						{
						
							options = true;
							play = false;
							fadeIN = true;
							GUI.FocusControl ( "" );
							GUI.FocusWindow ( 3 );
						} else {
					
							fadeOUT = true;
							fadeIN = false;
						}
					}
				
					GUILayout.FlexibleSpace ();
					GUILayout.EndHorizontal ();
				}
			
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}

#endregion
		
			GUI.color = guicolor;
			
			if ( networkManager.connectionType == NetworkManager.ConnectionType.None )
			{
			
				if ( play == true  )
				{
					
					GUILayout.Window ( 2, controlWindowRect, PlayWindow, "", windowStyle );
				}
				
				if ( options == true )
				{
					
					GUILayout.Window ( 3, controlWindowRect, OptionsWindow, "", windowStyle );
				}
			}
			
			if ( networkManager.hosting == true )
			{
				
				if ( networkManager.connectionType == NetworkManager.ConnectionType.Playing )
				{
				
					GUILayout.Window ( 6, gameWindowRect, GameWindow, "", emptyStyle );
					
					if ( preferencesManager.preferences.allowChat != "False" )
					{
						
						GUI.Window ( 7, chatWindowRect, ChatWindow, "", windowStyle );
					}
				} else {
					
					GUILayout.Window ( 5, controlWindowRect, HostingWindow, "", windowStyle );
				}
			}
			
			if ( networkManager.connecting == true )
			{
				
				
			}
		}
		
		GUI.color = Color.white;
		GUI.SetNextControlName ( "" );
	}
	
	
	void StartupWindow ( int windowID ) // Window 0
	{
		
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		GUILayout.Label ( "TradingCard Game", labelCenterLargeStyle );
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		
		startupWindowScrollPosition = GUILayout.BeginScrollView ( startupWindowScrollPosition, false, false );
		GUILayout.FlexibleSpace ();
		
		for ( int index = 0; index < debugLog.debugLog.Count; index += 1 )
		{
			
			GUILayout.Label ( debugLog.debugLog[index] );
		}
		
		if ( externalInformation.startup == false )
		{
			
			if ( GUILayout.Button ( "Click to Continue", buttonCenterLargeStyle ))
			{
				
				Screen.SetResolution ( 1617, 910, false );
				startMenu = true;
			}
		} else {
			
			if ( externalInformation.error == false )
			{
				
				startupWindowScrollPosition.y = Mathf.Infinity;
			}
		}
			
		GUILayout.EndScrollView ();

		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
		
		GUI.FocusWindow ( 0 );
	}
	
	
	void PlayWindow ( int windowID ) // Window 2
	{
		
		playWindowScrollView = GUILayout.BeginScrollView ( playWindowScrollView, false, false, GUILayout.Width ( controlWindowRect.width ), GUILayout.Height ( controlWindowRect.height ));
		
		GUILayout.BeginVertical ();
		
		GUILayout.Space ( 5 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Host Match", hiddenCenterLargeStyle ))
		{
			
			if ( hostSection == true )
				hostSection = false;
			else
				hostSection = true;
		}
		GUILayout.EndHorizontal ();
		
		if ( hostSection == true )
		{
			
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ( "Host Match on: ", labelLeftMediumStyle );
			networkManager.activePort = GUILayout.TextField ( networkManager.activePort, 5, textFieldStyle );
			if ( GUILayout.Button ( "Host", buttonCenterSmallStyle ))
			{
				
				debugLog.ReceiveMessage ( "\nInitializing Host" );
				if ( networkManager.SetupHost ())
				{
					
					debugLog.ReceiveMessage ( "\tHosting Enabled Sucessfully" );
				} else {
					
					debugLog.ReceiveMessage ( "\tERROR: Unable to Initialize Hosting" );
				}
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}
		
		
		GUILayout.Space ( 40 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Official Servers", hiddenCenterLargeStyle ))
		{
			
			if ( officialConnectSection == true )
				officialConnectSection = false;
			else
				officialConnectSection = true;
		}
		GUILayout.EndHorizontal ();
		
		if ( officialConnectSection == true )
		{
		
			foreach ( OfficialServer officialServer in serversManager.officialServerList.officialServers )
			{
				
				if ( GUILayout.Button ( officialServer.name, buttonLeftMediumStyle ))
				{
					
/*					Connect to Official Server	*/		
					networkManager.AttemptConnection ( officialServer.ipaddress, officialServer.port );
				}
			}
		}
		
		
		GUILayout.Space ( 40 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Direct Connection", hiddenCenterLargeStyle ))
		{
			
			if ( directConnectSection == true )
				directConnectSection = false;
			else
				directConnectSection = true;
		}
		GUILayout.EndHorizontal ();
		
		if ( directConnectSection == true )
		{
				
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ( "Connect to: ", labelLeftMediumStyle );
			directIP = GUILayout.TextField ( RemoveChar ( directIP ), 22, textFieldStyle, GUILayout.MinWidth ( 120 ));
			directPort = GUILayout.TextField ( RemoveChar ( directPort ), 5, textFieldStyle, GUILayout.MinWidth ( 50 ));
			directName = GUILayout.TextField ( directName, 22, textFieldStyle, GUILayout.MinWidth ( 105 ));
			if ( GUILayout.Button ( "Save Server", buttonCenterSmallStyle ))
			{
				
				debugLog.ReceiveMessage ( "\nSaving Server [" + directIP + ", " + directPort + ", " + directName + "]" );
				externalInformation.SaveServer ( directIP, directPort, directName );
			}
			if ( GUILayout.Button ( "Connect", buttonCenterSmallStyle ))
			{
				
				networkManager.AttemptConnection ( directIP, directPort );
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}
		
		
		if ( serversManager.savedServerList.savedServers != null && serversManager.savedServerList.savedServers.Count > 0 )
		{
				
			GUILayout.Space ( 40 );
			GUILayout.BeginHorizontal ();
			GUILayout.Space ( 5 );
			if ( GUILayout.Button ( "Saved Servers", hiddenCenterLargeStyle ))
			{
					
				if ( savedServerSection == true )
					savedServerSection = false;
				else
					savedServerSection = true;
			}
			GUILayout.EndHorizontal ();
				
			if ( savedServerSection == true )
			{
				
				foreach ( SavedServer savedServer in serversManager.savedServerList.savedServers )
				{
					
					GUILayout.BeginHorizontal ();
					if ( GUILayout.Button ( savedServer.name, buttonLeftMediumStyle ))
					{
						
/*						Connect to Saved Server	*/
						networkManager.AttemptConnection ( savedServer.ipaddress, savedServer.port );	
					}
					
					if ( GUILayout.Button ( "Delete", buttonCenterMediumStyle, GUILayout.Width ( 100 )))
					{
						
						debugLog.ReceiveMessage ( "\nDeleting Server " + savedServer.name + " (" + savedServer.index + ")" );
						externalInformation.RemoveSavedServer ( savedServer.index );
					}
					GUILayout.EndHorizontal ();
				}
			}
		}
			
		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();
	}
	
	
	void OptionsWindow ( int windowID ) // Window 3
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "General Options", labelLeftLargeStyle );
		

		GUILayout.BeginHorizontal ();
		if ( GUILayout.Button ( "Save", buttonCenterMediumStyle ))
		{
			
			preferencesManager.preferences.allowChat = preferencesManager.tempAllowChat.ToString ();
			externalInformation.WritePreferences ();
		}
		
		if ( GUILayout.Button ( "Revert", buttonCenterMediumStyle ))
		{
			
			externalInformation.ReadPreferences ();
		}
		
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.Label ( "", labelLeftSmallStyle );
		
		
		optionsWindowScrollView = GUILayout.BeginScrollView ( optionsWindowScrollView );
		
		GUILayout.BeginHorizontal ();
		preferencesManager.preferences.playerName = GUILayout.TextField ( preferencesManager.preferences.playerName, 12, textFieldStyle, GUILayout.Width ( 120 ));
		GUILayout.Label ( "PlayerName", labelLeftMediumStyle );
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		preferencesManager.tempAllowChat = GUILayout.Toggle ( preferencesManager.tempAllowChat, "", toggleStyle );
		GUILayout.Label ( "Allow Chat", labelLeftMediumStyle );
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.EndScrollView ();
		GUILayout.EndVertical ();
	}
	
	
	void ConnectingWindow ( int windowID ) // Window 4
	{
		
		
	}
	
	
	void HostingWindow ( int windowID ) // Window 5
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		
		if ( networkManager.info == false )
		{
		
			if ( networkManager.hosting == true && networkManager.connectionType == NetworkManager.ConnectionType.Hosting )
			{
			
				GUILayout.Label ( "Waiting for Opponent", labelCenterMediumStyle );
				
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( loadingImage.CurrentLoadingImage ());
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}
			
			if ( networkManager.hosting == true && networkManager.connectionType == NetworkManager.ConnectionType.Connected )
			{
				
				GUILayout.Label ( preferencesManager.preferences.playerName + " VS " + networkManager.opponent.name, labelCenterLargeStyle );
				
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Match length: Indefinite", labelLeftMediumStyle );
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Cards in MasterDeck: " + deckManager.masterDeck.gameCards.Length, labelLeftMediumStyle );
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
				
				if ( networkManager.options == false )
				{
				
					GUILayout.Space ( 20 );
					if ( GUILayout.Button ( "Begin Match", buttonCenterLargeStyle ))
					{
						
						deckManager.SetupDecks ();
						networkManager.connectionType = NetworkManager.ConnectionType.Playing;
						networkManager.BrodcastChatMessage ( "Match between " + preferencesManager.preferences.playerName + " and " + networkManager.opponent.name + " started at " + System.DateTime.Now.ToString ( "HH:mm" ) + "!" );
					}
					
					GUILayout.Space ( 5 );
					if ( GUILayout.Button ( "Match Options", buttonCenterMediumStyle ))
					{
						
						networkManager.options = true;
					}
					
					if ( GUILayout.Button ( "Boot Opponent", buttonCenterMediumStyle ))
					{
						
						debugLog.ReceiveMessage ( "\nBooting Opponent" );
						if ( networkManager.BootOpponent ())
						{
							
							debugLog.ReceiveMessage ( "\tOpponent Disconnected Successfully" );
						} else {
							
							debugLog.ReceiveMessage ( "\tERROR: Unable to Disconnect Opponent" );
						}
					}
				
				} else {
				
					GUILayout.FlexibleSpace ();
					if ( GUILayout.Button ( "Back", buttonCenterMediumStyle ))
					{
						
						networkManager.options = false;
					}
				}
			}
		} else {
		
			GUILayout.Label ( networkManager.infoString, labelCenterMediumStyle );
		}
		
		if ( networkManager.options == false )
		{
			
			GUILayout.FlexibleSpace ();
			if ( GUILayout.Button ( "Disable Hosting", buttonCenterMediumStyle ))
			{
				
				debugLog.ReceiveMessage ( "\nShutting Down Server" );
				if ( networkManager.ShutdownHost ())
				{
					
					debugLog.ReceiveMessage ( "\tHosting Disabled Successfully" );
				} else {
					
					debugLog.ReceiveMessage ( "\tERROR: Unable to Disable Hosting" );
				}
					
				play = true;
				options = false;
					
				GUI.FocusControl ( "" );
				GUI.FocusWindow ( 1 );
			}
		}
			
		GUILayout.EndVertical ();
	}
	
	
	void GameWindow ( int windowID ) // Window 6
	{
		
		GUI.skin = guiSkin;
		
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
			
			GUILayout.Label ( preferencesManager.preferences.playerName + " VS " + networkManager.opponent.name, labelCenterLargeStyle );
		
		GUILayout.FlexibleSpace ();	
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 10 );
		GUILayout.BeginVertical ();
			
			GUILayout.Label ( networkManager.opponent.name, labelLeftLargeStyle );
			GUILayout.Label ( "1000/1000 HP", labelLeftMediumStyle );
			
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
    	GUILayout.FlexibleSpace ();
			
			int fieldOpponentIndex = 0;
			while ( fieldOpponentIndex < 3 )
			{
				
				GUILayout.Button ( deckManager.field.opponentCards[fieldOpponentIndex].image, cardRedStyle );
				fieldOpponentIndex += 1;
			}
    	         
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.Space ( 10 );
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
    	         
			int fieldPlayerIndex = 0;
			while ( fieldPlayerIndex < 3 )
			{
				
				if ( deckManager.field.playerCards[fieldPlayerIndex].cardIdentifier.Substring ( 0, 1 ) == "R" )
				{
					
					fieldCardStyle = cardPurpleStyle;
				} else {
					
					fieldCardStyle = cardBlueStyle;
				}
				
				if ( GUILayout.Button ( deckManager.field.playerCards[fieldPlayerIndex].image, fieldCardStyle ))
				{
		
					if ( deckManager.heldCard != null )
					{
			
						deckManager.field.playerCards[fieldPlayerIndex] = deckManager.heldCard;
						deckManager.heldCard = null;
						deckManager.heldFromDeckIndex = 0;
					}
				}
			     
				fieldPlayerIndex += 1;
			}
		         
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginArea ( bottomRect );
		GUILayout.BeginHorizontal ();
			
			if ( GUILayout.Button ( deckManager.masterDeck.supportCards[0].image, cardBlueStyle ))
			{
				
				if ( deckManager.hand.cards.Count < 4 && deckManager.heldCard == null )
				{
					
					deckManager.hand.cards.Add ( deckManager.masterDeck.gameCards[UnityEngine.Random.Range ( 0, deckManager.masterDeck.gameCards.Length )] );
				}
			}
			
		GUILayout.FlexibleSpace ();
		gameHandScrollView = GUILayout.BeginScrollView ( gameHandScrollView, windowStyle, GUILayout.Width ( 836 ), GUILayout.Height ( 270 ));
		GUILayout.BeginHorizontal ();
		
			for ( int handIndex = deckManager.hand.cards.Count - 1; handIndex >= 0; handIndex -= 1 )
			{
				
				if ( GUILayout.Button ( deckManager.hand.cards[handIndex].image, cardBlueStyle, GUILayout.Width ( 204 )))
				{
					
					if ( deckManager.heldCard == null )
					{
					
						deckManager.heldCard = deckManager.hand.cards[handIndex];
						deckManager.hand.cards.RemoveAt ( handIndex );
						deckManager.heldFromDeckIndex = handIndex;
					}
				}
			}

		GUILayout.EndHorizontal ();
		GUILayout.EndScrollView ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginVertical ();
				
			GUILayout.Label ( preferencesManager.preferences.playerName, labelLeftLargeStyle );
			GUILayout.Label ( "1000/1000 HP", labelLeftMediumStyle );
			
			if ( preferencesManager.preferences.allowChat == "True" )
			{
				
				chatMessage = GUILayout.TextField ( chatMessage, textFieldStyle, GUILayout.Width ( 166 ));
				if ( GUILayout.Button ( "Send Message", buttonCenterMediumStyle ) || Event.current.keyCode == KeyCode.Return )
				{
					
					networkManager.SendChatMessage ( chatMessage );
					chatMessage = "";
				}
			} else {
				
				GUILayout.Label ( "", labelLeftSmallStyle );
			}
			
			if ( GUILayout.Button ( "End Turn", buttonCenterMediumStyle ))
			{
				
				networkManager.EndTurn ();
			}
			
			GUILayout.Label ( "", labelLeftSmallStyle );
			
			if ( GUILayout.Button ( "Leave Game", buttonCenterMediumStyle ))
			{
				
				networkManager.ShutdownHost ();
			}
				
		GUILayout.EndVertical ();
		GUILayout.Space ( 10 );
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		GUILayout.EndVertical ();
		
		if ( deckManager.heldCard != null )
		{

			GUI.Label ( new Rect ( Input.mousePosition.x, Screen.height - Input.mousePosition.y, 192, 256 ), deckManager.heldCard.image );
		}
	}
	
	
	void ChatWindow ( int windowID ) // Window 7
	{
		
		GUI.skin = emptySkin;
		
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		GUILayout.BeginVertical ();
		chatWindowScrollView = GUILayout.BeginScrollView ( chatWindowScrollView, false, false, GUILayout.Width ( 298 ));
		
		int chatMessageIndex = 0;
		while ( chatMessageIndex < networkManager.chatMessages.Count )
		{
			    	
			GUILayout.Label ( networkManager.chatMessages[chatMessageIndex], labelLeftSmallStyle );
			chatMessageIndex += 1;
		}
		
		GUILayout.EndScrollView ();
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
		
		chatWindowScrollView.y = Mathf.Infinity;
	}
}
