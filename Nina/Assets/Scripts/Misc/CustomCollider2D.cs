using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider2D : MonoBehaviour
{
    public event Action<CustomCollider2D> OnTouched;

    private void OnMouseDown()
    {
        if (OnTouched != null)
            OnTouched(this);
    }
}
