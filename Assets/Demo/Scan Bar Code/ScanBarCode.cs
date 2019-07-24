// using BarcodeScanner;
// using BarcodeScanner.Scanner;
// using System;
// using System.Collections;
using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using Wizcorp.Utils.Logger;

using ZXing;

public class ScanBarCode : MonoBehaviour 
{
	public Texture2D[] inputTextures;

	BarcodeReader barCodeReader;

	void Start () 
	{
		foreach(Texture2D texture2D in inputTextures)
		{
			string resDecode = ReadQRCode(texture2D);

			switch (resDecode)
			{
				case "AngelFish":
					Debug.Log("AngelFish Kuy");
					break;
				case "BabyShark":
					Debug.Log("BabyShark Kuy");
					break;
				case "NemoFish":
					Debug.Log("NemoFish Kuy");
					break;
				default:
					Debug.Log("Error, Unknwon Fish : " + resDecode);
					break;
			} 
		}
	}

	string ReadQRCode (Texture2D texture2D)
	{
		// create a barcode reader instance
        IBarcodeReader reader = new BarcodeReader();

		Texture2D tempTexture = GetReadableTexture(texture2D);

        // get texture Color32 array
        var barcodeBitmap = tempTexture.GetPixels32();
        
		// detect and decode the barcode inside the Color32 array
        var result = reader.Decode(barcodeBitmap, tempTexture.width, tempTexture.height);

		// do something with the result
        if (result != null)
        {
            // Debug.Log(result.BarcodeFormat.ToString() + " Result : " + result.Text);

			return(result.Text);
        }

		return "";
	}

	Texture2D GetReadableTexture (Texture2D texture2D) 
	{
		// Create a temporary RenderTexture of the same size as the texture
		RenderTexture tmp = RenderTexture.GetTemporary( 
							texture2D.width,
							texture2D.height,
							0,
							RenderTextureFormat.Default,
							RenderTextureReadWrite.Linear);

		// Blit the pixels on texture to the RenderTexture
		Graphics.Blit(texture2D, tmp);

		// Backup the currently set RenderTexture
		RenderTexture previous = RenderTexture.active;

		// Set the current RenderTexture to the temporary one we created
		RenderTexture.active = tmp;

		// Create a new readable Texture2D to copy the pixels to it
		Texture2D myTexture2D = new Texture2D(texture2D.width, texture2D.height);

		// Copy the pixels from the RenderTexture to the new Texture
		myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
		myTexture2D.Apply();

		// Reset the active RenderTexture
		RenderTexture.active = previous;

		// Release the temporary RenderTexture
		RenderTexture.ReleaseTemporary(tmp);

		return myTexture2D;
	}
}
