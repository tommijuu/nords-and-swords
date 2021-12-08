using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExpBar : MonoBehaviour
{
    // Visible health bar ui:
    [SerializeField]
    private Slider healthbarDisplay;


    [Header("Main Variables:")]
    // Health variable: (default range: 0-100)
    [Tooltip("Health variable: (default range: 0-100)")] public float health = 100;
    public float previousHealth;
    public float counter;
    public float maxCounter;
    // Percentage of how full your health is: (0-100, no decimals)
    private string healthPercentage = "0";

    // Minimum possible heath:
    [Tooltip("Minimum possible heath: (default is 0)")] public float minimumHealth = 0;

    // Maximum possible health:
    [Tooltip("Maximum possible heath: (default is 100)")] public float maximumHealth = 100;

    // If the character has this health or less, consider them having low health:
    [Tooltip("Low health is less than or equal to this:")] public int lowHealth = 33;

    // If the character has between this health and "low health", consider them having medium health:
    // If they have more than this health, consider them having highHealth:
    [Tooltip("High health is greater than or equal to this:")] public int highHealth = 66;

    [Space]

    [Header("Regeneration:")]
    // If 'regenerateHealth' is checked, character will regenerate health/sec at the rate of 'healthPerSecond':
    public bool regenerateHealth;
    public float healthPerSecond;

    [Space]

    [Header("Healthbar Colors:")]
    public Color highHealthColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);


    [Header("Tmp:")]
    public GameObject numbers;

    [Header("EXP variables:")]
    [SerializeField]
    public bool firstTime = true;
    public int enemyLevel;
    public float scalingFactorExp;

    private void Start()
    {

        // If the healthbar hasn't already been assigned, then automatically assign it.
        if (healthbarDisplay == null)
        {
            healthbarDisplay = GetComponent<Slider>();
        }
        numbers = transform.Find("Text (TMP)").gameObject;
        // Set the minimum and maximum health on the healthbar to be equal to the 'minimumHealth' and 'maximumHealth' variables:
        health = 0;
        previousHealth = 0;
        maximumHealth = PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl);
        scalingFactorExp = 0.1836734694f;
        enemyLevel = PlayerAttributes.pLvl;
        firstTime = true;
        // health = maximumHealth;
        // Change the starting visible health to be equal to the variable:
        UpdateHealth();
        TakeDamage(PlayerAttributes.currentExp + ((PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl)) / (1 + scalingFactorExp * (enemyLevel - 1)) + 2));
    }

    // Every frame:
    private void Update()
    {
        UpdateHealth();
        // healthPercentage = int.Parse((Mathf.Round(maximumHealth * (health / 100f))).ToString());

        // If the player's health is below the minimum health, then set it to the minimum health:
        if (health < minimumHealth)
        {
            health = minimumHealth;
        }

        // If the player's health is above the maximum health, then set it to the maximum health:
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }
        if (counter > maxCounter)
        {
            previousHealth = health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }


    }

    // Set the health bar to display the same health value as the health variable:
    public void UpdateHealth()
    {
        if (firstTime == true)
        {
            float expAmount = PlayerAttributes.currentExp + ((PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl)) / (1 + scalingFactorExp * (enemyLevel - 1)) + 2);

            float healthpercentagecounter = Mathf.Min(100f, (expAmount / (PlayerAttributes.CalculateExpRequirement(PlayerAttributes.pLvl))) * 100f);

            healthPercentage = string.Format("{0:0}", healthpercentagecounter);
            numbers.GetComponent<TMP_Text>().text = healthPercentage + " / 100";

            healthbarDisplay.value = Mathf.Lerp(previousHealth / maximumHealth, health / maximumHealth, counter / maxCounter);
        }
    }

    public void GainHealth(float amount)
    {

        health += amount;
        UpdateHealth();
    }

    public void TakeDamage(float amount)
    {
        previousHealth = healthbarDisplay.value * maximumHealth;
        health = amount;
        counter = 0;
        UpdateHealth();

    }

    public void ChangeHealthbarColor(Color colorToChangeTo)
    {
        transform.Find("Bar").GetComponent<Image>().color = colorToChangeTo;
    }

    public void ToggleRegeneration()
    {
        regenerateHealth = !regenerateHealth;
    }

    public void SetHealth(float value)
    {
        health = value;
        UpdateHealth();
    }
}

