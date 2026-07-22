using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicScound, sfxScound;
    public AudioSource musicScource, sfxScource;

    //to make instance of the script
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayMusic("Background");
    }

    //called to change the background music that is playing
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicScound, x => x.name == name);//try to find the sound in the array 
        
        //prints a error message if the sound is not found in the array
        if(s ==null)
        {
            Debug.Log("Sound not Found");
        }
        //if the sound is found then replace it with the current playing sound
        else
        {
            musicScource.clip = s.clip;
            musicScource.loop = true;
            musicScource.Play();
        }
    }

    //caleed to play the sfx
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxScound, x => x.name == name);//find the sfx in the array

        //prints a error message if the sfx is not found
        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        //plays the sound once if the sfx is found
        else
        {
            sfxScource.PlayOneShot(s.clip);
        }
    }


}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
