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
        if (pawn.team == "blue")
        {
            numEnemy = gSet.rList.Length;

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
            numEnemy = gSet.bList.Length;

            if (targetEnemy == -1 || gSet.bList[targetEnemy] == null)
            {
                targetEnemy = Random.Range(0, numEnemy);
                //Eskiden numEnemy-1'e kadardý, NumEnemy yapýnca sorun çözüldü sonuna kadar arýyor
            }
            Vector3 target = gSet.bList[targetEnemy].transform.position;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if (numEnemy == 0) { Debug.Log("gameOver"); }
        }
    }

}
