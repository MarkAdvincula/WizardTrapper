using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Controller3D : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public GameObject bombs;
    public Animator charAnim;
    // public Transform cam;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.slopeLimit = 45f;
        controller.stepOffset = 0.3f;
        controller.skinWidth = 0.08f;
        controller.minMoveDistance = 0.001f;
        controller.center = new Vector3(0f, 1f, 0f);

        charAnim = GetComponent<Animator>();
    }

    void Update(){
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0){
            playerVelocity.y = 0f;
            charAnim.SetInteger("condition", 0);
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero){
            gameObject.transform.forward = move;
            charAnim.SetInteger("condition", 1);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire2") && bombs != null && groundedPlayer ) {
            GameObject newBombs = Instantiate(bombs, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
            newBombs.SetActive(true);
        }
    }
}