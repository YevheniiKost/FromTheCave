using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movemevt options")]
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _flightSpeedReduser = 0.5f;
    [SerializeField] private float _ladderClimbingSpeed = 1f;

    [Header("Environment check properties")]
    [SerializeField] private float _groundDistance = 1f;
    [SerializeField] private float _footOffeset = 1f;
    [SerializeField] private float _ladderCheckOffcest = .5f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _ladderMask;

    [Header("Developer")]
    [SerializeField] private bool _drawDebugRaycasts = true;

    [HideInInspector]
    public bool IsOnGround;
    [HideInInspector]
    public bool IsClimbing;
    [HideInInspector]
    public bool IsJumping;

    private bool _isOnLadder;
    private int _direction = 1;
    private float _originalScale;
    private float _originalGravityScale;

    private Rigidbody2D rb;
    private CapsuleCollider2D _collider;
    private PlayerInput input;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        _originalScale = transform.localScale.x;
        _originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        Jumping();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ClimbingTheLadder(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            IsClimbing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeRotationalOffcetOfPlatform(collision);
    }

    private void ChangeRotationalOffcetOfPlatform(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            
            if (IsClimbing && input.VerticalInput < 0)
            {
                collision.collider.GetComponent<PlatformEffector2D>().rotationalOffset = 180f;
            }
            else 
            {
                collision.collider.GetComponent<PlatformEffector2D>().rotationalOffset = 0f;
            }
        }
        else if(collision.collider.GetComponent<PlatformEffector2D>())
        {
            collision.collider.GetComponent<PlatformEffector2D>().rotationalOffset = 0f;
        }
    }

    private void PhysicsCheck()
    {
        RaycastHit2D rightLegGroundCheck = Raycast(new Vector2(_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);
        RaycastHit2D leftLegGroundCheck = Raycast(new Vector2(-_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);
       

        if (rightLegGroundCheck || leftLegGroundCheck )
        {
            IsOnGround = true;
            IsJumping = false;
        }
        else if (!IsClimbing)
        {
            IsOnGround = false;
            IsJumping = false;
        }
        else
        {
            IsOnGround = false;
        }

        if(!IsClimbing)
            rb.gravityScale = _originalGravityScale;

        if (Raycast(new Vector2(_ladderCheckOffcest * _direction, _ladderCheckOffcest), Vector2.left * _direction, .5f, _ladderMask))
        {
            _isOnLadder = true;
        }
        else { _isOnLadder = false; }
    }
    private void GroundMovement()
    {
        float xVelocity = _horizontalSpeed * input.HorizontalInput;

        if (xVelocity * _direction < 0f)
        {
            FlipCharacterDirection();
        }

        if (IsOnGround)
        {
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(xVelocity * _flightSpeedReduser, rb.velocity.y);
        }
    }

    private void Jumping()
    {
        if (IsOnGround && input.JumpInput && !IsClimbing)
        {
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            IsJumping = true;
        }
    }

    private void ClimbingTheLadder(Collider2D collider)
    {
        if (collider.CompareTag("Ladder") && !IsJumping) {
            if ((input.VerticalInput > 0) && Mathf.Abs(rb.velocity.y) < .05f)
            {
                rb.gravityScale = 0;
                transform.position += Vector3.up * input.VerticalInput * _ladderClimbingSpeed * Time.deltaTime;
                IsClimbing = true;
                IsJumping = false;
            } else if (input.VerticalInput < 0)
            {
                rb.gravityScale = 0;
                transform.position += Vector3.up * input.VerticalInput * _ladderClimbingSpeed * Time.deltaTime;
                IsClimbing = true;
                IsJumping = false;
            }
            else if (input.VerticalInput == 0 && IsOnGround)
            {
                IsClimbing = false;
            } 
        }
    }

    private void FlipCharacterDirection()
    {
        _direction *= -1;

        Vector3 scale = transform.localScale;

        scale.x = _originalScale * _direction;

        transform.localScale = scale;
    }

    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        if (_drawDebugRaycasts)
        {
            Color color = hit ? Color.red : Color.green;
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        return hit;
    }
}
