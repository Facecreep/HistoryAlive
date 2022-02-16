using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    [SerializeField] private GameObject _image;
    
    // public IEnumerable<WaitForEndOfFrame> TakeAPicture()
    // {
    //     yield return new WaitForEndOfFrame();
    //
    //     // Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
    //     // //Get Image from screen
    //     // screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //     // screenImage.Apply();
    //     // //Convert to png
    //     // byte[] imageBytes = screenImage.EncodeToJPG();
    //     //
    //     // //Save image to gallery
    //     // NativeGallery.SaveImageToGallery(imageBytes, "AR", "Screenshot.jpg");
    //     
    //     Debug.Log("Photo Taken!");
    // }

    public void TakeAPicture()
    {
        StartCoroutine(CaptureScreen());
    }
    
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
 
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();
 
        // Take screenshot
        NativeGallery.SaveImageToGallery(ScreenCapture.CaptureScreenshotAsTexture(), "History Alive", "Photo");
 
        // Show UI after we're done
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        
        _image.GetComponent<Animation>().Play();
    }

    public void BuildNavMesh()
    {
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }
}
