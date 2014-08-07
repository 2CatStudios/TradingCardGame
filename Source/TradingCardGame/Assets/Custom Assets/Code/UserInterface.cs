using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	ExternalInformation externalInformation;
	NetworkManager networkManager;
	LoadingImage loadingImage;
	
	public GUISkin guiskin;
	internal Rect homePaneRect;
	Rect controlWindowRect;
	
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
	
	GUIStyle hiddenLargeStyle;
	GUIStyle hiddenMediumStyle;
	GUIStyle hiddenSmallStyle;


	
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
	
	Color guicolor;
	Vector2 scrollView;
	bool fadeIN = false;
	bool fadeOUT = false;
	
	
	void Start ()
	{
		
		externalInformation = GameObject.FindGameObjectWithTag ( "ExternalInformation" ).GetComponent<ExternalInformation>();
		networkManager = GameObject.FindGameObjectWithTag ( "NetworkManager" ).GetComponent<NetworkManager>();
		loadingImage = gameObject.GetComponent<LoadingImage>();
	
		homePaneRect = new Rect ( 0, 0, Screen.width, Screen.height );
		controlWindowRect = new Rect ( Screen.width/2 - 386, 204, 772, 360 );
		
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
		
#region BaseMenu
		
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
				GUI.FocusWindow ( 1 );
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
				GUI.FocusWindow ( 2 );
			} else {
		
				fadeOUT = true;
				fadeIN = false;
			}
		}
		
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
#endregion
#region Windows
		
		GUI.color = guicolor;
		
		if ( play == true && networkManager.connectionType == NetworkManager.ConnectionType.None )
		{
			
			GUILayout.Window ( 1, controlWindowRect, PlayWindow, "", windowStyle );
		}
		
		if ( options == true && networkManager.connectionType == NetworkManager.ConnectionType.None )
		{
			
			GUILayout.Window ( 2, controlWindowRect, OptionsWindow, "", windowStyle );
		}
		
		if ( networkManager.connectionType == NetworkManager.ConnectionType.Hosting )
		{
			
			GUILayout.Window ( 3, controlWindowRect, HostingWindow, "", windowStyle );
		}
		
		GUI.color = Color.white;
		
#endregion
		
		GUI.SetNextControlName ( "" );	
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
				
				networkManager.SetupHost ( hostPort );
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
				
				externalInformation.SaveServer ( directIP, directPort, directName );
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
		
		GUILayout.Label ( "Options", labelLeftLargeStyle );
		GUILayout.Label ( "Nothing lives here right now.", labelLeftMediumStyle );
	
		GUILayout.EndVertical ();
	}
	
	
	void HostingWindow ( int windowID )
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "Waiting for Opponent", labelMiddleLargeStyle );
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.Label ( loadingImage.CurrentLoadingImage ());
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.FlexibleSpace ();
		if ( GUILayout.Button ( "Disable Hosting", buttonMediumStyle ))
		{
			
			networkManager.ShutdownHost ();
			
			play = true;
			options = false;
			
			GUI.FocusControl ( "" );
			GUI.FocusWindow ( 1 );
		}
		
		GUILayout.EndVertical ();
	}
}
