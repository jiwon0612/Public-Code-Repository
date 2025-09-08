using UnityEngine;
using UnityEngine.Events;

public class BossRoomEnterTrigger : MonoBehaviour
{
    public UnityEvent BossRoomEnterEvent;
    [SerializeField] private GameObject _transparentWall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            _transparentWall.SetActive(true);
            BossRoomEnterEvent?.Invoke();
        }
        gameObject.SetActive(false);
    }
}
