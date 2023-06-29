using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonMove : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement.Normalize(); // Normalize the movement vector to prevent faster diagonal movement

        bool isRunning = movement.magnitude > 0f;
        animator.SetBool("Run", isRunning);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, toRotation, rotationSpeed * Time.deltaTime));
        }

        Vector3 newPosition = rb.position + movement * speed * Time.deltaTime;
        rb.MovePosition(CheckWallCollision(newPosition));
    }

    private Vector3 CheckWallCollision(Vector3 newPosition)
    {
        BoxCollider playerCollider = GetComponent<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(newPosition + playerCollider.center, playerCollider.size / 2f, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                // If the new position would collide with a wall, return the current position instead
                return rb.position;
            }
        }

        return newPosition;
    }
}
