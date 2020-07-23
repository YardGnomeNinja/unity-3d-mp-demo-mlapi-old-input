using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Useful when not using a Rigidbody
    CharacterController characterController;
    public float speed = 5f;
    public float jumpVelocity = 4f;
    private float gravity = -9.81f;
    Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    // Note: For resons I don't yet comprehend, moving the gravity calculations below movement causes undesireable effects for isGrounded. Hence they happen first.
    void Update()
    {
        // Calculate Gravity
        if(characterController.isGrounded) {
            // If the character is on the ground, continue to apply downward force to keep them there
            playerVelocity.y = gravity * Time.deltaTime;

            // Only allow the character to jump if they are on the ground
            if(Input.GetKeyDown(KeyCode.Space)) {
                // If the player presses space, overcome gravity to jump
                playerVelocity.y = jumpVelocity;
            }
        }
        else
        {
            // If the character is not on the ground, increase the force of gravity gradually
            playerVelocity.y += gravity * Time.deltaTime;
        }
        
        // Get movement input
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        playerVelocity.x = horizontalInput;
        playerVelocity.z = verticalInput;

        // Store the character's movement for multiple uses
        var characterMovement = new Vector3(playerVelocity.x, 0, playerVelocity.z);

        // Move the character
        characterController.Move(characterMovement * speed * Time.deltaTime);

        // If the character moved, face them in the appropriate direction
        if(characterMovement != Vector3.zero) {
            transform.forward = characterMovement;
        }

        // Apply gravity
        characterController.Move(new Vector3(0, playerVelocity.y, 0) * Time.deltaTime);
    }
}
