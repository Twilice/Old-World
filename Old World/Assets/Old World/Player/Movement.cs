using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {

    Animator anim;
    protected CharacterController controller;
    GameObject playerCam;

    // On Script load, runs once after script is loaded (always runs, runs before start)
    void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
    // On Script start, runs once before the first update (not run if disabled)
    void Start()
    {

    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Move(DirectionRelativeObject(inputDirection, playerCam));
    }

    // FixedUpdate is called the same ammount of times per second
    void FixedUpdate()
    {
       
    }

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveVector = Vector3.zero;
  
    // Moves the player based on the input direction
    void Move(Vector3 direction)
    {
        if (direction.magnitude > 1)
            direction.Normalize();
        direction *= speed;

        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
                moveVector.y = jumpSpeed;
        }
        moveVector.y -= gravity * Time.deltaTime;
        moveVector.x = direction.x;
        moveVector.z = direction.z;
        controller.Move(moveVector * Time.deltaTime);


        anim.SetFloat("Forward", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("Sideways", Input.GetAxisRaw("Horizontal"));
    }
    Vector3 DirectionRelativeObject(Vector3 direction, GameObject relativeObject)
    {
        return relativeObject.transform.TransformDirection(direction);
    }
}
