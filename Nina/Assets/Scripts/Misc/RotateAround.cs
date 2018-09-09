using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public Vector3 rotation;
    public bool active = true;
	
	// Update is called once per frame
	void Update () {
        if (active)
            transform.Rotate(rotation * Time.deltaTime);
	}
}
