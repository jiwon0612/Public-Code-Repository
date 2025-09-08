using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CooldownInfoEvent(float current, float total);

public class PlayerSkill : MonoBehaviour
{
    public bool skillEnabled = false;
    public LayerMask whatIsEnemy;
    public float _cooldown;
    [SerializeField] protected int _maxCheckEnemy = 5; //스킬이 적을 체크할건데 갯수

    public float _cooldownTimer;
    public bool IsCooldown => _cooldownTimer > 0;

    protected Player _player;
    protected Collider2D[] _colliders; //충돌처리를 위한 변수

    public event CooldownInfoEvent OnCooldownEvent;

    protected virtual void Start()
    {
        _player = PlayerManager.Instance.Player;
        _colliders = new Collider2D[_maxCheckEnemy];
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0)
                _cooldownTimer = 0;

            OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool AttemptUseSkill()
    {
        if (_cooldownTimer <= 0 && skillEnabled)
        {
            _cooldownTimer = _cooldown;
            return true;
        }
        Debug.Log("skill cooldown or locked");
        return false;
    }


    public virtual List<Enemy> FindEnemiesInRange(Transform checkTransform, float radius)
    {
        int cnt = Physics2D.OverlapCircle(
            checkTransform.position,
            radius,
            new ContactFilter2D() { layerMask = whatIsEnemy, useLayerMask = true },
            _colliders);

        List<Enemy> list = new List<Enemy>();

        for (int i = 0; i < cnt; ++i)
        {
            if (_colliders[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (enemy.isDead == false)
                    list.Add(enemy);
            }
        }

        return list;
    }

    public virtual Transform FindClosestEnemy(Transform checkTransform, float radius)
    {
        Transform closestEnemy = null;

        int cnt = Physics2D.OverlapCircle(
            checkTransform.position,
            radius,
            new ContactFilter2D() { layerMask = whatIsEnemy, useLayerMask = true },
            _colliders);

        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < cnt; ++i)
        {
            if (_colliders[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (enemy.isDead) continue;

                float distanceToEnemy = Vector2.Distance(
                        checkTransform.position, _colliders[i].transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = _colliders[i].transform;
                }
            }
        }
        return closestEnemy;
    }
}
