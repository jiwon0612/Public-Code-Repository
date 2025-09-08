using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using Unity.VisualScripting;
using UnityEngine;

public class BloodBolt : MonoBehaviour
{
    [SerializeField] private Transform _damageTrm;
    [SerializeField] private float radius;
    [SerializeField] private ContactFilter2D contact;
    [SerializeField] private int maxHitCount;
    [SerializeField] private int damage;
    public Vector2[] knockbackPower;
    private Collider2D[] _collider;
    public AudioClip _boltSound;

    private void Awake()
    {
        _collider = new Collider2D[maxHitCount];

        if (_damageTrm == null)
        {
            _damageTrm = transform.Find("DamageTrm");
        }
    }

    public void Attack()
    {
        int cnt = Physics2D.OverlapCircle(
            _damageTrm.position,
            radius,
            contact,
            _collider
        );

        for (int i = 0; i < cnt; i++)
        {
            Vector2 direction = (_collider[i].transform.position - _damageTrm.position).normalized;

            if (_collider[i].TryGetComponent(out IDamageable target))
            {
                target.ApplyDamage(damage, direction, knockbackPower[0]);
            }
        }
    }

    public void EndAniamation()
    {
        //나중에 풀링
        Destroy(gameObject);
    }
    
    public void PlaySound() => EazySoundManager.PlaySound(_boltSound);

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (_damageTrm != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_damageTrm.position,radius);
            Gizmos.color = Color.white;
        }
    }

#endif
}
