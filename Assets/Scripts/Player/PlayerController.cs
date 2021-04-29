using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveState
{
    [Header("Movemevt options")]
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _flightSpeedReduser = 0.5f;
    [SerializeField] private float _ladderClimbingSpeed = 1f;

    [Header("Environment check properties")]
    [SerializeField] private float _groundDistance = 1f;
    [SerializeField] private float _footOffeset = 1f;
    [SerializeField] private float _coyoteDuration = .05f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _ladderMask;

    [Header("Developer")]
    [SerializeField] private bool _drawDebugRaycasts = true;

    [HideInInspector]
    public bool IsOnGround;
    [HideInInspector]
    public bool IsClimbing;
    [HideInInspector]
    public int CurrentScores = 0;

    private int _direction = 1;
    private float _originalScale;
    private float _originalGravityScale;
    private float _coyoteTime;

    private Rigidbody2D rb;
    private CapsuleCollider2D _collider;
    private PlayerInput input;
    
    public void GetScore()
    {
        CurrentScores++;
        GameEvents.RaiseOnChangeScoreEvent(CurrentScores);
    }

    #region Saving
    public void Save()
    {
        var jsonPosition = JsonUtility.ToJson(transform.position);
        PlayerPrefs.SetString("PlayerPosition", jsonPosition);
        PlayerPrefs.SetInt("PlayerScore", CurrentScores);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPosition"));
            CurrentScores = PlayerPrefs.GetInt("PlayerScore");
            GameEvents.RaiseOnChangeScoreEvent(CurrentScores);
        }
    }
    #endregion

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

    void Update() => Jumping();

    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
    }

    private void OnTriggerStay2D(Collider2D collision) => ClimbingTheLadder(collision);

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
            IsClimbing = false;
    }

    private void OnCollisionStay2D(Collision2D collision) => ChangePlatformProperties(collision);

    private void ChangePlatformProperties(Collision2D collision)
    {
       if (IsClimbing && input.VerticalInput < 0)
            ChangeRotationalOffcetOfPlatforms(180, collision);
       else 
            ChangeRotationalOffcetOfPlatforms(0, collision);
    }

    private void ChangeRotationalOffcetOfPlatforms(float degrees, Collision2D collision)
    {
        foreach (var collider in collision.contacts)
        {
            if(collider.collider.TryGetComponent(out PlatformEffector2D platform) && collider.collider.gameObject.layer == 9)
            {
                platform.rotationalOffset = degrees;
            }
        }
    }

  
    private void GroundMovement()
    {
        if (!GetComponent<PlayerCombat>().IsBlockUp)
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
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (IsOnGround)
            _coyoteTime = Time.time + _coyoteDuration;
    }

    private void Jumping()
    {
        if ((IsOnGround || _coyoteTime > Time.time) && input.JumpInput && !IsClimbing && !GetComponent<PlayerCombat>().IsBlockUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            AudioManager.Instance.PlaySFX(SoundsFx.Jump);
        }
    }

    private void ClimbingTheLadder(Collider2D collider)
    {
        if (collider.CompareTag("Ladder")) {
            if ((input.VerticalInput > 0) && Mathf.Abs(rb.velocity.y) < .05f)
            {
                rb.gravityScale = 0;
                transform.position += Vector3.up * input.VerticalInput * _ladderClimbingSpeed * Time.deltaTime;
                IsClimbing = true;
            } else if (input.VerticalInput < 0)
            {
                rb.gravityScale = 0;
                transform.position += Vector3.up * input.VerticalInput * _ladderClimbingSpeed * Time.deltaTime;
                IsClimbing = true;
            }
            else if (input.VerticalInput == 0 && IsOnGround)
            {
                IsClimbing = false;
            } 
        }
    }
    private void PhysicsCheck()
    {
        RaycastHit2D rightLegGroundCheck = Raycast(new Vector2(_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);
        RaycastHit2D leftLegGroundCheck = Raycast(new Vector2(-_footOffeset, .2f), Vector2.down, _groundDistance, _groundMask);


        if (rightLegGroundCheck || leftLegGroundCheck)
        {
            IsOnGround = true;
        }
        else
        {
            IsOnGround = false;
        }

        if (!IsClimbing)
            rb.gravityScale = _originalGravityScale;

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
