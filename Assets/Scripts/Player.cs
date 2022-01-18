using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform groundCheckTransform;

	private bool jumpKeyWasPressed;
	private float horizontalInput;
	private Rigidbody rigidbodyComponent;
	private bool isGrounded;


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
        if (!isGrounded)
        {
			return;
        }

		if (jumpKeyWasPressed)
		{
			rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
			jumpKeyWasPressed = false;
		}

		rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);
	}

    private void OnCollisionEnter(Collision collision)
    {
		isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
		isGrounded = false;
    }
}
