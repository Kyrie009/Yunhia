using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    //references
    public CharacterController2D controller;
    public Animator animator;
    public Animator weaponAnimator;
    //Configs
    public float runSpeed = 5f;
    public float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public float attackCooldown = 0.5f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); //Movement Animation 

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (attackCooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //timer between attacks tba
                animator.SetTrigger("Attack1");
                weaponAnimator.SetTrigger("Attack1");
                attackCooldown = 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                animator.SetTrigger("Attack2");
                weaponAnimator.SetTrigger("Attack2");
                attackCooldown = 0.5f;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }

    }

    public void OnLanding()
    {
        
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

}
