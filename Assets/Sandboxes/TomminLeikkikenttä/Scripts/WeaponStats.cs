using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WeaponStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int cost;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopToolTip.ShowTooltip_Static("Cost: " + cost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopToolTip.HideToolTip_Static();
    }
}
