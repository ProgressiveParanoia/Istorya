using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCam : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - target.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate() {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
