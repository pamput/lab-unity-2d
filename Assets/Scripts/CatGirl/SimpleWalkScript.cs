using UnityEngine;
using System.Collections;

namespace CatGirl {

    [RequireComponent(typeof(ControllerScript))]
    public class SimpleWalkScript : MonoBehaviour {

        public float speed = 15f;

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

            input.OnMove += OnMove;
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
