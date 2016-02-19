using UnityEngine;
using System.Collections;

public class CatGirlController : MonoBehaviour {

    public float walkSpeed = 15f;
    public float jumpForce = 850f;
    public Transform groundChecker;
    public LayerMask whatIsGround;

    // Facing direction state
    float direction = 0f;

    // Is trying to jump state
    bool isJumping = false;

    // Is in mid air after jump state
    bool isInJump = false;

    // Is on ground state
    bool isGrounded = false;

    new Rigidbody2D rigidbody2D;
    Animator animator;

    // Use this for initialization
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        isGrounded = IsOnGround();

        if (isGrounded) {
            isInJump = false;
        }

        DoMoveHorizontally(direction);
        DoJump(isJumping);
    }

    void Update() {
        direction = IsMoving();
        isJumping = IsJumping();

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isHMoving", direction != 0);
        animator.SetFloat("hSpeed", direction);
        animator.SetBool("isJump", isInJump);
    }

    float IsMoving() {
        return Input.GetAxisRaw("Horizontal");
    }

    bool IsJumping() {
        return Input.GetAxisRaw("Jump") > 0;
    }

    bool IsOnGround() {
        return Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);
    }

    void DoMoveHorizontally(float direction) {
        float distance = direction * walkSpeed;

        rigidbody2D.velocity = new Vector2(distance, rigidbody2D.velocity.y);

        if (distance > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (distance < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void DoJump(bool isJumping) {
        if (isJumping && isGrounded) {
            isInJump = true;
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, jumpForce));
        }
    }
}
