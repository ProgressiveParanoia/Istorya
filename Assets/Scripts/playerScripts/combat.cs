using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour {
    [SerializeField]
    private Transform attackStateSpawn;

    [SerializeField]
    private GameObject playerStatePref_boloAttack;
    [SerializeField]
    private GameObject playerStatePref_axeAttack;
    [SerializeField]
    private GameObject playerStatePref_gunAttack;

    private GameObject playerState_boloAttack;
    private GameObject playerState_axeAttack;
    private GameObject playerState_gunAttack;
    
	void Update () {

        if (GetComponent<Movement>().isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (transform.name.Contains("bolo"))
                {
                    useBolo();
                }

                if (transform.name.Contains("axe") || transform.name.Contains("unarmed")) //unarmed for place holder purposes
                {
                    useAxe();
                }

                if (transform.name.Contains("gun"))
                {
                    useGun();
                }
            }
        }
	}
    

    void useBolo()
    {
        playerState_boloAttack = Instantiate(playerStatePref_boloAttack, attackStateSpawn.position, Quaternion.Euler(0, 90, 0));
        playerState_boloAttack.transform.localScale = transform.localScale;

        playerState_boloAttack.GetComponent<attackState>().myPlayer = gameObject;
    }

    void useAxe()
    {
        playerState_axeAttack = Instantiate(playerStatePref_axeAttack, attackStateSpawn.position, Quaternion.Euler(0, 90, 0));
        playerState_axeAttack.transform.localScale = transform.localScale;

        playerState_axeAttack.GetComponent<attackState>().myPlayer = gameObject;
    }

    void useGun()
    {
        playerState_gunAttack = Instantiate(playerStatePref_gunAttack, attackStateSpawn.position, Quaternion.Euler(0, 90, 0));
        playerState_gunAttack.transform.localScale = transform.localScale;

        playerState_gunAttack.transform.position = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);

        playerState_gunAttack.GetComponent<attackState>().myPlayer = gameObject;
    }
}
