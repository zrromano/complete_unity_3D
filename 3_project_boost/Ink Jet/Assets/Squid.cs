using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Squid : MonoBehaviour {

    [SerializeField] float rotationThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float ascensionTime = 1f;
    [SerializeField] float deathTime = 1f;
    [SerializeField] AudioClip thrust;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip death;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { ALIVE, DYING, ASCENDING}

    State state = State.ALIVE;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.ALIVE)
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ignore collisions when dead
        if(state != State.ALIVE){return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    break;
                }
            case "Finish":
                {
                    state = State.ASCENDING;
                    audioSource.Stop();
                    audioSource.PlayOneShot(win);
                    Invoke("LoadNextLevel", ascensionTime);
                    break;
                }
            default:
                {
                    state = State.DYING;
                    audioSource.Stop();
                    audioSource.PlayOneShot(death);
                    Invoke("LoadFirstLevel", deathTime);
                    break;
                }

        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void RespondToRotationInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeTorque(Vector3.forward * rotationThrust, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeTorque(Vector3.back * rotationThrust, ForceMode.Force);
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrust);
            }
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
