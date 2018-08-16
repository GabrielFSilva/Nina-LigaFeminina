using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public event Action<DestroyAfterTime> OnDestroyed;

    public bool playOnAwake = true;
    public float timer = 1f;

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", timer);
	}
	
	private void DestroySelf()
    {
        if (OnDestroyed != null)
            OnDestroyed(this);
        Destroy(gameObject);
    }
}
