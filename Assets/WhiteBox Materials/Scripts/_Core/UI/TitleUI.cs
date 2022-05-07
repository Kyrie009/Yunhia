using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : GameBehaviour
{
    public Animator anim;
    public SceneLoader sceneLoader;
    public AudioClip howl;
    //public AudioClip introBgm;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void PlayIntroScene()
    {
        _AM.GetButtonSound();
        anim.SetTrigger("IntroScene");       
    }
    //animation events from intro scene animation
    public void PlayHowl()
    {
        _AM.softSfx.PlayOneShot(howl);
    }
    public void StartNewGame()
    {
        sceneLoader.LoadNewGame();
    }
}
