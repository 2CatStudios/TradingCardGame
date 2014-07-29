using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	ExternalInformation externalInformation;
	
	public GUISkin guiskin;
	internal Rect homePaneRect;
	Rect controlWindowRect;
	
	GUIStyle labelLargeStyle;
	GUIStyle labelMediumStyle;
	GUIStyle labelSmallStyle;
	
	GUIStyle buttonLargeStyle;
	GUIStyle buttonMediumStyle;
	GUIStyle buttonSmallStyle;
	
	GUIStyle textFieldStyle;
	GUIStyle windowStyle;
	
	GUIStyle hiddenLargeStyle;
	GUIStyle hiddenMediumStyle;
	GUIStyle hiddenSmallStyle;


	
	bool play = false;
	bool hostSection = true;
	string hostPort = "52531";
	
	bool directConnectSection = true;
	string directIP = "192.168.1.1";
	string directPort = "52531";
	string directName = "Server Name";
	
	bool officialConnectSection = true;
	bool savedServerSection = true;
	
	bool options = false;
	
	Color guicolor;
	Vector2 scrollView;
	bool fadeIN = false;
	bool fadeOUT = false;
	
	
	void Start ()
	{
		
		externalInformation = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<ExternalInformation>();
	
		homePaneRect = new Rect ( 0, 0, Screen.width, Screen.height );
		controlWindowRect = new Rect ( Screen.width/2 - 386, 204, 772, 360 );
		
		labelLargeStyle = new GUIStyle ();
		labelLargeStyle.fontSize = 48;
		labelLargeStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelLargeStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelMediumStyle = new GUIStyle ();
		labelMediumStyle.fontSize = 24;
		labelMediumStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelMediumStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		labelSmallStyle = new GUIStyle ();
		labelSmallStyle.fontSize = 16;
		labelSmallStyle.padding = new RectOffset ( 0, 0, 3, 3 );
		labelSmallStyle.margin = new RectOffset ( 4, 4, 4, 4 );
		
		
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
		
		hiddenMediumStyle = new GUIStyle ();
		hiddenMediumStyle.fontSize = 24;
		
		hiddenSmallStyle = new GUIStyle ();
		hiddenSmallStyle.fontSize = 16;
		
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
		GUILayout.Label ( "Tradingcard Game", labelLargeStyle );
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		GUILayout.Label ( "Because OSX 10.10 Doesn't Run Games", labelMediumStyle );
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
		
		if ( play == true )
		{
			
			GUILayout.Window ( 1, controlWindowRect, PlayWindow, "", windowStyle );
		}
		
		if ( options == true )
		{
			
			GUILayout.Window ( 2, controlWindowRect, OptionsWindow, "", windowStyle );
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
			GUILayout.Label ( "Host Match on: ", labelMediumStyle );
			hostPort = GUILayout.TextField ( hostPort, 5, textFieldStyle );
			if ( GUILayout.Button ( "Host", buttonSmallStyle ))
			{
				
				
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
			GUILayout.Label ( "Connect to: ", labelMediumStyle );
			directIP = GUILayout.TextField ( directIP, 22, textFieldStyle, GUILayout.MinWidth ( 120 ));
			directPort = GUILayout.TextField ( directPort, 5, textFieldStyle, GUILayout.MinWidth ( 50 ));
			directName = GUILayout.TextField ( directName, 22, textFieldStyle, GUILayout.MinWidth ( 100 ));
			if ( GUILayout.Button ( "Save Server", buttonSmallStyle ))
			{
				
				externalInformation.SaveServer ( directIP, directPort, directName );
			}
			if ( GUILayout.Button ( "Connect", buttonSmallStyle ))
			{
				
				
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}

		
		GUILayout.Space ( 40 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		if ( GUILayout.Button ( "Official Servers", labelLargeStyle ))
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
				
				GUILayout.Button ( officialServer.name, buttonMediumStyle );
			}
		}
		
		
		if ( externalInformation.savedServerList.savedServers != null && externalInformation.savedServerList.savedServers.Count > 0 )
		{
				
			GUILayout.Space ( 40 );
			GUILayout.BeginHorizontal ();
			GUILayout.Space ( 5 );
			if ( GUILayout.Button ( "Saved Servers", labelLargeStyle ))
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
		
		GUILayout.Label ( "Options", labelLargeStyle );
	
		GUILayout.EndVertical ();
	}
}
