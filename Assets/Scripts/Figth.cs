using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Figth : MonoBehaviour
{
    public GameObject scripts;
    public float speed = 5;
    public int numEnemy;
    public int targetEnemy;

    private float effAttack = 2;
    private float notEffAttack = 1;
    private float notrAttack = 0.5f;

    Pawns obje1;
    Pawns obje2;
    public string type1;
    public string type2;
    public string obje1Team;
    public string obje2Team;
    public string enemyTeam;
    //public StreamWriter swWriter;

    private void OnTriggerEnter(Collider other)
    {

    }


    private void OnTriggerStay(Collider other)
    {
        obje1 = gameObject.GetComponent<Pawns>();
        obje2 = other.gameObject.GetComponent<Pawns>();

        type1 = gameObject.GetComponent<Pawns>().type;
        type2 = other.gameObject.GetComponent<Pawns>().type;

        obje1Team = obje1.team;
        obje2Team = obje2.team;

        if (obje1Team == "blue") { enemyTeam = "red"; }
        else if (obje1Team == "red") { enemyTeam = "blue"; }


        if (type1 == "rock" && type2 == "scissors" && obje2Team == enemyTeam)
        {
            obje1.health -= notEffAttack;
            obje2.health -= effAttack;
        }
        if (type1 == "rock" && type2 == "paper" && obje2Team == enemyTeam)
        {
            obje1.health -= effAttack;
            obje2.health -= notEffAttack;
        }
        if (type1 == "rock" && type2 == "rock" && obje2Team == enemyTeam)
        {
            obje1.health -= notrAttack;
            obje2.health -= notrAttack;
        }
        if (type1 == "scissors" && type2 == "rock" && obje2Team == enemyTeam)
        {
            obje1.health -= effAttack;
            obje2.health -= notEffAttack;
        }


        if (type1 == "scissors" && type2 == "paper" && obje2Team == enemyTeam)
        {
            obje1.health -= notEffAttack;
            obje2.health -= effAttack;
        }

        if (type1 == "scissors" && type2 == "scissors" && obje2Team == enemyTeam)
        {
            obje1.health -= notrAttack;
            obje2.health -= notrAttack;
        }
        if (type1 == "paper" && type2 == "rock" && obje2Team == enemyTeam)
        {
            obje1.health -= notEffAttack;
            obje2.health -= effAttack;

        }
        if (type1 == "paper" && type2 == "scissors" && obje2Team == enemyTeam)
        {
            obje1.health -= effAttack;
            obje2.health -= notEffAttack;
        }

        if (type1 == "paper" && type2 == "paper" && obje2Team == enemyTeam)
        {
            obje1.health -= notrAttack;
            obje2.health -= notrAttack;
        }

    }


    public void stopContact()
    {
        transform.localPosition = transform.localPosition;
    }
}
