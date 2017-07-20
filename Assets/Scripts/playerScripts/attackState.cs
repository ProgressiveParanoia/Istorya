using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : MonoBehaviour {
    [SerializeField]
    private GameObject projectile_prefab;
    [SerializeField]
    private GameObject MeleeBlade;
    [SerializeField]
    private Transform projectileSpawn;

    private bool hasFired;

    internal GameObject myPlayer;

	void Start () {
        myPlayer.SetActive(false);
	}

	void Update () {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            myPlayer.SetActive(true);
            Destroy(gameObject);
        }

        //gun timer
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.25f)
        {
            if (transform.name.Contains("Gun"))
            {
                if (!hasFired)
                {
                    GameObject p = Instantiate(projectile_prefab, projectileSpawn.position, Quaternion.identity);

                    if (transform.localScale.z > 0)
                    {
                        p.GetComponent<Rigidbody>().AddForce(Vector3.right * 5000);
                    }
                    else
                        p.GetComponent<Rigidbody>().AddForce(Vector3.left * 5000);

                    Destroy(p, 2f);

                    hasFired = true;
                }
            }
        }

        //melee timer
        if (!transform.name.Contains("Gun"))
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                MeleeBlade.GetComponent<meleeBehavior>().attack = true;
            }
        }
	}
}
