using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioImmortality : MonoBehaviour
{
    public static AudioImmortality immortal;
    private AudioSource backgroundMusic;
    public AudioClip stageSelection;


    void Awake()
    {
        if (immortal == null)
        {
            DontDestroyOnLoad(gameObject);
            immortal = this;

        }
        else { Destroy(gameObject); }
        backgroundMusic = GetComponent<AudioSource>();
    }
    void Update()
    {

    }
    public void ChangeBackgroundMusic(string sceneName)
    {

        if (sceneName == "äänitesti")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = stageSelection;
            backgroundMusic.Play();
        }

    }

}
