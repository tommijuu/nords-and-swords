using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVol : MonoBehaviour
{
    //public Image filler;
    public AudioMixer musicmixer;
    //public Image handle;


    public Slider mainSlider;

  



    public void SetLevelAll(float sliderValue)
    {
        

        // SetFloatin string argumentti pitää olla audiomixerin tietyn groupin parametri.
        // parametreja saa tehtyä klikkaamalla halutun groupin jäsentä ja oikeaklikkauksella inspectorissa,
        // volyymin kohdalla ja valitsemalla Expose.
        // sitten alapalkissa audiomixer ikkunassa oikeassa yläkulmassa on exposed parameters ja uusi exposattu parametri 
        // oli muistaakseni myNewParam. sitä voi oikeaklikata ja tulee vaihtoehto rename.
        // tämä parametrin nimi menee setfloattiin stringi argumenttiin.
         
        musicmixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);

    }
    public void SetLeveleffects(float sliderValue)
    {
        //filler.fillAmount = sliderValue / 1.0000f;
        //handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("EffectsVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelMusic(float sliderValue)
    {
       // filler.fillAmount = sliderValue / 1.0000f;
       // handle.fillAmount = sliderValue / 1.0000f;
        musicmixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
