using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawns : MonoBehaviour { 

    public double health;
    public string type;
    public string team;
    private float xRange;
    private float zRange;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        xRange = 30;
        zRange = 20;
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }

    }
  
}