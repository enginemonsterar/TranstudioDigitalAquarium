// Get a rectangular area of a texture and place it into
// a new texture the size of the rectangle.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRCodeScanner : Singleton<QRCodeScanner>
{
    // Source texture and the rectangular area we want to extract.
    public Texture2D sourceTexture;    
    private List<Rect> sourceBotLeftRects = new List<Rect>();
    private List<Rect> sourceBotRightRects;
    private List<Rect> sourceTopLeftRects;
    private List<Rect> sourceTopRightRects;
    private Rect sourceRect;
   
    public MeshRenderer targetMeshRenderer;

    private string resDecode;


    BarcodeReader barCodeReader;

    bool barcodeIsRead;
    
    int index;

    void Start()
    {
        

        
        // StartCoroutine(StartScanning(sourceTexture,0f));
                
    }

    public IEnumerator StartScanning(Texture2D texture, float scanningTime){
        barcodeIsRead = false;
        sourceTexture = texture;

        // Bot Left
        for (int i = 0; i < 10; i++)
        {                  
            for (int j = 0; j < 10; j++)
            {    
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(i * 5, j * 5, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
            for (int j = 0; j < 10; j++)
            {                                
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(j * 5, i * 5, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
        }  

        // Bot Right
        for (int i = 0; i < 10; i++)
        {                  
            for (int j = 0; j < 10; j++)
            {    
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(i * 5 + 2000, j * 5, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
            for (int j = 0; j < 10; j++)
            {                                
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(j * 5 + 2000, i * 5, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
        }      

        // Top Left
        for (int i = 0; i < 10; i++)
        {                  
            for (int j = 0; j < 10; j++)
            {    
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(i * 5, j * 5 + 1350, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
            for (int j = 0; j < 10; j++)
            {                                
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(j * 5, i * 5 + 1350, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
        } 

        // Top Right
        for (int i = 0; i < 10; i++)
        {                  
            for (int j = 0; j < 10; j++)
            {    
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(i * 5 + 2000, j * 5 + 1350, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
            for (int j = 0; j < 10; j++)
            {                                
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(j * 5 + 2000, i * 5 + 1350, 285, 230);
                    ScanQRCode();
                    yield return new WaitForSeconds(scanningTime);
                }else{
                    break;
                }                                            
            }      
        } 

        ImageToMaterial.Instance.CodeReadedAndSetTexture();
        
                         
    }

    public void StartScanning(Texture2D texture){
        barcodeIsRead = false;
        sourceTexture = texture;
        
        // Bot Left
        for (int i = 0; i < 10; i++)
        {                  
            for (int j = 0; j < 10; j++)
            {    
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(i * 5, j * 5, 285, 230);
                    ScanQRCode();                    
                }else{
                    break;
                }                                            
            }      
            for (int j = 0; j < 10; j++)
            {                                
                if(!barcodeIsRead){                            
                    sourceRect = new Rect(j * 5, i * 5, 285, 230);
                    ScanQRCode();                    
                }else{
                    break;
                }                                            
            }      
        }  

                      
    }
    
    public void SetTexture(Texture2D destTex){
        targetMeshRenderer.material.SetTexture("_MainTex", destTex);
    }


    public void ScanQRCode(){
                             
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
        SetTexture(destTex);
        
        string tempResDecode = ReadQRCode(destTex);        
        
        switch (tempResDecode)
        {
            case "AngelFish":
                Debug.Log("AngelFish Kuy");                
                barcodeIsRead = true;
                resDecode = tempResDecode; 
                ImageToMaterial.Instance.CodeReadedAndSetTexture();
                break;
            case "BabyShark":
                Debug.Log("BabyShark Kuy");                
                barcodeIsRead = true;
                resDecode = tempResDecode; 
                ImageToMaterial.Instance.CodeReadedAndSetTexture();
                break;
            case "NemoFish":
                Debug.Log("NemoFish Kuy");                
                barcodeIsRead = true;
                resDecode = tempResDecode; 
                ImageToMaterial.Instance.CodeReadedAndSetTexture();
                break;
            default:
                Debug.Log("Error, Unknwon Fish : " + resDecode + ". Find another barcodes");                
                resDecode = tempResDecode; 
                
                break;
        } 
    
    }

    public string GetResDecode(){
        return resDecode;
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
