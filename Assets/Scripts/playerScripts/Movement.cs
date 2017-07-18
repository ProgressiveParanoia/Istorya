using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float gravitySpeed;
    [SerializeField]
    private float distanceToGround;
    [SerializeField]
    private float playerJumpSpeed;

    private GameManager GM;

    public GameObject footCollider;
    public GameObject currentStage;

    public bool facingRight;

    private float zPosition;

    private CharacterController playerCont;

    private Vector3 playerDirection;

    void Awake()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

	void Start () {
        playerCont = GetComponent<CharacterController>();

        zPosition = transform.position.z;
        facingRight = true;
    }
	
	// Update is called once per frame
	void Update () {
        move();
	}

    void move()
    {
        if (isGrounded)
        {
            float leftRight = Input.GetAxis("Horizontal") * playerSpeed;

            playerDirection.y = 0;
            playerDirection = new Vector3(leftRight, 0, 0);

            GetComponent<Animator>().SetBool("grounded", true);

            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("grounded", false);
                playerDirection.y += playerJumpSpeed;
            }

            if (leftRight > 0 && !facingRight)
                Flip();
            else
                if (leftRight < 0 && facingRight)
                Flip();

            GetComponent<Animator>().SetFloat("speed", Mathf.Abs(leftRight));
        }
        else
        if (!isGrounded) 
        {
            playerDirection.y -= gravitySpeed * Time.deltaTime;
        }

        playerCont.Move(playerDirection * Time.deltaTime);

        stabilize();

        if (Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("stage").transform.position.y) > 300)
        {
            GM.resetPlayerPosition();
            return;
        }

        //Debug.Log("Y Distance:" + Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("stage").transform.position.y));
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tScale = transform.localScale;
        tScale.z *= -1;
        transform.localScale = tScale;
    }

    void stabilize()
    {
        if(transform.position.z != zPosition)
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    public bool isGrounded
    {
        get
        {
            if (footCollider.GetComponent<footCollider>().grounded)
                return true;
            else
                return false;
        }
    }

    public void hitMove()
    {
        footCollider.GetComponent<footCollider>().grounded = false;

        if (facingRight)
        {
            playerDirection = new Vector3(-playerSpeed , playerSpeed, 0);
            playerCont.Move(playerDirection * Time.deltaTime);
        }else
        {
            playerDirection = new Vector3(playerSpeed , playerSpeed, 0);
            playerCont.Move(playerDirection * Time.deltaTime);
        }
    }

    void OnDisable()
    {
        GM.playerLocalScaleZ = transform.localScale.z;
    }
}
