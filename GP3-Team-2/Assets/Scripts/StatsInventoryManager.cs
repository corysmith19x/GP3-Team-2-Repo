using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsInventoryManager : MonoBehaviour
{
    StatsHolder savedStats;

    [Header("Stat Variables")]
    public float playerHealth;
    public float playerMaxHealth;
    public float playerStam;
    public float playerMaxStam;
    public int playerDamage;

    [Header("Stat Level Variables")]
    public int characterLevel;
    public int characterExp;

    [Header("Progression Variables")]
    public bool hasFireweed;
    public bool hasMothmanCapture;
    public bool hasMushrooms;
    public bool hasSirenCapture;
    public bool hasCactus;
    public bool hasFresnoCapture;

    [Header("UI Elements")]
    public Image healthBar;
    public Image staminaBar;

    public void Start()
    {
        StartStats();
        //Debug.Log("All Stats are base");
        //IncreaseStats();
        //Debug.Log("All Stats are increased");
        //WriteStats();
        //Debug.Log("All Stats are saved");
        //StartStats();
    }

    public void StartStats()
    {
        characterLevel = StatsHolder.savedCharacterLevel;
        characterExp = StatsHolder.savedCharacterExp;
        // Character Progression
        hasFireweed = StatsHolder.savedFireweed;
        hasMothmanCapture = StatsHolder.savedMothman;
        // Level 1
        hasMushrooms = StatsHolder.savedMushrooms;
        hasSirenCapture = StatsHolder.savedSiren;
        // Level 2
        hasCactus = StatsHolder.savedCactus;
        hasFresnoCapture = StatsHolder.savedFresno;
        // Level 3
        RefreshStats();
        playerHealth = playerMaxHealth;
        playerStam = playerMaxStam;
    }

    public void RefreshStats()
    {
        playerMaxHealth = 90 + (characterLevel * 10);
        playerMaxStam = 90 + (characterLevel * 10);
        playerDamage = 8 + (characterLevel * 2);
    }

    public void Update()
    {
        healthBar.fillAmount = (float)playerHealth / playerMaxHealth;
        staminaBar.fillAmount = (float)playerStam / playerMaxStam;

        Debug.Log("Player health is " + playerHealth);
        CheckHealth();
    }
    
    public void WriteStats()
    {
        StatsHolder.savedCharacterLevel = characterLevel;
        StatsHolder.savedCharacterExp = characterExp;
        // Character Progression
        StatsHolder.savedFireweed = hasFireweed;
        StatsHolder.savedMothman = hasMothmanCapture;
        // Level 1
        StatsHolder.savedMushrooms = hasMushrooms;
        StatsHolder.savedSiren = hasSirenCapture;
        // Level 2
        StatsHolder.savedCactus = hasCactus;
        StatsHolder.savedFresno = hasFresnoCapture;
        // Level 3
    }

    public void UpdateHealth(float mod)
    {
        // Updating health
        playerHealth += mod;
    }

    public void CheckHealth()
    {
        var player = GameObject.FindWithTag("Player");
        if (playerHealth <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Game Over");
        }
    }

    public void CheckExp()
    {
        if (characterExp >= 10)
        {
            characterLevel += 1;
            characterExp = 0;
            RefreshStats();
            playerHealth = playerMaxHealth;
            playerStam = playerMaxStam;
        }
    }

    // Dummied Out Stat Levels

    /*public void CheckStamExp()
    {
        if (stamExp >= 10)
        {
            stamLevel += 1;
            stamExp = 0;
            RefreshStats();
            playerStam = playerMaxStam;
        }
    }*/

    /*public void CheckDamageExp()
    {
        if (damageExp >= 10)
        {
            damageLevel += 1;
            damageExp = 0;
            RefreshStats();
        }
    }*/

    /*public void IncreaseStats()
    {
        healthLevel += 1;
        stamLevel += 1;
        damageLevel += 1;
    }*/




}