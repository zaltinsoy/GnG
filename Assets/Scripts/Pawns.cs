using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawns : MonoBehaviour
{

    public double health;
    public string type;
    public string team;
    private float xRange;
    private float zRange;
    public int typeNo;


    void Start()
    {
        health = 300;
        xRange = 30;
        zRange = 20;
    }

    void FixedUpdate()
    {
        //Define the border of the game - düþüþlü mod

        if (transform.position.x < -xRange)
        {
            health -= 10;
        }
        if (transform.position.x > xRange)
        {
            health -= 10;
        }

        if (transform.position.z < -zRange)
        {
            health -= 10;
        }
        if (transform.position.z > zRange)
        {
            health -= 10;
        }
        if (health < 0)
        {
            Destroy(gameObject);
        }


        //Define the border of the game 
        //kenara gelince takýlý kalýyor bunda
        /*
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        */


        //kenara gelince ýþýnlanma aþaðýsý
        /*
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
        
        */



    }

}
