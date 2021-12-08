using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GolemAI : MonoBehaviour
{
    private enum Action { JumpLeft, MoveLeft, Plunge, JumpRight, MoveRight, Rest, HeavyAttack, MediumAttack, Quickattack } //These are just to clarify the switch case ints
    private enum State { ADVANCE, ATTACK, RETREAT } //Advance is the default state
    private Rigidbody rb;
    private int agilityMultiplier;
    private int jumpMultiplier, moveMultiplier;

    private GameManager gm;

    private IEnumerator coroutine;

    private CharacterController player;

    [Header("Stamina variables:")]
    [SerializeField]
    private GameObject staminaBar;
    [SerializeField]
    private float _baseStaminaMove;
    [SerializeField]
    private float _staminaMoveScaling;
    [SerializeField]
    private float _staminaMoveTotal;
    public float _staminaAmountPlayer;
    [SerializeField]
    private float _maxEnergy;
    [SerializeField]
    private float _baseStaminaJump;
    [SerializeField]
    private float _staminaJumpScaling;
    [SerializeField]
    private float _staminaJumpTotal;
    [SerializeField]
    private float _staminaAttackBase;
    [SerializeField]
    private float _staminaQuickAttackMultiplier;
    [SerializeField]
    private float _staminaMediumAttackMultiplier;
    [SerializeField]
    private float _staminaHeavyAttackMultiplier;
    [SerializeField]
    private float _staminaBaseRecovery;
    [SerializeField]
    private float _staminaRecovery;
    [SerializeField]
    private float _staminaRecoveryMultiplier;
    [SerializeField]
    private float _staminaTotalRecoveryMultiplier;
    [SerializeField]
    private float _staminaMaxEnRatio;
    [SerializeField]
    private float _staminaRecoveryAmount;

    private GameObject healthBar;
    private GameObject armorBar;
    private bool wait = false;
    private bool once = false;


    private int state; //helper variable for the State

    private float attackType;
    private float attackMultiplier;

    private float playerDefenseMultiplier;

    private float _finalDamage;
    private float _weaponDamage;

    private float waitScalar;


    public Animator enemyAnimator;

    public bool plungePerformed;
    public bool isGrounded;
    public bool boundHit;

    public GameObject effect;



    void Start()
    {
        waitScalar = 0.5f;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        moveMultiplier = 10;
        jumpMultiplier = 10;
        agilityMultiplier = 15;
        attackMultiplier = 1f;
        playerDefenseMultiplier = 0f;
        _finalDamage = 0f;
        _weaponDamage = gm.golemAttributes.GetBaseDamage(gm.golemLevel);
        player = gm.player.GetComponent<CharacterController>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina Enemy");
        healthBar = GameObject.FindGameObjectWithTag("Health");
        armorBar = GameObject.FindGameObjectWithTag("Armor");
        enemyAnimator = GetComponent<Animator>();
        plungePerformed = false;
        isGrounded = true;
        boundHit = false;
        state = (int)State.ADVANCE;

        // setting stamina variables;
        _maxEnergy = staminaBar.GetComponent<Healthbar>().maximumHealth;
        _baseStaminaMove = 10f;
        _staminaMoveScaling = 0.05f;
        _staminaMoveTotal = _baseStaminaMove + (_maxEnergy * _staminaMoveScaling);
        _baseStaminaJump = 30f;
        _staminaJumpScaling = 0.15f;
        _staminaJumpTotal = _baseStaminaJump + (_maxEnergy * _staminaJumpScaling);
        _staminaAttackBase = 20f;
        _staminaQuickAttackMultiplier = 0.7f;
        _staminaMediumAttackMultiplier = 1f;
        _staminaHeavyAttackMultiplier = 1.5f;
        // stamina recovery variables;
        _staminaBaseRecovery = 10f;
        _staminaMaxEnRatio = 0.1f;
        _staminaRecovery = _staminaBaseRecovery + _maxEnergy * _staminaMaxEnRatio;
        _staminaRecoveryMultiplier = 0.075f;
        _staminaTotalRecoveryMultiplier = 1 + _staminaRecoveryMultiplier * gm.golemAttributes.GetSta(gm.golemLevel);
        _staminaRecoveryAmount = _staminaBaseRecovery * _staminaTotalRecoveryMultiplier;

        effect = GameObject.Find("Effect");
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
        while (wait == true) //false when a move is finally done
        {
            Action a;
            switch (state)
            {
                case (int)State.ADVANCE:
                    Debug.Log("ADVANCE STATE");
                    if (staminaBar.GetComponent<Healthbar>().health <= 0) //force rest when stam = 0
                    {
                        a = (Action)5; //Rest
                        Rest();
                        gm.turn++;
                    }
                    else
                    {
                        if (gm.distance >= gm.attackingDistance) //not within attacking range
                        {
                            if (healthBar.GetComponent<Healthbar>().health >= healthBar.GetComponent<Healthbar>().maximumHealth / 2) //hp more than half
                            {
                                a = (Action)Random.Range(0, 3);
                                switch (a)
                                {
                                    case Action.JumpLeft:
                                        JumpLeft();
                                        gm.turn++;
                                        break;
                                    case Action.MoveLeft:
                                        MoveLeft();
                                        //gm.turn++;
                                        break;
                                    case Action.Plunge:
                                        Plunge();
                                        gm.turn++;
                                        break;
                                }
                            }
                            else //hp less than half
                            {
                                if (staminaBar.GetComponent<Healthbar>().health <= staminaBar.GetComponent<Healthbar>().maximumHealth / 3) //stam less than third
                                {
                                    a = (Action)5; //Rest
                                    Rest();
                                    gm.turn++;
                                }
                                else
                                {
                                    if (gm.distance <= gm.attackingDistance) //enough stam but low hp, also within attacking range
                                    {
                                        state = (int)State.ATTACK;
                                    }
                                    else //not within attacking range, just escape
                                    {
                                        state = (int)State.RETREAT;
                                    }
                                }
                            }

                        }
                        else
                        {
                            state = (int)State.ATTACK;
                        }
                    }
                    break;

                case (int)State.RETREAT:
                    Debug.Log("RETREAT STATE");
                    if (!boundHit) //map's bound not hit
                    {
                        a = (Action)Random.Range(3, 5);
                        switch (a)
                        {
                            case Action.JumpRight:
                                JumpRight();
                                gm.turn++;
                                break;
                            case Action.MoveRight:
                                MoveRight();
                                //gm.turn++;
                                break;
                        }
                    }
                    else //map's bound hit
                    {
                        if (gm.distance <= gm.attackingDistance) //bound hit and player close, all you can do is attack.
                        {
                            state = (int)State.ATTACK;
                        }
                        else //not within attacking range, so advance
                        {
                            state = (int)State.ADVANCE;
                        }
                    }
                    break;

                case (int)State.ATTACK:
                    Debug.Log("ATTACK STATE");
                    a = (Action)Random.Range(6, 9);
                    switch (a)
                    {
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
                    break;
            }
        }
    }

    public void JumpRight()
    {
        Debug.Log("AI Chose: Jump Right");
        enemyAnimator.applyRootMotion = false;
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", _staminaJumpTotal); }
        rb.AddForce((Vector3.right + Vector3.up) * agilityMultiplier * jumpMultiplier);
        state = (int)State.ADVANCE;
        wait = false;
        once = false;
    }

    public void JumpLeft()
    {
        Debug.Log("AI Chose: Jump Left");
        enemyAnimator.applyRootMotion = false;
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", _staminaJumpTotal); }
        rb.AddForce((Vector3.left + Vector3.up) * agilityMultiplier * jumpMultiplier);
        state = (int)State.ADVANCE;
        wait = false;
        once = false;
    }

    public void MoveRight()
    {
        Debug.Log("AI Chose: Move Right");

        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", _staminaMoveTotal); }

        wait = false;
        once = false;
        state = (int)State.ADVANCE;
        StartCoroutine(WalkBackward());
        gm.turn++;
    }

    public void MoveLeft()
    {
        Debug.Log("AI Chose: Move Left");


        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", _staminaMoveTotal); }

        wait = false;
        once = false;
        state = (int)State.ADVANCE;
        StartCoroutine(WalkForward());
        gm.turn++;
    }

    public void Plunge() //damage dealing done in Airborne(), so it deals damage when grounded
    {
        Debug.Log("AI Chose: Plunge");
        enemyAnimator.applyRootMotion = false;
        plungePerformed = true;
        if (staminaBar != null) { staminaBar.SendMessage("TakeDamage", 35); }
        enemyAnimator.SetTrigger("Punch");
        rb.AddForce((Vector3.left + Vector3.up) * agilityMultiplier * jumpMultiplier);
        state = (int)State.ADVANCE;
        wait = false;
        once = false;
    }

    public void Rest()
    {
        Debug.Log("AI Chose: Rest");
        if (staminaBar != null)
        {
            staminaBar.SendMessage("GainHealth", _staminaRecoveryAmount);
            //TODO: health recovery here if we want that for enemy too
        }
        state = (int)State.ADVANCE;
        wait = false;
        once = false;
    }

    public void HeavyAttack()
    {

        Debug.Log("AI Chose: Heavy Attack");
        attackType = 0.7f;
        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + gm.golemAttributes.GetStr(gm.golemLevel) * _staminaHeavyAttackMultiplier);
            staminaBar.SendMessage("TakeDamage", totalCost);
        }
        if (healthBar != null)
        {
            enemyAnimator.SetTrigger("HeavyPunch");

            float hitChance = (gm.golemAttributes.GetAtk(gm.golemLevel) * attackType * attackMultiplier) / ((gm.golemAttributes.GetAtk(gm.golemLevel) + PlayerAttributes.pDef) + playerDefenseMultiplier);
            float hitRandom = Random.Range(0.00001f, 10.00000000001f) / 10;
            _finalDamage = 0;
            _finalDamage = (gm.golemAttributes.GetStr(gm.golemLevel) * 2 + _weaponDamage) * 1.5f;

            if (hitRandom < hitChance)
            {
                effect.GetComponent<Effect>().PlayEnemyHeavy();
                StartCoroutine(player.ReactHeavy(0.7f));
                if (_finalDamage >= GameManager.instance._armorPlayer)
                {
                    _finalDamage -= GameManager.instance._armorPlayer;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer = 0f;
                    GameManager.instance._healthPlayer -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer -= _finalDamage;
                }
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }
            else
            {
                StartCoroutine(player.Dodge(0.4f));
                Debug.Log("AI missed");
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }

        }
    }

    public void MediumAttack()
    {
        enemyAnimator.SetTrigger("MediumPunch");
        Debug.Log("AI Chose: Medium Attack");
        attackType = 1f;
        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + gm.golemAttributes.GetStr(gm.golemLevel) * _staminaMediumAttackMultiplier);
            staminaBar.SendMessage("TakeDamage", totalCost);
        }
        if (healthBar != null)
        {
            float hitChance = (gm.golemAttributes.GetAtk(gm.golemLevel) * attackType * attackMultiplier) / ((gm.golemAttributes.GetAtk(gm.golemLevel) + PlayerAttributes.pDef) + playerDefenseMultiplier);
            float hitRandom = Random.Range(0.00001f, 10.00000000000000001f) / 10;
            _finalDamage = 0;
            _finalDamage = (gm.golemAttributes.GetStr(gm.golemLevel) * 2 + _weaponDamage) * 1f;

            if (hitRandom < hitChance)
            {
                effect.GetComponent<Effect>().PlayEnemyMedium();
                StartCoroutine(player.ReactLight(0.7f));
                if (_finalDamage >= GameManager.instance._armorPlayer)
                {
                    _finalDamage -= GameManager.instance._armorPlayer;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer = 0f;
                    GameManager.instance._healthPlayer -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer -= _finalDamage;
                }
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }
            else
            {
                if (effect != null)
                {
                    effect.GetComponent<Effect>().PlayPlayerBlock(0.5f);
                }
                StartCoroutine(player.Block(0.5f));
                Debug.Log("AI missed");
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }

        }
    }

    public void QuickAttack()
    {
        
        Debug.Log("AI Chose: Quick Attack");
        attackType = 1.3f;
        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + gm.golemAttributes.GetStr(gm.golemLevel) * _staminaQuickAttackMultiplier);
            staminaBar.SendMessage("TakeDamage", 13);
        }
        if (healthBar != null)
        {
            enemyAnimator.SetTrigger("Punch");
            float hitChance = (gm.golemAttributes.GetAtk(gm.golemLevel) * attackType * attackMultiplier) / ((gm.golemAttributes.GetAtk(gm.golemLevel) + PlayerAttributes.pDef) + playerDefenseMultiplier);
            float hitRandom = Random.Range(0.00001f, 10.0000000000000000001f) / 10;
            _finalDamage = 0;
            _finalDamage = (gm.golemAttributes.GetStr(gm.golemLevel) * 2 + _weaponDamage) * 0.7f;

            if (hitRandom < hitChance)
            {
                effect.GetComponent<Effect>().PlayEnemyQuick();
                StartCoroutine(player.ReactLight(0.7f));
                if (_finalDamage >= GameManager.instance._armorPlayer)
                {
                    _finalDamage -= GameManager.instance._armorPlayer;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer = 0f;
                    GameManager.instance._healthPlayer -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorPlayer -= _finalDamage;
                }
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }
            else
            {
                if (effect != null)
                {
                    effect.GetComponent<Effect>().PlayPlayerBlock(0.5f);
                }
                StartCoroutine(player.Block(0.5f));
                Debug.Log("AI missed");
                state = (int)State.ADVANCE;
                wait = false;
                once = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            enemyAnimator.applyRootMotion = true;
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
        while (isGrounded != true) // waiting until grounded again
        {
            yield return null;
        }

        if (gm.distance <= gm.attackingDistance && plungePerformed) //plunge stuff
        {
            attackType = 1.3f;
            if (healthBar != null)
            {
                float hitChance = (gm.golemAttributes.GetAtk(gm.golemLevel) * attackType * attackMultiplier) / ((gm.golemAttributes.GetAtk(gm.golemLevel) + PlayerAttributes.pDef) + playerDefenseMultiplier);
                float hitRandom = Random.Range(0.00001f, 10.0000000000000000001f) / 10;
                _finalDamage = 0;
                _finalDamage = (gm.golemAttributes.GetStr(gm.golemLevel) * 2 + _weaponDamage) * 0.7f;
                if (hitRandom < hitChance)
                {
                    effect.GetComponent<Effect>().PlayEnemyQuick();
                    if (_finalDamage >= GameManager.instance._armorPlayer)
                    {
                        _finalDamage -= GameManager.instance._armorPlayer;
                        armorBar.SendMessage("TakeDamage", 100000);
                        healthBar.SendMessage("TakeDamage", _finalDamage);
                        GameManager.instance._armorPlayer = 0f;
                        GameManager.instance._healthPlayer -= _finalDamage;
                    }
                    else
                    {
                        armorBar.SendMessage("TakeDamage", _finalDamage);
                        GameManager.instance._armorPlayer -= _finalDamage;
                    }
                    plungePerformed = false;
                }
                else
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayPlayerBlock();
                    }
                    Debug.Log("AI missed");
                    plungePerformed = false;
                }
            }
        }
        else
        {
            plungePerformed = false;
        }
    }

    IEnumerator WalkForward()
    {
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("WalkForward", true);
        float waitTime = 1f + waitScalar * gm.golemAttributes.GetAgi(gm.golemLevel);
        yield return new WaitForSeconds(waitTime);
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("WalkForward", false);

    }

    IEnumerator WalkBackward()
    {
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("WalkBackward", true);
        float waitTime = 1f + waitScalar * gm.golemAttributes.GetAgi(gm.golemLevel);
        yield return new WaitForSeconds(waitTime);
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("WalkBackward", false);

    }

    public IEnumerator Block(float waitTime)
    {
        //audioSource.PlayOneShot(block1);
        yield return new WaitForSeconds(waitTime);
        enemyAnimator.SetTrigger("Block");
    }
}
