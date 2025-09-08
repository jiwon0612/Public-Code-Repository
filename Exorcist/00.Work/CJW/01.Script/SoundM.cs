using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class SoundM : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;

    private void Awake()
    {
        EazySoundManager.PlayMusic(bgm);
    }
}
