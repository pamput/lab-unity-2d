using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(CatGirlControllerScript))]
    public class CatGirlMovementScript : MonoBehaviour {

        public float speed = 15f;
        public float jumpForce = 850f;
        bool grounded;
         
        CatGirlControllerScript input;
        Rigidbody2D rigidbody2D;

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

        void OnJump(bool jump) {
            if (jump && grounded) {
                rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, jumpForce));
            }
        }

        void OnGrounded(bool grounded) {
            this.grounded = grounded;
        }
    }
}
