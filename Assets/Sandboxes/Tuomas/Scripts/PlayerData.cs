using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentStr;
    public int currentAtk;
    public int currentDef;
    public int currentAgi;
    public int currentVit;
    public int currentSta;
    public int currentbaseAttributes;

    public int currentUnspentAttributePoints;
    public int currentAttributePoinsPerLvl;

    public int currentLvl;
    public float ccurrentExp;

    public float currentBaseExp;

    public PlayerData() //tallennettavat tiedot PlayerAttributes-luokasta
    {
        currentStr = PlayerAttributes.pStr;
        currentAtk = PlayerAttributes.pAtk;
        currentDef = PlayerAttributes.pDef;
        currentAgi = PlayerAttributes.pAgi;
        currentVit = PlayerAttributes.pVit;
        currentSta = PlayerAttributes.pSta;
        currentbaseAttributes = PlayerAttributes.baseAttributes;

        currentUnspentAttributePoints = PlayerAttributes.unspentAttributePoints;
        currentAttributePoinsPerLvl = PlayerAttributes.attributePointsPerLevel;


        currentLvl = PlayerAttributes.pLvl;
        ccurrentExp = PlayerAttributes.currentExp;

        currentBaseExp = PlayerAttributes.baseExp;
    }
}