using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAIRandom : MonoBehaviour
{
    enum Action { JumpLeft, MoveLeft, Plunge, JumpRight, MoveRight, Rest, HeavyAttack, MediumAttack, Quickattack }
    Rigidbody rb;
    int agility;
    int agilityMultiplier;
    int jumpMultiplier, moveMultiplier;

    GameManager gm;

    private IEnumerator coroutine;

    private CharacterController player;

    [SerializeField]
    private GameObject staminaBar;
    [SerializeField]
    private GameObject healthBar;
    private bool wait = false;
    private bool once = false;
    Animator anim;

    public bool plungePerformed;
    public bool isGrounded;
    public bool boundHit;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        moveMultiplier = 15;
        jumpMultiplier = 10;
        agility = 15;
        player = gm.player.GetComponent<CharacterController>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina Enemy");
        healthBar = GameObject.FindGameObjectWithTag("Health");
        anim = GetComponent<Animator>();
        plungePerformed = false;
        isGrounded = true;
        boundHit = false;
    }

    void Update()
    {
        if (gm.turn % 2 == 1) //using WaitForSeconds because otherwise enemy turn would be instant
        {
            wait = true;
            if (once == false)
            {
                coroutine = EnemyTurn(2f);
                StartCoroutine(coroutine);
            }

        }
    }

    public IEnumerator EnemyTurn(float waitTime)
    {
        once = true;
        yield return new WaitForSeconds(waitTime);
        if (wait == true)
        {
            Action a;
            if (staminaBar.GetComponent<Healthbar>().health <= 0) //force rest when stamina 0
            {
                a = (Action)5; //Rest
            }
            else if (boundHit) //Choosing other actions than moving right upon bound hit
            {
                int i = Random.Range(1, 2);

                if (i == 1)
                {
                    a = (Action)Random.Range(1, 3);
                }
                else if (i == 2 && gm.distance <= gm.attackingDistance)
                {
                    //Don't choose rest when stamina is max
                    if (staminaBar.GetComponent<Healthbar>().health >= staminaBar.GetComponent<Healthbar>().maximumHealth)
                        a = (Action)Random.Range(7, 9);
                    else
                        a = (Action)Random.Range(6, 9);
                }
                else
                {
                    a = (Action)5; //Rest
                }
                Debug.Log("Bound hit so AI chose : " + a);
            }
            else
            {
                if (gm.distance >= gm.attackingDistance)
                {
                    //Don't choose rest when stamina is max
                    if (staminaBar.GetComponent<Healthbar>().health >= staminaBar.GetComponent<Healthbar>().maximumHealth)
                        a = (Action)Random.Range(1, 5);
                    else
                        a = (Action)Random.Range(1, 6);
                }
                else
                {
                    int i = Random.Range(1, 2);
                    //Don't choose rest when stamina is max
                    if (staminaBar.GetComponent<Healthbar>().health >= staminaBar.GetComponent<Healthbar>().maximumHealth)
                    {
                        if (i == 1)
                        {
                            a = (Action)Random.Range(6, 9);
                        }
                        else
                        {
                            a = (Action)4;
                        }
                    }
                    else
                    {
                        a = (Action)Random.Range(4, 9); //Within attacking range, so don't move left
                    }
                }
            }
            Debug.Log("AI Chose : " + a);

            switch (a)
            {
                case Action.JumpRight:
                    JumpRight();
                    gm.turn++;
                    break;
                case Action.JumpLeft:
                    JumpLeft();
                    gm.turn++;
                    break;
                case Action.MoveRight:
                    MoveRight();
                    gm.turn++;
                    break;
                case Action.MoveLeft:
                    MoveLeft();
                    gm.turn++;
                    break;
                case Action.Plunge:
                    Plunge();
                    gm.turn++;
                    break;
                case Action.Rest:
                    Rest();
                    gm.turn++;
                    break;
                case Action.HeavyAttack:
                    HeavyAttack();
                    gm.turn++;
                    break;
                case Action.MediumAttack:
                    MediumAttack();
                    gm.turn++;
                    break;
                case Action.Quickattack:
                    QuickAttack();
                    gm.turn++;
                    break;
            }
        }


    }

    public void JumpRight()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 25); }
        rb.AddForce((Vector3.right + Vector3.up) * agility * jumpMultiplier);
        once = false;
    }

    public void JumpLeft()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 25); }
        rb.AddForce((Vector3.left + Vector3.up) * agility * jumpMultiplier);
        once = false;
    }

    public void MoveRight()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 22); }
        rb.AddForce(Vector3.right * agility * moveMultiplier);
        once = false;
    }

    public void MoveLeft()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 22); }
        rb.AddForce(Vector3.left * agility * moveMultiplier);
        once = false;
    }

    public void Plunge()
    {
        plungePerformed = true;
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 35); }
        rb.AddForce((Vector3.left + Vector3.up) * agility * jumpMultiplier);
        anim.SetTrigger("Punch");
        once = false;
    }

    public void Rest()
    {
        if (staminaBar != null) { staminaBar.SendMessage("GainHealth", 53); }
        once = false;
    }

    public void HeavyAttack()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 13); }
        if (healthBar != null)
        {
            healthBar.SendMessage("TakeDamage", 42);
            GameManager.instance._healthPlayer -= 42;
            once = false;
            anim.SetTrigger("HeavyPunch");
        }
    }

    public void MediumAttack()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 13); }
        if (healthBar != null)
        {
            healthBar.SendMessage("TakeDamage", 25);
            GameManager.instance._healthPlayer -= 25;
            once = false;
            anim.SetTrigger("Punch");
        }
    }

    public void QuickAttack()
    {
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 13); }
        if (healthBar != null)
        {
            healthBar.SendMessage("TakeDamage", 18);
            GameManager.instance._healthPlayer -= 18;
            once = false;
            anim.SetTrigger("Punch");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("Bound"))
        {
            boundHit = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            StartCoroutine(Airborne());
        }
        else if (col.gameObject.CompareTag("Bound"))
        {
            boundHit = false;
        }
    }

    IEnumerator Airborne()
    {
        while (isGrounded != true)
        {
            yield return null; // waiting until grounded again
        }

        if (gm.distance <= gm.attackingDistance && plungePerformed)
        {
            if (healthBar != null)
            {
                healthBar.SendMessage("TakeDamage", 16);
                GameManager.instance._healthEnemy -= 16;
                GameManager.instance.once = false;
                plungePerformed = false;
            }
        }
        else
        {
            plungePerformed = false;
        }
    }
}
