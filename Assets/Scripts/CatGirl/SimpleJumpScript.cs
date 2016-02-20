using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(ControllerScript))]
    public class SimpleJumpScript : MonoBehaviour {

        public float jumpForce = 1500f;

        bool grounded;
        bool canJump = true;

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
            if (grounded) {
                if (!buttonJump) {
                    canJump = true;
                }

                if (buttonJump && canJump) {
                    rigidbody2D.AddForce(Vector2.up * jumpForce);
                    canJump = false;
                }
            }
        }

        void OnGrounded(bool grounded) {
            this.grounded = grounded;

            if (grounded && rigidbody2D.velocity.y < 0) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }
        }
    }
}
