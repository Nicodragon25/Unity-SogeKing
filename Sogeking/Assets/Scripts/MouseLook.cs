using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public GameObject player;
    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY ;
        xRotation = Mathf.Clamp(xRotation, -95f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
