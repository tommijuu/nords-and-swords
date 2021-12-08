using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraniningGuySquire : MonoBehaviour
{


    [Header("Weapons and armor on training guy:")]
    [Header("Handheld:")]
    public GameObject shields;
    public GameObject axes;
    public GameObject swords;
    public GameObject hammers;
    public GameObject activeWeapon;
    //armors 
    [Header("Arms:")]
    public GameObject armArmor;
    public GameObject armArmor_Left;
    public GameObject handArmor;
    public GameObject handArmor_Left;
    [Header("Chest and shoulders:")]
    public GameObject shoulderArmor;
    public GameObject shoulderArmor_Left;
    public GameObject chestArmor;
    [Header("Legs:")]
    public GameObject legArmor;
    public GameObject legArmor_Left;
    public GameObject shinArmor;
    public GameObject shinArmor_Left;
    public GameObject thighArmor;
    public GameObject thighArmor_Left;
    [Header("Helmets:")]
    public GameObject helmets;
    //private randomized ints of armors
    private int pShield;
    private int pArm;
    private int pLegs;
    private int pChest;
    private int pHelmet;
    public int pWeaponClass;
    //[Header("Armor Bonuses")]
    //public int[] helmetBonusArmor;
    //public int[] chestBonusArmor;
    //public int[] armBonusArmor;
    //public int[] shieldBonusArmor;
    //public int[] legBonusArmor;


    void Start()
    {
        Roll();

        if (pWeaponClass == 2)
        {
            int i = Random.Range(0, 5);
            axes.transform.GetChild(i).gameObject.SetActive(true);
            activeWeapon = axes.transform.GetChild(i).gameObject;

        }
        if (pWeaponClass == 0)
        {
            int i = Random.Range(0, 5);
            hammers.transform.GetChild(Random.Range(0, 5)).gameObject.SetActive(true);
            activeWeapon = hammers.transform.GetChild(i).gameObject;

        }
        if (pWeaponClass == 1)
        {
            int i = Random.Range(0, 5);
            swords.transform.GetChild(Random.Range(0, 8)).gameObject.SetActive(true);
            activeWeapon = swords.transform.GetChild(i).gameObject;
        }
        if (pShield > 0)
        {
            shields.transform.GetChild(pShield - 1).gameObject.SetActive(true);
        }
        if (pHelmet > 0)
        {
            helmets.transform.GetChild(pHelmet - 1).gameObject.SetActive(true);
        }
        if (pArm > 0)
        {
            if (pArm >= 4)
            {
                handArmor.transform.GetChild(pArm - 4).gameObject.SetActive(true);
                handArmor_Left.transform.GetChild(pArm - 4).gameObject.SetActive(true);
            }
            armArmor.transform.GetChild(pArm - 1).gameObject.SetActive(true);
            armArmor_Left.transform.GetChild(pArm - 1).gameObject.SetActive(true);
        }
        if (pChest > 0)
        {
            chestArmor.transform.GetChild(pChest - 1).gameObject.SetActive(true);
            shoulderArmor.transform.GetChild(pChest - 1).gameObject.SetActive(true);
            shoulderArmor_Left.transform.GetChild(pChest - 1).gameObject.SetActive(true);
        }
        if (pLegs > 0)
        {
            shinArmor.transform.GetChild(pLegs - 1).gameObject.SetActive(true);
            shinArmor_Left.transform.GetChild(pLegs - 1).gameObject.SetActive(true);

            if (pLegs >= 4)
            {
                thighArmor.transform.GetChild(pLegs - 4).gameObject.SetActive(true);
                thighArmor_Left.transform.GetChild(pLegs - 4).gameObject.SetActive(true);
                legArmor.transform.GetChild(pLegs - 4).gameObject.SetActive(true);
                legArmor_Left.transform.GetChild(pLegs - 4).gameObject.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Roll()
    {
        pShield = Random.Range(0, 6);
        pArm = Random.Range(0, 10);
        pLegs = Random.Range(0, 10);
        pChest = Random.Range(0, 10); ;
        pHelmet = Random.Range(0, 6);
        pWeaponClass = Random.Range(0, 3);
    }
    //public float CalculateArmorBonus()
    //{
    //    float armorSum = 0;
    //    armorSum += shieldBonusArmor[pShield];
    //    armorSum += helmetBonusArmor[pHelmet];
    //    armorSum += armBonusArmor[pArm];
    //    armorSum += chestBonusArmor[pChest];
    //    armorSum += legBonusArmor[pLegs];
    //    return armorSum;
    //}
}
