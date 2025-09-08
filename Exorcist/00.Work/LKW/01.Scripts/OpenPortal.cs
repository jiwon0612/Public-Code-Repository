using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OpenPortal : MonoBehaviour
{
    [SerializeField] private GameObject _potalToRobby;
    

    public void OpenPotalToRobby()
    {
        Debug.Log("열려라");
        _potalToRobby.SetActive(true); 
    }
}
