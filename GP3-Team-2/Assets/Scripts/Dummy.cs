using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    public GameObject uiShoot;
    public GameObject uiCap;

    [Header("Health Parameters")]
    public float health;
    public float maxHealth = 500f;
    bool isCapturable;

    [Header("Healthbar")]
    public Image healthBar;

    void Start()
    {
        health = maxHealth;
        isCapturable = false;
    }

    void Update()
    {
        if (!isCapturable)
        {
            //ui to shoot
            uiCap.SetActive(false);
            uiShoot.SetActive(true);
        }
        else if (isCapturable) 
        {
            //ui to press E to capture
            uiShoot.SetActive(false);
            uiCap.SetActive(true);
        }
        
        CheckCapturable();
        CheckHealth();

        healthBar.fillAmount = (float)health / maxHealth;
    }

    private void CheckCapturable()
    {
        if (health <= 100)
        {
            isCapturable = true;
        }
    }

    private void CheckHealth()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health <= 0)
        {
            health = 0;
        }
    }


    private void OnCollisionEnter(Collision other)
    {   
        if (other.gameObject.tag == "Bullet")
        {
            health -= 50f;
        }
        
        if (isCapturable)
        {
            if(other.gameObject.tag == "Net")
            {
                health = maxHealth;
                isCapturable = false;
            }
        }
    }
}
