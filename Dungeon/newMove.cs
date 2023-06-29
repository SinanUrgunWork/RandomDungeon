using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMove : MonoBehaviour
{
    public float movementSpeed = 5f;

    private void Update()
    {
        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Apply movement to the player
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}
