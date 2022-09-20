using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using SFB;

public class ScreenshotManager : MonoBehaviour
{
    public RectTransform rectT; // Assign the UI element which you wanna capture

    public TMP_Text errorText;
    string errorString = "";
    int width; // width of the object to capture
    int height; // height of the object to capture
    // Start is called before the first frame update
    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width); 
        height = System.Convert.ToInt32(rectT.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Capture()
    {
       StartCoroutine(takeScreenShot ());
       StartCoroutine(resetText()); 
    }

    public IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame (); // it must be a coroutine 
        Vector3[] corners = new Vector3[4];
        rectT.GetWorldCorners(corners);
        var startX = corners[0].x;
        var startY = corners[0].y;

        int width = (int)corners[3].x - (int)corners[0].x;
        int height = (int)corners[1].y - (int)corners[0].y;
        var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
        tex.ReadPixels (new Rect(startX, startY, width, height), 0, 0);
        tex.Apply ();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("save as "+path);
        errorString = "save file as "+ path; 
        errorText.text = errorString.ToString();       
    }

    public IEnumerator resetText()
    {
        yield return new WaitForSeconds (3); // it must be a coroutine 
        errorText.text = "";
    }
    
}
