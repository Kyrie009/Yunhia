using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //UI data
    public string areaName;
    //UI
    public TMP_Text healthText;
    public TMP_Text corruptionText;
    public TMP_Text areaText;
    //Visual
    public Slider healthBar;
    public Slider corruptionBar;
    public Animator animator;
    //Screens
    public GameObject gameOverScreen;
    public GameObject menuScreen;
    public GameObject weaponScreen;
    public GameObject itemScreen;
    public GameObject materialScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        menuScreen.SetActive(false);
        healthBar.maxValue = _P.maxHealth;
        corruptionBar.maxValue = _P.maxCorruption;
        OpeningScreen();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    //Updates player's new status
    public void UpdateStatus()
    {
        healthBar.value = _P.currentHealth;
        healthText.text = _P.currentHealth + " / " + _P.maxHealth;
    }

    public void UpdateCorruptionTimer(float _timer)
    {
        //updates time on slider
        corruptionBar.value = _timer;
        //timer formating
        _timer += 1; //adjusts timer to display zero only when it hits zero.

        float minutes = Mathf.FloorToInt(_timer / 60); //convert the timer into individual minutes and seconds as integers.
        float seconds = Mathf.FloorToInt(_timer % 60);

        corruptionText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Display timer in minute:second format
    }
    //Transitionscreen animation
    public void OpeningScreen()
    {
        areaText.text = areaName;
        animator.SetTrigger("OpeningScene");

    }
    public void TransitionScreen()
    {
        areaText.text = areaName;
        animator.SetTrigger("Transition");

    }
    //UI Navigation
    public void ShowPauseMenu()
    {
        menuScreen.SetActive(true);
        ShowWeapons();
        GameEvents.ReportGamePause();
    }
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
    public void ReturnFromMenu()
    {
        menuScreen.SetActive(false);
        GameEvents.ReportGamePlaying();
    }
    public void GameOver(Player _p)
    {
        gameOverScreen.SetActive(true);
        GameEvents.ReportGameOver();
    }
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
