using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int step_count = 0;
    Vector3 player_pos_enc; 
    public  int encounter_seed = 5;

    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator; 
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
       // Debug.Log(step_count); 
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove(); 
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
