using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI; 

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update


    public int max_health = 50; 
    public int step_count = 0;
    Vector3 player_pos_enc; 
    public  int encounter_seed = 5;
    public int current_health; 
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;



    //public HealthBar healthbar;

    public static PlayerMovement Instance { get; private set; }
    private Vector3 player_pos_before_encounter;
    public void setPOS(Vector3 temp)
    {
        player_pos_before_encounter = temp;
    }
    public Vector3 getPOS()
    {
        return player_pos_before_encounter;
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

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        current_health = max_health;
        if(dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.setMaxHealth(max_health); 
        }
        

    }




    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
    }

    public void takeDamage(int damage)
    {
        current_health -= damage;

        if (dontDestroyOnLoad_health_bar.healthBar != null)
        {
            dontDestroyOnLoad_health_bar.healthBar.SetHealth(current_health);
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            step_count++; 
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
}
