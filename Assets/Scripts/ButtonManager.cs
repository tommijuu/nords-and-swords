using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public List<GameObject> buttonsList; //Holds the starting button prefabs from the scene (not from prefabs folder)
    public List<GameObject> replaceableButtons; //Buttons which are replaced when in range
    public List<GameObject> replacingButtons; //Buttons which replace the replaceables when in range

    private GameObject buttonsJump; //container which holds jumpLeft & jumpRight
    private GameObject buttonsMove; //container which holds moveLeft & moveRight
    private GameObject buttonsAction; //container which holds rest & plunge

    private GameManager gm;

    private bool buttonsReplaced;

    private GameObject buttons;

    private GameObject player;

    private Button moveLeftButton;
    private Button jumpLeftButton;
    private Button restButton;

    //This script replaces attack buttons when in range,
    //disables player's left movement buttons when the left bound of the map is hit
    //and rest button too when stamina is 100

    //Button replacements could be figured out better (a bit harcoded for now)

    void Start()
    {
        buttons = this.gameObject;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = gm.playerPrefab.transform.GetChild(0).gameObject;

        buttonsJump = transform.GetChild(0).gameObject;
        buttonsMove = transform.GetChild(1).gameObject;
        buttonsAction = transform.GetChild(2).gameObject;

        buttonsReplaced = false;

        moveLeftButton = buttonsList[0].gameObject.GetComponent<Button>();
        jumpLeftButton = buttonsList[2].gameObject.GetComponent<Button>();
        restButton = buttonsList[4].gameObject.GetComponent<Button>();

        foreach (GameObject go in buttonsList)
        {
            go.GetComponent<Button>().interactable = true;
        }
    }

    void Update()
    {
        if (gm.distance <= gm.attackingDistance && !buttonsReplaced)
        {
            AddReplacingButtons();
            buttonsReplaced = true;
        }
        else if (gm.distance > gm.attackingDistance && buttonsReplaced)
        {
            AddReplaceableButtons();
            buttonsReplaced = false;
        }
    }

    private void AddReplacingButtons()
    {
        Destroy(buttonsJump.transform.GetChild(1).gameObject);
        Instantiate(replacingButtons[0], buttonsJump.transform);

        Destroy(buttonsMove.transform.GetChild(1).gameObject);
        Instantiate(replacingButtons[1], buttonsMove.transform);

        Destroy(buttonsAction.transform.GetChild(1).gameObject);
        Instantiate(replacingButtons[2], buttonsAction.transform);
    }

    private void AddReplaceableButtons()
    {
        Destroy(buttonsJump.transform.GetChild(1).gameObject);
        Instantiate(replaceableButtons[0], buttonsJump.transform);

        Destroy(buttonsMove.transform.GetChild(1).gameObject);
        Instantiate(replaceableButtons[1], buttonsMove.transform);

        Destroy(buttonsAction.transform.GetChild(1).gameObject);
        Instantiate(replaceableButtons[2], buttonsAction.transform);
    }

    public void MovementLeftActivation(bool activateLeftButtons)
    {
        if (!activateLeftButtons)
        {
            Debug.Log("Player hit a bound, let's deactivate left movement buttons");
            moveLeftButton.interactable = false;
            jumpLeftButton.interactable = false;
        }
        else
        {
            Debug.Log("Player exits the bound, let's activate left movement buttons again");
            moveLeftButton.interactable = true;
            jumpLeftButton.interactable = true;
        }
    }

    public void RestButtonActivation(bool activateRestButton)
    {
        if (!activateRestButton)
        {
            restButton.interactable = false;
        }
        else
        {
            restButton.interactable = true;
        }
    }
}
