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
    public TMP_Text expText;
    public TMP_Text areaText;   
    public Slider healthBar;
    public Slider expBar;
    public Animator animator;
    [Header("References")]
    public GameObject gameOverScreen;
    public GameObject menuScreen;
    public GameObject weaponScreen;
    public GameObject itemScreen;
    public GameObject materialScreen;
    //Timerstuff
    //public TMP_Text timerText; //Text box for your timer
    //public Slider timerSlider; //Slider based on time
    bool paused = false;


    private void Start()
    {
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
            menuScreen.SetActive(paused);
            GameEvents.ReportGamePause(); //Reports PauseState
        }
        else
        {
            menuScreen.SetActive(false);
            GameEvents.ReportGamePlaying(); //Reports Resume
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
