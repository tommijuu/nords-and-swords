using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider EffectsSlider;
    public Slider MusicSlider;
    public AudioMixer audioMixer;
    [SerializeField]
    GameObject menuObject;

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

    public void SetSliders()
    {
        MasterSlider.value = GetMasterVolume();
        EffectsSlider.value = GetEffectsVolume();
        MusicSlider.value = GetMusicVolume();
    }

    private void OnEnable()
    {
        SetSliders();
    }
}
