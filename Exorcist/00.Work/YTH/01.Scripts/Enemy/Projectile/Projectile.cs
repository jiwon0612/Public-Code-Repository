using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private CircleCollider2D _collider;
    float time = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().ApplyDamage(8, Vector2.zero, Vector2.zero);
            _collider.enabled = false;
            _rb.velocity = Vector2.zero;
            transform.DOScale(6, 0.8f);
            _animator.SetBool("trigger", true);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }


    private void Update()
    {
        time += Time.deltaTime;
        if(time > 3)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(int number, int maxCount)
    {
        Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * number / maxCount),
            Mathf.Sin(Mathf.PI * 2 * number / maxCount));
        _rb.AddForce(dir.normalized * 8, ForceMode2D.Impulse);
    }

    private void RemoveProjectile()
    {
        Destroy(gameObject);
    }

    private void AddCollider()
    {
        //DOTween.To(() => _collider.radius, x => _collider.radius = x, 0.07f, 0.4f);
    }
}
