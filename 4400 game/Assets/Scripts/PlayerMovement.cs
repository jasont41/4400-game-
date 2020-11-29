using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    // This is to disallow player from moving at all, whenever that would be applicable 
    public bool prevent_movement;

    public int max_health = 50; 
    //public int step_count = 0; // uncomment for persistence debugging
    Vector3 player_pos_enc; 

    // For the randomized encounter trigger. A 5 here would mean a 1 in 5 chance of a random battle at each time 
    public int encounter_seed = 5;

    public int current_health;
    public int player_experience;
    public int player_tier; // Tier == level 
    public int experienceForNextTier = 100; //base, will increase for each tier

    //basic variables 
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    //turn based combat stats 
    public int base_attack;
    public int heal_value; 
    public int attack_damage;
    public Vector3 player_pos_before_encounter;
    public Vector3 player_battle_pos = new Vector3(-4, 0, 0); 

    //Below is a holder for the enemy prefab that is chosen in the previous scene
    public GameObject enemyPrefab; 
    
    //level up stuff 
    bool level_up = false;

    //Inventory variables 
    public string[] inventoryItemNames;
    public bool[] hasSeenInventoryItemsBefore;
    public int[] inventoryItemQuantity;

    //Inventory Icons 
    public GameObject healthPotionIcon;

    public int enemyLevel = 1; 

    //Room one variables

    public bool hasSpokenToOldMan;
    public bool hasLeftRoomOneForFirstTime;

    public Vector2 maxPosition;
    public Vector2 minPosition;
    //Starting values for all player functions. Will certainly add this as the game gets larger 
    void Start()
    {
        healthPotionIcon = canvas_dont_destroy.Instance.HealPotionIcon;
        canvas_dont_destroy.Instance.potionCount.text = inventoryItemQuantity[0].ToString(); 
        inventoryItemNames[0] = "Normal Potion";
        hasSeenInventoryItemsBefore[0] = false; 
        player_tier = 1; 
        attack_damage = 5;
        heal_value = 5;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        current_health = max_health;
        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.setMaxHealth(max_health);
        }
        if (experience_bar.exp_bar != null)
        {
            experience_bar.exp_bar.setMaxExperience(experienceForNextTier);
        }
        prevent_movement = true;
    }
    // Singleton Instance
    // Allows other scripts to use any public function or variable using this method: PlayerMovement.Instance.<Whatever you need> 
    public static PlayerMovement Instance { get; private set; }
    private void OnEnable()
    {
        if (player_pos_before_encounter != Vector3.zero)
        {
            transform.position = player_pos_before_encounter;
            player_pos_before_encounter = Vector3.zero; 
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // End of the code that makes this instance a singleton 
    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
        canvas_dont_destroy.Instance.potionCount.text = inventoryItemQuantity[0].ToString();
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (prevent_movement == true) { UpdateAnimationAndMove(); } // JE added. Prevents movement during combat 
        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = player_pos_before_encounter; 
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    //Referenced by EncounterTrigger.cs to store player position so the player spawns at the each place the battle started 
    public void setPOS(Vector3 temp) 
    {
        player_pos_before_encounter = temp;
    }
    //Reference twice by Encounter Trigger
    public Vector3 getPOS()
    {
        return player_pos_before_encounter;
    }

    // All movement scripts 
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            //step_count++; //debug for persistence testing 
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        myRigidBody.MovePosition(
             transform.position + change * speed * Time.deltaTime
            );
    }
    public Transform getPlayerPos()
    {
        return transform;
    }
    public void setMovingTrue()
    {
        animator.SetBool("moving", true); 
    }
    public void setMovingFalse()
    {
        animator.SetBool("moving", false);
        animator.SetFloat("moveX", 0.01f);
        animator.SetFloat("moveY", 0f);

        /*this is a little ghetto but it works. What's going on here is simple:
        I am changing the animator inputs ever so slightly to the right, and no change on the y axis 
        and preventing any movement so once it moves a little bit to the right it stays in the idle right position.*/    
    }

    //End of movement scripts 


    //Start of battle functions
    //referenced by damage test script and will likely be referenced by battlesystem to make things simpler 
    public void takeDamage(int damage)
    {
        current_health -= damage;

        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
    }
    public void add_experience(int more_experience)
    {
        player_experience += more_experience;

    }

    //Referenced by BattleSystem and HealTest scripts to be able to add health to player 
    public void addHealth(int heal_amt)
    {
        //bool heal_full;
        if ((current_health + heal_amt) > max_health) //This makes sure a healing event doesn't bring the player health above max_health 
        {
            current_health = max_health;
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
        else
        {
            current_health += heal_amt;
            if (dontDestroyOnLoad_health_bar.healthBar != null)
            {
                dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
            }
        }
    }
    //End of battle scripts 


    //Start of level up scripts 
    public void checkLevelUP()
    {
        int left_over_exp = 0;
        if (player_experience > experienceForNextTier)
        {
            left_over_exp -= experienceForNextTier;
            level_up = true;
        }
        else if (player_experience == experienceForNextTier)
        {
            level_up = true;
        }
        if(level_up == true)
        {
            player_experience = 0; 
            changePlayerStatsOnLevelUP(left_over_exp); 
        }
    }

    public void changePlayerStatsOnLevelUP(int leftOver)
    {
        level_up = false; // MUST reset this value 
        attack_damage += val_variance(attack_damage); //adds a randomISH value to the attack. Will do the same for healVal
        heal_value += val_variance(heal_value);
        //Changing experience needed for next level here 
        player_tier++; 
        experienceForNextTier = 0; //null it out just to be safe 
        add_experience(leftOver); 
        experienceForNextTier = (int)((100) + 4 * (math.pow(player_tier, 2))); // This is the function y = 100+4x^2 
        /*
         * The experience need for each level should be: 
         * Tier 1:  100
         * Tier 2:  116
         * Tier 3:  136
         * Tier 4:  164
         * Tier 5:  200
         * Tier 6:  244
         * Tier 7:  296
         * Tier 8:  356
         * Tier 9:  424
         * Tier 10: 500
         * 
         * Need to implement this stat into the stat menu and make sure that function is correct
         */
    }
     
    //end of level up scripts

    public int val_variance(int unit_val)
    {
        int new_val;
        int orig_val = unit_val;
        int half_orig_val = orig_val / 2;
        int high_val = orig_val + half_orig_val;
        int low_val = orig_val - half_orig_val;

        new_val = UnityEngine.Random.Range(low_val, high_val);

        return new_val;
    }
    public void addNormalPotion()
    {
        inventoryItemQuantity[0]++;
        checkIfPlayerHasSeenItemBefore(0); 
        healthPotionIcon.SetActive(true); 

    }
    public bool checkIfPlayerHasSeenItemBefore(int index)
    {
        if (hasSeenInventoryItemsBefore[index])
        {
            return true; 
        }
        else
        {
            hasSeenInventoryItemsBefore[index] = true;
            return false; 
        }
        
    }
    public void useHealthPotion()
    {
        if(inventoryItemQuantity[0] != 0)
        {
            inventoryItemQuantity[0]--; //decrementing regular potion quantity
            addHealth(10); 
        }
    }
    public void setBattlePOS()
    {
        transform.position = player_battle_pos; 
    }

    public void playerSave()
    {
        PlayerPrefs.SetInt("MaxHealth", max_health);
        PlayerPrefs.SetInt("CurrentHealth", current_health);
        PlayerPrefs.SetInt("PlayerTier", player_tier);
        PlayerPrefs.SetInt("AttackDamage", attack_damage);
        PlayerPrefs.SetInt("HealAmount", heal_value);
        PlayerPrefs.SetInt("PlayerExperience", player_experience);
        PlayerPrefs.SetInt("ExperienceForNextTier", experienceForNextTier); 
    }
}
