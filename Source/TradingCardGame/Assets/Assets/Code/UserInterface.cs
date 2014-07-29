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
	
	bool host = false;
	bool connect = false;
	
	bool fadeIN = false;
	bool fadeOUT = false;
	
	string directIP = "192.168.1.1";
	string directName = "Server Name";
	string directPort = "52531";
	
	Vector2 scrollView;
	
	Color guicolor;
	
	
	void Start ()
	{
		
		externalInformation = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<ExternalInformation>();
	
		homePaneRect = new Rect ( 0, 0, Screen.width, Screen.height );
		controlWindowRect = new Rect ( 14, 204, 772, 360 );
		
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

				host = false;
				connect = false;
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
		
		if ( GUILayout.Button ( "Host", buttonLargeStyle, GUILayout.Width ( 350 )))
		{
			
			if ( host == false )
			{
				
				host = true;
				connect = false;
				fadeIN = true;
				GUI.FocusControl ( "" );
				GUI.FocusWindow ( 1 );
			} else {
				
				fadeOUT = true;
				fadeIN = false;
			}
		}
		
		GUILayout.FlexibleSpace ();
		
		if ( GUILayout.Button ( "Connect", buttonLargeStyle, GUILayout.Width ( 350 )))
		{
			
			if ( connect == false )
			{
			
				connect = true;
				host = false;
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
		
		if ( host == true )
		{
			
			GUILayout.Window ( 1, controlWindowRect, HostingWindow, "", windowStyle );
		}
		
		if ( connect == true )
		{
			
			GUILayout.Window ( 2, controlWindowRect, ConnectingWindow, "", windowStyle );
		}
		
		GUI.color = Color.white;
		
#endregion
		
		GUI.SetNextControlName ( "" );	
	}
	
	
	void HostingWindow ( int windowID )
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "Hosting Window", labelLargeStyle );
		
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}
	
	
	void ConnectingWindow ( int windowID )
	{
		
		scrollView = GUILayout.BeginScrollView ( scrollView, GUILayout.Width ( controlWindowRect.width ), GUILayout.Height ( controlWindowRect.height ));
		
		GUILayout.BeginVertical ();
		
		GUILayout.Space ( 5 );
		GUILayout.Label ( "Direct Connection", labelLargeStyle );
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ( "Connect to: ", labelMediumStyle );
		directIP = GUILayout.TextField ( directIP, 22, textFieldStyle, GUILayout.MinWidth ( 120 ));
		GUILayout.FlexibleSpace ();
		directPort = GUILayout.TextField ( directPort, 5, textFieldStyle, GUILayout.MinWidth ( 50 ));
		GUILayout.FlexibleSpace ();
		directName = GUILayout.TextField ( directName, 22, textFieldStyle, GUILayout.MinWidth ( 100 ));
		GUILayout.FlexibleSpace ();
		if ( GUILayout.Button ( "Save Server", buttonSmallStyle ))
		{
			
			externalInformation.SaveServer ( directIP, directPort, directName );
		}
		GUILayout.FlexibleSpace ();
		if ( GUILayout.Button ( "Connect", buttonSmallStyle ))
		{
			
			
		}
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		
		GUILayout.Space ( 40 );
		GUILayout.Label ( "Official Servers", labelLargeStyle );
		
		foreach ( OfficialServer officialServer in externalInformation.officialServerList.officialServers )
		{
			
			GUILayout.Button ( officialServer.name, buttonMediumStyle );
		}
		
		if ( externalInformation.savedServerList.savedServers != null && externalInformation.savedServerList.savedServers.Count > 0 )
		{
			
			GUILayout.Space ( 40 );
			GUILayout.Label ( "Saved Servers", labelLargeStyle );
			
			foreach ( SavedServer savedServer in externalInformation.savedServerList.savedServers )
			{
				
				GUILayout.BeginHorizontal ();
				GUILayout.Button ( savedServer.name, buttonMediumStyle );
				if ( GUILayout.Button ( "Delete", buttonMediumStyle, GUILayout.Width ( 100 )))
				{
					
					externalInformation.RemoveSavedServer ( savedServer.index );
				}
				GUILayout.EndHorizontal ();
			}
		}
		
		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();
	}
}
