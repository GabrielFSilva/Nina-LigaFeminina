using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Moviment
    [Header("Moviment Attributes")]
    public Rigidbody2D rb;
    [SerializeField]
    [Range(0f, 10f)]
    private float moveSpeed = 3f;
    private float speedMultiplier = 1f;

    // Jump
    [Header("Jump")]
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    [Range(100f, 2500f)]
    private float jumpForce = 850f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    // Health
    [Space(10)]
    [Header("Health")]
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private Scrollbar healthBar;

    // Attack
    [Space(10)]
    [Header("Attack")]
    [SerializeField]
    private bool isAttacking;
    private bool canAttack = true;
    private CustomTrigger2D attackTrigger;
    [SerializeField]
    [Range(0f, 2f)]
    private float attackDuration = 1f;
    [SerializeField]
    [Range(0f, 3f)]
    private float attackCooldown = 1.5f;
    [SerializeField]
    private float attackSpeedMultiplier = 0.5f;

    // Defense
    [Space(10)]
    [Header("Defense")]
    [SerializeField]
    private bool isDefending;
    private CustomTrigger2D defenseTrigger;
    [SerializeField]
    private float defenseSpeedMultiplier = 0.0f;

   

    private SpriteRenderer sprite;
    private Animator anim;

	// Use this for initialization
	void Start () {
        health = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attackTrigger = transform.Find("AttackTrigger").
            GetComponentInChildren<CustomTrigger2D>();
        defenseTrigger = transform.Find("DefenseTrigger").
            GetComponentInChildren<CustomTrigger2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h >= 0.1f)
            transform.localScale = Vector3.one;
        else if (h <= -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

            anim.SetFloat("Speed", Mathf.Abs(h));
        anim.SetBool("Jumping", !isGrounded);

        if (Input.GetMouseButtonDown(0) && isGrounded && canAttack && 
            !isAttacking && !isDefending)
        {
            canAttack = false;
            EnableAttack(true);
            Invoke("StopAttack", attackDuration);
            Invoke("ResetCanAttack", attackCooldown);
        }
        else if (Input.GetMouseButtonDown(1) && isGrounded && !isAttacking && !isDefending)
        {
            EnableDefense(true);
        }
        if (Input.GetMouseButtonUp(1) && isDefending)
        {
            EnableDefense(false);
        }

        healthBar.size = health / maxHealth;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        UpdateSpeedMultiplier();
       
        if (Input.GetAxis("Horizontal") != 0f)
            Move();
        if (Input.GetButtonDown("Jump") && isGrounded && !isAttacking && !isDefending)
            Jump();
    }

    private void StopAttack()
    {
        EnableAttack(false);
    }
    private void ResetCanAttack()
    {
        canAttack = true;
    }
    private void EnableAttack(bool enable)
    {
        isAttacking = enable;
        anim.SetBool("Attacking", enable);
        attackTrigger.trigger.enabled = enable;
    }

    private void StopDefense()
    {
        EnableDefense(false);
    }
    private void EnableDefense(bool enable)
    {
        isDefending = enable;
        anim.SetBool("Defending", enable);
        defenseTrigger.trigger.enabled = enable;
    }

    private void UpdateSpeedMultiplier()
    {
        speedMultiplier = 1f;
        if (isAttacking)
            speedMultiplier *= attackSpeedMultiplier;
        if (isDefending)
            speedMultiplier *= defenseSpeedMultiplier;
    }

    private void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * speedMultiplier, rb.velocity.y);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }
}
