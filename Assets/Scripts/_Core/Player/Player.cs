using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    [Header("Movement")]
    public float runSpeed = 5f;
    public float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    [Header("References")]
    CharacterController2D controller;
    public Animator animator;

    public float attackCooldown = 0.5f;

    private void Start()
    {
        //Get refs
        controller = GetComponent<CharacterController2D>();
        //Setup
        Setup();
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); //Movement Animation based on ur speed

        if (attackCooldown > 0) //We need hit timers mannnn//
        {
            attackCooldown -= Time.deltaTime;
        }

        if (attackCooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //timer between attacks tba
                animator.SetTrigger("Attack1");
                attackCooldown = 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                animator.SetTrigger("Attack2");
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

    //Resets Health and other status effects to default
    public void Setup()
    {
        currentHealth = maxHealth;
        _UI.UpdateStatus();
    }

    #region Combat
    //Takes hit
    public void Hit(int _dmg)
    {
        currentHealth -= _dmg;       
        if (IsDead()) //check if you died
        {
            currentHealth = 0;
            GameEvents.ReportPlayerDied(this);
            GameEvents.ReportGameOver();
        }
        else //otherwise get hit as normal
        {
            StartCoroutine(GotHit());
        }
        _UI.UpdateStatus();
    }

    //Hit indicator
    IEnumerator GotHit()
    {
        yield return new WaitForSeconds(0.1f); //Prevent spam
        //hit animation or something here
    }

    //Check if Dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    #endregion

}
