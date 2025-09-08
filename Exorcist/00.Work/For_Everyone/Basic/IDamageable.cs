using UnityEngine;
public interface IDamageable
{
    public void Stun(float time);
    public bool ApplyDamage(int damage, Vector2 attackDirection, Vector2 knockbackPower);

}
