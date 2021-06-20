using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //public GameObject scripts;
    public GameSetting gSet;
    public float speed;
    public int numEnemy;
    public int targetEnemy;
    // Start is called before the first frame update
    void Start()
    {
        targetEnemy = -1;
        /*
        scripts=fin
        gSet = ScriptHolder;
        gSet=GetComponent<ScritHolder>.GameSettin
        */

    }

    // Update is called once per frame
    void Update()
    {
        numEnemy = gSet.rList.Length;
       

        if (targetEnemy == -1||gSet.rList[targetEnemy]==null)
        {
            targetEnemy = Random.Range(0, numEnemy - 1);
        }
        
        Debug.Log(targetEnemy);

        //transform.Translate(Vector3.right * Time.deltaTime * speed);

        //Vector3 target=gSet.rScissList[0].transform.position;

        Vector3 target = gSet.rList[targetEnemy].transform.position;
        
        //transform.Translate((target - transform.position) * Time.deltaTime * speed/5);
        //oha adamlar kodunu yazmýþ ya, ben niye uðraþýyorsam.
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
    }

}
