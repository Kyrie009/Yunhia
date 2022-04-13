using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int attack;
    [Header("Movement")]
    public float runSpeed = 5f;
    public float horizontalMove = 0f;
    bool jump = false;
    bool multiJump = false;
    int canJumpAgain = 0; //Multi jump check
    [SerializeField] private int extraJumps = 1; //Multi jump counter
    bool crouch = false;
    [Header("References")]
    CharacterController2D controller;
    public Animator anim;

    bool canMove = true;
    bool isAttacking = false;
    public float attackSpeed = 0.1f;

    private void Start()
    {
        //Get refs
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        //Setup
        Setup();
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GetInput();
        }       
        AttackInput();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, multiJump);
        jump = false;
        multiJump = false;
    }

    private void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove)); //Movement Animation based on ur speed      

        if (Input.GetButtonDown("Jump"))
        {
            if(canJumpAgain <= extraJumps)
            {
                jump = true;
                anim.SetBool("IsJumping", true);
                if (canJumpAgain == extraJumps) // the number of extra mid air jumps you can do.
                {
                    multiJump = true;
                    anim.SetBool("IsJumping", false);
                    anim.SetBool("IsDoubleJumping", true);
                }
                canJumpAgain++;
            }
            
        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }*/
    }

    public void AttackInput()
    {
        if (!isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canMove = false;
                isAttacking = true;
                anim.SetTrigger("Attack1"); //play hit animation
                StartCoroutine(AttackCooldown());
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
        canMove = true;
    }

    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsDoubleJumping", false);
        canJumpAgain = 0;
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
            Invoke(nameof(GameOver), 1f);
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
        anim.SetTrigger("Hit");
    }

    public void GameOver()
    {
        GameEvents.ReportGameOver();
    }

    //Check if Dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    #endregion

}
