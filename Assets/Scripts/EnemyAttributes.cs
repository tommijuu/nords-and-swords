using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes
{
    private float baseHpMulti;
    private float baseArmorMulti;
    private float baseStaminaMulti;
    private float baseDamageMulti;

    private float strMulti;
    private float atkMulti;
    private float defMulti;
    private float agiMulti;
    private float vitMulti;
    private float staMulti;

    private float attributesPerLevel = 1.5f;

    
    public  EnemyAttributes(float hpMult, float armorMult, float staminaMult, float baseDamageMult, float strMult, float atkMult, float defMult, float agiMult, float vitMult, float staMult)
    {
        baseHpMulti = hpMult;
        baseArmorMulti = armorMult;
        baseStaminaMulti = staMult;
        baseDamageMulti = baseDamageMult;

        strMulti = strMult;
        atkMulti = atkMult;
        defMulti = defMult;
        agiMulti = agiMult;
        vitMulti = vitMult;
        staMulti = staMult;

    }


    public float GetBaseHpMulti()
    {
        return baseHpMulti ;
    }

    public float GetBaseArmorMulti()
    {
        return baseArmorMulti;
    }

    public float GetBaseStaminaMulti()
    {
        return baseStaminaMulti;
    }

    public int GetBaseDamage(int enemyLevel)
    {
        return Mathf.FloorToInt(baseDamageMulti * enemyLevel);
    }


    public int GetStr(int enemyLevel)
    {
        return Mathf.FloorToInt(strMulti * enemyLevel * attributesPerLevel);
    }

    public int GetAtk(int enemyLevel)
    {
        return Mathf.FloorToInt(atkMulti * enemyLevel * attributesPerLevel);
    }

    public int GetDef(int enemyLevel)
    {
        return Mathf.FloorToInt(defMulti * enemyLevel * attributesPerLevel);
    }

    public int GetAgi(int enemyLevel)
    {
        return Mathf.FloorToInt(agiMulti * enemyLevel * attributesPerLevel);
    }

    public int GetVit(int enemyLevel)
    {
        return Mathf.FloorToInt(vitMulti * enemyLevel * attributesPerLevel);
    }

    public int GetSta(int enemyLevel)
    {
        return Mathf.FloorToInt(staMulti * enemyLevel * attributesPerLevel);
    }
}
