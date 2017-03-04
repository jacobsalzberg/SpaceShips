using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void LoadLevel(string name)
    {
        Debug.Log("Level load load: " + name);
        
        SceneManager.LoadScene(name);
    }
    public void LoadNextLevel()
    {
       
        Debug.Log("load next level: " + name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //level index
    }
    public void QuitRequest()
    {
        Debug.Log("Quit request");
        Application.Quit();

    }
   
}