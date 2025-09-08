using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PortalToRobby : MonoBehaviour
{
    private bool isInPlayer;
    [SerializeField] private StageManager _stageManager;
    [SerializeField] private InteractionUI _interactionUI;
    public UnityEvent bossRoomClear;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interactionUI.ShowLabel();
            isInPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interactionUI.HideLabel();
            isInPlayer = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInPlayer)
        {
            bossRoomClear?.Invoke();
        }
    }
}
