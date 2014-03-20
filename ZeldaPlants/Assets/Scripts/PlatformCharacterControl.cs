using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformCharacterControl : MonoBehaviour {
        public float JumpForce = 100f;
        public float MaxSpeed = 10f;
        
        private bool _jump = false;

        private float _moveHorizontal;

        private bool _facingRight;

        private Animator _animator;

        void Start () {
            _facingRight = true;
            _animator = GetComponent<Animator>();
        }
	
        void Update () {	
            if (Input.GetButtonDown ("Jump")) {
                _jump = true;		
            }

            _moveHorizontal = Input.GetAxis("Horizontal");
        }
	
        void FixedUpdate () {

            _animator.SetFloat("Speed", Mathf.Abs(_moveHorizontal));

            // Change direction
            if ((!_facingRight && _moveHorizontal > 0) || (_facingRight && _moveHorizontal < 0))
            {
                Flip();
                transform.Translate(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z, Space.World);
            }

            // Move along x axis
            rigidbody2D.velocity = new Vector2(_moveHorizontal * MaxSpeed, rigidbody2D.velocity.y);

            // Jump
            if (_jump) {
                rigidbody2D.AddForce (new Vector2 (0f, JumpForce));
                _jump = false;
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