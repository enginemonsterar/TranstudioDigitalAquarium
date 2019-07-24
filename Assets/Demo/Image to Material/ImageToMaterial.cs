using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Drawing;



public class ImageToMaterial : Singleton<ImageToMaterial> 
{
	[HeaderAttribute("References")]
	public GameObject prefabSpawnObj;
	public Transform parentSpawnedObj;
	public Transform[] spawnLocationTransforms;
	// public Material material;

	[HeaderAttribute("Attributes")]
	public string dirPath = "";
	public float repeatRate = 1f;
	public Vector3 spawnRot;

	[HeaderAttribute("Current")]
	[SerializeField] bool isLoadingImage;
	[SerializeField] string[] currFileImagePaths;
	[SerializeField] List<string> savedFileImagePaths;

	[SerializeField] List<Texture2D> alphaTextures;
	
	int dirPathLength;
	GameObject currSpawnedObj;
	Texture2D tempTex;
	Vector3 spawnPos;

	int spawnedFish = 1;

	int fishType;

	float zDistanceForDiffFish;

	private int fishMaxCount = 18;

	bool canAddFish = true;

	void Start ()
	{		
		isLoadingImage = false;
				
		dirPathLength = dirPath.Length;

		InvokeRepeating("CheckImageDirectory", 0f, repeatRate);
	}

	void CheckImageDirectory ()
	{
		if (isLoadingImage) return; /// RETURN TO PREVENT TWO COURUTINES RUN SIMULTANEOUSLY
				
		currFileImagePaths = Directory.GetFiles(dirPath, "*.png");

		//currFileImagePaths.Length
		for (int i = 0; i < currFileImagePaths.Length ; i++)
		{			
			if (!savedFileImagePaths.Contains(currFileImagePaths[i]) && canAddFish)
			{					
				canAddFish = false;
				savedFileImagePaths.Add(currFileImagePaths[i]);

				SpawnNewObj(currFileImagePaths[i]);	

				//check if fish more than fish maks count
				if(parentSpawnedObj.childCount > fishMaxCount){
					DestroyObject(parentSpawnedObj.GetChild(0).gameObject);
				}

				break;
			}
		} 
	}

	void SpawnNewObj (string filePath)
	{
		isLoadingImage = true;
		spawnPos = spawnLocationTransforms[Random.Range(0,spawnLocationTransforms.Length)].position;
		GameObject newSpawnedObj = Instantiate(prefabSpawnObj, spawnPos, Quaternion.Euler(spawnRot));
		currSpawnedObj = newSpawnedObj;		
		currSpawnedObj.transform.position = new Vector3(currSpawnedObj.transform.position.x, currSpawnedObj.transform.position.y, currSpawnedObj.transform.position.z + zDistanceForDiffFish);
		zDistanceForDiffFish += 1f;
		currSpawnedObj.transform.parent = parentSpawnedObj;
		currSpawnedObj.name = filePath.Substring(dirPathLength);

		System.Drawing.Image img =   System.Drawing.Image.FromFile(filePath);

		if(img.Height > 2000){
			img.RotateFlip(RotateFlipType.Rotate90FlipNone);	

			if(System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);		
			
			img.Save(filePath);
			img.Dispose();
		}	
		currSpawnedObj.SetActive(false);	
		StartCoroutine("GetImage", filePath);
	}

	private Bitmap RotateImage(Bitmap bmp, float angle) {
		Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
		using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(rotatedImage)) {
			// Set the rotation point to the center in the matrix
			g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
			// Rotate
			g.RotateTransform(angle);
			// Restore rotation point in the matrix
			g.TranslateTransform(- bmp.Width / 2, - bmp.Height / 2);
			// Draw the image on the bitmap
			g.DrawImage(bmp, new Point(0, 0));
		}

		return rotatedImage;
	}

	public IEnumerator GetImage (string filePath)
	{
		tempTex = new Texture2D(2338, 1654, TextureFormat.RGB24, false);
		
		WWW loadImage = new WWW("file:///" + filePath);		

		yield return loadImage;

		if (!string.IsNullOrEmpty(loadImage.error))
		{
			Debug.Log("error " + filePath);
		}
		else
		{
					
			loadImage.LoadImageIntoTexture(tempTex);
			
			StartCoroutine(QRCodeScanner.Instance.StartScanning(tempTex,0f));
			// yield return new WaitForSeconds(5);
			
		}

		
		
		isLoadingImage = false;
	}

	public void CodeReadedAndSetTexture(){
		#region withBarcode
		string fishCode = QRCodeScanner.Instance.GetResDecode() + "";			
		// Debug.Log(fishCode);
		switch (fishCode)
		{
			case "AngelFish":
				Debug.Log("Angle");
				fishType = 0;
				break;
			case "BabyShark":
				Debug.Log("Baby");
				fishType = 1;
				break;
			case "NemoFish":
				Debug.Log("Nemo");
				fishType = 2;
				break;
			
			default:
				fishType = -1;
				break;
		}
		#endregion
		
		//Set White Texture
		SetTexture(tempTex, currSpawnedObj.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SkinnedMeshRenderer>());			
		if(fishType >= 0)
			SetTexture(alphaTextures[fishType], currSpawnedObj.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<SkinnedMeshRenderer>());
		else
			currSpawnedObj.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);

		currSpawnedObj.SetActive(true);
		canAddFish = true;

	}

	public void SetTexture(Texture2D texture, SkinnedMeshRenderer sk){
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary( 
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);
        
        // Blit the pixels on texture to the RenderTexture
        UnityEngine.Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;
        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;
        
        // Create a new readable Texture2D to copy the pixels to it
        // Texture2D myTexture2D = new Texture2D(texture.width - 570, texture.height - 485);
		Texture2D myTexture2D = new Texture2D(texture.width - 570, texture.height - 485);
        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(300, 250, texture.width, texture.height), 0, 0);
		
        myTexture2D.Apply();
        // Reset the active RenderTexture
        RenderTexture.active = previous;
        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        // "myTexture2D" now has the same pixels from "texture" and it's readable.        
		sk.material.SetTexture("_MainTex", myTexture2D);				
    }
}
