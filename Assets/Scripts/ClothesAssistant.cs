using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothesAssistant : MonoBehaviour
{
    [Header("Items in store player prefab:")]
    public GameObject shields;
    public GameObject axes;
    public GameObject swords;
    public GameObject helmets;
    public GameObject hammers;
    public GameObject chestPlate;
    public GameObject shoulderPads;
    public GameObject armGuards;
    public GameObject handGuards;
    public GameObject shoulderPads_Left;
    public GameObject armGuards_Left;
    public GameObject handGuards_Left;
    public GameObject footGuards;
    public GameObject thighGuards;
    public GameObject shinGuards;
    public GameObject footGuards_Left;
    public GameObject thighGuards_Left;
    public GameObject shinGuards_Left;
    [Header("Selected Item category:")]
    public bool buyShields;
    public bool buyAxes;
    public bool buySwords;
    public bool buyHelmets;
    public bool buyHammers;
    public bool buyChestPlate;
    public bool buyArmGuards;
    public bool buyFeetGuards;

    public int selectedItem;
    [Header("Item Prices:")]
    public int[] shieldPrices = new int[5];
    public int[] helmetPrices = new int[5];
    public int[] axePrices = new int[5];
    public int[] swordPrices = new int[8];
    public int[] hammerPrices = new int[5];
    public int[] chestPrices = new int[9];
    public int[] armPrices = new int[9];
    public int[] feetPrices = new int[9];
    [Header("Item Stats:")]
    public string[] shieldStats = new string[5];
    public string[] helmetStats = new string[5];
    public string[] axeStats = new string[5];
    public string[] swordStats = new string[8];
    public string[] hammerStats = new string[5];
    public string[] chestStats = new string[9];
    public string[] armStats = new string[9];
    public string[] feetStats = new string[9];
    [Header("Text fields")]
    public TMP_Text itemName;
    public TMP_Text cost;
    public TMP_Text stats;
    public TMP_Text currentGold;
    [Header("Item icons:")]
    public GameObject shieldIcon;
    public GameObject axeIcon;
    public GameObject swordIcon;
    public GameObject helmetIcon;
    public GameObject hammerIcon;
    public GameObject handIcon;
    public GameObject chestIcon;
    public GameObject feetIcon;

    [Header("Item icons in black:")]
    public Sprite shieldIconBlack;
    public Sprite axeIconBlack;
    public Sprite swordIconBlack;
    public Sprite helmetIconBlack;
    public Sprite hammerIconBlack;
    public Sprite handIconBlack;
    public Sprite chestIconBlack;
    public Sprite feetIconBlack;
    [Header("Item icons in white:")]
    public Sprite shieldIconWhite;
    public Sprite axeIconWhite;
    public Sprite swordIconWhite;
    public Sprite helmetIconWhite;
    public Sprite hammerIconWhite;
    public Sprite handIconWhite;
    public Sprite chestIconWhite;
    public Sprite feetIconWhite;
    void Start()
    {
        selectedItem = 0;
        buyAxes = false;
        buySwords = false;
        buyHelmets = false;
        buyHammers = false;
        buyShields = false;
        buyFeetGuards = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentGold.text = "CURRENT GOLD: " + PlayerAttributes.currentGold;
    }

    public void SwitchItemRight()
    {

        if (buyShields == true)
        {
            selectedItem = Math.Min(4, selectedItem + 1);
            Switcher3(shields);
            Switcher4(shields, shieldPrices, shieldStats);
        }
        if (buyAxes == true)
        {
            selectedItem = Math.Min(4, selectedItem + 1);
            Switcher3(axes);
            Switcher4(axes, axePrices, axeStats);
        }
        if (buySwords == true)
        {
            selectedItem = Math.Min(7, selectedItem + 1);
            Switcher3(swords);
            Switcher4(swords, swordPrices, swordStats);
        }
        if (buyHelmets == true)
        {
            selectedItem = Math.Min(4, selectedItem + 1);
            Switcher3(helmets);
            Switcher4(helmets, helmetPrices, helmetStats);
        }
        if (buyHammers == true)
        {
            selectedItem = Math.Min(4, selectedItem + 1);
            Switcher3(hammers);
            Switcher4(hammers, hammerPrices, hammerStats);
        }
        if (buyArmGuards == true)
        {
            selectedItem = Math.Min(8, selectedItem + 1);
            Switcher3(armGuards);
            Switcher3(handGuards);
            Switcher3(armGuards_Left);
            Switcher3(handGuards_Left);
            ArmSwitcher4(armGuards, armGuards_Left, handGuards_Left, handGuards, armPrices, armStats);
        }
        if (buyChestPlate == true)
        {
            selectedItem = Math.Min(8, selectedItem + 1);
            Switcher3(chestPlate);
            Switcher3(shoulderPads);
            Switcher3(shoulderPads_Left);
            ChestSwitcher4(chestPlate, shoulderPads, shoulderPads_Left, chestPrices, chestStats);
        }
        if (buyFeetGuards == true)
        {
            selectedItem = Math.Min(8, selectedItem + 1);
            Switcher3(shinGuards);
            Switcher3(shinGuards_Left);
            Switcher3(thighGuards);
            Switcher3(thighGuards_Left);
            Switcher3(footGuards);
            Switcher3(footGuards_Left);
            LegSwitcher4(thighGuards, thighGuards_Left, shinGuards, shinGuards_Left, footGuards, footGuards_Left, feetPrices, feetStats);
        }
    }
    public void SwitchItemLeft()
    {

        if (buyShields == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(shields);
            Switcher4(shields, shieldPrices, shieldStats);
        }
        if (buyAxes == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(axes);
            Switcher4(axes, axePrices, axeStats);
        }
        if (buySwords == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(swords);
            Switcher4(swords, swordPrices, swordStats);
        }
        if (buyHelmets == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(helmets);
            Switcher4(helmets, helmetPrices, helmetStats);
        }
        if (buyHammers == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(hammers);
            Switcher4(hammers, hammerPrices, hammerStats);
        }
        if (buyArmGuards == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(armGuards);
            Switcher3(handGuards);
            Switcher3(armGuards_Left);
            Switcher3(handGuards_Left);
            ArmSwitcher4(armGuards, armGuards_Left, handGuards_Left, handGuards, armPrices, armStats);
        }
        if (buyChestPlate == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(chestPlate);
            Switcher3(shoulderPads);
            Switcher3(shoulderPads_Left);
            ChestSwitcher4(chestPlate, shoulderPads, shoulderPads_Left, chestPrices, chestStats);
        }
        if (buyFeetGuards == true)
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            Switcher3(shinGuards);
            Switcher3(shinGuards_Left);
            Switcher3(thighGuards);
            Switcher3(thighGuards_Left);
            Switcher3(footGuards);
            Switcher3(footGuards_Left);
            LegSwitcher4(thighGuards, thighGuards_Left, shinGuards, shinGuards_Left, footGuards, footGuards_Left, feetPrices, feetStats);
        }
    }
    public void Switcher4(GameObject weaponClass, int[] prices, string[] statArray)
    {
        weaponClass.transform.GetChild(selectedItem).gameObject.SetActive(true);
        itemName.text = "NAME: " + weaponClass.transform.GetChild(selectedItem).gameObject.name;
        cost.text = "COST: " + prices[selectedItem];
        stats.text = statArray[selectedItem];


    }
    public void ArmSwitcher4(GameObject weaponClass, GameObject otherArm, GameObject handLeft, GameObject handRight, int[] prices, string[] statArray)
    {
        if (selectedItem > 2)
        {
            handGuards.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
            handGuards_Left.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
        }
        weaponClass.transform.GetChild(selectedItem).gameObject.SetActive(true);
        otherArm.transform.GetChild(selectedItem).gameObject.SetActive(true);
        itemName.text = "NAME: " + weaponClass.transform.GetChild(selectedItem).gameObject.name;
        cost.text = "COST: " + prices[selectedItem];
        stats.text = statArray[selectedItem];


    }
    public void LegSwitcher4(GameObject weaponClass, GameObject thigh, GameObject otherShin, GameObject shin, GameObject foot, GameObject foot_Left, int[] prices, string[] statArray)
    {
        if (selectedItem > 2)
        {
            weaponClass.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
            thigh.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
            foot.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
            foot_Left.transform.GetChild(selectedItem - 3).gameObject.SetActive(true);
        }

        shin.transform.GetChild(selectedItem).gameObject.SetActive(true);
        otherShin.transform.GetChild(selectedItem).gameObject.SetActive(true);

        itemName.text = "NAME: " + shin.transform.GetChild(selectedItem).gameObject.name;
        cost.text = "COST: " + prices[selectedItem];
        stats.text = statArray[selectedItem];


    }

    public void ChestSwitcher4(GameObject weaponClass, GameObject rightHand, GameObject leftHand, int[] prices, string[] statArray)
    {
        weaponClass.transform.GetChild(selectedItem).gameObject.SetActive(true);
        rightHand.transform.GetChild(selectedItem).gameObject.SetActive(true);
        leftHand.transform.GetChild(selectedItem).gameObject.SetActive(true);
        itemName.text = "NAME: " + weaponClass.transform.GetChild(selectedItem).gameObject.name;
        cost.text = "COST: " + prices[selectedItem];
        stats.text = statArray[selectedItem];


    }
    private void Switcher3(GameObject parent)
    {

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(false);
        }

    }
    public void Switcher2()
    {
        shieldIcon.GetComponent<Image>().sprite = shieldIconBlack;
        axeIcon.GetComponent<Image>().sprite = axeIconBlack;
        swordIcon.GetComponent<Image>().sprite = swordIconBlack;
        helmetIcon.GetComponent<Image>().sprite = helmetIconBlack;
        hammerIcon.GetComponent<Image>().sprite = hammerIconBlack;
        handIcon.GetComponent<Image>().sprite = handIconBlack;
        chestIcon.GetComponent<Image>().sprite = chestIconBlack;
        feetIcon.GetComponent<Image>().sprite = feetIconBlack;
    }
    public void Shields()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pShield - 1);
        SwitchCategory();
        buyShields = true;
        if (PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pShield > 0)
        {
            shields.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        else
        {
            shields.transform.GetChild(0).gameObject.SetActive(true);

        }
        Switcher4(shields, shieldPrices, shieldStats);
        shieldIcon.GetComponent<Image>().sprite = shieldIconWhite;
    }
    public void Legs()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pLegs - 1);
        SwitchCategory();
        buyFeetGuards = true;
        if (PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pLegs > 0)
        {
            if (PlayerAttributes.pLegs > 3)
            {
                thighGuards.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                thighGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                footGuards.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                footGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
            }

            shinGuards.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);
            shinGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);
        }
        else
        {

            shinGuards.transform.GetChild(0).gameObject.SetActive(true);
            shinGuards_Left.transform.GetChild(0).gameObject.SetActive(true);

        }
        LegSwitcher4(thighGuards, thighGuards_Left, shinGuards, shinGuards_Left, footGuards, footGuards_Left, feetPrices, feetStats);
        feetIcon.GetComponent<Image>().sprite = feetIconWhite;
    }
    public void ArmGuards()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pArm - 1);
        SwitchCategory();
        buyArmGuards = true;
        if (PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pArm > 0)
        {
            if (PlayerAttributes.pArm > 3)
            {
                handGuards.transform.GetChild(PlayerAttributes.pArm - 4).gameObject.SetActive(true);
                handGuards_Left.transform.GetChild(PlayerAttributes.pArm - 4).gameObject.SetActive(true);

            }
            armGuards.transform.GetChild(PlayerAttributes.pArm - 1).gameObject.SetActive(true);
            armGuards_Left.transform.GetChild(PlayerAttributes.pArm - 1).gameObject.SetActive(true);
        }
        else
        {
            armGuards.transform.GetChild(0).gameObject.SetActive(true);
            armGuards_Left.transform.GetChild(0).gameObject.SetActive(true);

        }
        ArmSwitcher4(armGuards, armGuards_Left, handGuards_Left, handGuards, armPrices, armStats);
        handIcon.GetComponent<Image>().sprite = handIconWhite;
    }
    public void ChestPlate()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pChest - 1);
        SwitchCategory();
        buyChestPlate = true;
        if (PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pChest > 0)
        {
            chestPlate.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
            shoulderPads.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
            shoulderPads_Left.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
        }
        else
        {
            chestPlate.transform.GetChild(0).gameObject.SetActive(true);
            shoulderPads.transform.GetChild(0).gameObject.SetActive(true);
            shoulderPads_Left.transform.GetChild(0).gameObject.SetActive(true);
        }
        ChestSwitcher4(chestPlate, shoulderPads, shoulderPads_Left, chestPrices, chestStats);
        chestIcon.GetComponent<Image>().sprite = chestIconWhite;
    }
    public void Axes()
    {
        selectedItem = Math.Max(0, PlayerAttributes.pAxe - 1);
        SwitchCategory();
        buyAxes = true;
        if (PlayerAttributes.pAxe > 0)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        else
        {
            axes.transform.GetChild(0).gameObject.SetActive(true);
        }
        Switcher4(axes, axePrices, axeStats);
        axeIcon.GetComponent<Image>().sprite = axeIconWhite;
    }
    public void Swords()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pSwords - 1);
        SwitchCategory();
        buySwords = true;
        if (PlayerAttributes.pSwords > 0)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        else
        {
            swords.transform.GetChild(0).gameObject.SetActive(true);
        }
        Switcher4(swords, swordPrices, swordStats);
        swordIcon.GetComponent<Image>().sprite = swordIconWhite;
    }
    public void Maces()
    {
        selectedItem = Math.Max(0, PlayerAttributes.pHammers - 1);
        SwitchCategory();
        buyHammers = true;
        if (PlayerAttributes.pHammers > 0)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
        }
        else
        {
            hammers.transform.GetChild(0).gameObject.SetActive(true);
        }
        Switcher4(hammers, hammerPrices, hammerStats);
        hammerIcon.GetComponent<Image>().sprite = hammerIconWhite;
    }
    public void Helmets()
    {

        selectedItem = Math.Max(0, PlayerAttributes.pHelmet - 1);
        SwitchCategory();
        buyHelmets = true;
        if (PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pHelmet > 0)
        {
            helmets.transform.GetChild(PlayerAttributes.pHelmet - 1).gameObject.SetActive(true);
        }
        else
        {
            helmets.transform.GetChild(0).gameObject.SetActive(true);
        }
        Switcher4(helmets, helmetPrices, helmetStats);
        helmetIcon.GetComponent<Image>().sprite = helmetIconWhite;
    }
    public void SwitchCategory()
    {   //HandHeld
        Switcher3(shields);
        Switcher3(helmets);
        Switcher3(axes);
        Switcher3(swords);
        Switcher3(hammers);
        //Chest
        Switcher3(shoulderPads);
        Switcher3(shoulderPads_Left);
        Switcher3(chestPlate);
        //Arms
        Switcher3(handGuards_Left);
        Switcher3(armGuards_Left);
        Switcher3(handGuards);
        Switcher3(armGuards);
        //Legs
        Switcher3(shinGuards);
        Switcher3(shinGuards_Left);
        Switcher3(thighGuards);
        Switcher3(thighGuards_Left);
        Switcher3(footGuards);
        Switcher3(footGuards_Left);
        //armori palaset resetataan mitä on päällä vaihdon yhteydessä
        if (PlayerAttributes.pShield > 0)
        {
            shields.transform.GetChild(Math.Max(0, PlayerAttributes.pShield - 1)).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pHelmet > 0)
        {
            helmets.transform.GetChild(Math.Max(0, PlayerAttributes.pHelmet - 1)).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pArm > 0)
        {
            if (PlayerAttributes.pArm > 3)
            {
                handGuards.transform.GetChild(Math.Max(0, PlayerAttributes.pArm - 4)).gameObject.SetActive(true);
                handGuards_Left.transform.GetChild(Math.Max(0, PlayerAttributes.pArm - 4)).gameObject.SetActive(true);
            }

            armGuards.transform.GetChild(Math.Max(0, PlayerAttributes.pArm - 1)).gameObject.SetActive(true);
            armGuards_Left.transform.GetChild(Math.Max(0, PlayerAttributes.pArm - 1)).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pChest > 0)
        {
            chestPlate.transform.GetChild(Math.Max(0, PlayerAttributes.pChest - 1)).gameObject.SetActive(true);
            shoulderPads.transform.GetChild(Math.Max(0, PlayerAttributes.pChest - 1)).gameObject.SetActive(true);
            shoulderPads_Left.transform.GetChild(Math.Max(0, PlayerAttributes.pChest - 1)).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pLegs > 0)
        {
            if (PlayerAttributes.pLegs > 3)
            {
                thighGuards.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                thighGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                footGuards.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                footGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
            }

            shinGuards.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);
            shinGuards_Left.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);
        }
        Switcher2();
        buyAxes = false;
        buySwords = false;
        buyHelmets = false;
        buyShields = false;
        buyHammers = false;
        buyArmGuards = false;
        buyChestPlate = false;
        buyFeetGuards = false;
    }
    public void Buy()
    {

        if (buyShields == true)
        {

            if (selectedItem > PlayerAttributes.pShield - 1 && PlayerAttributes.currentGold >= shieldPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= shieldPrices[selectedItem];
                PlayerAttributes.pShield = selectedItem + 1;

            }


        }
        if (buyAxes == true)
        {
            if (selectedItem > PlayerAttributes.pAxe - 1 && selectedItem > PlayerAttributes.pSwords - 2 && selectedItem > PlayerAttributes.pHammers - 2 && PlayerAttributes.currentGold >= axePrices[selectedItem])
            {
                PlayerAttributes.currentGold -= axePrices[selectedItem];
                PlayerAttributes.pAxe = selectedItem + 1;
                PlayerAttributes.pWeaponCategoryInUse = 2;
            }
        }
        if (buySwords == true)
        {
            if (selectedItem > PlayerAttributes.pAxe - 2 && selectedItem > PlayerAttributes.pSwords - 1 && selectedItem > PlayerAttributes.pHammers - 2 && PlayerAttributes.currentGold >= swordPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= swordPrices[selectedItem];
                PlayerAttributes.pSwords = selectedItem + 1;
                PlayerAttributes.pWeaponCategoryInUse = 1;
            }

        }
        if (buyHelmets == true)
        {
            if (selectedItem > PlayerAttributes.pHelmet - 1 && PlayerAttributes.currentGold >= helmetPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= helmetPrices[selectedItem];
                PlayerAttributes.pHelmet = selectedItem + 1;
            }
        }
        if (buyHammers == true)
        {
            if (selectedItem > PlayerAttributes.pAxe - 2 && selectedItem > PlayerAttributes.pSwords - 2 && selectedItem > PlayerAttributes.pHammers - 1 && PlayerAttributes.currentGold >= hammerPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= hammerPrices[selectedItem];
                PlayerAttributes.pHammers = selectedItem + 1;
                PlayerAttributes.pWeaponCategoryInUse = 3;
            }
        }
        if (buyChestPlate == true)
        {
            if (selectedItem > PlayerAttributes.pChest - 1 && PlayerAttributes.currentGold >= chestPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= chestPrices[selectedItem];
                PlayerAttributes.pChest = selectedItem + 1;
            }
        }
        if (buyArmGuards == true)
        {
            if (selectedItem > PlayerAttributes.pArm - 1 && PlayerAttributes.currentGold >= armPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= armPrices[selectedItem];
                PlayerAttributes.pArm = selectedItem + 1;
            }
        }
        if (buyFeetGuards == true)
        {
            if (selectedItem > PlayerAttributes.pLegs - 1 && PlayerAttributes.currentGold >= feetPrices[selectedItem])
            {
                PlayerAttributes.currentGold -= feetPrices[selectedItem];
                PlayerAttributes.pLegs = selectedItem + 1;
            }
        }

    }
}
