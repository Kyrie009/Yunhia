using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{

    //Stats
    public int maxHealth = 100;
    public int currentHealth;
    public int maxCorruption = 60;
    public float corruptionTimer;
    //Status Checks
    public bool isCorrupting = false;
    public bool freeze = false;

    // Start is called before the first frame update
    void Start()
    {
        DefaultStatus();
    }
    private void Update()
    {
        if (isCorrupting)
        {
            if (corruptionTimer > 0)
            {
                corruptionTimer -= Time.deltaTime;
                _UI.UpdateCorruptionTimer(corruptionTimer);
            }
            else
            {
                isCorrupting = false;
                corruptionTimer = 0;
                CorruptState();
                GameEvents.ReportCorrupted();
            }
        }
        //debug/ testing button
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddCorruptionResist(5);
        }
    }
    //adds time to resist corruption
    public void AddCorruptionResist(float _cr)
    {
        corruptionTimer += _cr;
        _UI.UpdateCorruptionTimer(corruptionTimer);
    }
    //Takes hit
    public void Hit(int _dmg)
    {
        currentHealth -= _dmg;
        StartCoroutine(GotHit());
        CheckIfDead();
        _UI.UpdateStatus();
    }

    //Hit indicator
    IEnumerator GotHit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    //lose health overtime when corrupted.
    public void CorruptState()
    {
        StartCoroutine(CorruptDmg());
    }

    IEnumerator CorruptDmg()
    {
        if (IsCorrupted())
        {
            yield return new WaitForSeconds(1f);
            currentHealth -= 5;
            CheckIfDead();
            _UI.UpdateStatus();
            StartCoroutine(CorruptDmg());
        }
    }
    
    //Resets Health and other status effects to default
    public void DefaultStatus()
    {
        currentHealth = maxHealth;
        corruptionTimer = maxCorruption;
        _UI.UpdateStatus();
        _UI.UpdateCorruptionTimer(corruptionTimer);
    }
    //Death condition
    private void CheckIfDead()
    {
        if (IsDead())
        {
            currentHealth = 0;
            GameEvents.ReportPlayerDied(this);
        }
    }
    //Check if Dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    //Check if corrupted
    public bool IsCorrupted()
    {
        return corruptionTimer <= 0;
    }
    //Corrupting checks
    public void IsCorrupting()
    {
        if (!IsCorrupted())
        {
            isCorrupting = true;
        }
        
    }
    public void StopCorrupting()
    {
        isCorrupting = false;
    }
    //player movement disable
    public void FreezePlayer(bool _fstatus)
    {
        freeze = _fstatus;
        if (freeze)
        {
            _PM.enabled = false;                                   //disable movement input
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;   //disable physics
            GetComponent<Animator>().SetFloat("Speed", 0);         //reset animation to idle  
        }
        else
        {
            _PM.enabled = true;
        }       
    }
    //Events
    private void OnEnable()
    {
        GameEvents.OnCorruptionStart += IsCorrupting;
        GameEvents.OnCorruptionStop += StopCorrupting;
        GameEvents.OnFreezeEvent += FreezePlayer;
    }
    private void OnDisable()
    {
        GameEvents.OnCorruptionStart -= IsCorrupting;
        GameEvents.OnCorruptionStop -= StopCorrupting;
        GameEvents.OnFreezeEvent -= FreezePlayer;
    }

}
