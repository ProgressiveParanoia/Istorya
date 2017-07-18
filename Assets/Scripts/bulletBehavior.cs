using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour {

	void Start () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<enemyHealth>().enemyHit = true;
            col.GetComponent<enemyHealth>().HitPoints -= 25;
        }

        if(!col.name.Contains("AttackCollider"))
        Destroy(gameObject);
    }

}
