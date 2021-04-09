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
    [SerializeField] private LayerMask _groundMask;

    [Header("Developer")]
    [SerializeField] private bool _drawDebugRaycasts = true;

    [HideInInspector]
    public bool IsOnGround;
    [HideInInspector]
    public bool IsClimbing;

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

    private void PhysicsCheck()
    {
        RaycastHit2D rightLegGroundCheck = Raycast(new Vector2(_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);
        RaycastHit2D leftLegGroundCheck = Raycast(new Vector2(-_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);

        if (rightLegGroundCheck || leftLegGroundCheck)
            IsOnGround = true;
        else
            IsOnGround = false;

        if(!IsClimbing)
            rb.gravityScale = _originalGravityScale;
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
        if (IsOnGround && input.JumpInput)
        {
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void ClimbingTheLadder(Collider2D collider)
    {
        if (collider.CompareTag("Ladder") && Mathf.Abs(input.VerticalInput) > 0)
        {
            rb.gravityScale = 0;
            transform.position += Vector3.up * input.VerticalInput * _ladderClimbingSpeed * Time.deltaTime;
            IsClimbing = true;
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
