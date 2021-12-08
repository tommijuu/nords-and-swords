using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    //public Rigidbody playerRB;
    private CharacterController characterController;
    private GameManager gm;
    [HideInInspector]
    public GameObject buttonIconWhite;
    [HideInInspector]
    public GameObject buttonIconBlack;
    //[SerializeField]
    //private GameObject staminaBar;
    //[SerializeField]
    //private GameObject healthBar;
    //public GameObject effect;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        buttonIconWhite = gameObject.transform.GetChild(1).gameObject;
        buttonIconBlack = gameObject.transform.GetChild(2).gameObject;
        buttonIconWhite.SetActive(true);
        buttonIconBlack.SetActive(false);
        //staminaBar = GameObject.FindGameObjectWithTag("Stamina");
        //healthBar = GameObject.FindGameObjectWithTag("Health Enemy");
        //effect = GameObject.Find("Effect");
    }

    //Had to attach this script to buttons and attach it to buttons' OnClick() for things to work when instantiating the prefabs.
    //Actions themselves are currently in CharacterController.cs (called through characterController)

    public void MoveLeft()
    {
        characterController.MoveLeft();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
    }

    public void MoveRight()
    {
        characterController.MoveRight();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;

    }

    public void JumpRight()
    {
        characterController.JumpRight();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
    }

    public void JumpLeft()
    {
        characterController.JumpLeft();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
    }

    public void Plunge()
    {
        characterController.Plunge();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
    }

    public void Rest()
    {
        characterController.Rest();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
    }

    public void HeavyAttack()
    {
        characterController.HeavyAttack();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
        //if (effect != null)
        //    effect.GetComponent<Effect>().PlayEffectHeavy();
    }

    public void MediumAttack()
    {
        characterController.MediumAttack();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
        //if (effect != null)
        //    effect.GetComponent<Effect>().PlayEffectMedium();
    }

    public void QuickAttack()
    {
        characterController.QuickAttack();
        gm.buttons.SetActive(false);
        ActivateWhiteIcon();
        //gm.isPlayerTurn = false;
        //gm.turn++;
        //if (effect != null)
        //    effect.GetComponent<Effect>().PlayEffectQuick();

    }

    public void ActivateWhiteIcon()
    {
        buttonIconWhite.SetActive(true);
        buttonIconBlack.SetActive(false);
    }

    public void ActivateBlackIcon()
    {
        buttonIconWhite.SetActive(false);
        buttonIconBlack.SetActive(true);
    }
}
