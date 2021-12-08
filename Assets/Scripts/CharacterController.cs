using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    int agility;
    public Text agilitytext;
    float jumpMultiplier, moveMultiplier;
    public ButtonManager buttonManager;
    public Animator playerAnimController;
    public bool isGrounded;

    GameManager gm;
    [Header("Weapons and armor on player:")]
    public GameObject shields;
    public GameObject axes;
    public GameObject swords;
    public GameObject helmets;
    public GameObject hammers;
    public GameObject activeWeapon;
    //armors 
    public GameObject armArmor;
    public GameObject armArmor_Left;
    public GameObject handArmor;
    public GameObject handArmor_Left;
    public GameObject shoulderArmor;
    public GameObject shoulderArmor_Left;
    public GameObject chestArmor;
    public GameObject legArmor;
    public GameObject legArmor_Left;
    public GameObject shinArmor;
    public GameObject shinArmor_Left;
    public GameObject thighArmor;
    public GameObject thighArmor_Left;


    [SerializeField]
    private GameObject healthBar;
    private GameObject armorBar;
    private GolemAI enemyGolemAI;
    private SkullAI enemySkullAI;
    private LeshenAI enemyLeshenAI;
    private TrainingGuyAI enemyTrainingAI;
    [Header("Attack Variables:")]
    public bool plungePerformed;
    public int _weaponDamage;
    public float _finalDamage;
    public float attackMultiplier;
    public float enemyDefense;
    public float enemyDefenseMultiplier;
    public float attackType;
    public float attackTypeQuick = 1.3f;
    public float attackTypeMedium = 1.0f;
    public float attackTypeHeavy = 0.7f;
    [Header("Weapon Damage variables:")]
    public int[] swordDamage;
    public int[] axeDamage;
    public int[] maceDamage;
    [Header("Armor Bonus variables:")]
    public int[] helmetBonusArmor;
    public int[] chestBonusArmor;
    public int[] armBonusArmor;
    public int[] shieldBonusArmor;
    public int[] legBonusArmor;
    public float armorSum;
    [Header("Weapon Enchant Damage variables:")]
    public int[] swordEnchantDamage;
    public int[] axeEnchantDamage;
    public int[] maceEnchantDamage;
    [Header("Weapon Enchant Materials:")]
    public Material[] maceEnchantMaterial;
    public Material axeEnchantMaterial;
    public Material[] swordEnchantMaterial;


    private bool isWalking;
    private float waitScalar;
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
    [Header("Health Recovery variables:")]
    [SerializeField]
    private float _healthRecoveryBase;
    [SerializeField]
    private float _healthRecoveryMaxHpMultiplier;
    [SerializeField]
    private float _healthRecoveryStaminaMultiplier;
    [SerializeField]
    private float _healthRecoveryTotalMultiplier;
    [SerializeField]
    private float _healthRecoveryTotalAmount;
    [SerializeField]
    private GameObject _healthBarPlayer;
    private GameObject _armorBarPlayer;





    [Header("Audio Resources:")]
    public AudioSource audioSource;
    public AudioClip block1;
    public AudioClip reactHeavy;
    public AudioClip reactLight;
    public AudioClip whoosh;
    public AudioClip metalHit1;
    public AudioClip metalHit2;

    public AudioClip fullJump;
    public AudioClip heal;
    public AudioClip playerDying;
    public AudioClip playerStep;
    public AudioClip playerDodge;
    public AudioClip playerOuch1;
    public AudioClip playerOuch2;

    public AudioClip golemHit1;
    public AudioClip golemHit2;
    public AudioClip golemHit3;
    public AudioClip golemOuch1;

    public AudioClip skullHit1;
    public AudioClip skullHit2;
    public AudioClip skullHit3;
    public AudioClip skullOuch1;
    public AudioClip skullOuch2;
    public AudioClip skullOuch3;

    public AudioClip leshenHit1;
    public AudioClip leshenHit2;
    public AudioClip leshenHit3;
    public AudioClip leshenOuch1;

    public AudioClip trainHit1;
    public AudioClip trainHit2;
    public AudioClip trainHit3;
    public AudioClip trainOuch1;
    public AudioClip trainOuch2;
    public AudioClip trainOuch3;

    public GameObject effect;



    void Start()
    {
        effect = GameObject.Find("Effect");
        buttonManager = GameObject.Find("CanvasButtons").gameObject.transform.GetChild(0).GetComponent<ButtonManager>();
        audioSource = GameObject.Find("SFXBoi").GetComponent<AudioSource>();
        waitScalar = 0.5f;
        isWalking = false;
        playerAnimController = GetComponent<Animator>();
        attackType = 0f;
        _weaponDamage = 0;
        _finalDamage = 0f;
        attackMultiplier = 1f;
        enemyDefense = 1f;
        enemyDefenseMultiplier = 0f;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina");
        healthBar = GameObject.FindGameObjectWithTag("Health Enemy");
        armorBar = GameObject.FindGameObjectWithTag("Armor Enemy");
        isGrounded = true;
        plungePerformed = false;
        moveMultiplier = 15;
        jumpMultiplier = 0.6f;
        agility = 15;
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
        _staminaHeavyAttackMultiplier = 1.3f;
        // stamina recovery variables;
        _staminaBaseRecovery = 10f;
        _staminaMaxEnRatio = 0.1f;
        _staminaRecovery = _staminaBaseRecovery + _maxEnergy * _staminaMaxEnRatio;
        _staminaRecoveryMultiplier = 0.075f;
        _staminaTotalRecoveryMultiplier = 1 + _staminaRecoveryMultiplier * PlayerAttributes.pSta;
        _staminaRecoveryAmount = _staminaBaseRecovery * _staminaTotalRecoveryMultiplier;
        // health recovery variables
        _healthRecoveryBase = 3f;
        _healthRecoveryMaxHpMultiplier = 0.05f;
        _healthRecoveryStaminaMultiplier = 0.05f;
        _healthRecoveryTotalMultiplier = 1 + _healthRecoveryStaminaMultiplier * (PlayerAttributes.pSta - 1);
        _healthRecoveryTotalAmount = _healthRecoveryBase * _healthRecoveryTotalMultiplier;
        //Setting correct armor and weapons on
        if (PlayerAttributes.pShield > 0)
        {
            shields.transform.GetChild(PlayerAttributes.pShield - 1).gameObject.SetActive(true);

        }
        if (PlayerAttributes.pAxe > 0 && PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);

            activeWeapon = axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject;
            _weaponDamage = axeDamage[PlayerAttributes.pAxe];
            Debug.Log("_wapondamage without enchant" + _weaponDamage);
            if (PlayerAttributes.pAxeEnchanted == PlayerAttributes.pAxe)
            {
                axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.GetComponent<Renderer>().material = axeEnchantMaterial;
                _weaponDamage += axeEnchantDamage[PlayerAttributes.pAxe - 1];
                Debug.Log("_wapondamage with enchant" + _weaponDamage);
            }
        }
        if (PlayerAttributes.pHammers > 0 && PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
            activeWeapon = hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject;
            _weaponDamage = maceDamage[PlayerAttributes.pHammers];
            if (PlayerAttributes.pHammersEnchanted == PlayerAttributes.pHammers)
            {
                hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.GetComponent<Renderer>().material = maceEnchantMaterial[PlayerAttributes.pHammersEnchanted - 1];
                _weaponDamage += maceEnchantDamage[PlayerAttributes.pHammers - 1];
            }
        }
        if (PlayerAttributes.pSwords > 0 && PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
            activeWeapon = swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject;
            _weaponDamage = swordDamage[PlayerAttributes.pSwords];
            if (PlayerAttributes.pSwordsEnchanted == PlayerAttributes.pSwords)
            {
                swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.GetComponent<Renderer>().material = swordEnchantMaterial[PlayerAttributes.pSwordsEnchanted];
                _weaponDamage += axeEnchantDamage[PlayerAttributes.pSwords - 1];
            }
        }
        if (PlayerAttributes.pWeaponCategoryInUse == 0)
        {
            swords.transform.GetChild(0).gameObject.SetActive(true);
            activeWeapon = swords.transform.GetChild(0).gameObject;

        }
        if (PlayerAttributes.pHelmet > 0)
        {
            helmets.transform.GetChild(PlayerAttributes.pHelmet - 1).gameObject.SetActive(true);

        }
        if (PlayerAttributes.pArm > 0)
        {

            if (PlayerAttributes.pArm > 3)
            {
                handArmor.transform.GetChild(PlayerAttributes.pArm - 4).gameObject.SetActive(true);
                handArmor_Left.transform.GetChild(PlayerAttributes.pArm - 4).gameObject.SetActive(true);
            }
            armArmor.transform.GetChild(PlayerAttributes.pArm - 1).gameObject.SetActive(true);
            armArmor_Left.transform.GetChild(PlayerAttributes.pArm - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pChest > 0)
        {

            chestArmor.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
            shoulderArmor.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
            shoulderArmor_Left.transform.GetChild(PlayerAttributes.pChest - 1).gameObject.SetActive(true);
        }
        if (PlayerAttributes.pLegs > 0)
        {

            shinArmor.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);
            shinArmor_Left.transform.GetChild(PlayerAttributes.pLegs - 1).gameObject.SetActive(true);

            if (PlayerAttributes.pLegs > 3)
            {
                thighArmor.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                thighArmor_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                legArmor.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
                legArmor_Left.transform.GetChild(PlayerAttributes.pLegs - 4).gameObject.SetActive(true);
            }
        }
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "CombatScene" || scene.name == "EnviroCombatScene" || scene.name == "enviro_02" || scene.name == "enviro_03_training")
        {
            if (gm.drawnEnemyType == 3) //training guy
            {
                enemyTrainingAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TrainingGuyAI>();
            }
            else
            {
                if (gm.drawnEnemyType == 0) //Golem
                {
                    enemyGolemAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GolemAI>();
                }
                else if (gm.drawnEnemyType == 1) //Skull
                {
                    enemySkullAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SkullAI>();
                }
                else
                {
                    enemyLeshenAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<LeshenAI>();
                }

            }

            rb = gameObject.GetComponent<Rigidbody>();
            _healthBarPlayer = GameObject.FindGameObjectWithTag("Health");
            _armorBarPlayer = GameObject.FindGameObjectWithTag("Armor");

        }

    }
    public float CalculateArmourAmount()
    {
        armorSum = 0;
        armorSum += shieldBonusArmor[PlayerAttributes.pShield];
        armorSum += helmetBonusArmor[PlayerAttributes.pHelmet];
        armorSum += armBonusArmor[PlayerAttributes.pArm];
        armorSum += chestBonusArmor[PlayerAttributes.pChest];
        armorSum += legBonusArmor[PlayerAttributes.pLegs];

        return armorSum;
    }
    private void Update()
    {
        if (buttonManager != null)
        {
            //Resting not optional while stamina is maxed
            if (staminaBar.GetComponent<Healthbar>().health >= staminaBar.GetComponent<Healthbar>().maximumHealth)
            {
                buttonManager.RestButtonActivation(false);
            }
            else
            {
                buttonManager.RestButtonActivation(true);
            }
        }
    }

    public void MoveRight()
    {
        audioSource.PlayOneShot(playerStep);
        if (staminaBar != null)
        {
            staminaBar.SendMessage("TakeDamage", _staminaMoveTotal);
            //GameManager.instance.once = false;
        }

        StartCoroutine(WalkForward());
    }

    public void MoveLeft()
    {
        audioSource.PlayOneShot(playerStep);
        if (staminaBar != null)
        {
            staminaBar.SendMessage("TakeDamage", _staminaMoveTotal);
            //GameManager.instance.once = false;

        }
        StartCoroutine(WalkBackward());
    }

    public void JumpRight()
    {
        audioSource.PlayOneShot(fullJump);
        StartCoroutine(JumpForward());
    }

    public void JumpLeft()
    {
        audioSource.PlayOneShot(fullJump);
        StartCoroutine(JumpBackward());
    }
    public IEnumerator PlungeTrail()
    {

        yield return new WaitForSeconds(0.2f);
        activeWeapon.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        activeWeapon.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Plunge()
    {
        audioSource.PlayOneShot(fullJump);
        playerAnimController.applyRootMotion = false;
        StartCoroutine(PlungeTrail());
        plungePerformed = true;
        Vector3 move = (Vector3.right + Vector3.up);
        rb.AddForce(move * agility * jumpMultiplier * 0.35f, ForceMode.Impulse);
        if (staminaBar != null)
        {
            staminaBar.SendMessage("TakeDamage", 13); //GameManager.instance.once = false; 
            playerAnimController.SetTrigger("LightAttack");
            audioSource.PlayOneShot(metalHit1);
        }
    }

    public void Rest()
    {
        if (staminaBar != null)
        {
            audioSource.PlayOneShot(heal);
            playerAnimController.SetTrigger("Rest");
            staminaBar.SendMessage("GainHealth", _staminaRecoveryAmount);

            _healthBarPlayer.SendMessage("GainHealth", _healthRecoveryTotalAmount);
            gm._healthPlayer += _healthRecoveryTotalAmount;

            GameManager.instance.once = false;
            if (gm._healthEnemy > 0)
            {
                gm.turn++;
            }
        }
    }
    public IEnumerator HeavyAttackTrail()
    {

        yield return new WaitForSeconds(0.5f);
        activeWeapon.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        activeWeapon.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void HeavyAttack()
    {
        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + PlayerAttributes.pStr) * _staminaHeavyAttackMultiplier;
            staminaBar.SendMessage("TakeDamage", totalCost);
        }
        if (healthBar != null)
        {
            playerAnimController.SetTrigger("HeavyAttack");
            StartCoroutine(HeavyAttackTrail());
            float hitchance = CalculatePlayerHitChance(attackTypeHeavy);
            float hitRandom = Random.Range(0.0001f, 10.00000000001f) / 10;
            Debug.Log("hitchance:" + hitchance);
            Debug.Log("hitrandom: " + hitRandom);
            _finalDamage = 0;
            _finalDamage = (PlayerAttributes.pStr * 2 + _weaponDamage) * 1.6f;

            if (hitRandom < hitchance)
            {
                if (effect != null)
                {
                    effect.GetComponent<Effect>().PlayEffectHeavy();
                }
                if (gm.drawnEnemyType == 0) //golem
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyGolemAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(golemHit3);
                    audioSource.PlayOneShot(golemOuch1);
                }
                else if (gm.drawnEnemyType == 1) //Skull
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemySkullAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }

                    audioSource.PlayOneShot(skullHit3);
                    audioSource.PlayOneShot(skullOuch3);
                }
                else if (gm.drawnEnemyType == 2) //leshen
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyLeshenAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(leshenHit3);
                    audioSource.PlayOneShot(trainHit3);
                    audioSource.PlayOneShot(leshenOuch1);
                }
                else //Training guy
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        StartCoroutine(enemyTrainingAI.ReactHeavy(0.7f));
                    }
                    audioSource.PlayOneShot(trainHit3);
                    audioSource.PlayOneShot(metalHit2);
                    audioSource.PlayOneShot(trainOuch3);
                }

                if (_finalDamage >= GameManager.instance._armorEnemy)
                {
                    _finalDamage -= GameManager.instance._armorEnemy;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy = 0f;
                    GameManager.instance._healthEnemy -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy -= _finalDamage;
                }


                Debug.Log("Hit Landed");

                if (gm.drawnEnemyType == 0)
                {
                    Vector3 bloodSpot = GameManager.instance.enemy.transform.GetChild(0).transform.position;
                    Quaternion bloodRotation = GameManager.instance.enemy.transform.GetChild(0).transform.rotation;
                    bloodRotation.eulerAngles = new Vector3(bloodRotation.eulerAngles.x, bloodRotation.eulerAngles.y - 90, bloodRotation.eulerAngles.z);
                    GameObject enemyblood = Instantiate(GameManager.instance._bloodGolem, bloodSpot, bloodRotation);
                    enemyblood.GetComponent<ParticleSystem>().Play();
                    Destroy(enemyblood, 6f);
                }
            }
            else
            {
                audioSource.PlayOneShot(whoosh);
                Debug.Log("Missed");

                if (gm.drawnEnemyType == 0) //golem
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                    StartCoroutine(enemyGolemAI.Block(0.8f));
                }
                else if (gm.drawnEnemyType == 1) //skull
                {
                    StartCoroutine(enemySkullAI.Dodge(0.8f));
                }
                else if (gm.drawnEnemyType == 2) //Leshen
                {
                    StartCoroutine(enemyLeshenAI.Dodge(0.8f));
                }
                else if (gm.drawnEnemyType == 3) //training guy
                {
                    StartCoroutine(enemyTrainingAI.Dodge(0.4f));
                }
            }

            GameManager.instance.once = false;

            if (gm._healthEnemy > 0)
            {
                gm.turn++;
            }

        }
    }
    public IEnumerator MediumAttackTrail()
    {

        yield return new WaitForSeconds(0.5f);
        activeWeapon.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        activeWeapon.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void MediumAttack()
    {

        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + PlayerAttributes.pStr) * _staminaMediumAttackMultiplier;
            staminaBar.SendMessage("TakeDamage", totalCost);
        }
        if (healthBar != null)
        {
            playerAnimController.SetTrigger("MediumAttack");
            StartCoroutine(MediumAttackTrail());
            float hitchance = CalculatePlayerHitChance(attackTypeMedium);
            float hitRandom = Random.Range(0.00001f, 10.00000000000000001f) / 10;
            Debug.Log("hitchance:" + hitchance);
            Debug.Log("hitrandom: " + hitRandom);
            _finalDamage = 0;
            _finalDamage = (PlayerAttributes.pStr * 2 + _weaponDamage) * 1f;

            if (hitRandom < hitchance)
            {
                if (effect != null)
                {
                    effect.GetComponent<Effect>().PlayEffectMedium();
                }

                if (gm.drawnEnemyType == 0) //golem
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyGolemAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(golemHit2);
                    audioSource.PlayOneShot(golemOuch1);
                }
                else if (gm.drawnEnemyType == 1) //Skull
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemySkullAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(skullHit2);
                    audioSource.PlayOneShot(skullOuch2);
                }
                else if (gm.drawnEnemyType == 2) //leshen
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyLeshenAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(leshenHit2);
                    audioSource.PlayOneShot(trainHit2);
                    audioSource.PlayOneShot(leshenOuch1);
                }
                else //Training guy
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        StartCoroutine(enemyTrainingAI.ReactLight(0.7f));
                    }
                    audioSource.PlayOneShot(trainHit2);
                    audioSource.PlayOneShot(metalHit2);
                    audioSource.PlayOneShot(trainOuch2);
                }

                if (_finalDamage >= GameManager.instance._armorEnemy)
                {
                    _finalDamage -= GameManager.instance._armorEnemy;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy = 0f;
                    GameManager.instance._healthEnemy -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy -= _finalDamage;
                }
                Debug.Log("Hit Landed");
            }
            else
            {
                audioSource.PlayOneShot(whoosh);
                Debug.Log("Missed");
                if (gm.drawnEnemyType == 0) //golem
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                    StartCoroutine(enemyGolemAI.Block(0.4f));
                }
                else if (gm.drawnEnemyType == 1) //skull
                {
                    StartCoroutine(enemySkullAI.Dodge(0.4f));
                }
                else if (gm.drawnEnemyType == 2) //Leshen
                {
                    StartCoroutine(enemyLeshenAI.Dodge(0.4f));
                }
                else if (gm.drawnEnemyType == 3) //training guy
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                    StartCoroutine(enemyTrainingAI.Block(0.5f));
                }
            }
            GameManager.instance.once = false;

            if (gm._healthEnemy > 0)
            {
                gm.turn++;
            }
        }
    }
    public IEnumerator QuickAttackTrail()
    {

        yield return new WaitForSeconds(0.2f);
        activeWeapon.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        activeWeapon.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void QuickAttack()
    {
        if (staminaBar != null)
        {
            float totalCost = (_staminaAttackBase + PlayerAttributes.pStr) * _staminaQuickAttackMultiplier;
            staminaBar.SendMessage("TakeDamage", totalCost);
        }
        if (healthBar != null)
        {
            playerAnimController.SetTrigger("LightAttack");
            StartCoroutine(QuickAttackTrail());
            float hitchance = CalculatePlayerHitChance(attackTypeQuick);
            float hitRandom = Random.Range(0.00001f, 10.0000000000000000001f) / 10;
            Debug.Log("hitchance:" + hitchance);
            Debug.Log("hitrandom: " + hitRandom);
            _finalDamage = 0;
            _finalDamage = (PlayerAttributes.pStr * 2 + _weaponDamage) * 0.7f;
            if (hitRandom < hitchance)
            {
                if (effect != null)
                {
                    effect.GetComponent<Effect>().PlayEffectQuick();
                }

                if (gm.drawnEnemyType == 0) //golem
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyGolemAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(golemHit1);
                    audioSource.PlayOneShot(golemOuch1);
                }
                else if (gm.drawnEnemyType == 1) //Skull
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemySkullAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(skullHit1);
                    audioSource.PlayOneShot(skullOuch1);
                }
                else if (gm.drawnEnemyType == 2) //leshen
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        enemyLeshenAI.enemyAnimator.SetTrigger("ReactToDmg");
                    }
                    audioSource.PlayOneShot(leshenHit1);
                    audioSource.PlayOneShot(trainHit1);
                    audioSource.PlayOneShot(leshenOuch1);
                }
                else //Training guy
                {
                    if (gm._healthEnemy - _finalDamage > 0)
                    {
                        StartCoroutine(enemyTrainingAI.ReactLight(0.7f));
                    }
                    audioSource.PlayOneShot(trainHit1);
                    audioSource.PlayOneShot(metalHit1);
                    audioSource.PlayOneShot(trainOuch1);
                }

                if (_finalDamage >= GameManager.instance._armorEnemy)
                {
                    _finalDamage -= GameManager.instance._armorEnemy;
                    armorBar.SendMessage("TakeDamage", 100000);
                    healthBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy = 0f;
                    GameManager.instance._healthEnemy -= _finalDamage;
                }
                else
                {
                    armorBar.SendMessage("TakeDamage", _finalDamage);
                    GameManager.instance._armorEnemy -= _finalDamage;
                }
                Debug.Log("Hit Landed");
            }
            else
            {
                audioSource.PlayOneShot(whoosh);
                Debug.Log("Missed");
                if (gm.drawnEnemyType == 0) //golem
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                    StartCoroutine(enemyGolemAI.Block(0.4f));
                }
                else if (gm.drawnEnemyType == 1)//skull
                {
                    StartCoroutine(enemySkullAI.Dodge(0.4f));
                }
                else if (gm.drawnEnemyType == 2) //Leshen
                {
                    StartCoroutine(enemyLeshenAI.Dodge(0.4f));
                }
                else if (gm.drawnEnemyType == 3) //training guy
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                    StartCoroutine(enemyTrainingAI.Block(0.5f));
                }
            }
            GameManager.instance.once = false;

            if (gm._healthEnemy > 0)
            {
                gm.turn++;
            }
        }
    }

    public float CalculatePlayerHitChance(float attackType)
    {
        float hitchance = (PlayerAttributes.pAtk * attackType * attackMultiplier) / ((PlayerAttributes.pAtk + enemyDefense) + enemyDefenseMultiplier);

        return hitchance;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerAnimController.applyRootMotion = true;
        }
        else if (col.gameObject.CompareTag("Bound"))
        {
            buttonManager.MovementLeftActivation(false);
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
            buttonManager.MovementLeftActivation(true);
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

            attackType = attackTypeQuick;
            if (healthBar != null)
            {
                float hitchance = (PlayerAttributes.pAtk * attackType * attackMultiplier) / ((PlayerAttributes.pAtk + enemyDefense) + enemyDefenseMultiplier);
                float hitRandom = Random.Range(0.00001f, 10.0000000000000000001f) / 10;
                Debug.Log("hitchance:" + hitchance);
                Debug.Log("hitrandom: " + hitRandom);
                _finalDamage = 0;
                _finalDamage = (PlayerAttributes.pStr * 2 + _weaponDamage) * 0.7f;
                if (hitRandom < hitchance)
                {
                    if (effect != null)
                    {
                        effect.GetComponent<Effect>().PlayEffectQuick();
                    }

                    if (gm.drawnEnemyType == 0) //golem
                    {
                        if (gm._healthEnemy - _finalDamage > 0)
                        {
                            enemyGolemAI.enemyAnimator.SetTrigger("ReactToDmg");
                        }
                        audioSource.PlayOneShot(metalHit1);
                    }
                    else if (gm.drawnEnemyType == 1) //Skull
                    {
                        if (gm._healthEnemy - _finalDamage > 0)
                        {
                            enemySkullAI.enemyAnimator.SetTrigger("ReactToDmg");
                        }

                        audioSource.PlayOneShot(metalHit2);
                    }
                    else if (gm.drawnEnemyType == 2) //leshen
                    {
                        if (gm._healthEnemy - _finalDamage > 0)
                        {
                            enemyLeshenAI.enemyAnimator.SetTrigger("ReactToDmg");
                        }
                        //soundeffect here
                    }
                    else //Training guy
                    {
                        if (gm._healthEnemy - _finalDamage > 0)
                        {
                            if (effect != null)
                            {
                                effect.GetComponent<Effect>().PlayEffectQuick();
                            }
                            StartCoroutine(enemyTrainingAI.ReactLight(0.7f));
                        }
                        //soundeffect here
                    }
                    if (_finalDamage >= GameManager.instance._armorEnemy)
                    {
                        _finalDamage -= GameManager.instance._armorEnemy;
                        armorBar.SendMessage("TakeDamage", 100000);
                        healthBar.SendMessage("TakeDamage", _finalDamage);
                        GameManager.instance._armorEnemy = 0f;
                        GameManager.instance._healthEnemy -= _finalDamage;
                    }
                    else
                    {
                        armorBar.SendMessage("TakeDamage", _finalDamage);
                        GameManager.instance._armorEnemy -= _finalDamage;
                    }
                    Debug.Log("Hit Landed");
                }
                else
                {
                    Debug.Log("Missed");
                    audioSource.PlayOneShot(whoosh);
                    if (gm.drawnEnemyType == 0) //golem
                    {
                        if (effect != null)
                        {

                            effect.GetComponent<Effect>().PlayEnemyBlock();
                        }
                        StartCoroutine(enemyGolemAI.Block(0.4f));
                    }
                    else if (gm.drawnEnemyType == 1) //skull
                    {
                        StartCoroutine(enemySkullAI.Dodge(0.4f));
                    }
                    else if (gm.drawnEnemyType == 2) //Leshen
                    {
                        StartCoroutine(enemyLeshenAI.Dodge(0.4f));
                    }
                    else if (gm.drawnEnemyType == 3) //training guy
                    {
                        StartCoroutine(enemyTrainingAI.Block(0.5f));
                        effect.GetComponent<Effect>().PlayEnemyBlock();
                    }
                }
            }
            Debug.Log("");
            //gm.turn++;
        }
        else
        {
            plungePerformed = false;
        }

        if (gm.turn % 2 == 0)
        {
            GameManager.instance.once = false;
            if (gm._healthEnemy > 0)
            {
                gm.turn++;
            }
        }

    }
    IEnumerator WalkForward()
    {

        playerAnimController.SetBool("Idle", false);
        playerAnimController.SetBool("WalkForward", true);
        float waitTime = 0.5f + waitScalar * PlayerAttributes.pAgi;
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetBool("Idle", true);
        playerAnimController.SetBool("WalkForward", false);
        GameManager.instance.once = false;
        if (gm._healthEnemy > 0)
        {
            gm.turn++;
        }
        audioSource.PlayOneShot(playerStep);
    }
    IEnumerator WalkBackward()
    {

        playerAnimController.SetBool("Idle", false);
        playerAnimController.SetBool("WalkBackwards", true);
        float waitTime = 0.2f + waitScalar * PlayerAttributes.pAgi;
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetBool("Idle", true);
        playerAnimController.SetBool("WalkBackwards", false);
        GameManager.instance.once = false;
        if (gm._healthEnemy > 0)
        {
            gm.turn++;
        }
        audioSource.PlayOneShot(playerStep);
    }
    IEnumerator JumpBackward()
    {
        // audioSource.PlayOneShot(fullJump);
        playerAnimController.applyRootMotion = false;

        Vector3 move = (Vector3.left);


        if (staminaBar != null)
        {

            staminaBar.SendMessage("TakeDamage", _staminaJumpTotal); //GameManager.instance.once = false;

        }
        playerAnimController.SetTrigger("JumpBackwards");
        yield return new WaitForSeconds(0.11f);
        rb.AddForce(move * agility * jumpMultiplier, ForceMode.Impulse);
        GameManager.instance.once = false;
        if (gm._healthEnemy > 0)
        {
            gm.turn++;
        }
        yield return new WaitForSeconds(0.7f);
        playerAnimController.applyRootMotion = true;
    }
    IEnumerator JumpForward()
    {
        // audioSource.PlayOneShot(fullJump);
        playerAnimController.applyRootMotion = false;

        Vector3 move = (Vector3.right);


        if (staminaBar != null)
        {

            staminaBar.SendMessage("TakeDamage", _staminaJumpTotal); //GameManager.instance.once = false;

        }
        playerAnimController.SetTrigger("JumpForward");
        yield return new WaitForSeconds(0.11f);
        rb.AddForce(move * agility * jumpMultiplier, ForceMode.Impulse);

        GameManager.instance.once = false;
        if (gm._healthEnemy > 0)
        {
            gm.turn++;
        }
        yield return new WaitForSeconds(0.7f);
        playerAnimController.applyRootMotion = true;
    }
    public IEnumerator ReactLight(float waitTime)
    {
        audioSource.PlayOneShot(reactLight);
        audioSource.PlayOneShot(playerOuch1);
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetTrigger("React Light");
    }
    public IEnumerator ReactHeavy(float waitTime)
    {
        audioSource.PlayOneShot(reactHeavy);
        audioSource.PlayOneShot(playerOuch2);
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetTrigger("React Heavy");
    }
    public IEnumerator Dodge(float waitTime)
    {
        audioSource.PlayOneShot(playerDodge);
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetTrigger("Dodge");
    }
    public IEnumerator Block(float waitTime)
    {
        audioSource.PlayOneShot(block1);
        yield return new WaitForSeconds(waitTime);
        playerAnimController.SetTrigger("Block");
    }



    //void Update()
    //{
    //    if (rb.velocity.magnitude != 0)
    //    {
    //        moving = true;
    //    }
    //    else
    //    {
    //        moving = false;
    //    }

    //    // Debug.Log("moving: " + moving);

    //    //agilitytext.text = "Agility: " + agility.ToString();
    //    if (Input.GetKeyDown("d"))
    //    {
    //        MoveRight();
    //    }
    //    if (Input.GetKeyDown("a"))
    //    {
    //        MoveLeft();
    //    }
    //    if (Input.GetKeyDown("e"))
    //    {
    //        JumpRight();
    //    }
    //    if (Input.GetKeyDown("q"))
    //    {
    //        JumpLeft();
    //    }
    //    if (Input.GetKeyDown("z"))
    //    {
    //        agility--;
    //    }
    //    if (Input.GetKeyDown("x"))
    //    {
    //        agility++;
    //    }
    //}
}
