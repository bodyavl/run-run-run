using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 10;
    public ParticleSystem dust;

    Vector2 _moveInput;
    Rigidbody2D _myRigidBody;
    CapsuleCollider2D _myCaptuleCollider;
    Transform _myTransform;
    Animator _myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myCaptuleCollider = GetComponent<CapsuleCollider2D>();
        _myTransform = GetComponent<Transform>();
        _myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
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

        bool hasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool("isRunning", hasHorizontalSpeed);
        if (hasHorizontalSpeed && _myCaptuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) dust.Play();
        else dust.Stop();
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed) _myTransform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);

    }
}
