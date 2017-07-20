using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    private GameManager GM;

    public GameObject myCanvas;
    public GameObject EnemyLifeBar;

    public Transform canvasPosition;

    public GameObject EnemyModel;

    public List<Color> colorsInEnemy;

    public float HitPoints;
    private float baseHealth;

    private float healthPercentage;

    public bool enemyHit;
    private bool changeDirection;
    private bool modifyCanvas;

    private float hitTimer;
    
    void Awake()
    {
        if(transform.name.Contains("Ramo"))
            HitPoints = 50;

        if (transform.name.Contains("Alipin"))
            HitPoints = 100;

        if (transform.name.Contains("Boss1"))
            HitPoints = 300;

        baseHealth = HitPoints;

        myCanvas.transform.parent = null;
    }

	void Start ()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();

        colorsInEnemy = new List<Color>();

        foreach(Material m in EnemyModel.GetComponent<SkinnedMeshRenderer>().materials)
        {
            colorsInEnemy.Add(m.color);
        }
	}

	void Update ()
    {
        if (enemyHit)
        {
            damageFlicker();

            if (!changeDirection)
            {
                changeDirection = true;
                GetComponent<enemyMovement>().moveFlip();
            } 
        }

        if (HitPoints <= 0)
        {
            if (transform.name.Contains("Ramo"))
                GM.currentExperience += 10;
            if (transform.name.Contains("Alipin"))
                GM.currentExperience += 15;
            if (transform.name.Contains("Boss1"))
                GM.currentExperience += 100;

            Destroy(myCanvas);
            Destroy(gameObject);
        }
        healthPercentage = Mathf.Abs((HitPoints - baseHealth) / baseHealth);
        myCanvas.transform.position = canvasPosition.position;
    }

    void hitFeedBack()
    {
        foreach(Material m in EnemyModel.GetComponent<SkinnedMeshRenderer>().materials)
        {
            m.color = Color.red;
        }
    }

    void decrementImage()
    {
        EnemyLifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(EnemyLifeBar.GetComponent<RectTransform>().sizeDelta.x - EnemyLifeBar.GetComponent<RectTransform>().sizeDelta.x * healthPercentage, EnemyLifeBar.GetComponent<RectTransform>().sizeDelta.y);
    }

    void damageFlicker()
    {
        if (hitTimer < 0.2f)
        {
            if (hitTimer % 0.1 == 0)
            {
                foreach (Material m in EnemyModel.GetComponent<SkinnedMeshRenderer>().materials)
                {
                    m.color = Color.red;
                }
            }
            else
            {
                for (int i = 0; i < colorsInEnemy.Count; i++)
                {
                    EnemyModel.GetComponent<SkinnedMeshRenderer>().materials[i].color = colorsInEnemy[i];
                }
            }
            hitTimer += Time.deltaTime;
        }
        else
        {
            enemyHit = false;
            changeDirection = false;

            hitTimer = 0;
            decrementImage();
        }
    }
}
