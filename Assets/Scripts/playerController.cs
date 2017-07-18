using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float movementSpeed;
    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;
    bool canAirMove;

    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;


	// Use this for initialization
	void Start () {
        canAirMove = true;

        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();

        facingRight = true;

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {

        if (grounded && Input.GetAxis("Jump")>0) {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpForce, 0));

        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
            grounded = true;
        else
            grounded = false;
        myAnim.SetBool("grounded", grounded);

        if (canAirMove)
        {
            float move = Input.GetAxis("Horizontal");

            myAnim.SetFloat("speed", Mathf.Abs(move));

            myRB.velocity = new Vector3((move * movementSpeed), myRB.velocity.y, 0);
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }

        if (grounded)
        {
            canAirMove = true;
        }

        Debug.Log(grounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tScale = transform.localScale;
        tScale.z *= -1;
        transform.localScale = tScale;
    }

}
