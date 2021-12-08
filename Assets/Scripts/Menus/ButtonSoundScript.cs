using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{

    public AudioSource ASource;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public AudioClip plusOneFx;
    public AudioClip AttContinueFx;

    public void HoverSound()
    {
        ASource.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        ASource.PlayOneShot(clickFx);
    }

    public void PlusSound()
    {
        ASource.PlayOneShot(plusOneFx);
    }
    public void AttContinueSound()
    {
        ASource.PlayOneShot(AttContinueFx);
    }

}
