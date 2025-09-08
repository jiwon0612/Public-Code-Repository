using Hellmade.Sound;
using System;
using UnityEngine;

public class PlayerGunState : PlayerState
{
    private readonly int shootHash = Animator.StringToHash("TryShoot");

    public PlayerGunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    //ī�޶� �־��ְ� target ī�޶� �־ ���԰� �ֱ� .

    public override void Enter()
    {
        base.Enter();

        _player.gunAni.transform.localScale = (new Vector3(1, _player.FacingDirection, 1));

        _player.gunAni.SetBool("Gun", true);

        _player.gunAniTrigger.ChangeState += HandleCanChange;


        Time.timeScale = 0.5f;
    }

    private void HandleCanChange()
    {
        if (_player.gunAniTrigger.isActive == false)
        {
            if (_rigidbody.velocity.y < 0)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Fall);
            }
            else
                _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.gunAni.SetBool("Gun", false);
        Time.timeScale = 1f;

        _player.gunAniTrigger.ChangeState -= HandleCanChange;

        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(_player.PlayerInput.AimPosition);
        bool mouseOnPlayerLeft = _player.transform.position.x > mousePos.x && _player.FacingDirection > 0;
        bool mouseOnPlayerRight = _player.transform.position.x < mousePos.x && _player.FacingDirection < 0;

        Vector2 dir = (mousePos - (Vector2)_player.gunAni.transform.position);
        _player.gunAni.transform.right = dir;


        if (mouseOnPlayerLeft || mouseOnPlayerRight)
        {
            _player.Flip();
            _player.gunAni.transform.localScale = (new Vector3(1, _player.FacingDirection, 1));
        }



        if (Input.GetMouseButtonDown(0))
        {
            if (_player.gunAni.GetBool(shootHash))
                return;
            _player.gunAni.SetBool(shootHash, true);
            EazySoundManager.PlaySound(_player.playerAudio[2]);
            _player.DamageCasterCompo.GunShoot(dir);
            _player.gunAniTrigger.isActive = true;  //�� �Ұ� �ٲ���� 
        }
    }
}
