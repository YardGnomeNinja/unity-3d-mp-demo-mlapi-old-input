using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 5f;
    private float gravity = -9.81f;
    Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.isGrounded) {
            playerVelocity.y = gravity * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)) {
                playerVelocity.y = 5f;
            }
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        playerVelocity.x = horizontalInput;
        playerVelocity.z = verticalInput;

        var characterMovement = new Vector3(playerVelocity.x, 0, playerVelocity.z);
        characterController.Move(characterMovement * speed * Time.deltaTime);

        if(characterMovement != Vector3.zero) {
            transform.forward = characterMovement;
        }

        characterController.Move(new Vector3(0, playerVelocity.y, 0) * Time.deltaTime);
    }
}
