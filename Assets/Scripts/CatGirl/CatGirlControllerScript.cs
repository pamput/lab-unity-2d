using UnityEngine;
using System.Collections;

namespace CatGirl {
    public class CatGirlControllerScript : MonoBehaviour {

        public delegate void MoveAction(float rawDirection,float direction);

        public delegate void JumpAction(bool jumped);

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
        bool axisJump;

        void Update() {
            axisHorizontal = Input.GetAxis("Horizontal");
            axisHorizontalRaw = Input.GetAxisRaw("Horizontal");
            axisJump = Input.GetAxisRaw("Jump") > 0;
        }

        void FixedUpdate() {
            CheckGrounded();
            CheckJump();
            CheckMove();
        }

        void CheckJump() {
            if (OnJump != null) {
                OnJump(axisJump);
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

