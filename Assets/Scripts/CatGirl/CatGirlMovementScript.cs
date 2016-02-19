using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(CatGirlControllerScript))]
    public class CatGirlMovementScript : MonoBehaviour {

        public float speed = 15f;
        public float jumpForce = 300f;
        public float jumpMaxForce = 850f;
        public float jumpDelta = 10f;

        bool grounded;
        float totalJumpForceApplied = 0;
         
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

        void OnJump(float rawJump, float jump) {
            if (jump == 0 && grounded) {
                this.totalJumpForceApplied = 0f;

            }

            if (jump > 0) {
                if (grounded) {
                    rigidbody2D.AddForce(Vector2.up * jumpForce);
                    totalJumpForceApplied = jumpForce;
                }

                if (!grounded && totalJumpForceApplied <= jumpMaxForce) {
                    totalJumpForceApplied += jumpDelta;
                    rigidbody2D.AddForce(Vector2.up * jumpDelta);
                }

                if (rigidbody2D.velocity.y < 0 && totalJumpForceApplied == 0f) {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                }
            }

        }

        void OnGrounded(bool grounded) {
            this.grounded = grounded;
        }
    }
}
