using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombat : MonoBehaviour {
    [SerializeField]
    private GameObject attackCollider;

    internal GameObject myEnemy;

    public bool hasBeenHit;
    public bool lookingForward;
    
	// Use this for initialization
	void Start () {
        myEnemy.SetActive(false);

        if (!lookingForward)
        {
            flip();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && !attackCollider.GetComponent<enemyAttackCollider>().attack)
        {
            if (hasBeenHit)
            {
                myEnemy.GetComponent<enemyHealth>().HitPoints -= 25;
            }  
        }

        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            myEnemy.SetActive(true);

            myEnemy.GetComponent<enemyMovement>().moveFlip();

            Destroy(gameObject);
        }
	}

    void flip()
    {
        Vector3 tScale = transform.localScale;
        tScale.z *= -1;
        transform.localScale = tScale;
    }
}
