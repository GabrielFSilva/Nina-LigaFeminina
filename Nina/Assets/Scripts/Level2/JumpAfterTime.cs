using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAfterTime : MonoBehaviour {

    public Rigidbody2D rb;
    public Vector2 jumpForce;
    public float timeBetweenJumps;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (timeBetweenJumps >= 0f)
            InvokeRepeating("Jump", timeBetweenJumps, timeBetweenJumps);
        else
            Jump();
	}
	
    private void Jump()
    {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        SoundManager.instance.PlaySFX(SoundManager.SFXType.JUMP, 0.8f);
    }
}
