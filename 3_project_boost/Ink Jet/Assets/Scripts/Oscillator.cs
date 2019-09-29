using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [SerializeField] bool biDirectional = true;

    // todo remove from inspector later
    [Range(-1,1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(period == 0) { return; }

        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2; //about 6.28
        float sinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        Vector3 offset;
        if (biDirectional)
        {
            offset = movementVector * sinWave;
        }
        else
        {
            offset = movementVector * (sinWave / 2f + 0.5f);
        }

        transform.position = startingPos + offset;
	}
}
