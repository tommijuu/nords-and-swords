using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeButtonTextGoWhiteXD : MonoBehaviour
{

    private TMP_Text buttonText;

    void Awake()
    {
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void White()
    {
        buttonText.color = Color.white;
    }

    public void Black()
    {
        buttonText.color = Color.black;
    }
}
