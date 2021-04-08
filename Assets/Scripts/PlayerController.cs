using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Environment check properties")]
    [SerializeField] private float _groundDistance = 1f;
    [SerializeField] private float _footOffeset = 1f;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private bool _drawDebugRaycasts = true;

    private bool _isOnGround;
    private bool _isJumping;
    private int _direction = 1;
    private float _originalScale;

    private Rigidbody2D rb;
    private CapsuleCollider2D collider;
    private PlayerInput input;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        _originalScale = transform.localScale.x;
    }

    void Update()
    {
        PhysicsCheck();
        Jumping();
    }

    private void FixedUpdate()
    {
       
        GroundMovement();
        
    }


    private void GroundMovement()
    {
        float xVelocity = _horizontalSpeed * input.HorizontalInput;

        if (xVelocity * _direction < 0f)
        {
            FlipCharacterDirection();
        }

        if (_isOnGround)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    private void Jumping()
    {
        if (_isOnGround && input.JumpInput)
        {
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }


    private void PhysicsCheck()
    {
        RaycastHit2D groundCheck = Raycast(new Vector2(_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);

        if (groundCheck)
            _isOnGround = true;
        else
            _isOnGround = false;
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
