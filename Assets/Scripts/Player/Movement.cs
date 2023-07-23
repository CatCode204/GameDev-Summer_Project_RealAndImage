using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [Header("Info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    /* * * Processing Variables * * */
    private Rigidbody2D _rb;

    /* * * Control Variables * * */
    private float moveX;
    private bool jumpPressed;

    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        GetInput();
        Move();
        Jump();
    }

    private void GetInput() {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        moveX = inputHorizontal != 0 ? inputHorizontal : moveX;
    }

    private void Move() {
        _rb.velocity = new Vector2(moveX * _moveSpeed,_rb.velocity.y);
        moveX = 0;
    }

    private void Jump() {
        if (jumpPressed) _rb.AddForce(Vector2.up * _jumpForce,ForceMode2D.Impulse);
    }
}
