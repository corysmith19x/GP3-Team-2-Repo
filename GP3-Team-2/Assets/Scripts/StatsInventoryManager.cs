using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int healthLevel;
    public int stamLevel;
    public int damageLevel;

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
        healthLevel = StatsHolder.savedHealthLevel;
        stamLevel = StatsHolder.savedStamLevel;
        damageLevel = StatsHolder.savedDamageLevel;
        RefreshStats();
    }

    public void RefreshStats()
    {
        playerMaxHealth = 90 + (healthLevel * 10);
        playerHealth = playerMaxHealth;
        playerMaxStam = 90 + (stamLevel * 10);
        playerStam = playerMaxStam;
        playerDamage = 8 + (damageLevel * 2);
    }

    public void Update(){
        healthBar.fillAmount = (float)playerHealth / playerMaxHealth;
        staminaBar.fillAmount = (float)playerStam / playerMaxStam;
    }
    
    public void WriteStats()
    {
        StatsHolder.savedHealthLevel = healthLevel;
        StatsHolder.savedStamLevel = stamLevel;
        StatsHolder.savedDamageLevel = damageLevel;
    }

    public void UpdateHealth(float mod)
    {
        // Updating health
        playerHealth += mod;
    }

    /*public void IncreaseStats()
    {
        healthLevel += 1;
        stamLevel += 1;
        damageLevel += 1;
    }*/




}
