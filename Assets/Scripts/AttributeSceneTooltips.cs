using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSceneTooltips : MonoBehaviour
{

    private string text = "";

    public void HideTooltip()
    {
        ScreenSpaceTooltip.HideTooltip_Static();
    }
    
    public void ShowStr()
    {
        text = "Increases damage of attacks\nSlightly increases energy cost of attacks";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }
    public void ShowAtk()
    {
        text = "Increases chance to hit enemy";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowDef()
    {
        text = "Increases chance for enemy attacks to miss";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowAgi()
    {
        text = "Increases movement speed and jump distance";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowVit()
    {
        text = "Increases maximum health\nSlightly increases maximum energy";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowSta()
    {
        text = "Increases maximum energy\nIncreases amount of energy and health recovered by resting";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }
}
