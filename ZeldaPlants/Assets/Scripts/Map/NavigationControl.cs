using UnityEngine;

namespace Assets.Scripts
{
    public class NavigationControl : MonoBehaviour {

        public float MaxSpeed = 0.1f;

        private float _horizontal;
        private float _vertical;

        void Start () {	
        }
	
        void Update () {	
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }
	
        void FixedUpdate () {

            //if (Mathf.Abs (rigidbody2D.velocity.x) > MaxSpeed) {
            //    rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
            //}

            //rigidbody2D.AddForce(Vector2.right * horizontal * forceAdjustment);
            //rigidbody2D.AddForce(Vector2.up * vertical * forceAdjustment);
            transform.Translate(Vector2.right * _horizontal * MaxSpeed, Space.World);
            transform.Translate(Vector2.up * _vertical * MaxSpeed, Space.World);
        }

    }
}