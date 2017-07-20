using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public GameObject playerModel;

    private GameManager GM;
   
    public List<Color> colorsInPlayer;

    public bool playerHit;

    public float damageGiven;

    private float hitTimer;

    void Awake()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

	void Start ()
    {
        colorsInPlayer = new List<Color>();

        foreach (Material m in playerModel.GetComponent<SkinnedMeshRenderer>().materials)
        {
            colorsInPlayer.Add(m.color);
        }
    }

	void Update ()
    {
        if (playerHit)
            damageFlicker();
	}

    void hitFeedBack()
    {
        foreach (Material m in playerModel.GetComponent<SkinnedMeshRenderer>().materials)
        {
            m.color = Color.red;
        }
    }

    void damageFlicker()
    {
        if (hitTimer < 0.2f)
        {
            if (hitTimer % 0.1 == 0)
            {
                foreach (Material m in playerModel.GetComponent<SkinnedMeshRenderer>().materials)
                {
                    m.color = Color.red;
                }
            }
            else
            {
                for (int i = 0; i < colorsInPlayer.Count; i++)
                {
                    playerModel.GetComponent<SkinnedMeshRenderer>().materials[i].color = colorsInPlayer[i];
                }
            }
            hitTimer += Time.deltaTime;
        }
        else
        {
            playerHit = false;
            hitTimer = 0;

            GM.playerHealthPoints -= damageGiven;
        }
    }

    void revertMaterials()
    {
        for (int i = 0; i < colorsInPlayer.Count; i++)
        {
            playerModel.GetComponent<SkinnedMeshRenderer>().materials[i].color = colorsInPlayer[i];
        }
    }
}
