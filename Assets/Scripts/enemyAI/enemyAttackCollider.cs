using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackCollider : MonoBehaviour {
    private GameManager GM;

    [SerializeField]
    private GameObject baboyPref_Attack;
    [SerializeField]
    private GameObject AlipinPref_Attack;
    [SerializeField]
    private GameObject Boss1Pref_Attack;

    private GameObject baboyState_Attack;
    private GameObject AlipinState_Attack;
    private GameObject Boss1State_Attack;

    public GameObject parentObject;

    public bool attack;

    void Start()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        parentObject = transform.root.gameObject;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Sharp") || col.transform.root.name.Contains("LVL"))
        {
            transform.root.GetComponent<enemyMovement>().moveFlip();
        }

        if (col.tag == "Player")
        {
            if (!transform.root.name.Contains("Attack"))
            {
                if (transform.root.name.Contains("Ramo"))
                {
                    parentObject.GetComponent<enemyMovement>().touchingPlayer = true;
                    baboyState_Attack = Instantiate(baboyPref_Attack);

                    baboyState_Attack.transform.position = transform.root.position;
                    baboyState_Attack.GetComponent<enemyCombat>().myEnemy = transform.root.gameObject;

                    baboyState_Attack.GetComponent<enemyCombat>().lookingForward = transform.root.GetComponent<enemyMovement>().moveForward;
                }

                if (transform.root.name.Contains("Alipin"))
                {
                    parentObject.GetComponent<enemyMovement>().touchingPlayer = true;
                    AlipinState_Attack = Instantiate(AlipinPref_Attack);

                    AlipinState_Attack.transform.position = transform.root.position;
                    AlipinState_Attack.GetComponent<enemyCombat>().myEnemy = transform.root.gameObject;

                    AlipinState_Attack.GetComponent<enemyCombat>().lookingForward = transform.root.GetComponent<enemyMovement>().moveForward;
                }
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.name.Contains("Sharp") || col.transform.root.name.Contains("LVL"))
        {
            transform.root.GetComponent<enemyMovement>().moveFlip();
        }

        if (col.tag == "Enemy")
        {
            if (col.name != transform.root.name)
            {
                transform.root.GetComponent<enemyMovement>().moveFlip();
            }
        }

        if (col.tag == "Player")
        {
            if (transform.root.name.Contains("Attack"))
            {
                col.GetComponent<Movement>().hitMove(transform.position.x);
                col.GetComponent<playerHealth>().playerHit = true;

                if (transform.root.name.Contains("Ramo"))
                {
                    col.GetComponent<playerHealth>().damageGiven = 10;
                }

                if (transform.root.name.Contains("Alipin"))
                {
                    col.GetComponent<playerHealth>().damageGiven = 15;
                }

                if (transform.root.name.Contains("Boss1"))
                {
                    col.GetComponent<playerHealth>().damageGiven = 25;
                }
            }
        }
    }

    void OnEnable()
    {
        if (!transform.root.name.Contains("Attack"))
        {
            parentObject.GetComponent<enemyMovement>().touchingPlayer = false;
        }
    }
}
