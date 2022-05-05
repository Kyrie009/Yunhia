using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("UI")]
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text areaText;   
    public Slider healthBar;
    public Slider manaBar;
    public TMP_Text runesText;
    public Animator animator;
    public GameObject key1;
    [Header("References")]
    public GameObject gameOverScreen;
    public GameObject menuScreen;
    public GameObject weaponScreen;
    public GameObject itemScreen;
    public GameObject materialScreen;
    public AudioSource audioSource;
    //Timerstuff
    //public TMP_Text timerText; //Text box for your timer
    //public Slider timerSlider; //Slider based on time
    bool paused = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //SetDrawOrder
        gameOverScreen.SetActive(false);
        menuScreen.SetActive(false);
        //Initialization
        healthBar.maxValue = _P.maxHealth;
        UpdateStatus();
        OpeningScreen();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused();
        }
    }

    //Updates the player's status
    public void UpdateStatus()
    {
        healthBar.value = _P.currentHealth;
        healthText.text = _P.currentHealth + " / " + _P.maxHealth;
        manaBar.value = _P.currentMana;
        manaText.text = _P.currentMana + " / " + _P.maxMana;
        runesText.text = "Runes: " + _P.runeCurrency;
    }
    /*
    public void UpdateTimer(float _timer)
    {
        //updates time on slider
        timerSlider.value = _timer;
        //timer formating
        _timer += 1; //adjusts timer to display zero only when it hits zero.

        float minutes = Mathf.FloorToInt(_timer / 60); //convert the timer into individual minutes and seconds as integers.
        float seconds = Mathf.FloorToInt(_timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Display timer in minute:second format
    }*/
    public void UpdateKey(bool _hasKey)
    {
        key1.SetActive(_hasKey);
    }

    //Scene Transitions
    public void OpeningScreen()
    {
        areaText.text = SceneManager.GetActiveScene().name;
        animator.SetTrigger("OpeningScene");

    }
    public void TransitionScreen()
    {
        animator.SetTrigger("Transition");
    }

    //PauseMenu Control
    public void IsPaused()
    {
        paused = !paused;
        if (paused == true)
        {
            GameEvents.ReportGamePause(); //Reports PauseState
            menuScreen.SetActive(paused);
        }
        else
        {
            GameEvents.ReportGamePlaying(); //Reports Resume
            menuScreen.SetActive(false); //set actives to false should be done last or the rest of the code after it won't execute
        }
               
    }
    
    public void GameOver(Player _p)
    {
        gameOverScreen.SetActive(true);
    }

    //Regions
    #region InventoryTabs
    public void ShowWeapons()
    {
        weaponScreen.SetActive(true);
        itemScreen.SetActive(false);
        materialScreen.SetActive(false);
    }
    public void ShowItems()
    {
        weaponScreen.SetActive(false);
        itemScreen.SetActive(true);
        materialScreen.SetActive(false);
    }
    public void ShowMaterials()
    {
        weaponScreen.SetActive(false);
        itemScreen.SetActive(false);
        materialScreen.SetActive(true);
    }
    #endregion

    //Events
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += GameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= GameOver;
    }
}
