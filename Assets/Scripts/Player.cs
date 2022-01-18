using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private Transform groundCheckTransform = null;
	[SerializeField] private LayerMask playerMask;

	private bool jumpKeyWasPressed;
	private float horizontalInput;
	private Rigidbody rigidbodyComponent;
	private bool isGrounded;
	private int superJumpsRemaining;


    // Use this for initialization
    void Start () {
		rigidbodyComponent = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
			jumpKeyWasPressed = true;
        }

		horizontalInput = Input.GetAxis("Horizontal");
	}

	//fixed update is called once every physics update
    private void FixedUpdate()
    {
		rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0); // take the horiztonal input to move back and forth, keep the vertical (y) same

		if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) // not colliding with anything
        {
			return; // then don't let it jump again until it's colliding again
        }

		if (jumpKeyWasPressed)
		{
			float jumpPower = 5;
			if (superJumpsRemaining > 0)
            {
				jumpPower *= 2;
				superJumpsRemaining--;
            }
			rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
			jumpKeyWasPressed = false;
		}

		
	}

    private void OnCollisionEnter(Collision collision)
    {
		isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
		isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // 7 is the coin layer
        {
			Destroy(other.gameObject);
        }
		if (other.gameObject.layer == 8) // 8 is jumpcube
        {
			superJumpsRemaining++;
			Destroy(other.gameObject);
		}
    }
}
