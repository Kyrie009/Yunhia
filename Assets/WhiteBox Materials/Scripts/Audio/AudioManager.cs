using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] bgmList;
    public AudioClip[] soundEffects;
    public AudioClip defaultBgm;
    public AudioClip titleBgm;
    public AudioClip buttonsound;
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioSource softSfx;

    private void Start()
    {
        SetDefaultBGM();
    }
    public void SetDefaultBGM()
    {
        bgm.clip = defaultBgm;
        bgm.Stop();
        bgm.Play();
    }
    public void GetButtonSound()
    {
        sfx.clip = buttonsound;
        sfx.Play();
    }
    public void GetTitleBGM()
    {
        defaultBgm = titleBgm;
        SetDefaultBGM();
    }

    public void GetNewGameBGM()
    {
        defaultBgm = bgmList[0];
        SetDefaultBGM();
    }
}
