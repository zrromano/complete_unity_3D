using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeTorque(Vector3.forward, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeTorque(Vector3.back, ForceMode.Force);
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up);
            rigidBody.AddRelativeForce(Vector3.up);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
