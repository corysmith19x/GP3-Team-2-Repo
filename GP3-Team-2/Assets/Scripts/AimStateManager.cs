using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSense;
    [SerializeField] float rightStickSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Value += Input.GetAxisRaw("Right Stick X") * rightStickSensitivity * Time.deltaTime;
        yAxis.Value -= Input.GetAxisRaw("Right Stick Y") * rightStickSensitivity * Time.deltaTime;

        xAxis.Value += (Input.GetAxisRaw("Mouse X") * mouseSense);
        yAxis.Value -= (Input.GetAxisRaw("Mouse Y") * mouseSense);
        yAxis.Value = Mathf.Clamp(yAxis.Value, -60, 60);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}