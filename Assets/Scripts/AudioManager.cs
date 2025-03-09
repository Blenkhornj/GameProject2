using Unity.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour


{

    //VARIABLES

    [SerializeField] int currentTrack;
    [SerializeField] bool isPaused;
    public AudioSource mainMenuMusic;
    public AudioSource[] backgroundMusic;
    public AudioSource[] sfx;
    


    public static AudioManager instance; //Allows to be called into other classes

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public void StopMusic() //Stops music
    
    {
        foreach (AudioSource source in backgroundMusic)
        {
            source.Stop();
        }

        mainMenuMusic.Stop();

    }
    /*

    public void PlayMainMenuMusic()
    {
        
    }

    public void PauseMusic()
    {
       
    }

    public void ResumeMusic()
    {
       

    }

    public void NextBackGroundMusic()
    {
        

    }

    //public void PlaySFXModified(int sfxItem)
   // {
        
        
    //}


    */

    public void PlaySFX(int sfxItem)
    {
        sfx[sfxItem].Play();
    }

    
    
}
