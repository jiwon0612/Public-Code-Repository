using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingAnimaTest : MonoBehaviour
{
    private GameObject _visual;
    private Animator _animaComp;

    private int playCount;

    [SerializeField] private AnimationClip _animaClip;

    private void Awake()
    {
        _visual = gameObject;
        _animaComp = GetComponent<Animator>();

        playCount = 0;
    }

    private void Start()
    {
        _animaComp.SetBool("IsIdle", true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestAnima();
        }
    }

    private void TestAnima()
    {
        _animaComp.SetBool("IsIdle", false);
        _animaComp.SetBool("IsCharge", true);
    }

    public void AnimatorTriggerEvent()
    {
        playCount++;
        if (playCount > 2f)
        {
            AnimatorTriggerEndEvent();
        }

    }

    public void AnimatorTriggerEndEvent()
    {
        Debug.Log("공격!");
        playCount = 0;
        _animaComp.SetBool("IsCharge", false);
        _animaComp.SetBool("IsDoubleSlash", true);
    }

    public void DoubleSlashEnd()
    {
        _animaComp.SetBool("IsDoubleSlash", false);
        _animaComp.SetBool("IsIdle", true);
    }
}
