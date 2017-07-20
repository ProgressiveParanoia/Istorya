using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeBehavior : MonoBehaviour {

    public bool attack;

	void Start ()
    {
        Debug.Log(transform.name);
	}
	
	void Update ()
    {

	}

    void OnTriggerEnter(Collider col)
    {
        if (attack)
        {
            if (transform.name.Contains("axe"))
            {
                if (col.tag == "Enemy")
                {
                    col.GetComponent<enemyHealth>().HitPoints -= 25;
                    col.GetComponent<enemyHealth>().enemyHit = true;
                }
            }

            if (transform.name.Contains("BOLO:pCube3"))
            {
                if(col.tag == "Enemy")
                {
                    col.GetComponent<enemyHealth>().HitPoints -= 25;
                    col.GetComponent<enemyHealth>().enemyHit = true;

                }
            }
        }

    }

}
