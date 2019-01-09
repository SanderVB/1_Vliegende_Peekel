﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovingPlatform : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 0f);
    [SerializeField] float period =2f;
    [Range(0,1)] float movementFactor;
    Vector3 startingPos;

	// Use this for initialization
	void Start ()
    {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(period<= Mathf.Epsilon) { return; } //prevents divide by zero
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles*tau); //goes from -1 to 1

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offSet = movementFactor * movementVector;
        transform.position = startingPos + offSet;
	}
}
