using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int gold = 1000;

    public int Gold { get { return gold; } set { gold = value; } }

    public GameObject buttons;
    public GameObject playerPrefab;
    public GameObject golemPrefab;
    public GameObject skullPefab;
    public GameObject leshenPrefab;
    public GameObject trainingGuyPrefab;
    [HideInInspector]
    public int drawnEnemyType; //for selecting the enemy prefab on random

    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public GameObject player;

    public Vector3 playerStartPos;
    public Vector3 enemyStartPos;
    private GameObject canvasButtons;

    public int turn;
    private IEnumerator coroutine;

    public float distance;

    public float attackingDistance = 3f;

    public float _healthPlayer;
    public float _armorPlayer;
    public float _healthEnemy;
    public float _armorEnemy;

    public TMP_Text victoryGoldText;


    [SerializeField]
    private GameObject _healthBarPlayer;

    [SerializeField]
    private GameObject _healthBarEnemy;
    private GameObject _armorBarPlayer;
    private GameObject _armorBarEnemy;
    [SerializeField]
    private GameObject _postMatch;
    [SerializeField]
    private GameObject _postMatchDefeat;
    public bool postMatchActive;
    public GameObject _bloodGolem;
    public bool forceRest;
    public bool once;
    [Header("Exp variables:")]
    public GameObject _expBar;
    public float scalingFactorExp;
    public float level50Kills;
    public float level1Kills;
    private bool lvlUp;
    public int enemyLevel;
    [HideInInspector]
    public EnemyAttributes golemAttributes;
    [HideInInspector]
    public EnemyAttributes skullAttributes;
    [HideInInspector]
    public EnemyAttributes leshenAttributes;
    [HideInInspector]
    public EnemyAttributes trainingGuyAttributes;
    [HideInInspector]
    public int golemLevel;
    [HideInInspector]
    public int skullLevel;
    [HideInInspector]
    public int leshenLevel;
    public int trainingGuyLevel = 1;
    [Header("Stamina variables:")]
    [SerializeField]
    private GameObject _staminaBarPlayer;
    [SerializeField]
    private GameObject _staminaBarEnemy;
    [SerializeField]
    private float _baseStamina;
    [SerializeField]
    private float _perStamina;
    [SerializeField]
    private float _perVitality;
    public float _staminaAmountPlayer;

    [Header("Audio Resources:")]
    public AudioSource audioSource;

    public AudioClip playerLoss;
    public AudioClip playerVictory;

    public AudioClip playerDies;
    public AudioClip golemDies;
    public AudioClip skullDies;
    public AudioClip leshenDies;

    public bool deathBool = true;
    public bool deathBool2 = true;

    public bool giveGold = false;
    public float calculatePlayerArmor()
    {


        return player.GetComponent<CharacterController>().CalculateArmourAmount();
    }

    public float calculateEnemyArmor(EnemyAttributes enemyType)
    {
        return (enemyType.GetBaseArmorMulti() * enemyLevel);
    }
    void Start()
    {
        //Tommi TODO: replace these with enemylevel if we decide so
        enemyLevel = PlayerAttributes.pLvl;
        golemLevel = PlayerAttributes.pLvl;
        skullLevel = PlayerAttributes.pLvl;
        leshenLevel = PlayerAttributes.pLvl;
        postMatchActive = false;
        _armorEnemy = 0f;
        scalingFactorExp = 0.1836734694f;
        lvlUp = false;
        level1Kills = 1f;
        level50Kills = 10f;
        instance = this;
        turn = 0; Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "CombatScene" || scene.name == "EnviroCombatScene" || scene.name == "enviro_02" || scene.name == "enviro_03_training")
        {
            audioSource = GameObject.Find("SFXBoi").GetComponent<AudioSource>();

            _healthEnemy = 50;
            //  _armorPlayer = calculatePlayerArmor();
            _healthPlayer = 40 + 10 * PlayerAttributes.pVit;
            canvasButtons = GameObject.Find("CanvasButtons");
            //playerStartPos = new Vector3(-3.5f, 0.9f, -5f); For EnviroCombatScene
            playerStartPos = new Vector3(-2.3f, 0.9f, -1.8f);

            //Instantiating player
            player = Instantiate(playerPrefab, playerStartPos, playerPrefab.transform.rotation);

            //Instantiating enemy
            if (scene.name == "enviro_03_training")
            {
                drawnEnemyType = 3;

                enemyStartPos = new Vector3(2.3f, 0.9f, -1.8f);
                enemy = Instantiate(trainingGuyPrefab, enemyStartPos, trainingGuyPrefab.transform.rotation);


            }
            else //"This isn't the killing house anymore! This is real life." -Some Counter-Terrorist
            {
                drawnEnemyType = UnityEngine.Random.Range(0, 3);
                Debug.Log("Enemy Type drawn: " + drawnEnemyType);
                if (drawnEnemyType == 0) //Golem
                {

                    if (scene.name == "EnviroCombatScene")
                    {
                        enemyStartPos = new Vector3(3.5f, 0.9f, -5f);
                    }
                    else if (scene.name == "enviro_02")
                    {
                        enemyStartPos = new Vector3(2.3f, 0.9f, -1.8f);
                    }

                    enemy = Instantiate(golemPrefab, enemyStartPos, golemPrefab.transform.rotation);
                }
                else if (drawnEnemyType == 1) //Skull
                {

                    if (scene.name == "EnviroCombatScene")
                    {
                        enemyStartPos = new Vector3(3.5f, -0.8f, -5f);
                    }
                    else if (scene.name == "enviro_02")
                    {
                        enemyStartPos = new Vector3(2.3f, -0.2f, -1.8f);
                    }
                    enemy = Instantiate(skullPefab, enemyStartPos, skullPefab.transform.rotation);
                }
                else //Hiisi/Leshen
                {

                    enemyStartPos = new Vector3(2.3f, 0.9f, -1.8f); //enviro_02
                    enemy = Instantiate(leshenPrefab, enemyStartPos, leshenPrefab.transform.rotation);
                }
            }


            _armorPlayer = calculatePlayerArmor();
            FollowPlayer cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
            cameraScript.enemy = enemy;
            cameraScript.player = player;
            buttons.SetActive(true);

            _staminaBarEnemy = GameObject.FindGameObjectWithTag("Stamina Enemy");
            _healthBarPlayer = GameObject.FindGameObjectWithTag("Health");
            _armorBarPlayer = GameObject.FindGameObjectWithTag("Armor");
            _armorBarEnemy = GameObject.FindGameObjectWithTag("Armor Enemy");
            _staminaBarPlayer = GameObject.FindGameObjectWithTag("Stamina");
            _healthBarEnemy = GameObject.FindGameObjectWithTag("Health Enemy");
            _expBar = _postMatch.transform.GetChild(0).transform.GetChild(2).gameObject;
            if (_healthBarEnemy != null)
            {
                _healthBarEnemy.GetComponent<Healthbar>().health = _healthEnemy;
                _healthBarEnemy.GetComponent<Healthbar>().maximumHealth = _healthEnemy;

            }
            if (_armorBarEnemy != null)
            {
                _armorBarEnemy.GetComponent<Healthbar>().health = _armorEnemy;
                _armorBarEnemy.GetComponent<Healthbar>().maximumHealth = _armorEnemy;

            }
            if (_armorBarPlayer != null)
            {
                _armorBarPlayer.GetComponent<Healthbar>().health = _armorPlayer;
                _armorBarPlayer.GetComponent<Healthbar>().maximumHealth = _armorPlayer;

            }
            if (_healthBarPlayer != null)
            {
                _healthBarPlayer.GetComponent<Healthbar>().health = _healthPlayer;
                _healthBarPlayer.GetComponent<Healthbar>().maximumHealth = _healthPlayer;

            }
            _perStamina = 10f;
            _perVitality = 5f;
            _baseStamina = 35f;
            _staminaBarPlayer.GetComponent<Healthbar>().maximumHealth = _baseStamina + (PlayerAttributes.pSta * _perStamina) + (PlayerAttributes.pVit * 5);

            forceRest = false;
            once = false;

            //Getting enemy's stats according to type and level (or making a new one if type not yet instantiated)
            if (drawnEnemyType == 3 && trainingGuyAttributes == null) //if enemy is training guy
            {
                trainingGuyAttributes = EnemyList.trainingGuyAttributes;
                _healthEnemy = Mathf.Floor((35 + trainingGuyAttributes.GetVit(trainingGuyLevel) * 10) * trainingGuyAttributes.GetBaseHpMulti());
                _healthBarEnemy.GetComponent<Healthbar>().maximumHealth = _healthEnemy;
                _healthBarEnemy.GetComponent<Healthbar>().health = _healthEnemy;
                _armorEnemy = calculateEnemyArmor(trainingGuyAttributes);
                _armorBarEnemy.GetComponent<Healthbar>().maximumHealth = _armorEnemy;
                _armorBarEnemy.GetComponent<Healthbar>().health = _armorEnemy;
                _staminaBarEnemy.GetComponent<Healthbar>().maximumHealth = _baseStamina + (trainingGuyAttributes.GetSta(trainingGuyLevel) * _perStamina) + (trainingGuyAttributes.GetVit(trainingGuyLevel) * 5);
            }
            else
            {
                if (drawnEnemyType == 0 && golemAttributes == null) //Golem
                {
                    golemAttributes = EnemyList.golemAttributes;
                    _healthEnemy = Mathf.Floor((40 + golemAttributes.GetVit(golemLevel) * 10) * golemAttributes.GetBaseHpMulti());
                    _healthBarEnemy.GetComponent<Healthbar>().maximumHealth = _healthEnemy;
                    _healthBarEnemy.GetComponent<Healthbar>().health = _healthEnemy;
                    _armorEnemy = calculateEnemyArmor(golemAttributes);
                    _armorBarEnemy.GetComponent<Healthbar>().maximumHealth = _armorEnemy;
                    _armorBarEnemy.GetComponent<Healthbar>().health = _armorEnemy;
                    _staminaBarEnemy.GetComponent<Healthbar>().maximumHealth = _baseStamina + (golemAttributes.GetSta(golemLevel) * _perStamina) + (golemAttributes.GetVit(golemLevel) * 5);
                }
                else if (drawnEnemyType == 1 && skullAttributes == null) //Skull
                {
                    skullAttributes = EnemyList.skullAttributes;
                    _healthEnemy = Mathf.Floor((40 + skullAttributes.GetVit(skullLevel) * 10) * skullAttributes.GetBaseHpMulti());
                    _healthBarEnemy.GetComponent<Healthbar>().maximumHealth = _healthEnemy;
                    _healthBarEnemy.GetComponent<Healthbar>().health = _healthEnemy;
                    _armorEnemy = calculateEnemyArmor(skullAttributes);
                    _armorBarEnemy.GetComponent<Healthbar>().maximumHealth = _armorEnemy;
                    _armorBarEnemy.GetComponent<Healthbar>().health = _armorEnemy;
                    _staminaBarEnemy.GetComponent<Healthbar>().maximumHealth = _baseStamina + (skullAttributes.GetSta(skullLevel) * _perStamina) + (skullAttributes.GetVit(skullLevel) * 5);
                }
                else
                {
                    leshenAttributes = EnemyList.leshenAttributes;
                    _healthEnemy = Mathf.Floor((40 + leshenAttributes.GetVit(leshenLevel) * 10) * leshenAttributes.GetBaseHpMulti());
                    _healthBarEnemy.GetComponent<Healthbar>().maximumHealth = _healthEnemy;
                    _healthBarEnemy.GetComponent<Healthbar>().health = _healthEnemy;
                    _armorEnemy = calculateEnemyArmor(leshenAttributes);
                    _armorBarEnemy.GetComponent<Healthbar>().maximumHealth = _armorEnemy;
                    _armorBarEnemy.GetComponent<Healthbar>().health = _armorEnemy;
                    _staminaBarEnemy.GetComponent<Healthbar>().maximumHealth = _baseStamina + (leshenAttributes.GetSta(leshenLevel) * _perStamina) + (leshenAttributes.GetVit(leshenLevel) * 5);
                }
            }
        }
    }
    void Update()
    {

        if (player != null && enemy != null)
        {
            distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        }


        if (turn != 0 && turn % 2 == 0)
        {
            if (_staminaBarPlayer != null)
            {
                _staminaAmountPlayer = _staminaBarPlayer.GetComponent<Healthbar>().health;
                if (_staminaAmountPlayer > 0)
                {


                    forceRest = true;
                    if (once == false)
                    {
                        StartCoroutine(InstantiateButtons(2f));
                    }
                }
                else if (_staminaAmountPlayer <= 0)
                {
                    forceRest = true;
                    if (once == false)
                    {
                        coroutine = ForceRest(3f);
                        StartCoroutine(coroutine);
                    }


                }
            }
        }

        if (turn % 2 == 1)
        {
            buttons.SetActive(false);
            ButtonClick buttonClick = buttons.GetComponentInChildren<ButtonClick>(); //ButtonClick.cs from each children (hopefully works :D)
            buttonClick.ActivateWhiteIcon(); //want to make sure that the icons are white after force rest
        }
        if (_healthPlayer <= 0f || _healthEnemy <= 0f)
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "CombatScene" || scene.name == "EnviroCombatScene" || scene.name == "enviro_02" || scene.name == "enviro_03_training")
            {
                buttons.SetActive(false);
                if (_postMatch != null)
                {
                    if (_healthPlayer <= 0f)
                    {
                        StartCoroutine(PlayerDeath());
                    }
                    else if (_healthEnemy <= 0f)
                    {
                        if (drawnEnemyType == 3) //training guy
                        {
                            enemy.GetComponent<TrainingGuyAI>().enemyAnimator.SetTrigger("Death");

                            if (deathBool == true)
                            {
                                audioSource.PlayOneShot(playerDies);
                                deathBool = false;
                            }

                        }
                        else
                        {
                            if (drawnEnemyType == 0) //golem
                            {
                                //enemy.GetComponent<GolemAI>().enemyAnimator.SetBool("Die", true);
                                enemy.GetComponent<GolemAI>().enemyAnimator.SetTrigger("Die");

                                if (deathBool == true)
                                {
                                    audioSource.PlayOneShot(golemDies);
                                    deathBool = false;
                                }

                            }
                            else if (drawnEnemyType == 1) //skull
                            {
                                enemy.GetComponent<SkullAI>().enemyAnimator.SetBool("Die", true);
                                var flameEffect = enemy.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                                flameEffect.SetActive(false);
                                if (deathBool == true)
                                {
                                    audioSource.PlayOneShot(skullDies);
                                    deathBool = false;
                                }

                            }
                            else //Leshen
                            {
                                enemy.GetComponent<LeshenAI>().enemyAnimator.SetBool("Die", true);

                                if (deathBool == true)
                                {
                                    audioSource.PlayOneShot(leshenDies);
                                    deathBool = false;
                                }

                            }
                        }

                        StartCoroutine(PlayerVictory());
                    }

                }
            }
        }
    }

    public IEnumerator ForceRest(float waitTime)
    {
        once = true;
        yield return new WaitForSeconds(waitTime);
        if (forceRest == true)
        {
            player.GetComponent<CharacterController>().Rest();
            //isPlayerTurn = false;
            // turn++;


            forceRest = false;

            //enemy.GetComponent<GolemAI>().EnemyTurn(2f);
            // once = false;
        }
    }

    public void ToTheArena()
    {
        SceneManager.LoadScene("enviro_02");
    }

    public void ToTheMerchant()
    {
        if (_healthEnemy <= 0f)
        {
            _expBar.GetComponent<ExpBar>().firstTime = false;
            enemyLevel = PlayerAttributes.pLvl;
            PlayerAttributes.currentExp += ((PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl)) / (1 + scalingFactorExp * (enemyLevel - 1)) + 2);
            Debug.Log("Current Exp: " + PlayerAttributes.currentExp);
            PlayerAttributes.CalculateLevelUp();
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "EnviroCombatScene" || scene.name == "enviro_02" || scene.name == "enviro_03_training")
            {
                if (PlayerAttributes.unspentAttributePoints > 0)
                {
                    SceneManager.LoadScene("AttributeScene");
                }
                else { SceneManager.LoadScene("MapScene"); }
            }
            else
            {
                SceneManager.LoadScene("MapScene");
            }
        }
        else { SceneManager.LoadScene("MapScene"); }
    }
    IEnumerator PlayerDeath()
    {
        if (_postMatchDefeat != null)
        {
            //buttons.SetActive(false); Ei riittävän hyvä! Pitää estää vuoron vaihto varmaan kokonaan

            player.GetComponent<CharacterController>().playerAnimController.SetTrigger("Death");
            yield return new WaitForSeconds(3f);
            _postMatchDefeat.SetActive(true);
            postMatchActive = true;

            if (deathBool2 == true)
            {
                audioSource.PlayOneShot(playerLoss);
                deathBool2 = false;
            }
        }
    }
    IEnumerator PlayerVictory()
    {
        if (_postMatch != null && giveGold == false)
        {
            //buttons.SetActive(false); Ei riittävän hyvä! Pitää estää vuoron vaihto varmaan kokonaan
            giveGold = true;
            player.GetComponent<CharacterController>().playerAnimController.SetTrigger("Victory");
            yield return new WaitForSeconds(3f);
            _postMatch.SetActive(true);
            if (drawnEnemyType == 3) //training guy is lower level so smaller gold reward
            {
                if (victoryGoldText != null)
                {
                    victoryGoldText.text = PlayerAttributes.CalculateGoldReward(trainingGuyLevel).ToString() + " GOLD EARNED!";
                }

                PlayerAttributes.GivePlayerGoldReward(trainingGuyLevel);
            }
            else
            {
                if (victoryGoldText != null)
                {
                    victoryGoldText.text = PlayerAttributes.CalculateGoldReward(enemyLevel).ToString() + " GOLD EARNED!";
                }

                PlayerAttributes.GivePlayerGoldReward(enemyLevel);
            }
            postMatchActive = true;

            if (deathBool2 == true)
            {
                audioSource.PlayOneShot(playerVictory);
                deathBool2 = false;
            }
        }
    }
    public IEnumerator InstantiateButtons(float waitTime)
    {
        once = true;
        yield return new WaitForSeconds(waitTime);
        if (forceRest == true && _healthPlayer > 0f && _healthEnemy > 0f)
        {
            forceRest = false;
            buttons.SetActive(true);

        }
    }

    public void ApproveSound()
    {
        deathBool = true;
        deathBool2 = true;
    }
}

