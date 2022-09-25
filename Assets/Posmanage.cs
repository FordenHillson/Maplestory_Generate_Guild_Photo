using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Posmanage : MonoBehaviour
{
    public string nameStr{get; set;}
    public TMP_Text nameTxt;

    // Start is called before the first frame update
    void Start()
    {
        nameTxt = this.transform.Find("Name").GetComponent<TMP_Text>(); 
        nameStr = "Name";      
    }

    // Update is called once per frame
    void Update()
    {
        nameTxt.text = nameStr;
    }
}
