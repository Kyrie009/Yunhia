using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int maxMana = 100;
    public int currentMana;
    public int attack;
    public int runeCurrency;
    [Header("Movement")]
    public float runSpeed = 5f;
    public float horizontalMove = 0f;
    bool jump = false;
    bool multiJump = false;
    int canJumpAgain = 0; //Multi jump check
    [SerializeField] private int extraJumps = 1; //Multi jump counter
    bool crouch = false;
    [Header("Collisions")]
    public Collider2D playerCollider;
    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip swordSwing;
    [Header("References")]
    public CharacterController2D controller;
    public Animator anim;
    public RecoveryCounter recoveryCounter;

    public bool hasKey; //key not using ruins system
    public bool recoveryJump;
    bool jumpingOff = false;
    //bool canMove = true;
    bool isAttacking = false;
    public float attackSpeed = 0.1f;

    private void Start()
    {
        //Get refs
        controller = GetComponent<CharacterController2D>();
        recoveryCounter = GetComponent<RecoveryCounter>();
        anim = GetComponent<Animator>();
        //Setup
        DefaultSetup();
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
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

        if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S))
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
        if (Input.GetButtonDown("Jump") && recoveryJump == true)
        {
            recoveryJump = false;
            multiJump = true;
            canJumpAgain++;
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsDoubleJumping", true);
        }

        if(jumpingOff == true)
        {
            if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.S))
            {
                jumpingOff = false;
                recoveryJump = true;
                StartCoroutine(JumpOff());
            }
        }
        
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
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
                if (controller.m_Grounded)
                {
                    horizontalMove = 0;
                }
                //canMove = false;
                _UI.audioSource.PlayOneShot(swordSwing);
                isAttacking = true;
                anim.SetBool("IsJumping", false);
                anim.SetTrigger("Attack1"); //play hit animation
                StartCoroutine(AttackCooldown());
            }
        }
    }
    //collision effects
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            jumpingOff = true;
        }
        else
        {
            jumpingOff = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        jumpingOff = false;
    }

    IEnumerator JumpOff()
    {
        crouch = true;
        anim.SetBool("IsJumping",true);
        yield return new WaitForSeconds(0.3f);
        crouch = false;
        jumpingOff = false;
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
        //canMove = true;
    }
    //events when player lands
    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsDoubleJumping", false);
        canJumpAgain = 0;
        recoveryJump = false;
    }
    //Resets Health and other status effects to default
    public void DefaultSetup()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        ManageKey(false);
        _UI.UpdateStatus();
    }
    //updates what the player gains from quest, pickups, killing enemies, etc.
    public void PlayerGains(int _hp, int _mp, int _runes)
    {
        currentHealth += _hp;
        currentMana += _mp;
        runeCurrency += _runes;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        _UI.UpdateStatus();
    }

    public void ManageKey(bool _hasKey)
    {
        hasKey = _hasKey;
        _UI.UpdateKey(_hasKey);
    }

    #region Combat
    //Takes hit
    public void Hit(int _dmg)
    {
        currentHealth -= _dmg;
        _UI.audioSource.PlayOneShot(hitSound);
        if (IsDead()) //check if you died
        {
            currentHealth = 0;
            anim.SetBool("isDead", true);
            GameEvents.ReportPlayerDied(this);
        }
        else //otherwise get hit as normal
        {
            controller.Knockback();
            StartCoroutine(GotHit());
            //recoveryCounter.counter = 0;
        }
        _UI.UpdateStatus();
    }

    //Hit indicator
    IEnumerator GotHit()
    {
        yield return new WaitForSeconds(0.1f); //Prevent spam        
        anim.SetTrigger("Hit");
    }

    //Check if Dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    #endregion

}
