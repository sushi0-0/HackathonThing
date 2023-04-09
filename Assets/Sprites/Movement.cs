using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform spawnPoint;

    //animation stuff
    public Animator animator;

    //Basic Movement Stuff
    private Rigidbody2D rb;
    private TrailRenderer tR;
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor;
    float lastTimeGrounded;


    //Dashing Stuff
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    //Flipping stuff
    float inputHorizontal;
    bool facingRight = true;

    //Health Stuff
    [SerializeField] private float trauma = 0.05f;

    //Knockback
    [SerializeField] private Transform center;

    private SpriteRenderer[] SpriteRenders;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenders = GetComponentsInChildren<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        tR = gameObject.GetComponent<TrailRenderer>();

    }



    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal > 0 && !facingRight)
        {
            flip();
        }
        if (inputHorizontal < 0 && facingRight)
        {
            flip();
        }
    }


    // Update is called once per frame
    void Update()
    {

        Jump();
        CheckIfGrounded();
        BetterJump();


        //Moving Update

        float moveBy = Input.GetAxisRaw("Horizontal") * speed;
        var lerpedXVelocity = Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3);


        rb.velocity = new Vector2(moveBy, rb.velocity.y);








        //Dashing Update
        var dashInput = Input.GetKeyDown(KeyCode.LeftShift);

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());

        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            animator.SetBool("IsDashing", true);
            tR.emitting = true;
        }

        if (!isDashing)
        {
            animator.SetBool("IsDashing", false);
            tR.emitting = false;
        }

        //Animator Update
        if (isGrounded)
        {
            canDash = true;
            //animator.SetBool("IsJump", false);
        }

        if (!isGrounded)
        {
            //animator.SetBool("IsJump", true);
        }

        //animator.SetFloat("Running", Mathf.Abs(moveBy));


    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }

    }




    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }

        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }





    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;


        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;


        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }


    void awake()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //animator.SetBool("GotHurt", true);
            gameObject.transform.position = spawnPoint.transform.position;

            StartCoroutine(unhurt());

        }
        if (collision.gameObject.tag == "Death")
        {
            //animator.SetBool("GotHurt", true);
            gameObject.transform.position = spawnPoint.transform.position;
            StartCoroutine(unhurt());



        }



        IEnumerator unhurt()
        {

            yield return new WaitForSeconds(trauma);
            //animator.SetBool("GotHurt", false);
        }





    }
}
