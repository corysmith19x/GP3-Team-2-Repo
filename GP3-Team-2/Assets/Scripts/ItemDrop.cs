using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody itemRb;
    public float dropForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody>();
        itemRb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
