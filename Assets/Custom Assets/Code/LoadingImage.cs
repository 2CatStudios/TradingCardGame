using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class LoadingImage : MonoBehaviour
{

	public Texture2D[] loadingImages = new Texture2D [ 6 ];
	int imageIndex = 0;
	bool delay = false;

	
	public Texture2D CurrentLoadingImage ()
	{
			
		if ( delay == false )
		{
			
			if ( imageIndex >= loadingImages.Length -1 )
				imageIndex = 0;
			else
				imageIndex++;
			delay = true;
			} else {
				
				delay = false;
			}
		
		return loadingImages [imageIndex];
	}
}
