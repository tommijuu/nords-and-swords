using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorShop : MonoBehaviour
{
    public GameObject buyTheItem;
    void Start()
    {
        buyTheItem = transform.Find("Buy the item").gameObject;
    }
    public void ItemClicked(string nameID)
    {
        buyTheItem.SetActive(true);
    }
    public void Buy()
    {



        buyTheItem.SetActive(false);
    }
    public void DontBuy()
    {

        buyTheItem.SetActive(false);

    }



}
