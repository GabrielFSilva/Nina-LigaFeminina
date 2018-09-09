using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger2D : MonoBehaviour {

    public event Action<Collider2D> OnCustomTriggerEnter2D;

    public BoxCollider2D trigger;
	// Use this for initialization
	void Start () {
        trigger = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnCustomTriggerEnter2D != null)
            OnCustomTriggerEnter2D(other);
    }
    
}
