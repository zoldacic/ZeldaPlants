using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformCharacterControl : MonoBehaviour {
        public float JumpForce = 200f;
        public float MaxSpeed = 10f;
        public Transform GroundCheck;
        public LayerMask WhatIsGround;

        private bool _grounded = false;

        private float _moveHorizontal;
        private bool _facingRight;
        private Animator _animator;
        private float _groundRadius = 0.6f;
        private bool _doubleJump = false;

        void Start () {
            _facingRight = true;
            _animator = GetComponent<Animator>();
        }
	
        void Update ()
        {
            _grounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
            _animator.SetBool("Ground", _grounded);

            Jump();

            _moveHorizontal = Input.GetAxis("Horizontal");
        }

        private void Jump()
        {

            if (_grounded)
            {
                _doubleJump = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(string.Format("Jumping - Grounded: {0}, DoubleJump: {1}", _grounded, _doubleJump));
                if (_grounded || !_doubleJump)
                {
                    rigidbody2D.AddForce(new Vector2(0f, JumpForce));

                    if (!_grounded)
                    {
                        _doubleJump = true;
                    }
                }
            }
        }

        void FixedUpdate()
        {

            _animator.SetFloat("Speed", Mathf.Abs(_moveHorizontal));

            // Change direction
            if ((!_facingRight && _moveHorizontal > 0) || (_facingRight && _moveHorizontal < 0))
            {
                if (_grounded)
                {
                    Flip();
                    rigidbody2D.velocity = new Vector2(_moveHorizontal*MaxSpeed, rigidbody2D.velocity.y);
                }

                //transform.Translate(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z, Space.World);
                //transform.position = new Vector3(transform.position.x + 2 * transform.localScale.x, transform.position.y, transform.position.z);
                //position.x += transform.localScale.x;
                //transform.Translate(transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(_moveHorizontal * MaxSpeed, rigidbody2D.velocity.y);
            }    

        }

        void Flip()
        {
            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            _facingRight = !_facingRight;
        }

    }
}

/*

References:
    http://unity3d.com/learn/tutorials/modules/beginner/2d/2d-controllers

*/