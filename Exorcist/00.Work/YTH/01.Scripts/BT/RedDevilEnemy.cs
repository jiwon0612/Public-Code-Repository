using BehaviorDesigner.Runtime;
using Cinemachine;
using Hellmade.Sound;
using UnityEngine;

public class RedDevilEnemy : BTEnemy
{
    public EnemyAnimationManager enemyAnimationManager { get; private set; }
    public GameObject RedProjectile;
    public BossHPUI BossHpBar;

    public Transform boxSideTrmR, boxSideTrmL, frontTrm, sideTrmR, sideTrmL, frontTrm2;
    public Transform boxRoadR, boxRoadR2, boxRoadL, boxRoadL2;

    public int AttackRadius = 5;
    public float width, height;

    private CinemachineImpulseSource _camImpulse;
    private ParticleSystem _hitParticles;

    [Space]
    [Header("Sound")]
    public AudioClip FadeSfx;
    public AudioClip AppearSfx;
    public AudioClip LazerSfx;
    public AudioClip SlashSfx;
    public AudioClip SideSlashSfx;
    public AudioClip MagicSfx;
    public AudioClip LazerTwoSfx;
    public AudioClip DeadSfx;
    public AudioClip HitSfx;


    public OpenPortal Portal;
    protected override void Awake()
    {
        base.Awake();
        SharedEnemy enemy = BT.GetVariable("enemy") as SharedEnemy;
        enemy.Value = this;
        BossHpBar.SetBoss(this);
        _camImpulse = transform.Find("CamImpulse").GetComponent<CinemachineImpulseSource>();
        _hitParticles = transform.Find("HitParticle").GetComponent<ParticleSystem>();
    }

    public void Shooting(int count)
    {
        EazySoundManager.PlaySound(MagicSfx);
        for (int i = 0; i < count; i++)
        {
            GameObject pj = Instantiate(RedProjectile, transform.position, Quaternion.identity);
            pj.GetComponent<Projectile>().Shoot(i, count);
        }
    }

    public override void Attack()
    {
    }

    public void OnBT()
    {
        BT.EnableBehavior();
    }
    protected override void HandleHitEvent()
    {
        _hitParticles.Play();
        Vector2 dir = -(Player.transform.position - transform.position).normalized;
        dir = dir / 2;
        CamImpulse(dir);
    }
    public void CamImpulse(Vector3 impulse)
    {
        _camImpulse.m_DefaultVelocity = impulse;
        _camImpulse.GenerateImpulse();
    }



#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (boxSideTrmR != null || boxSideTrmL != null || frontTrm != null || sideTrmL != null || sideTrmR != null || frontTrm2 != null)
        {
            Gizmos.DrawWireCube(boxSideTrmR.position, new Vector3(width, height, 0));
            Gizmos.DrawWireCube(boxSideTrmL.position, new Vector3(width, height, 0));
            Gizmos.DrawWireSphere(sideTrmL.position, AttackRadius);
            Gizmos.DrawWireSphere(sideTrmR.position, AttackRadius);
            Gizmos.DrawWireSphere(frontTrm.position, AttackRadius);
            Gizmos.DrawWireSphere(frontTrm2.position, AttackRadius);
        }

        Vector2 boxSize = new Vector2(width, height/2);
        //
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            boxRoadR.position, 
            Quaternion.Euler(0, 0, -45), 
            Vector3.one 
        );
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, 0));
        //
       rotationMatrix = Matrix4x4.TRS(
            boxRoadR2.position,
            Quaternion.Euler(0, 0, -45),
            Vector3.one
        );
        oldMatrix = Gizmos.matrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, 0));
        //
        rotationMatrix = Matrix4x4.TRS(
             boxRoadL.position,
             Quaternion.Euler(0, 0, 45),
             Vector3.one
         );
        oldMatrix = Gizmos.matrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, 0));
        //
        rotationMatrix = Matrix4x4.TRS(
             boxRoadL2.position,
             Quaternion.Euler(0, 0, 45),
             Vector3.one
         );
        oldMatrix = Gizmos.matrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, 0));

        Gizmos.matrix = oldMatrix;
    }
#endif

}



public class SharedEnemy : SharedVariable<RedDevilEnemy>
{
    public static implicit operator SharedEnemy(RedDevilEnemy value)
    {
        return new SharedEnemy { Value = value };
    }
}


