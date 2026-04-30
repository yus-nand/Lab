using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public float moveSpeed;
    public float mouseSensitivity = 2f;
    public Camera cam;
    
    float horizontal, vertical;
    Vector3 move;
    float xRotation = 0f;

    void Update()
    {
        Movement();
        Look();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        move = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WorlItem"))
        {
            WorldItem item = other.transform.GetComponent<WorldItem>();
            item.Pickup(inventory);
            Debug.Log($"Picking up {name}");
            Destroy(other.gameObject);
        }
    }
}
