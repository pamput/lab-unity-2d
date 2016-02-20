using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(ControllerScript))]
    public class HoldJumpScript : MonoBehaviour {

        public float startJumpVelocity = 15f;
        public float jumpVelocity = 30f;
        public float jumpMaxHoldTime = 0.08f;

        bool grounded;
        bool notInMidJump = true;

        float jumpHoldTime = 0;
        bool jumpAllow = true;
         
        ControllerScript input;
        new Rigidbody2D rigidbody2D;

        // Use this for initialization
        void Start() {
            input = GetComponent<ControllerScript>();
            rigidbody2D = GetComponent<Rigidbody2D>();

            input.OnJump += OnJump;
            input.OnGrounded += OnGrounded;
        }

        void OnJump(bool buttonJump, float buttonJumpTime) {
            
            if (buttonJump && grounded) {
                rigidbody2D.velocity = Vector2.up * startJumpVelocity;
            }

            bool fastJump = !grounded && !buttonJump && buttonJumpTime > 0;
            bool longJump = !grounded && buttonJump && buttonJumpTime >= jumpMaxHoldTime;
            bool isJump = fastJump || longJump;

            if (isJump && jumpAllow && rigidbody2D.velocity.y > 0) {
                float fraction = jumpVelocity / jumpMaxHoldTime;
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
