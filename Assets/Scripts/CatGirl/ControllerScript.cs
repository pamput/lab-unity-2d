using UnityEngine;
using System.Collections;

namespace CatGirl {
    public class ControllerScript : MonoBehaviour {

        public delegate void MoveAction(float rawDirection,float direction);

        public delegate void JumpAction(bool buttonJump,float buttonJumpTime);

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
        bool buttonJump;
        float buttonJumpTime = -1f;

        void Update() {
            axisHorizontal = Input.GetAxis("Horizontal");
            axisHorizontalRaw = Input.GetAxisRaw("Horizontal");

            // JUMP
            buttonJump = Input.GetButton("Jump");

            if (buttonJump && buttonJumpTime != -1f) {
                buttonJumpTime += Time.deltaTime;
            } else if (buttonJump && buttonJumpTime == -1f) {
                buttonJumpTime = 0;
            }
        }

        void FixedUpdate() {
            CheckGrounded();
            CheckJump();
            CheckMove();
        }

        void CheckJump() {
            if (OnJump != null) {
                OnJump(
                    buttonJump,
                    buttonJumpTime
                );

                if (!buttonJump) {
                    buttonJumpTime = -1f;
                }
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

