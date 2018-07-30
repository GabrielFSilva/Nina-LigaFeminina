using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    [SerializeField]
    [Range(0f, 10f)]
    private float moveSpeed = 3f;
    [SerializeField]
    [Range(0f, 2500f)]
    private float jumpForce = 700f;

    private SpriteRenderer sprite;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h >= 0.1f)
            sprite.flipX = false;
        else if (h <= -0.1f)
            sprite.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(h));
        anim.SetBool("Jumping", !isGrounded);
    }
    void FixedUpdate () {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetAxis("Horizontal") != 0f)
            Move();
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
	}
    private void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }
}
