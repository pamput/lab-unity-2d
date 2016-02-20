using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(CatGirlControllerScript))]
    public class CatGirlSimpleMovementScript : MonoBehaviour {

        public float speed = 15f;
        public float jumpForce = 1500f;

        bool grounded;
        bool canJump = true;

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
