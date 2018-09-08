using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject projectilePrefab;
    
    private bool inRange;
    
    [SerializeField]
    [Range(0.5f, 5f)]
    private float shootCooldown = 1f;
    private float currentShootCooldown;

	// Use this for initialization
	void Start ()
    {
        currentShootCooldown = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if(inRange)
        {
            currentShootCooldown -= Time.deltaTime;
            if (currentShootCooldown <= 0f)
            {
                Shoot();
                currentShootCooldown += shootCooldown;
            }
        }

	}

    private void Shoot()
    {
        Projectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.direction = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
