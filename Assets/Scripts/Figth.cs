using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figth : MonoBehaviour
{
    public GameObject scripts;
    // public GameSetting gSet;
    public float speed = 5;
    public int numEnemy;
    public int targetEnemy;

    private float effAttack = 2;
    private float notEffAttack = 1;
    private float notrAttack = 0.5f;


    //Movement MovScript;

    Pawns obje1;
    Pawns obje2;
    string type1;
    string type2;
    private Vector3 Vectasor3;
    //  public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        //  targetEnemy = -1;
        Vectasor3 = new Vector3(1, 2, 3);
        //     MovScript= gameObject.GetComponent<Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        //FollowEnemy();
        //  MovScript.FollowEnemy();
        //Movement.folow
    }
    private void OnTriggerEnter(Collider other)
    {

    }


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("claaash");
        //Þu an friendly fire açýk
        obje1 = gameObject.GetComponent<Pawns>();
        obje2 = other.gameObject.GetComponent<Pawns>();

        type1 = gameObject.GetComponent<Pawns>().type;
        type2 = other.gameObject.GetComponent<Pawns>().type;


        if (type1 == "rock" && type2 == "scissors")
        {
            obje1.health -= notEffAttack;
            obje2.health -= effAttack;
        }
        if (type1 == "rock" && type2 == "paper")
        {
            obje1.health -= effAttack;
            obje2.health -= notEffAttack;
        }
        if (type1 == "rock" && type2 == "rock")
        {
            obje1.health -= notrAttack;
            obje2.health -= notrAttack;
        }
        if (type1 == "scissors" && type2 == "rock")
        {

            obje1.health -= effAttack;
            obje2.health -= notEffAttack;
        }


        if (type1 == "scissors" && type2 == "paper")
        {

            obje1.health -= notEffAttack;
            obje2.health -= effAttack;


        }

        if (type1 == "scissors" && type2 == "scissors")
        {

            obje1.health -= notrAttack;
            obje2.health -= notrAttack;
        }
        if (type1 == "paper" && type2 == "rock")
        {
            obje1.health -= notEffAttack;
            obje2.health -= effAttack;

        }
        if (type1 == "paper" && type2 == "scissors")
        {

            obje1.health -= effAttack;
            obje2.health -= notEffAttack;

        }

        if (type1 == "paper" && type2 == "paper")
        {

            obje1.health -= notrAttack;
            obje2.health -= notrAttack;

        }

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
    /*
    public void FollowEnemy()
    {
        int numEnemy = gSet.rList.Length;

        //Burada düþmaný zekice seçmiyor dümdüz sýradan kovalýyor hepsini
        if (targetEnemy == -1 || gSet.rList[targetEnemy] == null)
        {
            targetEnemy = Random.Range(0, numEnemy - 1);
        }

        Debug.Log(targetEnemy);

        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        //Vector3 target=gSet.rScissList[0].transform.position;

        Vector3 target = gSet.rList[targetEnemy].transform.position;

        //transform.Translate((target - transform.position) * Time.deltaTime * speed/5);
        //oha adamlar kodunu yazmýþ ya, ben niye uðraþýyorsam.
        //go towards chosen enemy:
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
    }
    */
    //Buna çok da gerek yok zaten contact olunca duruyorlar.
    public void stopContact()
    {
        transform.localPosition = transform.localPosition;
    }
}
