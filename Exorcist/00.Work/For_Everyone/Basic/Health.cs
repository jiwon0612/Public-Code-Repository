using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("��� �� �� �� �ְ� �Ұ���")]
    public int healPosionCount;
    [Header("�� ��ŭ �� �ϰ� �Ұ���")]
    public int healingCount;
    [Header("ó�� ü�� ����")]
    public int MaxHealth;

    [SerializeField] private int currentHealth;

    public Action OnHit;
    public Action<Vector2> OnDead;
    public Action<Vector2> OnKnockback;
    public Action OnHealing;

    private Entity owner;

    private float stunDuration;

    public void SetOwner(Entity _owner)
    {
        owner = _owner;
        currentHealth = MaxHealth;
    }

    public bool ApplyDamage(int _damage, Vector2 _attackDir, Vector2 _knockbackPower)
    {
        if (owner.isDead) return true;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, MaxHealth);

        _knockbackPower.x *= _attackDir.x;

        HitFeedBack(_knockbackPower);

        return owner.isDead;
    }

    private void HitFeedBack(Vector2 knockbackPower)
    {
        OnHit?.Invoke();
        OnKnockback?.Invoke(knockbackPower);
        if (currentHealth <= 0)
        {
            OnDead?.Invoke(knockbackPower);
        }
    }

    public void AddHealthCount()
    {
        healPosionCount = 3;
    }

    // ���� ü�� �޾ƿ���
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Healing()
    {
        if (healPosionCount <= 0 || currentHealth == 100)
            return;
        Instantiate(PlayerManager.Instance.Player.healParticle, transform.position,transform.rotation);
        healPosionCount--;
        currentHealth += healingCount;
        OnHealing?.Invoke();

        if (currentHealth > 100)
            currentHealth = 100;
    }

    public void Stun(float time)
    {
        Debug.Log("ImStun");
    }
}
