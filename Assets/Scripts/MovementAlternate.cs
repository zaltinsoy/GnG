using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAlternate : MonoBehaviour
{

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //transform.Translate(Random.insideUnitCircle * Time.deltaTime * speed);}
    }
}
