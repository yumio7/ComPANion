using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VegeController : MonoBehaviour
{
    // [Header("Rigidbody")]
    private Rigidbody2D _rb;

    [Header("Movement")] [SerializeField] 
    private float moveSpeed;

    private float _horizontalInput, _verticalInput;
    
    private Vector2 _moveDirection;
    
    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        // rb.useGravity = false;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        var transform1 = this.transform;
        _moveDirection = transform1.up * _verticalInput + transform1.right * _horizontalInput;
        
        _rb.AddForce(_moveDirection.normalized * (moveSpeed * 100), ForceMode2D.Force );
    }

    private void SpeedControl()
    {
        var velocity = _rb.velocity;
        var flatVel = new Vector2(velocity.x, velocity.y);

        if (_horizontalInput == 0 || _verticalInput == 0)
            _rb.velocity = new Vector2(0, 0);
        
        if (!(flatVel.magnitude > moveSpeed)) return;
        var limitedVel = flatVel.normalized * moveSpeed;
        _rb.velocity = new Vector2(limitedVel.x, limitedVel.y);
  
    }
}
