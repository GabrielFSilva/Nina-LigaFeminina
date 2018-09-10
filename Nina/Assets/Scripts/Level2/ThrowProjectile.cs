using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject projectilePrefab;
    
    [SerializeField]
    private bool inRange;
    
    [SerializeField]
    [Range(0.5f, 5f)]
    private float shootCooldown = 1f;
    private float currentShootCooldown;

	// Use this for initialization
	void Start ()
    {
        currentShootCooldown = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) < 8f)
            inRange = true;
        else
            inRange = false;

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
        SoundManager.instance.PlaySFX(SoundManager.SFXType.ENEMY_SHOOT, 0.8f);
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
            Debug.Log(collision.gameObject.name);
        }
    }
}
