using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figth : MonoBehaviour
{
    Pawns obje1;
    Pawns obje2;
    string type1;
    string type2;
    private Vector3 Vectasor3;

    // Start is called before the first frame update
    void Start()
    {
   
        Vectasor3 = new Vector3(1, 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asd");

        //Debug.Log(gameObject);
        //Debug.Log(other.gameObject);
        //Destroy(gameObject);
        obje1 = gameObject.GetComponent<Pawns>();
        obje2 = other.gameObject.GetComponent<Pawns>();

        type1 = gameObject.GetComponent<Pawns>().type;
        type2 = other.gameObject.GetComponent<Pawns>().type;
        //Destroy(other.gameObject);
      //  Debug.Log(type1);
        //Debug.Log(type2);
        
        if(type1=="rock"&& type2== "scissors"){ Destroy(other.gameObject);}
        if(type1 == "rock" && type2 == "paper") { Destroy(gameObject);}
        if (type1 == "scissors" && type2 == "rock") { Destroy(gameObject); }
        if (type1 == "scissors" && type2 == "paper") { Destroy(other.gameObject); }
        if (type1 == "paper" && type2 == "rock") { Destroy(other.gameObject); }
        if (type1 == "paper" && type2 == "scissors") { Destroy(gameObject); }
        
    }
    /*
    private void OnCollisionStay(Collision collision)
    {
        if (type1 == "rock" && type2 == "scissors") {
            obje2.health -= 1;
            //    Destroy(other.gameObject);
            Debug.Log("obje2.healt");
        }
    }
    */
}
