using System;
using Hellmade.Sound;
using UnityEngine;

namespace Code.ETC
{
    public class PlayBGM : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;

        private void Start()
        {
            EazySoundManager.PlayMusic(clip,1,true,false);
            
        }
    }
}