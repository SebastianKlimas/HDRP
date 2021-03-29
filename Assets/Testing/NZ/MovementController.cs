using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform camera;
    private CharacterController controller;
    private float 
        smoothVelocity,
        speed = 6f, 
        ground = 0.1f, 
        gravity = -9.81f, 
        jump = -2f;
    private bool isGrounded;
    private Vector3 target, velocity;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void handleRunning()
    {
        anim.SetFloat("speed", 1, 0.1f, Time.deltaTime);
    }

    private void handleIdle()
    {
        anim.SetFloat("speed", 0, 0.1f, Time.deltaTime);
    }
    private void handleWalking()
    {
        anim.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Update()
    {
        // Sprawdź, czy obiekt gracza dotyka powierzchni
        isGrounded = Physics.CheckSphere(transform.position, ground, groundLayerMask);
        if (velocity.y < 0 && isGrounded) velocity.y = -2f;

        // Poruszanie się
        target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            if (target == Vector3.zero)
            {
                handleIdle();
            }
            else if(target != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                handleRunning();
            }

            else 
            {
                handleWalking();
                 
            }
           


            // Skakanie
            if (Input.GetKeyDown(KeyCode.Space)) velocity.y = Mathf.Sqrt(jump * gravity);
        }
        
        // Zmień pozycję
        if (target.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, 0.1f);
            //transform.rotation = Quaternion.Euler(0f, angle, 0);
            //target = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            target = Camera.main.transform.forward * target.z + Camera.main.transform.right * target.x;
            controller.Move(target * speed * Time.deltaTime);
        }

        if (target != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3f);
        }

        // Wykonaj skok
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
