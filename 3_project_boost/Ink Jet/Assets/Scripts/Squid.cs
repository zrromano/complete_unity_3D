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

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem successParticles;

    [SerializeField] bool safeMode = false;

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
        if (Debug.isDebugBuild)
        {
            DebugCommands();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ignore collisions when dead
        if(state != State.ALIVE || safeMode){return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    break;
                }
            case "Finish":
                {
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    StartDeathSequence();
                    break;
                }

        }
    }

    private void StartDeathSequence()
    {
        thrustParticles.Stop();
        state = State.DYING;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", deathTime);
    }

    private void StartSuccessSequence()
    {
        thrustParticles.Stop();
        state = State.ASCENDING;
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        successParticles.Play();
        Invoke("LoadNextLevel", ascensionTime);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToRotationInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeTorque(Vector3.forward * rotationThrust * Time.deltaTime, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeTorque(Vector3.back * rotationThrust * Time.deltaTime, ForceMode.Force);
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrust);
        }
        thrustParticles.Play();
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void DebugCommands()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            if (safeMode)
                safeMode = false;
            else
                safeMode = true;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(4);
        }
    }
}
