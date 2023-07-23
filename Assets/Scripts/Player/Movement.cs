using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float moveX;
    private bool jumpPress;

    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
        _col = this.GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        Gravity();
        _rb.velocity = new Vector2(moveX * _moveSpeed,_rb.velocity.y);
    }

    private void Gravity() {
        if (_rb.velocity.y > 0 && !jumpPress) //Jump Cut
            _rb.velocity += Vector2.up * Physics2D.gravity.y * _fallMultiplier * Time.fixedDeltaTime;
        else if (_rb.velocity.y < 0) _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
    }

    public void Move(InputAction.CallbackContext value) {
        moveX = value.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext value) {
        if (value.started && IsGround()) {
            jumpPress = true;
            _rb.AddForce(Vector2.up * _jumpForce,ForceMode2D.Impulse);
        }
        else if (value.canceled) jumpPress = false;
    }

    private bool IsGround() {
        return Physics2D.BoxCast(_col.bounds.center,_col.bounds.size,0,Vector2.down,_extraHeightCheck,_jumpableLayerMask);
    }
}