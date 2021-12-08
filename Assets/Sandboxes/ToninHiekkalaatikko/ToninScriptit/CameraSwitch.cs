using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    Camera[] cameras;
    [SerializeField]
    private AudioListener[] audioListeners;

    [SerializeField]
    private bool shop = true;

    public GameObject armorShop;

    public GameObject enchantShop;

    void Start()
    {
        audioListeners = new AudioListener[cameras.Length];
        for (var i = 0; i < cameras.Length; i++)
            audioListeners[i] = cameras[i].GetComponent<AudioListener>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) == true)
        {
            Debug.Log("switch camera");
            shop = !shop;
            SwitchCamera(shop);
        }
    }

    private void SwitchCamera(bool intoShop)
    {



        if (intoShop)
        {
            armorShop.SetActive(false);
            cameras[0].enabled = true;
            audioListeners[0].enabled = true;
            cameras[1].enabled = false;
            audioListeners[1].enabled = false;
        }
        if (!intoShop)
        {
            armorShop.SetActive(true);
            cameras[1].enabled = true;
            audioListeners[1].enabled = true;
            cameras[0].enabled = false;
            audioListeners[0].enabled = false;
        }


    }
    public void SwitchCameraOn_Click()
    {
        shop = !shop;


        if (shop)
        {
            armorShop.SetActive(false);
            cameras[0].enabled = true;
            audioListeners[0].enabled = true;
            cameras[1].enabled = false;
            audioListeners[1].enabled = false;
        }
        if (!shop)
        {

            armorShop.SetActive(true);
            cameras[1].enabled = true;
            audioListeners[1].enabled = true;
            cameras[0].enabled = false;
            audioListeners[0].enabled = false;
        }


    }
    public void SwitchEnchantOn_Click()
    {
        shop = !shop;

        if (shop)
        {
            enchantShop.SetActive(false);
            cameras[0].enabled = true;
            audioListeners[0].enabled = true;
            cameras[1].enabled = false;
            audioListeners[1].enabled = false;
        }
        if (!shop)
        {
            enchantShop.SetActive(true);
            cameras[1].enabled = true;
            audioListeners[1].enabled = true;
            cameras[0].enabled = false;
            audioListeners[0].enabled = false;
        }

    }
}
