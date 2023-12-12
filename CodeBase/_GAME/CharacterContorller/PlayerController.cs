using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Player1_Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        // Rotate the player towards the mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(target);
        }

        // Move the player
        movement = transform.TransformDirection(movement);
        movement *= speed;

        movement.y -= gravity * Time.deltaTime;

        controller.Move(movement * Time.deltaTime);

        // Set the appropriate animation based on the player's movement
        float moveSpeed = movement.magnitude / speed;
        animator.SetFloat("MoveSpeed", moveSpeed);

        float angle = Vector3.SignedAngle(transform.forward, movement, Vector3.up);
        animator.SetFloat("Turn", angle);
    }
}
