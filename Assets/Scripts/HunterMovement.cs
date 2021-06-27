using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMovement : MonoBehaviour
{
    private GameSetting gSet;
    float speed;
    private int numEnemy;
    public int targetEnemy;
    private Pawns pawn;
    private List<GameObject> targetList;

    void Start()
    {
        targetEnemy = -1;
        speed = 5;
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        pawn = gameObject.GetComponent<Pawns>();

    }

    void FixedUpdate()
    {
        FollowPrey();
    }

    public void FollowPrey()
    {

        if (pawn.team == "blue")
        {
            if (pawn.type == "rock") { targetList = gSet.redScissList; }
            else if (pawn.type == "scissors") { targetList = gSet.redPaperList; }
            else if (pawn.type == "paper") { targetList = gSet.redRockList; }
        }
        else if (pawn.team == "red")
        {
            if (pawn.type == "rock") { targetList = gSet.blueScissList; }
            else if (pawn.type == "scissors") { targetList = gSet.bluePaperList; }
            else if (pawn.type == "paper") { targetList = gSet.blueRockList; }
        }

        numEnemy = targetList.Count;


        if (numEnemy == 0)
        {

        }

        else
        {
            if (targetEnemy >= numEnemy)
            {
                targetEnemy = Random.Range(0, numEnemy);
            }
            else if (targetEnemy == -1 || targetList[targetEnemy] == null)
            {
                targetEnemy = Random.Range(0, numEnemy);
            }
            Vector3 target = targetList[targetEnemy].transform.position;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
        }
    }

}
