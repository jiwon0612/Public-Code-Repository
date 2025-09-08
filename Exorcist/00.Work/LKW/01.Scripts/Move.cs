using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    public float moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), _rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movement * moveSpeed;
    }
}