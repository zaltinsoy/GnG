using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour
{
    //public GameObject scripts;
    private GameSetting gSet;
    float speed;
   // private Rigidbody rBody;
    private int numEnemy;
    public int targetEnemy;
    private Pawns pawn;
    private List<GameObject> targetList;
 //   private float forceMultiplier;

    void Start()
    {
        targetEnemy = -1;
        speed = 5;
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        pawn = gameObject.GetComponent<Pawns>();
       // forceMultiplier = 5;
       // rBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate() //update'ti fixed update yaptým yeni.
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
