using UnityEngine;
using System.Collections;

namespace CatGirl {
    public class CatGirlControllerScript : MonoBehaviour {

        public delegate void MoveAction(float rawDirection,float direction);

        public delegate void JumpAction(float rawJump,float jump);

        public delegate void GroundedAction(bool grounded);

        public event MoveAction OnMove;
        public event GroundedAction OnGrounded;
        public event JumpAction OnJump;

        // Checker
        public Transform groundChecker;
        public LayerMask whatIsGround;

        // Axis check
        float axisHorizontal;
        float axisHorizontalRaw;
        float axisJump;
        float axisJumpRaw;

        void Update() {
            axisHorizontal = Input.GetAxis("Horizontal");
            axisHorizontalRaw = Input.GetAxisRaw("Horizontal");
            axisJump = Input.GetAxis("Jump");
            axisJumpRaw = Input.GetAxisRaw("Jump");
        }

        void FixedUpdate() {
            CheckGrounded();
            CheckJump();
            CheckMove();
        }

        void CheckJump() {
            if (OnJump != null) {
                OnJump(
                    axisJumpRaw,
                    axisJump
                );
            }
        }

        void CheckMove() {
            if (OnMove != null) {
                OnMove(
                    axisHorizontalRaw,
                    axisHorizontal
                );
            }
        }

        void CheckGrounded() {
            bool grounded = Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);

            if (OnMove != null) {
                OnGrounded(grounded);
            }
        }
    }
}

