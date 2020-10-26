using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI; 

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
    public int player_tier;
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
    private Vector3 player_pos_before_encounter;

    //Starting values for all player functions. Will certainly add this as the game gets larger 
    void Start()
    {
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
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (prevent_movement == true) { UpdateAnimationAndMove(); } // JE added. Prevents movement during combat 
        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
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
}
