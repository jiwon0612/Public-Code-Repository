using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private PlayerMove movePlayer;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteR;

    private void Awake()
    {
        rigid = transform.parent.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = transform.parent.transform;
        movePlayer = player.GetComponent<PlayerMove>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InputManager.Instance.onInput += PlayerMoveAnim;
    }

    private void Update()
    {
        JumpAnim(movePlayer.IsGround);
        WallAnim(movePlayer.walls);
    }

    public void PlayerFlip(float x)
    {
        if (x != 0 && !movePlayer.walls && !movePlayer.wallJumps && movePlayer.isCanMove)
        {
            player.transform.localScale = x > 0 ?
                new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z) :
                new Vector3(Mathf.Abs(player.transform.localScale.x) * -1, player.transform.localScale.y, player.transform.localScale.z);
        }
    }

    public void PlayerMoveAnim()
    {
        anim.SetFloat("Move", Mathf.Abs(rigid.velocity.x));
    }

    private void JumpAnim(bool isGround)
    {
        anim.SetBool("IsGround", isGround);
    }

    private void WallAnim(bool isWall)
    {
        anim.SetBool("IsWall", isWall);
        spriteR.flipX = isWall;
    }
}
