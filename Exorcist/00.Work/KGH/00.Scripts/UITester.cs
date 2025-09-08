using System;
using UnityEngine;

public class UITester : MonoBehaviour
{
    public Action DeathUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DeathUI?.Invoke();
        }
    }
}
