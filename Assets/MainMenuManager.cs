using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Sceneload()
    {
        string buttomName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttomName);
        SceneManager.LoadScene("Scene"+buttomName);
    }
}
