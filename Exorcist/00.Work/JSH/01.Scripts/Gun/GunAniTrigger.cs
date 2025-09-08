using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAniTrigger : MonoBehaviour
{
    public bool isActive = false;
    Animator ani;
    public Action ChangeState;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void IsActive()
    {
        isActive = false;    // �̰��ؾ��� �Ф�
        ani.SetBool("TryShoot", false);
        ani.SetBool("Gun", false);
        ChangeState?.Invoke();
    }
}
