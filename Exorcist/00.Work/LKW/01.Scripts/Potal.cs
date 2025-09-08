using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Potal : MonoBehaviour
{
    [SerializeField] private TransitionUI _transitionUI;
    [SerializeField] private InteractionUI _interactionUI;
    
    [FormerlySerializedAs("_boosRoomData")] [SerializeField] private BossRoomSO _bossRoomData;
    private Animator _animator;
    private bool isInPlayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.CompareTag("Player"));
        if (other.gameObject.CompareTag("Player") && _bossRoomData.isOpen)
        {
            _interactionUI.ShowLabel();
            _animator.enabled = true;
            isInPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && _bossRoomData.isOpen)
        {
            _interactionUI.HideLabel();
            _animator.enabled = false;
            isInPlayer = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInPlayer)
        {
            _transitionUI.TransitionStart(()=>SceneManager.LoadScene(_bossRoomData.sceneIndex));
        }
    }
}
