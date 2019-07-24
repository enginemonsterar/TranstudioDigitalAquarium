using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class ImageRotator : Singleton<ImageRotator>
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(System.Drawing.Image img){
        // path = @"C:\ScannedFish\Fish.jpg";   
        // Debug.Log("Rotate 90: " + path);     
        // System.Drawing.Image img = System.Drawing.Image.FromFile(path);           
        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
    }
}
