using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistMusicScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip mapMusic;
    public AudioClip combatMusic;

    Scene scene;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        audioSource = GameObject.Find("MusicBoi").GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        SceneChanged();
        scene = SceneManager.GetActiveScene();
    }

    void SceneChanged()
    {
        if (scene.name == "Main Menu")
        {
            if (audioSource.clip != menuMusic)
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }

        if (scene.name == "MapScene")
        {
            if (audioSource.clip != mapMusic)
            {
                audioSource.clip = mapMusic;
                audioSource.Play();
            }
        }

        if (scene.name == "enviro_02")
        {
            if (audioSource.clip != combatMusic)
            {
                audioSource.clip = combatMusic;
                audioSource.Play();
            }
        }

        if (scene.name == "enviro_03_training")
        {
            if (audioSource.clip != combatMusic)
            {
                audioSource.clip = combatMusic;
                audioSource.Play();
            }
        }
    }


}
