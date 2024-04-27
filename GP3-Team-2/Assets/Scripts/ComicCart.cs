using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpeningCutscene : MonoBehaviour
{
    public CinemachineDollyCart myDollyCart;

    void Start()
    {
        //cam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void OnTriggerEnter(Collider collision)
    {
        StartCoroutine("PauseCart");
    }

    IEnumerator PauseCart()
    {
        myDollyCart.m_Speed = 0f;
        yield return new WaitForSeconds(5f); //how long the cart waits before moving again
        myDollyCart.m_Speed = 2f;
    }
}