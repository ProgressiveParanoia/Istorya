using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //PLAYER VARIABLES
    [SerializeField]
    private GameObject playerPref_Bolo;
    [SerializeField]
    private GameObject playerPref_Gun;
    [SerializeField]
    private GameObject playerPref_Whip;
    [SerializeField]
    private GameObject playerPref_Axe;
    [SerializeField]
    private GameObject playerPref_Unarmed;

    public Transform spawnPoint;
    private Vector3 playerPosition;

    private GameObject player_Bolo;
    private GameObject player_Axe;
    private GameObject player_Gun;
    private GameObject player_Whip;
    private GameObject player_Unarmed;

    //UI VARIABLES
    private GameObject mainCanvas;

    private GameObject healthInfo;
    private GameObject xpInfo;
    private GameObject levelInfo;

    public Sprite potionImage;

    public float playerLocalScaleZ;

    //player vars
    public bool playerHit;
    private bool levelHasChanged;

    public float playerHealthPoints;
    private float playerHealthBase;

    public float damageGiven;

    public int playerLevel;
    public int currentStageLevel;

    public int currentExperience;
    public int requiredExperience;

    //inventory vars
    public List<GameObject> inventorySpace;

    public GameObject Inventory;

    private int potion_amount;

    //items in the environment
    private List<GameObject> potionsForPickup;
    void OnEnable()
    {
        transform.position = GameObject.Find("CamSpawn").transform.position;
    }
    void Awake()
    {
        DontDestroyOnLoad(transform);

        potionsForPickup = new List<GameObject>();

        spawnPoint = GameObject.Find("SpawnPoint").transform;

        xpInfo = GameObject.Find("XPInfo");
        healthInfo = GameObject.Find("HealthInfo");
        levelInfo = GameObject.Find("levelInfo");

        mainCanvas = GameObject.Find("mainCanvas");

        potionsForPickup.AddRange(GameObject.FindGameObjectsWithTag("Potion"));

        player_Unarmed = Instantiate(playerPref_Unarmed, spawnPoint.position, Quaternion.Euler(0, 90, 0));
        player_Bolo = Instantiate(playerPref_Bolo, spawnPoint.position, Quaternion.Euler(0, 90, 0));
        //  player_Axe = Instantiate(playerPref_Axe, spawnPoint.position, Quaternion.Euler(0, 90, 0));
        player_Gun = Instantiate(playerPref_Gun, spawnPoint.position, Quaternion.Euler(0, 90, 0));

        DontDestroyOnLoad(player_Unarmed);
        DontDestroyOnLoad(player_Bolo);
        DontDestroyOnLoad(player_Gun);

        DontDestroyOnLoad(mainCanvas);

        player_Unarmed.SetActive(false);
        player_Gun.SetActive(false);
        Inventory.SetActive(false);

        playerLocalScaleZ = 1.0f;

        playerLevel = 1;
        playerHealthPoints = 100;
        playerHealthBase = playerHealthPoints;

        requiredExperience = 50;
        currentStageLevel = 1;

        potion_amount = 0;
        
    }
	
    void Start()
    {
     
    }

	void Update ()
    {
        //if (GameObject.FindGameObjectsWithTag("Player").Length > 1 && GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
        //{
        //    Destroy(GameObject.FindGameObjectWithTag("Player"));
        //    Destroy(GameObject.FindGameObjectsWithTag("MainCamera")[1]);
        //}
            if (levelHasChanged)
            {
                resetPlayerPosition();

                levelHasChanged = false;
            }
        if (GameObject.FindGameObjectWithTag("Player") != null && !GameObject.FindGameObjectWithTag("Player").name.Contains("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                reset();

                player_Bolo.SetActive(true);
                player_Bolo.transform.position = playerPosition;
                GetComponent<DynamicCam>().target = player_Bolo.transform;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                reset();

                player_Unarmed.SetActive(true);
                player_Unarmed.transform.position = playerPosition;
                GetComponent<DynamicCam>().target = player_Unarmed.transform;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                reset();

                player_Gun.SetActive(true);
                player_Gun.transform.position = playerPosition;

                GetComponent<DynamicCam>().target = player_Gun.transform;
            }

            if (GameObject.FindGameObjectWithTag("Player") != null)
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!Inventory.activeInHierarchy)
                Inventory.SetActive(true);
            else
                Inventory.SetActive(false);
        }

        if (playerHit)
        {
            playerHealthPoints -= damageGiven;
            playerHit = false;
        }

        if(playerHealthPoints <= 0)
        {
            resetPlayerPosition();
        }

        UIActivity();
        potionTracker();

        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeLevel();
        }

    }

    //xp and level handling
    void experienceTracker()
    {
        if(currentExperience >= requiredExperience)
        {
            levelUp();
        }
    }

    void levelUp()
    {
        requiredExperience = (50 * (playerLevel ^ 2));
        playerLevel++;

        playerHealthBase += 50;
        playerHealthPoints = playerHealthBase;

        currentExperience = 0;
    }

    //inventory handling

    void addPotionToInventory()
    {
        foreach (GameObject space in inventorySpace)
        {
            if (space.GetComponent<Image>().sprite == null)
            {
                space.GetComponent<Image>().sprite = potionImage;
                break;
            }
        }
    }

    void potionTracker()
    {
        foreach (GameObject Pots in potionsForPickup)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                //align with player
                Pots.transform.position = new Vector3(Pots.transform.position.x, Pots.transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);

                if (Vector3.Distance(Pots.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 9.5f)
                {
                    Debug.Log("pick up");
                    if (potion_amount < 4)
                    {
                        Destroy(Pots);
                        potionsForPickup.Remove(Pots);

                        addPotionToInventory();

                        break;
                    }
                }
            }else
            {
                Debug.Log("cant find");
            }
        }

        potion_amount = inventorySpace.Where(item => item.GetComponent<Image>().sprite != null).Count();
        Debug.Log(potion_amount + "Pots amount:"+ potionsForPickup.Count);
    }

    public void remove_1()
    {
        if (GameObject.Find("INV_space_1").GetComponent<Image>().sprite != null)
        {
            healPlayer();
            GameObject.Find("INV_space_1").GetComponent<Image>().sprite = null;
        }
    }

    public void remove_2()
    {
        if (GameObject.Find("INV_space_2").GetComponent<Image>().sprite != null)
        {
            healPlayer();
            GameObject.Find("INV_space_2").GetComponent<Image>().sprite = null;
        }
    }

    public void remove_3()
    {
        if (GameObject.Find("INV_space_3").GetComponent<Image>().sprite != null)
        {
            healPlayer();
            GameObject.Find("INV_space_3").GetComponent<Image>().sprite = null;
        }
    }

    public void remove_4()
    {
        if (GameObject.Find("INV_space_4").GetComponent<Image>().sprite != null)
        {
            healPlayer();
            GameObject.Find("INV_space_4").GetComponent<Image>().sprite = null;
        }
    }

    void healPlayer()
    {
        playerHealthPoints = playerHealthBase;
    }
    //general UI stuff
    void UIActivity()
    {
        experienceTracker();

        healthInfo.GetComponent<Text>().text = "Health:" + playerHealthPoints;
        xpInfo.GetComponent<Text>().text = "XP:" + currentExperience + " / " + requiredExperience;
        levelInfo.GetComponent<Text>().text = ""+playerLevel;
    }

    void reset()
    {
        player_Bolo.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;
        player_Gun.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;
        player_Unarmed.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;

        player_Bolo.SetActive(false);
        player_Gun.SetActive(false);
        player_Unarmed.SetActive(false);
    }

    void findEverything()
    {
        potionsForPickup = new List<GameObject>();

        spawnPoint = GameObject.Find("SpawnPoint").transform;

        xpInfo = GameObject.Find("XPInfo");
        healthInfo = GameObject.Find("HealthInfo");
        levelInfo = GameObject.Find("levelInfo");

        potionsForPickup.AddRange(GameObject.FindGameObjectsWithTag("Potion"));

    }

    public void resetPlayerPosition()
    {

        if (player_Bolo.activeInHierarchy)
        {
            player_Bolo.transform.position = GameObject.Find("SpawnPoint").transform.position;
            player_Bolo.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;
        }
        if (player_Unarmed.activeInHierarchy)
        {
            player_Unarmed.transform.position = GameObject.Find("SpawnPoint").transform.position;
            player_Unarmed.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;
        }
        if (player_Gun.activeInHierarchy)
        {
            player_Gun.transform.position = GameObject.Find("SpawnPoint").transform.position;
            player_Gun.GetComponent<Movement>().footCollider.GetComponent<footCollider>().grounded = false;
        }

        transform.position = GameObject.Find("CamSpawn").transform.position;

        findEverything();

        playerHealthPoints = playerHealthBase;
    }

    void ChangeLevel()
    {
        if(currentStageLevel == 4)
        {
            currentStageLevel = 1;

            Application.LoadLevel("Level" + currentStageLevel);
        }
        else 
        if(currentStageLevel != 4)
        {
            currentStageLevel++;

            Application.LoadLevel("Level"+currentStageLevel);

            levelHasChanged = true;
        }
    }
}
