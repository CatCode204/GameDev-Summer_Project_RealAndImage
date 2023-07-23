using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    
    [Header("Jump")]
    [SerializeField] private float _extraHeightCheck;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private LayerMask _jumpableLayerMask;

    /* * * Component Variables * * */
    private Rigidbody2D _rb;
    private Collider2D _col;

    /* * * Control Variables * * */
    private float moveX; //0 is standing, 1 move right, -1 move left
    private bool jumpPress; //check jump button is pressing

    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
        _col = this.GetComponent<Collider2D>();
    }

    private void Update() {
        GetInput();
        Move();
        Jump();
    }

    private void FixedUpdate() {
        Gravity();
    }

    private void GetInput() {
        moveX = Input.GetAxisRaw("Horizontal");
        jumpPress = Input.GetButtonDown("Jump");
    }

    private void Move() {
        _rb.velocity = new Vector2(moveX * _moveSpeed,_rb.velocity.y);
    }

    private void Jump() {
        if (jumpPress && IsGround())
            _rb.AddForce(Vector2.up * _jumpForce * _jumpMultiplier,ForceMode2D.Impulse);
    }

    private void Gravity() {
        if (_rb.velocity.y > 0 && !Input.GetButton("Jump")) //Jump Cut
            _rb.velocity += Vector2.up * Physics2D.gravity.y * _fallMultiplier * Time.fixedDeltaTime;
        else if (_rb.velocity.y < 0) _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
    }

    private bool IsGround() {
        return Physics2D.BoxCast(_col.bounds.center,_col.bounds.size,0,Vector2.down,_extraHeightCheck,_jumpableLayerMask);
    }
}