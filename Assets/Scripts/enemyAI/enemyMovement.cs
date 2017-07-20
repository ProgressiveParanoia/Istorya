using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {

    [SerializeField]
    private Transform footCollider;

    public bool moveForward;
    public bool touchingPlayer;

    public float movementDelay;

	void Start () {
        moveForward = true;
    }

    void Update()
    {
   
        if (!touchingPlayer)
        {
            if (moveForward)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * 10));
            }
            else
            {
                transform.Translate(Vector3.back * (Time.deltaTime * 10));
            }
        }

        moveTimer();

        if(transform.name.Contains("Ramo"))
            transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        if (transform.name.Contains("Alipin"))
            transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z - 3);
    }

    void moveTimer()
    {
        if(movementDelay > 3)
        {
            movementDelay = 0;

            if (moveForward)
            {
                moveForward = false;
            }
            else
            {               
                moveForward = true;
            }
            flip();
        }
        movementDelay += Time.deltaTime;
    }

    void flip()
    {
        Vector3 tScale = transform.localScale;
        tScale.z *= -1;
        transform.localScale = tScale;
    }

    public void moveFlip()
    {
        if (moveForward)
        {
            moveForward = false;
        }else
        {
            moveForward = true;
        }
        flip();
    }
}
