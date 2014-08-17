using UnityEngine;
using System.Collections;
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
	
	public GUISkin guiskin;
	internal Rect homePaneRect;
	Rect controlWindowRect;
	Rect gameWindowRect;
	
	
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
	GUIStyle windowStyle;
	
	GUIStyle emptyStyle;
	
	
	GUIStyle hiddenCenterLargeStyle;
	GUIStyle hiddenCenterMediumStyle;
	GUIStyle hiddenCenterSmallStyle;
	
	GUIStyle hiddenLeftLargeStyle;
	GUIStyle hiddenLeftMediumStyle;
	GUIStyle hiddenLeftSmallStyle;
	
	Color guicolor;
	

	bool startMenu = false;
	
	
	bool play = false;
	bool hostSection = false;
	string hostPort = "52531";
	
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
	
	bool fadeIN = false;
	bool fadeOUT = false;
	
	
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
		buttonLeftLargeStyle.normal.background = guiskin.button.normal.background;
		buttonLeftLargeStyle.hover.background = guiskin.button.hover.background;
		buttonLeftLargeStyle.active.background = guiskin.button.active.background;
		buttonLeftLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonLeftMediumStyle = new GUIStyle ();
		buttonLeftMediumStyle.fontSize = 24;
		buttonLeftMediumStyle.alignment = TextAnchor.MiddleLeft;
		buttonLeftMediumStyle.normal.background = guiskin.button.normal.background;
		buttonLeftMediumStyle.hover.background = guiskin.button.hover.background;
		buttonLeftMediumStyle.active.background = guiskin.button.active.background;
		buttonLeftMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonLeftSmallStyle = new GUIStyle ();
		buttonLeftSmallStyle.fontSize = 16;
		buttonLeftSmallStyle.alignment = TextAnchor.MiddleLeft;
		buttonLeftSmallStyle.normal.background = guiskin.button.normal.background;
		buttonLeftSmallStyle.hover.background = guiskin.button.hover.background;
		buttonLeftSmallStyle.active.background = guiskin.button.active.background;
		buttonLeftSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLeftSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLeftSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		buttonCenterLargeStyle = new GUIStyle ();
		buttonCenterLargeStyle.fontSize = 48;
		buttonCenterLargeStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterLargeStyle.normal.background = guiskin.button.normal.background;
		buttonCenterLargeStyle.hover.background = guiskin.button.hover.background;
		buttonCenterLargeStyle.active.background = guiskin.button.active.background;
		buttonCenterLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonCenterMediumStyle = new GUIStyle ();
		buttonCenterMediumStyle.fontSize = 24;
		buttonCenterMediumStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterMediumStyle.normal.background = guiskin.button.normal.background;
		buttonCenterMediumStyle.hover.background = guiskin.button.hover.background;
		buttonCenterMediumStyle.active.background = guiskin.button.active.background;
		buttonCenterMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonCenterSmallStyle = new GUIStyle ();
		buttonCenterSmallStyle.fontSize = 16;
		buttonCenterSmallStyle.alignment = TextAnchor.MiddleCenter;
		buttonCenterSmallStyle.normal.background = guiskin.button.normal.background;
		buttonCenterSmallStyle.hover.background = guiskin.button.hover.background;
		buttonCenterSmallStyle.active.background = guiskin.button.active.background;
		buttonCenterSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonCenterSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonCenterSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		textFieldStyle = new GUIStyle ();
		textFieldStyle.font = guiskin.font;
		textFieldStyle.font.material.color = Color.black;
		textFieldStyle.border = new RectOffset ( 4, 4, 4, 4 );
		textFieldStyle.padding = new RectOffset ( 3, 3, 3, 3 );
		textFieldStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		textFieldStyle.normal.background = guiskin.textField.normal.background;
		textFieldStyle.hover.background = guiskin.textField.hover.background;
		
		windowStyle = new GUIStyle ();
		windowStyle.border = new RectOffset ( 6, 6, 6, 4 );
		windowStyle.normal.background = guiskin.button.normal.background;
		windowStyle.onNormal.background = guiskin.button.normal.background;
		
		emptyStyle = new GUIStyle ();
		
		
		hiddenCenterLargeStyle = new GUIStyle ();
		hiddenCenterLargeStyle.fontSize = 48;
		hiddenCenterLargeStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterLargeStyle.hover.background = guiskin.button.normal.background;
		hiddenCenterLargeStyle.active.background = guiskin.button.active.background;
		hiddenCenterLargeStyle.onNormal.background = guiskin.button.active.background;
		hiddenCenterLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenCenterMediumStyle = new GUIStyle ();
		hiddenCenterMediumStyle.fontSize = 24;
		hiddenCenterMediumStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterMediumStyle.hover.background = guiskin.button.normal.background;
		hiddenCenterMediumStyle.active.background = guiskin.button.active.background;
		hiddenCenterMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenCenterSmallStyle = new GUIStyle ();
		hiddenCenterSmallStyle.fontSize = 16;
		hiddenCenterSmallStyle.alignment = TextAnchor.MiddleCenter;
		hiddenCenterSmallStyle.hover.background = guiskin.button.normal.background;
		hiddenCenterSmallStyle.active.background = guiskin.button.active.background;
		hiddenCenterSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenCenterSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenCenterSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		hiddenLeftLargeStyle = new GUIStyle ();
		hiddenLeftLargeStyle.fontSize = 48;
		hiddenLeftLargeStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftLargeStyle.hover.background = guiskin.button.normal.background;
		hiddenLeftLargeStyle.active.background = guiskin.button.active.background;
		hiddenLeftLargeStyle.onNormal.background = guiskin.button.active.background;
		hiddenLeftLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenLeftMediumStyle = new GUIStyle ();
		hiddenLeftMediumStyle.fontSize = 24;
		hiddenLeftMediumStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftMediumStyle.hover.background = guiskin.button.normal.background;
		hiddenLeftMediumStyle.active.background = guiskin.button.active.background;
		hiddenLeftMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenLeftSmallStyle = new GUIStyle ();
		hiddenLeftSmallStyle.fontSize = 16;
		hiddenLeftSmallStyle.alignment = TextAnchor.MiddleLeft;
		hiddenLeftSmallStyle.hover.background = guiskin.button.normal.background;
		hiddenLeftSmallStyle.active.background = guiskin.button.active.background;
		hiddenLeftSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLeftSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLeftSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
		guicolor = new Color ( 1, 1, 1, 0 );
	}
	
	
	void Update ()
	{

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
			}
		}
	}
	
	void OnGUI ()
	{
		
		GUI.skin = guiskin;
		
		
		if ( debugLog.debugLogActive == true )
		{
			
			GUI.skin = guiskin;
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
				
					if ( GUILayout.Button ( "Play", buttonCenterLargeStyle, GUILayout.Width ( 350 )))
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
					
					if ( GUILayout.Button ( "Options", buttonCenterLargeStyle, GUILayout.Width ( 350 )))
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
				
					GUILayout.Window ( 5, gameWindowRect, GameWindow, "", emptyStyle );
				} else {
					
					GUILayout.Window ( 4, controlWindowRect, HostingWindow, "", windowStyle );
				}
			}
		}
		
		GUI.color = Color.white;
		GUI.SetNextControlName ( "" );
	}
	
	
	void StartupWindow ( int windowID )
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
			
			startupWindowScrollPosition.y = Mathf.Infinity;
		}
			
		GUILayout.EndScrollView ();

		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
		
		GUI.FocusWindow ( 0 );
	}
	
	
	void PlayWindow ( int windowID )
	{
		
		playWindowScrollView = GUILayout.BeginScrollView ( playWindowScrollView, GUILayout.Width ( controlWindowRect.width ), GUILayout.Height ( controlWindowRect.height ));
		
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
			GUILayout.Label ( "Host Match on: ", labelLeftMediumStyle );
			hostPort = GUILayout.TextField ( hostPort, 5, textFieldStyle );
			if ( GUILayout.Button ( "Host", buttonCenterSmallStyle ))
			{
				
				UnityEngine.Debug.Log ( "\nInitializing Host" );
				if ( networkManager.SetupHost ( hostPort ))
				{
					
					UnityEngine.Debug.Log ( "\tHosting Enabled Sucessfully" );
				} else {
					
					UnityEngine.Debug.LogError ( "\tUnable to Initialize Hosting" );
				}
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
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
			GUILayout.Label ( "Connect to: ", labelLeftMediumStyle );
			directIP = GUILayout.TextField ( directIP, 22, textFieldStyle, GUILayout.MinWidth ( 120 ));
			directPort = GUILayout.TextField ( directPort, 5, textFieldStyle, GUILayout.MinWidth ( 50 ));
			directName = GUILayout.TextField ( directName, 22, textFieldStyle, GUILayout.MinWidth ( 100 ));
			if ( GUILayout.Button ( "Save Server", buttonCenterSmallStyle ))
			{
				
				UnityEngine.Debug.Log ( "\nSaving Server [" + directIP + ", " + directPort + ", " + directName + "]" );
				externalInformation.SaveServer ( directIP, directPort, directName );
			}
			if ( GUILayout.Button ( "Connect", buttonCenterSmallStyle ))
			{
				
/*				Connect to IP	*/
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
				}
			}
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
					}
					
					if ( GUILayout.Button ( "Delete", buttonCenterMediumStyle, GUILayout.Width ( 100 )))
					{
						
						UnityEngine.Debug.Log ( "\nDeleting Server " + savedServer.name + " (" + savedServer.index + ")" );
						externalInformation.RemoveSavedServer ( savedServer.index );
					}
					GUILayout.EndHorizontal ();
				}
			}
		}
			
		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();
	}
	
	
	void OptionsWindow ( int windowID )
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "General Options", labelLeftLargeStyle );
		

		GUILayout.BeginHorizontal ();
		if ( GUILayout.Button ( "Save", buttonCenterMediumStyle ))
		{
			
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
		GUILayout.Label ( "PlayerName: ", labelLeftMediumStyle );
		preferencesManager.preferences.playerName = GUILayout.TextField ( preferencesManager.preferences.playerName, 12, textFieldStyle, GUILayout.Width ( 120 ));
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.EndScrollView ();
		GUILayout.EndVertical ();
	}
	
	
	void HostingWindow ( int windowID )
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
					}
					
					GUILayout.Space ( 5 );
					if ( GUILayout.Button ( "Match Options", buttonCenterMediumStyle ))
					{
						
						networkManager.options = true;
					}
					
					if ( GUILayout.Button ( "Boot Opponent", buttonCenterMediumStyle ))
					{
						
						UnityEngine.Debug.Log ( "\nBooting Opponent" );
						if ( networkManager.BootOpponent ())
						{
							
							UnityEngine.Debug.Log ( "\tOpponent Disconnected Successfully" );
						} else {
							
							UnityEngine.Debug.LogError ( "\tUnable to Disconnect Opponent" );
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
				
				UnityEngine.Debug.Log ( "\nShutting Down Server" );
				if ( networkManager.ShutdownHost ())
				{
					
					UnityEngine.Debug.Log ( "\tHosting Disabled Successfully" );
				} else {
					
					UnityEngine.Debug.LogError ( "\tUnable to Disable Hosting" );
				}
					
				play = true;
				options = false;
					
				GUI.FocusControl ( "" );
				GUI.FocusWindow ( 1 );
			}
		}
			
		GUILayout.EndVertical ();
	}
	
	
	void GameWindow ( int windowID )
	{
		
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
			
			GUILayout.Label ( "", labelLeftSmallStyle );
			
			GUILayout.Label ( networkManager.opponent.cards.Count + " Cards in Personal Deck", labelLeftMediumStyle );
			GUILayout.Label ( "0 Cards Defeated", labelLeftSmallStyle );
			GUILayout.Label ( "0 Cards Won", labelLeftSmallStyle );
			
		GUILayout.EndVertical ();	
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
			
			if ( GUILayout.Button ( deckManager.masterDeck.supportCards[0].image, hiddenCenterSmallStyle ))
			{
				
				UnityEngine.Debug.Log ( "Draw/Play Card" );
			}
			
		GUILayout.FlexibleSpace ();
		GUILayout.BeginVertical ();
				
			GUILayout.Label ( preferencesManager.preferences.playerName, labelLeftLargeStyle );
			GUILayout.Label ( "1000/1000 HP", labelLeftMediumStyle );
				
			GUILayout.Label ( "", labelLeftSmallStyle );
				
			GUILayout.Label ( deckManager.personalDeck.cards.Count + " Cards in Personal Deck", labelLeftMediumStyle );
			GUILayout.Label ( "0 Cards Defeated", labelLeftSmallStyle );
			GUILayout.Label ( "0 Cards Won", labelLeftSmallStyle );
				
		GUILayout.EndVertical ();
		GUILayout.Space ( 10 );
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}
}
