using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMovement : MonoBehaviour
{
    //public GameObject scripts;
    private GameSetting gSet;
    float speed;
    private int numEnemy;
    public int targetEnemy;
    private Pawns pawn;
    private GameObject targetObje;
    private GameObject[] targetList;

    void Start()
    {
        targetEnemy = -1;
        speed = 5;
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        pawn = gameObject.GetComponent<Pawns>();

    }

    void Update()
    {
        FollowPrey();
    }

    public void FollowPrey()
    {

      

        if (pawn.team == "blue")
        {
            if (pawn.type == "rock") { targetList = gSet.rScissList; }
            else if (pawn.type == "scissors") { targetList = gSet.rPaperList; }
            else if (pawn.type == "paper") { targetList = gSet.rRockList; }
        }
        else if (pawn.team == "red")
        {
            if (pawn.type == "rock") { targetList = gSet.bScissList; }
            else if (pawn.type == "scissors") { targetList = gSet.bPaperList; }
            else if (pawn.type == "paper") { targetList = gSet.bRockList; }
        }


        numEnemy = targetList.Length;

        if (targetEnemy == -1 || targetList[targetEnemy] == null)
        {
            targetEnemy = Random.Range(0, numEnemy);
        }
        Vector3 target = targetList[targetEnemy].transform.position;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target,Time.deltaTime * speed);

    }

}
