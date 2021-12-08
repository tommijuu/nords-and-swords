using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttributes : MonoBehaviour
{
    public static int pCurrentHP;

    public static int pStr = 1;
    public static int pAtk = 1;
    public static int pDef = 1;
    public static int pAgi = 1;
    public static int pVit = 1;
    public static int pSta = 1;
    public static int baseAttributes = 10; //Total player's base attributes at level 1 here.

    public static int unspentAttributePoints = 0;
    public static int attributePointsPerLevel = 5;

    public static int pLvl = 1;
    public static float currentExp = 0.0f;

    public static float baseExp = 50f;


    public TMP_Text playTextField;

    public static int pHelmet = 0;
    public static int pShield = 0;
    public static int pAxe = 0;
    public static int pSwords = 0;
    public static int pHammers = 0;
    public static int pChest = 0;
    public static int pArm = 0;
    public static int pLegs = 0;
    public static int pWeaponCategoryInUse = 0;

    public static int pHelmetEnchanted = 0;
    public static int pShieldEnchanted = 0;
    public static int pAxeEnchanted = 0;
    public static int pSwordsEnchanted = 0;
    public static int pHammersEnchanted = 0;

    public static float currentGold = 100f;
    public static float baseGoldReward = 50f;
    public static float goldRewardExponent = 1.05f;


    public static float CalculateGoldReward(int enemyLevel)
    {
        float reward = Mathf.Floor(baseGoldReward * Mathf.Pow(goldRewardExponent, (enemyLevel - 1)));
        return reward;
    }

    public static void GivePlayerGoldReward(int enemyLevel)
    {
        float reward = CalculateGoldReward(enemyLevel);
        currentGold += reward;
    }
    public static float CalculateExpRequirement(float currentLevel)
    {
        float exp = baseExp * Mathf.Pow(1.19f, currentLevel - 1);

        return exp;
    }

    public static void CalculateAttributePoints()
    {
        int totalAttributes = baseAttributes + (pLvl - 1) * attributePointsPerLevel;
        int currentAttributes = pStr + pAtk + pAgi + pDef + pVit + pSta;
        unspentAttributePoints = totalAttributes - currentAttributes;
    }

    public static void CalculateLevelUp()
    {
        float requiredExp = CalculateExpRequirement(pLvl);
        if (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            pLvl++;
            CalculateAttributePoints();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this); //tuomas/scripts/savesystem
    }

    public void LoadPlayer() //attributet tiedostosta
    {
        PlayerData data = SaveSystem.LoadPlayer();
        pStr = data.currentStr;
        pAtk = data.currentAtk;
        pDef = data.currentDef;
        pAgi = data.currentAgi;
        pVit = data.currentVit;
        pSta = data.currentSta;
        baseAttributes = data.currentbaseAttributes;

        unspentAttributePoints = data.currentUnspentAttributePoints;
        attributePointsPerLevel = data.currentAttributePoinsPerLvl;


        pLvl = data.currentLvl;
        currentExp = data.ccurrentExp;

        baseExp = data.currentBaseExp;


        playTextField.text = "CONTINUE";
    }



}
