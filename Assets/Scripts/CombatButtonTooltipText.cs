using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtonTooltipText : MonoBehaviour
{

    private GameManager gm;

    public void HideTooltip()
    {
        ScreenSpaceTooltip.HideTooltip_Static();
    }

    //Replace calculate functions with code to actually calculate hit chance of attacks when implemented on character controller!
    private string CalculatePlunge()  
    {

        float hitchance = 100 * gm.player.GetComponent<CharacterController>().CalculatePlayerHitChance(gm.player.GetComponent<CharacterController>().attackTypeQuick);
        hitchance = Mathf.Min(hitchance, 100f);
        string hitString = hitchance.ToString("0.00") + "%";
        return hitString;
    }

    private string CalculateQuick()
    {
        float hitchance = 100 * gm.player.GetComponent<CharacterController>().CalculatePlayerHitChance(gm.player.GetComponent<CharacterController>().attackTypeQuick);
        hitchance = Mathf.Min(hitchance, 100f);
        string hitString = hitchance.ToString("0.00") + "%";
        return hitString;
    }

    private string CalculateMedium()
    {
        float hitchance = 100 * gm.player.GetComponent<CharacterController>().CalculatePlayerHitChance(gm.player.GetComponent<CharacterController>().attackTypeMedium);
        hitchance = Mathf.Min(hitchance, 100f);
        string hitString = hitchance.ToString("0.00") + "%";
        return hitString;
    }

    private string CalculateHeavy()
    {
        float hitchance = 100 * gm.player.GetComponent<CharacterController>().CalculatePlayerHitChance(gm.player.GetComponent<CharacterController>().attackTypeHeavy);
        hitchance = Mathf.Min(hitchance, 100f);
        string hitString = hitchance.ToString("0.00") + "%";
        return hitString;
    }

    public void ShowPlunge()
    {
        string text = "Plunge attack\nHit chance: <color=#E0E300>" + CalculatePlunge() + "</color>.";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowQuick()
    {
        string text = "Quick attack\nHit chance: <color=#E0E300>" + CalculateQuick() + "</color>.";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowMedium()
    {
        string text = "Medium attack\nHit chance: <color=#E0E300>" + CalculateMedium() + "</color>.";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowHeavy()
    {
        string text = "Heavy attack\nHit chance: <color=#E0E300>" + CalculateHeavy() + "</color>.";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowRest()
    {
        string text = "Rest";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowMoveLeft()
    {
        string text = "Move left";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowMoveRight()
    {
        string text = "Move right";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowJumpLeft()
    {
        string text = "Jump left";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    public void ShowJumpRight()
    {
        string text = "Jump right";
        ScreenSpaceTooltip.ShowTooltip_Static(text);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
