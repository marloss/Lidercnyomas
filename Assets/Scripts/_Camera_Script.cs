using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class _Camera_Script : MonoBehaviour
{
    [Header("Camera attributes")]
    public float sensitivity = 1;
    [Space]
    public float maximum_clamp;
    float xRotation = 0;
    [Header("Parent Body")]
    [Tooltip("For Horizontal rotation")]public Transform playerBody;


    void Start()
    {
        if (playerBody == null)
        {
            playerBody = GetComponentInParent<Transform>();
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float yMouse = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -maximum_clamp, maximum_clamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * xMouse);
    }
}