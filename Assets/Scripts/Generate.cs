using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;
using SFB;
using TMPro;

public class Generate : MonoBehaviour
{   
    [SerializeField]
    private List<GameObject> Pos = new List<GameObject>();
    
    [SerializeField]
    private List<Texture2D> imageLists = new List<Texture2D>();

    [SerializeField]
    private List<String> nameLists = new List<string>();
    [SerializeField]
    private GameObject PosHolder; 
    string _path;
    int pathLenght;
    int totoalPos;
    [SerializeField]
    int totalImage;
    [SerializeField]
    int has = 0;
    

    public Text amountMemTxt;
    public Text amountImageTxt;

    public TMP_Text errorTxt;

    public Posmanage posmanage;
    
    private System.Random _random = new System.Random();    
    // Start is called before the first frame update
    void Start()
    {        
        totoalPos = PosHolder.transform.childCount;
        for (int i = 0; i < totoalPos; i++)
        {
            Pos.Add(PosHolder.transform.GetChild(i).gameObject);
            Pos[i].GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Sprite");           
        }
        posmanage = gameObject.GetComponent<Posmanage>();         
    }

    private void Update() 
    {
        totalImage = imageLists.Count;
        amountImageTxt.text = "Amount images : "+totalImage.ToString();        
        amountMemTxt.text = "Amount members : "+has.ToString();
    }

    public void OpenFile()
    {
        //openfile with StandaloneFileBrowser by gkngkc      
        var extensions = new [] 
        {
            new ExtensionFilter("Image Files", "png")
        };
        var _path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        pathLenght = _path.Length;
        Debug.Log(_path.Length);        

        //assgin to imageLists array
        if(Pos.Count > pathLenght)
        {
            for (int i = 0; i < _path.Length; i++)
            {
                string filename = Path.GetFileNameWithoutExtension(_path[i]);
                WWW www = new WWW("file:///"+_path[i]);
                Debug.Log(filename);
                nameLists.Add(filename);
                Pos[i].GetComponent<Posmanage>().nameStr = filename;             
                imageLists.Add(www.texture);
                totalImage ++;
            }             
            GeneratePicture();
        }
               
    } 

    public void ShuffleImage()
    {
        int p =  pathLenght;
        int totalHas = 0;        
        for (int i = 0; i < totoalPos; i++)
        {
            if(Pos[i].activeSelf)
            {
                totalHas ++; 
                has = totalHas;
            }
        }

        for (int n = totalHas-1; n > 0 ; n--)
        {
            int r = _random.Next(0, n);
            Texture2D t = imageLists[r];
            string o = nameLists[r];
            imageLists[r] = imageLists[n];
            nameLists[r] = nameLists[n];
            imageLists[n] = t;  
            nameLists[n] = o;                  
        }

        for(int i = 0; i <  has; i++)
        {
            Pos[i].GetComponent<RawImage>().texture = imageLists[i];
            Pos[i].GetComponent<Posmanage>().nameStr = nameLists[i];           
        }
    }  

    void GeneratePicture()
    {
        int p = totoalPos - imageLists.Count;        

        if(totoalPos > imageLists.Count)
        {
            for (int j = imageLists.Count; j < totoalPos; j++)
            {
                Pos[j].gameObject.SetActive(false);
                int totalHas = 0;        
                for (int i = 0; i < totoalPos; i++)
                {
                    if(Pos[i].activeSelf)
                    {
                        totalHas ++; 
                        has = totalHas;
                    }
                }                                
            }

            /*for (int i = 0; i < p; i++)
            {                
                imageLists.Add(Resources.Load<Texture2D>("null"));                
            }*/

            for (int i = 0; i < has; i++)
            {
                Pos[i].GetComponent<RawImage>().texture = imageLists[i];                
            }
        }
    }

    public void DecresePos()
    {        
        if(has > -1)
        {
            Pos[has-1].gameObject.SetActive(false);
            Pos[has-1].GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Sprite");
            if(imageLists.Count != 0)
            {
                totalImage --;
                imageLists.RemoveAt(imageLists.Count-1);
            }
            has--;
        }
        
    }

    public void IncresePos()
    {
        if(has < totoalPos)
        {            
            Pos[has].gameObject.SetActive(true);
            Pos[has].GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Sprite");                          
            if(has == -1)
            {
                Pos[has+1].gameObject.SetActive(true);
                Pos[has].GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Sprite");
                has ++; 
            }
            has++;  
        }            
    } 

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   
    //this will split multiple Windows directory files to array code by StandaloneFileBrowser by gkngkc 
    public void WriteResult(string[] paths) {
        if (paths.Length == 0) {
            return;
        }

        _path = "";
        foreach (var p in paths) {
            _path += p + "\n";
        }      
            
    } 

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }  
    
}
