using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip BeforeStartBgm;
    public AudioClip AfterStartBgm;
    public AudioClip GameClearSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        //if(Instance != null)
        //{
        //    Destroy(Instance);
        //}
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = BeforeStartBgm;
        _audioSource.Play();
    }

    public void ChangeBGM()
    {
        _audioSource.clip = AfterStartBgm;
        _audioSource.Play();
    }

    public void GameClearSFX()//ÀÌ°É ±× ui¶Ù¿ï¶§ ¶ì¿ì¸é µÊ
    {
        EazySoundManager.PlaySound(GameClearSound);
    }
}
