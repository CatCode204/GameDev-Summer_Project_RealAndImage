using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    /* * * Constant Variables * * */
    const string BOUNCING_ANIM = "Bounce";
    const string LAND_ANIM = "Land";
    const string PLATFORM_TAG = "Platform";

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    
    [Header("Jump")]
    [SerializeField] private GameObject _foot;
    [SerializeField] private float _extraHeightCheck;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private LayerMask _jumpableLayerMask;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpCoyoteTime;

    [Header("Animator")]
    [SerializeField] private Animator _anim;

    [Header("Visual FX")]
    [SerializeField] private ParticleSystem _landDustPS;

    [Header("Physics")]
    [SerializeField] private float _onPlatformGravityScale;
    [SerializeField] private float _bonusOnPlatformJumpForce;

    /* * * Component Variables * * */
    private Rigidbody2D _rb;
    private Collider2D _col;
    private Collider2D _footCollider;

    /* * * Control Variables * * */
    private float moveX;
    private bool jumpPress;

    /* * * Animation Variables * * */
    private bool isLanding;
    private float counterJumpDuration;
    private float counterJumpCoyote;


    /* * * Physics Variables * * */
    public Vector2 bonusVelocity {get; set;}
    private float startGravityScale;

    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
        _col = this.GetComponent<Collider2D>();
        _footCollider = _foot.GetComponent<Collider2D>();
        startGravityScale = _rb.gravityScale;
    }

    private void Update() {
        UpdateTimer();
        if (!isLanding && IsGround()) {
            _anim.Play(LAND_ANIM);
            _landDustPS.Play();
            counterJumpDuration = _jumpDuration;
        }
        isLanding = IsGround();
    }

    private void FixedUpdate() {
        Gravity();
       _rb.velocity = new Vector2(moveX * _moveSpeed,_rb.velocity.y) + bonusVelocity;
       bonusVelocity = Vector2.zero;
    }

    private void Gravity() {
        _rb.gravityScale = IsOnPlatform() ? _onPlatformGravityScale : startGravityScale;
        if (_rb.velocity.y > 0 && !jumpPress) //Jump Cut
            _rb.velocity += Vector2.up * Physics2D.gravity.y * _jumpMultiplier * _fallMultiplier * _rb.gravityScale * Time.fixedDeltaTime;
        else if (_rb.velocity.y < 0) _rb.velocity += Vector2.up * Physics2D.gravity.y * _rb.gravityScale * Time.fixedDeltaTime;
    }

    private void UpdateTimer() {
        counterJumpDuration -= Time.deltaTime;
        counterJumpDuration = counterJumpDuration > 0 ? counterJumpDuration : 0;
        counterJumpCoyote = (Mathf.Abs(_rb.velocity.y) > 0.001f && !IsOnPlatform()) ? counterJumpCoyote - Time.deltaTime : _jumpCoyoteTime;
        counterJumpCoyote = counterJumpCoyote > 0 ? counterJumpCoyote : 0;
    }

    public void Move(InputAction.CallbackContext value) {
        moveX = value.ReadValue<float>();
        if (moveX != 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * moveX,transform.localScale.y,transform.localScale.z);
    }

    public void Jump(InputAction.CallbackContext value) {
        if (value.started && counterJumpDuration == 0 && counterJumpCoyote > 0) {
            _anim.Play(BOUNCING_ANIM);
            jumpPress = true;
            counterJumpCoyote = 0;
            float jumpForceTotal = IsOnPlatform() ? _jumpForce + _bonusOnPlatformJumpForce : _jumpForce;
            _rb.AddForce(Vector2.up * (jumpForceTotal * _jumpMultiplier - _rb.velocity.y),ForceMode2D.Impulse);
        }
        else if (value.canceled) jumpPress = false;
    }

    private bool IsGround() {
        return Physics2D.BoxCast(_footCollider.bounds.center,_footCollider.bounds.size,0,Vector2.down,_extraHeightCheck,_jumpableLayerMask);
    }

    private bool IsOnPlatform() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_footCollider.bounds.center,_footCollider.bounds.size,0,Vector2.down,_extraHeightCheck,_jumpableLayerMask);
        foreach(var hit in hits)
            if (hit.collider.gameObject.CompareTag(PLATFORM_TAG)) return true;
        return false;
    }
}