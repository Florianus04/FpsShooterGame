using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 3f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float verticalLookRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Hareket giriþlerini al
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizontalInput, 0f, verticalInput);
        moveInput = moveInput.normalized * moveSpeed;

        // Fare hareketlerini al
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        // Yatay dönüþü karakterin kendisi üzerinde yap
        transform.Rotate(Vector3.up * mouseX * lookSpeed);

        // Dikey dönüþü kameraya yap ve aþýrý dönüþleri sýnýrla
        verticalLookRotation += mouseY * lookSpeed;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -40f, 40f);
        Camera.main.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void FixedUpdate()
    {
        // Hareketi uygula
        rb.velocity = transform.TransformDirection(moveInput);
    }
}
