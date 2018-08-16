using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger2D : MonoBehaviour {

    public event Action<Collider2D> OnCustomTriggerEnter2D;

    public BoxCollider2D trigger;
    public SpriteRenderer debugSprite;
	// Use this for initialization
	void Start () {
        trigger = GetComponent<BoxCollider2D>();
        debugSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (debugSprite != null)
            debugSprite.enabled = trigger.enabled;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnCustomTriggerEnter2D != null)
            OnCustomTriggerEnter2D(other);
    }
    
}
