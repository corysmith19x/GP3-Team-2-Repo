using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeGiveMaxHealth : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerManager controller = other.GetComponent<PlayerManager>();
        if (controller != null)
        {
            controller.healthLevel += 1;
            controller.RefreshStats();
            Destroy(this.gameObject);
        }
    }
}
