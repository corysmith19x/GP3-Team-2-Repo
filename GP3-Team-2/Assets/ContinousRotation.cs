using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust the speed of rotation as needed

    void Update()
    {
        // Rotate the object continuously on the Z axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
