using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 3f;
    public Vector2 direction;

    [SerializeField]
    public float speed = 1f;

    [SerializeField]
    private Rigidbody2D rb;

	void Start ()
    {
        rb.velocity = (direction - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
        Destroy(gameObject, lifeSpan);
	}
}
