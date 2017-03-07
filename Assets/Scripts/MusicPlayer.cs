using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    void Start()
    {
        //Debug.Log("Music player Awake" + GetInstanceID());
        if (instance != null && instance != this) //if an instance of a musicplayer exists
        {
            Destroy(gameObject);
            print("Duplicate Music player self-destructing!!!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject); // instance of the music player
            music = GetComponent<AudioSource>(); //pega o componente do audiosource
            music.clip = startClip;
            music.loop = true;
            music.Play();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        
    }
    //ADD A delegate

   void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
       //pra nao dar null reference
       if (music == null)
       {
           music = GetComponent<AudioSource>();
       }
       else
       {
            music.Stop();
       }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("MusicPlayer: Loaded Level " + SceneManager.GetActiveScene().buildIndex);
            music.clip = startClip;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("MusicPlayer: Loaded Level " + SceneManager.GetActiveScene().buildIndex);
            music.clip = gameClip;
        }
        if (SceneManager.GetActiveScene().buildIndex ==2)
        {
            Debug.Log("MusicPlayer: Loaded Level " + SceneManager.GetActiveScene().buildIndex);
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }




    // Use this for initialization
    //void Start () {
    //Debug.Log("Music player Start" + GetInstanceID());

    //}

    // Update is called once per frame

    /*DEPRECATED
void OnLevelWasLoaded(int level)
{
    //bugfix pra nao travar
    if (music == null)
    {
        music = GetComponent<AudioSource>();
    }
    else
    {
        music.Stop();
    }

    if (level == 0)
    { 
    music.clip = startClip;
    }
    if (level == 1)
    {
    music.clip = gameClip;
    }
    if (level == 2)
    {
        music.clip = endClip;
    }
    music.loop = true;
    music.Play();
} */
}
