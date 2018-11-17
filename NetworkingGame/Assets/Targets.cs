using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour {

    public float delay = 1f;
    public GameObject cube;
    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", delay, delay);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(cube, new Vector3(Random.Range(-15, 25), 10, 10), Quaternion.identity);
    }
}
