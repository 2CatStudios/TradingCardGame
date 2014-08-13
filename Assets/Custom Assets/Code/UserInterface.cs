using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	DebugLog debugLog;
	ExternalInformation externalInformation;
	NetworkManager networkManager;
	LoadingImage loadingImage;
	
	public GUISkin guiskin;
	internal Rect homePaneRect;
	Rect controlWindowRect;
	Rect gameWindowRect;
	
	GUIStyle labelLeftLargeStyle;
	GUIStyle labelMiddleLargeStyle;
	GUIStyle labelLeftMediumStyle;
	GUIStyle labelMiddleMediumStyle;
	GUIStyle labelLeftSmallStyle;
	GUIStyle labelMiddleSmallStyle;
	
	GUIStyle buttonLargeStyle;
	GUIStyle buttonMediumStyle;
	GUIStyle buttonSmallStyle;
	
	GUIStyle textFieldStyle;
	GUIStyle windowStyle;
	GUIStyle emptyStyle;
	
	GUIStyle hiddenLargeStyle;
	GUIStyle hiddenMediumStyle;
	GUIStyle hiddenSmallStyle;

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
	string playerName = "Ember";
	
	Color guicolor;
	Vector2 scrollView;
	Vector2 scrollPosition;
	Vector2 startupWindowScrollPosition;
	bool fadeIN = false;
	bool fadeOUT = false;
	
	
	void Start ()
	{
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
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
		
		labelMiddleLargeStyle = new GUIStyle ();
		labelMiddleLargeStyle.fontSize = 48;
		labelMiddleLargeStyle.alignment = TextAnchor.MiddleCenter;
		labelMiddleLargeStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelMiddleLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelMiddleMediumStyle = new GUIStyle ();
		labelMiddleMediumStyle.fontSize = 24;
		labelMiddleMediumStyle.alignment = TextAnchor.MiddleCenter;
		labelMiddleMediumStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelMiddleMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelMiddleSmallStyle = new GUIStyle ();
		labelMiddleSmallStyle.fontSize = 16;
		labelMiddleSmallStyle.alignment = TextAnchor.MiddleCenter;
		labelMiddleSmallStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelMiddleSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonLargeStyle = new GUIStyle ();
		buttonLargeStyle.fontSize = 48;
		buttonLargeStyle.alignment = TextAnchor.MiddleCenter;
		buttonLargeStyle.normal.background = guiskin.button.normal.background;
		buttonLargeStyle.hover.background = guiskin.button.hover.background;
		buttonLargeStyle.active.background = guiskin.button.active.background;
		buttonLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonMediumStyle = new GUIStyle ();
		buttonMediumStyle.fontSize = 24;
		buttonMediumStyle.alignment = TextAnchor.MiddleCenter;
		buttonMediumStyle.normal.background = guiskin.button.normal.background;
		buttonMediumStyle.hover.background = guiskin.button.hover.background;
		buttonMediumStyle.active.background = guiskin.button.active.background;
		buttonMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		buttonSmallStyle = new GUIStyle ();
		buttonSmallStyle.fontSize = 16;
		buttonSmallStyle.alignment = TextAnchor.MiddleCenter;
		buttonSmallStyle.normal.background = guiskin.button.normal.background;
		buttonSmallStyle.hover.background = guiskin.button.hover.background;
		buttonSmallStyle.active.background = guiskin.button.active.background;
		buttonSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		buttonSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		buttonSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );	
		
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
		
		hiddenLargeStyle = new GUIStyle ();
		hiddenLargeStyle.fontSize = 48;
		hiddenLargeStyle.hover.background = guiskin.button.normal.background;
		hiddenLargeStyle.active.background = guiskin.button.active.background;
		hiddenLargeStyle.onNormal.background = guiskin.button.active.background;
		hiddenLargeStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenLargeStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenMediumStyle = new GUIStyle ();
		hiddenMediumStyle.fontSize = 24;
		hiddenMediumStyle.hover.background = guiskin.button.normal.background;
		hiddenMediumStyle.active.background = guiskin.button.active.background;
		hiddenMediumStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenMediumStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		hiddenSmallStyle = new GUIStyle ();
		hiddenSmallStyle.fontSize = 16;
		hiddenSmallStyle.hover.background = guiskin.button.normal.background;
		hiddenSmallStyle.active.background = guiskin.button.active.background;
		hiddenSmallStyle.border = new RectOffset ( 6, 6, 6, 4 );
		hiddenSmallStyle.padding = new RectOffset ( 6, 6, 3, 3 );
		hiddenSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
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
			scrollPosition = GUILayout.BeginScrollView ( scrollPosition, false, false, GUILayout.Width ( Screen.width ), GUILayout.Height ( Screen.height ));
			GUILayout.FlexibleSpace ();
			
			for ( int index = 0; index < debugLog.debugLog.Count; index += 1 )
			{
				
				GUILayout.Label ( debugLog.debugLog[index] );
			}
			
			scrollPosition.y = Mathf.Infinity;
				
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
				
					if ( GUILayout.Button ( "Play", buttonLargeStyle, GUILayout.Width ( 350 )))
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
					
					if ( GUILayout.Button ( "Options", buttonLargeStyle, GUILayout.Width ( 350 )))
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
		GUILayout.Label ( "TradingCard Game", labelMiddleLargeStyle );
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
			
			if ( GUILayout.Button ( "Click to Continue", buttonLargeStyle ))
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
		
		scrollView = GUILayout.BeginScrollView ( scrollView, GUILayout.Width ( controlWindowRect.width ), GUILayout.Height ( controlWindowRect.height ));
		
		GUILayout.BeginVertical ();
		
		GUILayout.Space ( 5 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Host Match", hiddenLargeStyle ))
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
			if ( GUILayout.Button ( "Host", buttonSmallStyle ))
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
		if ( GUILayout.Button ( "Direct Connection", hiddenLargeStyle ))
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
			if ( GUILayout.Button ( "Save Server", buttonSmallStyle ))
			{
				
				UnityEngine.Debug.Log ( "\nSaving Server [" + directIP + ", " + directPort + ", " + directName + "]" );
				externalInformation.SaveServer ( directIP, directPort, directName );
				
				/*if ( externalInformation.SaveServer ( directIP, directPort, directName ))
				{
					
					UnityEngine.Debug.Log ( "\tServer Saved Successfully" );
				} else {
					
					UnityEngine.Debug.LogError ( "\tUnable to Save Server" );
				}*/
			}
			if ( GUILayout.Button ( "Connect", buttonSmallStyle ))
			{
				
/*				Connect to IP	*/
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}

		
		GUILayout.Space ( 40 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Official Servers", hiddenLargeStyle ))
		{
			
			if ( officialConnectSection == true )
				officialConnectSection = false;
			else
				officialConnectSection = true;
		}
		GUILayout.EndHorizontal ();
		
		if ( officialConnectSection == true )
		{
		
			foreach ( OfficialServer officialServer in externalInformation.officialServerList.officialServers )
			{
				
				if ( GUILayout.Button ( officialServer.name, buttonMediumStyle ))
				{
					
/*					Connect to Official Server	*/					
				}
			}
		}
		
		
		if ( externalInformation.savedServerList.savedServers != null && externalInformation.savedServerList.savedServers.Count > 0 )
		{
				
			GUILayout.Space ( 40 );
			GUILayout.BeginHorizontal ();
			GUILayout.Space ( 5 );
			if ( GUILayout.Button ( "Saved Servers", hiddenLargeStyle ))
			{
					
				if ( savedServerSection == true )
					savedServerSection = false;
				else
					savedServerSection = true;
			}
			GUILayout.EndHorizontal ();
				
			if ( savedServerSection == true )
			{
				
				foreach ( SavedServer savedServer in externalInformation.savedServerList.savedServers )
				{
					
					GUILayout.BeginHorizontal ();
					if ( GUILayout.Button ( savedServer.name, buttonMediumStyle ))
					{
						
/*						Connect to Saved Server	*/						
					}
					
					if ( GUILayout.Button ( "Delete", buttonMediumStyle, GUILayout.Width ( 100 )))
					{
						
						UnityEngine.Debug.Log ( "\nDeleting Server " + savedServer.name + " (" + savedServer.index + ")" );
						externalInformation.RemoveSavedServer ( savedServer.index );
						
						/*if ( externalInformation.RemoveSavedServer ( savedServer.index ))
						{
							
							UnityEngine.Debug.Log ( "\tServer Deleted Successfully" );
						} else {
							
							UnityEngine.Debug.LogError ( "\tUnable to Delete Server" );
						}*/
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
		
		GUILayout.Label ( "Options", labelLeftLargeStyle );
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ( "PlayerName: ", labelLeftMediumStyle );
		playerName = GUILayout.TextField ( playerName, 12, textFieldStyle, GUILayout.MinWidth ( 120 ));
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
	
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
			
				GUILayout.Label ( "Waiting for Opponent", labelMiddleMediumStyle );
				
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( loadingImage.CurrentLoadingImage ());
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
			}
			
			if ( networkManager.hosting == true && networkManager.connectionType == NetworkManager.ConnectionType.Connected )
			{
				
				GUILayout.Label ( playerName + " VS " + networkManager.opponentName, labelMiddleLargeStyle );
				
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Match length: Indefinite", labelLeftMediumStyle );
				GUILayout.FlexibleSpace ();
				GUILayout.Label ( "Cards in MasterDeck: 0", labelLeftMediumStyle );
				GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal ();
				
				if ( networkManager.options == false )
				{
				
					GUILayout.Space ( 20 );
					if ( GUILayout.Button ( "Begin Match", buttonLargeStyle ))
					{
						
						networkManager.connectionType = NetworkManager.ConnectionType.Playing;
					}
					
					GUILayout.Space ( 5 );
					if ( GUILayout.Button ( "Match Options", buttonMediumStyle ))
					{
						
						networkManager.options = true;
					}
					
					if ( GUILayout.Button ( "Boot Opponent", buttonMediumStyle ))
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
					if ( GUILayout.Button ( "Back", buttonMediumStyle ))
					{
						
						networkManager.options = false;
					}
				}
			}
		} else {
		
			GUILayout.Label ( networkManager.infoString, labelMiddleMediumStyle );
		}
		
		if ( networkManager.options == false )
		{
			
			GUILayout.FlexibleSpace ();
			if ( GUILayout.Button ( "Disable Hosting", buttonMediumStyle ))
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
		GUILayout.Label ( "Game goes here.", labelMiddleLargeStyle );
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();

		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
	}
}
