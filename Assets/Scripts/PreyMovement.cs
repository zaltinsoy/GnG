using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyMovement : MonoBehaviour
{
    private GameSetting gSet;
    float speed;
    private int numEnemy;
    private int targetEnemy;
    private Pawns pawn;
    private GameObject targetObje;
    private GameObject[] targetList;

    void Start()
    {
        targetEnemy = -1;
        speed = 6;
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        pawn = gameObject.GetComponent<Pawns>();

    }

    void FixedUpdate()
    {
        RunfromHunter();
    }

    public void RunfromHunter()
    {

        if (pawn.team == "blue")
        {
            if (pawn.type == "paper") { targetList = gSet.rScissList; }
            else if (pawn.type == "rock") { targetList = gSet.rPaperList; }
            else if (pawn.type == "scissors") { targetList = gSet.rRockList; }
        }
        else if (pawn.team == "red")
        {
            if (pawn.type == "paper") { targetList = gSet.bScissList; }
            else if (pawn.type == "rock") { targetList = gSet.bPaperList; }
            else if (pawn.type == "scissors") { targetList = gSet.bRockList; }
        }

        numEnemy = targetList.Length;

        if (targetEnemy == -1 || targetList[targetEnemy] == null)
        {
            targetEnemy = Random.Range(0, numEnemy);
        }
        Vector3 target = targetList[targetEnemy].transform.position;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, -1 * Time.deltaTime * speed);

    }

}
