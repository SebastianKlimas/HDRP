using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private CharacterController controller;
    private float 
        speed = 6f, 
        ground = 0.1f, 
        gravity = -9.81f, 
        jump = -2f;
    private bool isGrounded;
    private Vector3 target, velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void handleRunning()
    {
    }

    private void handleIdle()
    {
    }

    private void Update()
    {
        // Sprawd�, czy obiekt gracza dotyka powierzchni
        isGrounded = Physics.CheckSphere(transform.position, ground, groundLayerMask);
        if (velocity.y < 0 && isGrounded) velocity.y = -2f;

        // Poruszanie si�
        target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (isGrounded)
        {
            if (target != Vector3.zero)
            {
                handleRunning();
            }
            else
            {
                handleIdle();
            }

            // Skakanie
            if (Input.GetKeyDown(KeyCode.Space)) velocity.y = Mathf.Sqrt(jump * gravity);
        }

        // Zmie� pozycj� obiektu
        controller.Move(target * speed * Time.deltaTime);

        // Skakanies
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
