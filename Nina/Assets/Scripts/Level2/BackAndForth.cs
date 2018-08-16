using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveSpeed;
    [SerializeField]
    [Range(0f, 10f)]
    private float movementDuration;
    private bool isForward;
	// Use this for initialization

	void Start () {
        if (movementDuration >= 0.1f)
            InvokeRepeating("Turn", Random.Range(0f, movementDuration),movementDuration);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(moveSpeed * Time.deltaTime);
	}

    private void Turn()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        isForward = !isForward;
        moveSpeed *= -1f;
    }
}
