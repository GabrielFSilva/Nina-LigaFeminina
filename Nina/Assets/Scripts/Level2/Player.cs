﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public event Action<Player> OnDied;

    private SoundManager soundManager;

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
    [Range(1f, 100f)]
    private float jumpForce = 5f;
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
    private SpriteRenderer attackSprite;
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
    private SpriteRenderer defenseSprite;
    [SerializeField]
    private float defenseSpeedMultiplier = 0.0f;

    // Blinking
    [Space(10)]
    [Header("Blinking")]
    [SerializeField]
    private bool blinking = false;
    private float blinkingDuration = 1f;

    private SpriteRenderer sprite;
    private Animator anim;
    private Blink blink;


	// Use this for initialization
	void Start () {
        soundManager = SoundManager.instance;

        health = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<Blink>();
        attackTrigger = transform.Find("AttackTrigger").
            GetComponentInChildren<CustomTrigger2D>();
        defenseTrigger = transform.Find("DefenseTrigger").
            GetComponentInChildren<CustomTrigger2D>();
        defenseTrigger.OnCustomTriggerEnter2D += OnDefenseTrigger;
    }

    private void OnDefenseTrigger(Collider2D obj)
    {
        if(obj.gameObject.tag == "EnemyProjectile")
        {
            soundManager.PlaySFX(SoundManager.SFXType.DEFEND);
            Destroy(obj.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h >= 0.1f)
            transform.localScale = Vector3.one;
        else if (h <= -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        UpdateSpeedMultiplier();

        if (Input.GetAxis("Horizontal") != 0f)
            Move();
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
            && isGrounded)
            Jump();

        if (Input.GetMouseButtonDown(0) && canAttack && 
            !isAttacking && !isDefending)
        {
            canAttack = false;
            EnableAttack(true);
            Invoke("StopAttack", attackDuration);
            Invoke("ResetCanAttack", attackCooldown);
            soundManager.PlaySFX(SoundManager.SFXType.PLAYER_ATTACK, 0.5f);
        }
        else if (Input.GetMouseButtonDown(1) && !isAttacking && !isDefending)
        {
            EnableDefense(true);
        }
        if (Input.GetMouseButtonUp(1) && isDefending)
        {
            EnableDefense(false);
        }

        healthBar.size = health / maxHealth;

        if (blinking)
        {
            blink.StartBlink();
        }
        else
        {
            blink.StopBlink();
        }
        
    }

    void FixedUpdate()
    {
      
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
        attackTrigger.trigger.enabled = enable;
        attackSprite.enabled = enable;
        anim.SetBool("Attacking", enable);
    }

    private void StopDefense()
    {
        EnableDefense(false);
    }
    private void EnableDefense(bool enable)
    {
        isDefending = enable;
        defenseTrigger.trigger.enabled = enable;
        defenseSprite.enabled = enable;
        anim.SetBool("Defending", enable);
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        soundManager.PlaySFX(SoundManager.SFXType.JUMP);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"  && !blinking)
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyProjectile" && !blinking)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health -= 10f;

        gameObject.layer = LayerMask.NameToLayer("PlayerBlink");
        blinking = true;
        if (health <= 0f && OnDied != null)
        {
            OnDied(this);
            this.enabled = false;
        }
        else
            StartCoroutine(EndBlinkingAfterTime(blinkingDuration));
    }

    IEnumerator EndBlinkingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        blinking = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
