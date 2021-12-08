using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject shopMain;
    public Animator noGoldAnimator;
    public GameObject canvasShop;
    private GameManager gm;

    public bool shopActive;


    public GameObject items;
    public GameObject cameraSwitchScript;
    public GameObject shopPlayer;
    public GameObject enchantShopButton;
    public GameObject backgroundImage;
    void Start()
    {
        canvasShop = GameObject.Find("CanvasShop");
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        shopActive = false;
        shopMain = gameObject.transform.GetChild(0).gameObject;
        cameraSwitchScript = GameObject.Find("Directional Light");
    }



    public void MerchantClicked()
    {
        shopPlayer.SetActive(true);
        shopMain.SetActive(true);
        shopActive = true;

        cameraSwitchScript.GetComponent<CameraSwitch>().SwitchCameraOn_Click();
        enchantShopButton.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void CloseShop()
    {
        shopPlayer.SetActive(false);
        shopActive = false;

        cameraSwitchScript.GetComponent<CameraSwitch>().SwitchCameraOn_Click();
        enchantShopButton.SetActive(true);
        backgroundImage.SetActive(true);
    }
    public void ItemCategorySelected()
    {
        shopActive = true;
        items.SetActive(true);
        Debug.Log("rotators item category: " + Rotator.selectedItemCategory);


    }
    public void EnchantClicked()
    {
        shopActive = true;
        items.SetActive(true);
        items.GetComponent<EnchantRotator>().SpawnOwnedItems();

        cameraSwitchScript.GetComponent<CameraSwitch>().SwitchEnchantOn_Click();
        enchantShopButton.SetActive(false);
        backgroundImage.SetActive(false);
    }
    public void CloseEnchantShop()
    {
        shopActive = false;
        items.SetActive(false);

        cameraSwitchScript.GetComponent<CameraSwitch>().SwitchEnchantOn_Click();
        enchantShopButton.SetActive(true);
        backgroundImage.SetActive(true);
        items.GetComponent<EnchantRotator>().TurnOffOldweapons();
    }
    public void MapScene()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void HelmetsSelected()
    {
        Rotator.setItemCategory("helmets");
        ItemCategorySelected();
    }

    public void HandsSelected()
    {
        Rotator.setItemCategory("hands");
        ItemCategorySelected();
    }

    public void ChestsSelected()
    {
        Rotator.setItemCategory("chests");
        ItemCategorySelected();
    }

    public void ShieldsSelected()
    {
        Rotator.setItemCategory("shields");
        ItemCategorySelected();
    }

    public void PantsSelected()
    {
        Rotator.setItemCategory("pants");
        ItemCategorySelected();
    }

    public void BootsSelected()
    {
        Rotator.setItemCategory("boots");
        ItemCategorySelected();
    }



















    public void Buy(int value)
    {
        if (GameManager.instance.Gold >= value)
        {
            GameManager.instance.Gold -= value;
            Debug.Log("Weapon bought with " + value + " gold.");
            Debug.Log("Player has " + GameManager.instance.Gold + " gold now.");
        }
        else
        {
            if (!noGoldAnimator.GetCurrentAnimatorStateInfo(0).IsName("textFadeIn"))
                noGoldAnimator.Play("textFadeIn", 0, 0.25f);
        }
    }
}