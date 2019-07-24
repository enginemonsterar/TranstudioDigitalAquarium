// Get a rectangular area of a texture and place it into
// a new texture the size of the rectangle.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class Cropper : Singleton<Cropper>
{
    // Source texture and the rectangular area we want to extract.
    public Texture2D sourceTexture;    
    public Rect sourceRect;
    public MeshRenderer targetMeshRenderer;

    BarcodeReader barCodeReader;
    
  

    void Start()
    {
        // SetTexture();
    }

    public void SetSourceTexture(Texture2D sourceTexture){
        this.sourceTexture = sourceTexture;
        SetTexture();
    }

    public void SetTexture(){

     

        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTexture.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();
        
        // Set the current object's texture to show the
        // extracted rectangle.
        targetMeshRenderer.material.SetTexture("_MainTex", destTex);

        string resDecode = ReadQRCode(destTex);

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
