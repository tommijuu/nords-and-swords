using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class AttributeScene : MonoBehaviour
{
    [SerializeField]
    protected GameObject DebugPanel;

    [SerializeField]
    protected TextMeshProUGUI lvlText;
    [SerializeField]
    protected TextMeshProUGUI strText;
    [SerializeField]
    protected TextMeshProUGUI atkText;
    [SerializeField]
    protected TextMeshProUGUI defText;
    [SerializeField]
    protected TextMeshProUGUI agiText;
    [SerializeField]
    protected TextMeshProUGUI vitText;
    [SerializeField]
    protected TextMeshProUGUI staText;
    [SerializeField]
    protected TextMeshProUGUI attrText;
    [SerializeField]
    protected TextMeshProUGUI expText;

    [SerializeField]
    protected InputField pLvlDebug;
    [SerializeField]
    protected InputField expDebug;

    public void ToggleDebugMenu()
    {
        DebugPanel.SetActive(!DebugPanel.activeSelf);
    }

    public void AddStr()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pStr++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }

    public void AddAtk()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pAtk++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }
    public void AddDef()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pDef++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }
    public void AddAgi()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pAgi++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }
    public void AddVit()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pVit++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }

    public void AddSta()
    {
        if (PlayerAttributes.unspentAttributePoints > 0)
        {
            PlayerAttributes.pSta++;
            PlayerAttributes.unspentAttributePoints--;
        }
    }

    private void UpdateAttributeScene()
    {
        PlayerAttributes.CalculateAttributePoints();
        PlayerAttributes.CalculateLevelUp();
        lvlText.text = "LEVEL = " + PlayerAttributes.pLvl;
        strText.text = "STRENGTH = " + PlayerAttributes.pStr;
        atkText.text = "ATTACK = " + PlayerAttributes.pAtk;
        defText.text = "DEFENSE = " + PlayerAttributes.pDef;
        agiText.text = "AGILITY = " + PlayerAttributes.pAgi;
        vitText.text = "VITALITY = " + PlayerAttributes.pVit;
        staText.text = "STAMINA = " + PlayerAttributes.pSta;
        attrText.text = "ATTRIBUTE POINTS = " + PlayerAttributes.unspentAttributePoints;

        //Updating exp% display text
        string expString;
        float expPercent;
        float required = PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl);
        if (required > 0) //Avoid edge cases of dividing by 0 or negative exp requirements.
        {
            expPercent = PlayerAttributes.currentExp / required * 100f;
        }
        else
        {
            expPercent = 0.00f;
        }

        // ToString(0.00) = 2 decimals
        expString = "EXP = " + expPercent.ToString("0.00") + "%";
        expText.text = expString;

    }

    public void DebugChangePlayerLevel()
    {
        string inputString = pLvlDebug.text;
        int parsedInt;
        bool parseResult;
        parseResult = int.TryParse(inputString, out parsedInt);
        if (parseResult && parsedInt >= 1) //Only edit variable if parsing string to int succeeded and input is a positive integer
        {
            PlayerAttributes.pLvl = parsedInt;
            UpdateAttributeScene();
        }
        else
        {
            pLvlDebug.text = "Input valid integer";
        }

    }

    public void DebugChangeCurrentExp()
    {
        string inputString = expDebug.text;
        float parsedFloat;
        bool parseResult;
        parseResult = float.TryParse(inputString, out parsedFloat);
        if (parseResult && parsedFloat >= 0.00) //Only edit variable if parsing string to float succeeded and input is non-negative
        {
            PlayerAttributes.currentExp = parsedFloat;
            UpdateAttributeScene();
        }
        else
        {
            pLvlDebug.text = "Input valid integer";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateAttributeScene();

    }

    // Update is called once per frame
    void Update()
    {

        UpdateAttributeScene();
    }
}
