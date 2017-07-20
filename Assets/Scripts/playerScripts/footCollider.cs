using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footCollider : MonoBehaviour {
    public bool grounded;

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.name.Contains("Rock") || col.transform.root.name.Contains("LVL"))
        {
            grounded = true;
        }
        else
            grounded = false;    
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.root.name.Contains("Rock") || col.transform.root.name.Contains("LVL"))
        {
            grounded = true;
        }
        else
            grounded = false;

        if (transform.root.tag == "Player")
        {
            if (col.tag == "Enemy")
            {
                transform.root.GetComponent<Movement>().hitMove(col.transform.position.x);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        grounded = false;
    }
}
