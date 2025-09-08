using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Stat myStat;
    private float moveSpeed;
    private float jumpForce;
    private float clouchSpeed;
    private float walkSpeed;


    private Rigidbody2D rigid;
    private CapsuleCollider2D capCollider;
    private Camera cam;

    //점프 감지
    private Vector2 footPos;
    [SerializeField] private LayerMask Ground;
    public bool IsGround { get; set; }

    //벽점프 감지
    [SerializeField] private LayerMask wall;
    [SerializeField] private Transform wallJumpPos;
    [SerializeField] private Vector2 size;
    public bool isWall { get; set; }
    [SerializeField] private float slidingSpeed;
    [SerializeField] private float wallJumpForce;
    private bool isWallJump;

    private float rightNum;

    [SerializeField] private bool isDownKey;

    public bool isCanMove = true;
    public bool walls { get; set; }
    public bool wallJumps { get; set; }

    private WaitForSeconds ws;


    private void Awake()
    {
        ws = new WaitForSeconds(0.05f);
        rigid = GetComponent<Rigidbody2D>();
        capCollider = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
        cam = Camera.main;
        Init();
    }
    private void Init()
    {
        if (myStat != null)
        {
            moveSpeed = myStat.moveSpeed;
            jumpForce = myStat.JumpForce;
            clouchSpeed = myStat.clouchSpeed;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        rightNum = transform.localScale.x > 0 ? 1 : -1;


        Bounds bounds = capCollider.bounds;
        footPos = new Vector2(bounds.center.x, bounds.min.y);
        IsGround = Physics2D.OverlapCircle(footPos, 0.1f, Ground);
        if (!IsGround)
        {
            walls = isWall;
            isWall = Physics2D.OverlapBox(wallJumpPos.position, size, 0, wall);
        }
        else
        {
            walls = false;
            isCanMove = true;
        }

    }


    public void PlayerMovemante(float x)
    {
        if (isCanMove)
        {
            rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);
        }

    }

    public void PlayerUpDown(float x)
    {
        if (x < 0)
        {
            isDownKey = true;
        }
        else isDownKey = false;
    }

    public void PlayerJump()
    {
        if (IsGround)
        {
            rigid.velocity = Vector2.up * jumpForce;
        }
    }

    public void WallSliding()
    {
        if (IsGround)
        {
            isWallJump = false;
            isCanMove = true;
        }
        else if (!IsGround && isWall && !isDownKey)
        {
            isCanMove = false;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                isWallJump = true;
                //rigid.velocity = Vector2.zero;
                transform.localScale = new Vector3(rightNum * -1, transform.localScale.y, transform.localScale.z);
                rigid.velocity = new Vector2(-rightNum * wallJumpForce, wallJumpForce * 1.6f);

                //rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * wallJumpForce * 1.5f);
                StartCoroutine(Startc());
            }
            else if (isWall && !isWallJump && !IsGround)
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y * slidingSpeed);
            }
            else if (isWall && IsGround)
            {
                isCanMove = true;
            }
        }


        wallJumps = isWallJump;
    }

    private IEnumerator Startc()
    {
        yield return ws;
        isWallJump = false;
    }

    public void PlayerClouch()
    {
        //cam.fieldOfView
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(wallJumpPos.position, size);
    }


}
