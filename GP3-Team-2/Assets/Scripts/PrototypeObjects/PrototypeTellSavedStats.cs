using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeTellSavedStats : MonoBehaviour
{
    int health = StatsHolder.savedHealthLevel;
    int stam = StatsHolder.savedStamLevel;
    int damage = StatsHolder.savedDamageLevel;

    void Start()
    {
        Debug.Log("HP = " + health + " stam = " + stam + " dmg = " + damage);
    }
}
