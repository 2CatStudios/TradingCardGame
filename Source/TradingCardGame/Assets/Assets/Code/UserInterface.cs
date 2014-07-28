using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	public GUISkin guiskin;
	internal Rect homePaneRect;
	Rect controlWindowRect;
	
	GUIStyle labelLargeStyle;
	GUIStyle labelSmallStyle;
	GUIStyle windowStyle;
	
	bool host = false;
	bool connect = false;
	
	bool fadeIN = false;
	bool fadeOUT = false;
	
	Color guicolor;
	
	
	void Start ()
	{
	
		homePaneRect = new Rect ( 0, 0, Screen.width, Screen.height );
		controlWindowRect = new Rect ( 14, 204, 772, 360 );
		
		labelLargeStyle = new GUIStyle ();
		labelLargeStyle.fontSize = 48;
		
		labelSmallStyle = new GUIStyle ();
		labelSmallStyle.fontSize = 24;
		
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
		GUI.Window ( 0, homePaneRect, HomePane, "" );
	}


	void HomePane ( int windowID )
	{
		
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		GUILayout.Label ( "Multiplayer Game", labelLargeStyle );
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		GUILayout.Label ( "Subtitle goes here", labelSmallStyle );
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		
		GUILayout.Space ( 50 );
		
		GUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		
		if ( GUILayout.Button ( "Host", GUILayout.Width ( 350 )))
		{
			
			if ( host == true )
			{
				
				fadeOUT = true;
			} else {
				
				host = true;
				connect = false;
				fadeIN = true;
			}
			
			UnityEngine.Debug.Log ( "Host" );
		}
		
		GUILayout.FlexibleSpace ();
		
		if ( GUILayout.Button ( "Connect", GUILayout.Width ( 350 )))
		{
			
			if ( connect == false )
			{
			
				connect = true;
				host = false;
				fadeIN = true;
			} else {
		
				fadeOUT = true;
			}
			
			UnityEngine.Debug.Log ( "Connect" );
		}
		
		GUILayout.FlexibleSpace ();
		
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
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
		
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}
	
	
	void HostingWindow ( int windowID )
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "Host", labelSmallStyle );
		
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}
	
	
	void ConnectingWindow ( int windowID )
	{

		GUILayout.BeginVertical ();
		GUILayout.Space ( 5 );
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( 5 );
		
		GUILayout.Label ( "Connect", labelSmallStyle );
		
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}
}
