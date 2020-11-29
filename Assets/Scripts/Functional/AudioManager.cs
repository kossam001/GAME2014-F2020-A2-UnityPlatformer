using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public enum SoundIndex
    {
        HIT,
        SWING,
    }



    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {

    }

    public void PlaySound(AudioClip clip)
    {

    }
}
