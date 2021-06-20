using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameSetting : MonoBehaviour
{
    public GameObject[] bRockList;
    public GameObject[] bPaperList;
    public GameObject[] bScissList;
    public GameObject[] rRockList;
    public GameObject[] rPaperList;
    public GameObject[] rScissList;
    public GameObject[] rList;
    public GameObject[] bList;


    // Start is called before the first frame update
    void Start()
    {
        
        bRockList = GameObject.FindGameObjectsWithTag("bRock");
        bPaperList = GameObject.FindGameObjectsWithTag("bPaper");
        bScissList = GameObject.FindGameObjectsWithTag("bSciss");
        rRockList = GameObject.FindGameObjectsWithTag("rRock");
        rPaperList = GameObject.FindGameObjectsWithTag("rPaper");
        rScissList = GameObject.FindGameObjectsWithTag("rSciss");

        rList = rRockList;
        rList = rList.Concat(rScissList).ToArray();
        rList = rList.Concat(rPaperList).ToArray();

        bList = bRockList;
        bList = bList.Concat(bScissList).ToArray();
        bList = bList.Concat(bPaperList).ToArray();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        bRockList = GameObject.FindGameObjectsWithTag("bRock");
        bPaperList = GameObject.FindGameObjectsWithTag("bPaper");
        bScissList = GameObject.FindGameObjectsWithTag("bSciss");
        rRockList = GameObject.FindGameObjectsWithTag("rRock");
        rPaperList = GameObject.FindGameObjectsWithTag("rPaper");
        rScissList = GameObject.FindGameObjectsWithTag("rSciss");

        rList = rRockList;
        rList = rList.Concat(rScissList).ToArray();
        rList = rList.Concat(rPaperList).ToArray();

        bList = bRockList;
        bList = bList.Concat(bScissList).ToArray();
        bList = bList.Concat(bPaperList).ToArray();


        //rList.concat(rPaperList,rPaperList);
        //                gravy = gravy.Concat(lazy).ToArray();
        */


    }
    /*
    public static Vector3 moveTowards(Vector3 ini,Vector3 target)
    {
        target-ini
            
        return new Vector3d(v.x / mag, v.y / mag, v.z / mag);
    }
    */
}
