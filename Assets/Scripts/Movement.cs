using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioSource moveSound;
    Rigidbody myRigidBody;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationThrust = 100f;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        moveSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Thrust();
        Rotate();
	}
    private void Thrust()
    {
        //float thrustThisFrame = thrustSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            //thrust
            //myRigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            myRigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
            if (!moveSound.isPlaying) //stops layering
            {
                moveSound.Play();
            }
        }
        else
            moveSound.Stop();
    }

    private void Rotate()
    {

        float rotationThisFrame = rotationThrust * Time.deltaTime;

        myRigidBody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            //rotate left
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rotate right
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        myRigidBody.freezeRotation = false; //physics take over again

    }
}
