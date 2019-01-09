using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    AudioSource myAudioSource;
    Rigidbody myRigidBody;

    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] ParticleSystem engineEffect;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] ParticleSystem winEffect;
    [SerializeField] GameObject engineLight; //betere methode zoeken

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

    public bool collisionSwitch = false;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if(Debug.isDebugBuild)
            DebugMode();
	}

    private void DebugMode()
    {
      bool keyPressed = false;
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!keyPressed)
            {
                keyPressed = true;
                LoadNextLevel();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!keyPressed)
            {
                keyPressed = true;
                LoadPreviousLevel();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!keyPressed)
            {
                keyPressed = true;
                collisionSwitch = !collisionSwitch;
            }
        }

        if(Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.P))
        {
            keyPressed = false; 
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }
        else
        {
            myAudioSource.Stop();
            engineEffect.Stop();
            engineLight.SetActive(false);
        }
    }

        private void Thrust()
    {
        engineLight.SetActive(true);
        float thrustThisFrame = thrustSpeed * Time.deltaTime;
        myRigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        //myRigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
        if (!myAudioSource.isPlaying) //stops layering
        {
            myAudioSource.PlayOneShot(engineSound);
            engineEffect.Play();
        }
    }

    private void RespondToRotateInput()
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

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Friendly")
        {

        }
        else
            Destroy(gameObject);*/

        if (state != State.Alive) return; //don't handle collision if dead

        if (!collisionSwitch)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    //stay alive
                    break;

                case "Fuel":
                    //refuel 
                    break;

                case "Finish":
                    //load next level
                    StartWinSequence();
                    break;

                default:
                    //handle death
                    StartDeathSequence();
                    break;
            }
        }
    }


    private void StartWinSequence()
    {
        state = State.Transcending;
        myAudioSource.Stop();
        winEffect.Play();
        myAudioSource.PlayOneShot(winSound);
        Invoke("LoadNextLevel", waitTime);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        myAudioSource.Stop();
        deathEffect.Play();
        myAudioSource.PlayOneShot(deathSound);
        Invoke("RestartLevel", waitTime);
    }

    private void LoadNextLevel() 
    {
        FindObjectOfType<LevelManager>().LoadNextLevel();
    }

    private void LoadPreviousLevel()
    {
        FindObjectOfType<LevelManager>().LoadPreviousLevel();
    }


    private void RestartLevel()
    {
        FindObjectOfType<LevelManager>().RestartLevel();
    }
}
