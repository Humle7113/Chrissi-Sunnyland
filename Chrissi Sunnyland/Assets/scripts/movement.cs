using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;





public class movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingpower = 7f;
    private bool isFacingRight = true;
    public float playerSpeed;  //allows us to be able to change speed in Unity
    public Vector2 jumpHeight;
    int u = 1;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator;



    void Update()
    {
        int o = 0;

        //if (Input.GetKeyDown(KeyCode.Space))  //makes player jump
        //{
        //    GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);

        //}

        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
            u = 0;
            animator.SetBool("IsJumping", true);
            

        }
        if (Input.GetButtonDown("Jump")&& rb != IsGrounded()&&u==0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
            u = 1;
            animator.SetBool("IsJumping", true);

        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
            animator.SetBool("IsJumping", false);
        }
        
        

        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("punch", true);
        }
        

        else if (Input.GetMouseButtonUp(0)) 
        {

            animator.SetBool("punch", false);

        }

        Flip();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        animator.SetBool("IsJumping", true);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;


        }



    }
}
