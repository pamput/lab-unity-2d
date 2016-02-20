using UnityEngine;
using System.Collections;

namespace CatGirl {
    
    [RequireComponent(typeof(CatGirlControllerScript))]
    public class CatGirlAnimatorScript : MonoBehaviour {
         
        CatGirlControllerScript input;
        Animator animator;

        bool grounded;
        bool jumped;

        // Use this for initialization
        void Start() {
            input = GetComponent<CatGirlControllerScript>();
            animator = GetComponent<Animator>();

            input.OnMove += OnMove;
            input.OnJump += OnJump;
            input.OnGrounded += OnGrounded;
        }

        void OnMove(float rawDirection, float direction) {
            animator.SetBool("isMoving", rawDirection != 0);
            animator.SetFloat("hSpeed", Mathf.Abs(direction));
        }

        void OnJump(bool buttonJump, float buttonJumpTime) {
            if (buttonJump && grounded) {
                jumped = true;
            }

            animator.SetBool("isJump", jumped && !grounded);
        }

        void OnGrounded(bool grounded) {
            this.grounded = grounded;

            if (grounded) {
                animator.SetBool("isJump", false);
                jumped = false;
            }

            animator.SetBool("isGrounded", grounded);
        }
    }
}