using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 10;

    Vector2 _moveInput;
    Rigidbody2D _myRigidBody;
    CapsuleCollider2D _myCaptuleCollider;
    Transform _myTransform;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myCaptuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!_myCaptuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            _myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new(_moveInput.x * runSpeed, _myRigidBody.velocity.y);
        _myRigidBody.velocity = playerVelocity;
    }
}
