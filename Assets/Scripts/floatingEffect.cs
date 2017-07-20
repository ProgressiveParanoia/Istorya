using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingEffect : MonoBehaviour {

    private float maxPosY;
    private float minPosY;

    private bool moveUp;
	void Start () {
        maxPosY = transform.position.y + 1;
        minPosY = transform.position.y - 1;
    }
	
	void Update () {
        if (!moveUp)
        {
            if(transform.position.y > minPosY)
            {
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,minPosY,transform.position.z),Time.deltaTime * 5);
            }else
            {
                moveUp = true;
            }
        }

        if (moveUp)
        {
            if(transform.position.y < maxPosY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxPosY, transform.position.z), Time.deltaTime * 5);
            }else
            {
                moveUp = false;
            }
        }
	}
}
