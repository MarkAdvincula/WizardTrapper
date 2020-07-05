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
    public float jumpHeight = 0.1f;
    public float gravityValue = -9.81f;
    public GameObject bombs;
    public Animator charAnim;

    protected JumpButton jumpbutton;
    protected Joystick joystick;
    protected TrapButton trapbutton;
    // public Transform cam;
    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        jumpbutton = FindObjectOfType<JumpButton>();
        trapbutton = FindObjectOfType<TrapButton>();
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

        Vector3 move = new Vector3(joystick.Horizontal * playerSpeed, 0, joystick.Vertical * playerSpeed);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero){
            gameObject.transform.forward = move;
            charAnim.SetInteger("condition", 1);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") || jumpbutton.Pressed && groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -0.4f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire2") || trapbutton.Pressed && bombs != null && groundedPlayer ) {
            GameObject newBombs = Instantiate(bombs, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
            newBombs.SetActive(true);
        }
    }
}