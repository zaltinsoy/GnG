using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour
{
    //public GameObject scripts;
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

    void Update()
    {
        FollowEnemy();
    }

    public void FollowEnemy()
    {
        if (pawn.team == "blue") { targetList = gSet.redList; }
        else if (pawn.team == "red") { targetList = gSet.blueList; }
        numEnemy = targetList.Count;

        if (numEnemy == 0) { Debug.Log("Oyun Bitti"); }
        else
        {
            if(targetEnemy >= numEnemy)
            {
                targetEnemy = Random.Range(0, numEnemy);
            }

            else if (targetEnemy == -1 || targetList[targetEnemy] == null)
            {
                targetEnemy = Random.Range(0, numEnemy);
                //Eskiden numEnemy-1'e kadardý, NumEnemy yapýnca sorun çözüldü sonuna kadar arýyor
            }
            Vector3 target = targetList[targetEnemy].transform.position;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
        }

        /*
        if (pawn.team == "blue")
        {
            //numEnemy = gSet.rList.Length;
            numEnemy = gSet.redList.Count;

            if (targetEnemy == -1 || gSet.rList[targetEnemy] == null)
            {
                targetEnemy = Random.Range(0, numEnemy);
                //Eskiden numEnemy-1'e kadardý, NumEnemy yapýnca sorun çözüldü sonuna kadar arýyor
            }
            Vector3 target = gSet.rList[targetEnemy].transform.position;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if (numEnemy == 0) { Debug.Log("gameOver"); }
        }
        else if (pawn.team == "red")
        {
            //numEnemy = gSet.bList.Length;
            numEnemy = gSet.blueList.Count;

            if (targetEnemy == -1 || gSet.bList[targetEnemy] == null)
            {
                targetEnemy = Random.Range(0, numEnemy);
                //Eskiden numEnemy-1'e kadardý, NumEnemy yapýnca sorun çözüldü sonuna kadar arýyor
            }
            Vector3 target = gSet.bList[targetEnemy].transform.position;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if (numEnemy == 0) { Debug.Log("gameOver"); }
        }

        */
    }

}
