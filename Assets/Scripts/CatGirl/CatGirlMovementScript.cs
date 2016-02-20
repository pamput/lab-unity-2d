using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(CatGirlControllerScript))]
    public class CatGirlMovementScript : MonoBehaviour {

        public float speed = 15f;
        public float startJumpForce = 15f;
        public float jumpForce = 30f;
        public float jumpMaxHoldTime = 0.08f;

        bool grounded;
        bool notInMidJump = true;

        float jumpHoldTime = 0;
        bool jumpAllow = true;
         
        CatGirlControllerScript input;
        new Rigidbody2D rigidbody2D;

        // Use this for initialization
        void Start() {
            input = GetComponent<CatGirlControllerScript>();
            rigidbody2D = GetComponent<Rigidbody2D>();

            input.OnMove += OnMove;
            input.OnJump += OnJump;
            input.OnGrounded += OnGrounded;
        }

        void OnMove(float rawDirection, float direction) {
            float distance = rawDirection * speed;
            rigidbody2D.velocity = new Vector2(distance, rigidbody2D.velocity.y);

            if (distance > 0) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else if (distance < 0) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        void OnJump(bool buttonJump, float buttonJumpTime) {
            
            if (buttonJump && grounded) {
                rigidbody2D.velocity = Vector2.up * startJumpForce;
            }

            bool fastJump = !grounded && !buttonJump && buttonJumpTime > 0;
            bool longJump = !grounded && buttonJump && buttonJumpTime >= jumpMaxHoldTime;
            bool isJump = fastJump || longJump;

            if (isJump && jumpAllow && rigidbody2D.velocity.y > 0) {
                float fraction = jumpForce / jumpMaxHoldTime;
                float force = fraction * Mathf.Min(jumpMaxHoldTime, buttonJumpTime);
                force = Mathf.Max(force, rigidbody2D.velocity.y);
                rigidbody2D.velocity = Vector2.up * force;
                jumpAllow = false;
            }
        }

        void OnGrounded(bool grounded) {
            this.grounded = grounded;

            if (grounded) {
                jumpAllow = true;
            }

            if (grounded && rigidbody2D.velocity.y < 0) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }
        }
    }
}
