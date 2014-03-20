using UnityEngine;

namespace Assets.Scripts
{
    public class NavigationControl : MonoBehaviour {
        private bool _jump = false;

        public float JumpForce = 100f;
        public float MaxSpeed = 10f;

        private float horizontal;
        private float vertical;

        void Start () {	
        }
	
        void Update () {	
            if (Input.GetButtonDown ("Jump")) {
                _jump = true;		
            }

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
	
        void FixedUpdate () {


            //if (Mathf.Abs (rigidbody2D.velocity.x) > MaxSpeed) {
            //    rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
            //}

            //rigidbody2D.AddForce(Vector2.right * horizontal * forceAdjustment);
            //rigidbody2D.AddForce(Vector2.up * vertical * forceAdjustment);
            transform.Translate(Vector2.right * horizontal * MaxSpeed, Space.World);
            transform.Translate(Vector2.up * vertical * MaxSpeed, Space.World);

            if (_jump) {
                rigidbody2D.AddForce (new Vector2 (0f, JumpForce));
                _jump = false;
            }
        }

    }
}