using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider EffectsSlider;
    public Slider MusicSlider;
    public AudioMixer audioMixer;
    [SerializeField]
    protected GameObject menuObject;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }


    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVol", Mathf.Log10(volume) * 20);
    }

    public float GetMasterVolume()
    {
        float vol;
        audioMixer.GetFloat("Master", out vol);
        vol = Mathf.Pow(10, (vol / 20f));
        return vol;
    }

    public float GetEffectsVolume()
    {
        float vol;
        audioMixer.GetFloat("EffectsVol", out vol);
        vol = Mathf.Pow(10, (vol / 20f));
        return vol;
    }

    public float GetMusicVolume()
    {
        float vol;
        audioMixer.GetFloat("MusicVol", out vol);
        vol = Mathf.Pow(10, (vol / 20f));
        return vol;
    }
    public void ToggleMenu()
    {
        menuObject.SetActive(!menuObject.activeSelf);
        if (menuObject.activeSelf)
        {
            MasterSlider.value = GetMasterVolume();
            EffectsSlider.value = GetEffectsVolume();
            MusicSlider.value = GetMusicVolume();
        }
    }

    public void QuitToMenu()
    {

        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting application / Quitting test mode");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gm.postMatchActive)
        {
            ToggleMenu();
        }
    }

}
