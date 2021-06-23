using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawns : MonoBehaviour { 

    public double health;
    public string type;
    public string team;
    private float xRange;
    private float zRange;
    public int typeNo;


    // Start is called before the first frame update
    void Start()
    {
        health = 300;
        xRange = 30;
        zRange = 20;
    }

    // Update is called once per frame
    void Update()
    {

        //Define the border of the game 
       
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




        if (health<0) { 
            Destroy(gameObject); 
        }

        
    }
  
}
