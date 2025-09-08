using DG.Tweening;
using UnityEngine;
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();


        _player.isDead = true;
        float deadFadeDelay = 3f;
        _player.StartDelayCallback(deadFadeDelay, () =>
        {
            _rigidbody.gravityScale = 0;
            _player.ColliderCompo.enabled = false;
            _player.SpriteRendererCompo.DOFade(0f, 1f).OnComplete(() =>
            {
                _player.deathUI.ShowDeathUI();
            });
        });
    }
}
