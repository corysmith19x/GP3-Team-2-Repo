using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeGiveDoubleJump : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerManager controller = other.GetComponent<PlayerManager>();
        if (controller != null)
        {
            controller.hasDoubleJump = true;
            Destroy(this.gameObject);
        }
    }
}
