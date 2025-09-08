using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UI Sound")]
public class UISoundSO : ScriptableObject
{
    public AudioClip click;
    public AudioClip hover;
    public AudioClip loadGame;
    public AudioClip closeUI;
    public AudioClip openUI;
    public AudioClip fadeIn;
    public AudioClip fadeOut;
}
