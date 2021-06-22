using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

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

    public int remBlue;
    public int remRed;

    public List<GameObject> blueList; //Ýçerde neyin listesini olduðunu tanýmladýk
    public List<GameObject> redList;
    public List<GameObject> blueRockList;
    public List<GameObject> bluePaperList;
    public List<GameObject> blueScissList;
    public List<GameObject> redRockList;
    public List<GameObject> redPaperList;
    public List<GameObject> redScissList;


    // Start is called before the first frame update
    void Start()
    {

        bRockList = GameObject.FindGameObjectsWithTag("bRock");
        bPaperList = GameObject.FindGameObjectsWithTag("bPaper");
        bScissList = GameObject.FindGameObjectsWithTag("bSciss");
        rRockList = GameObject.FindGameObjectsWithTag("rRock");
        rPaperList = GameObject.FindGameObjectsWithTag("rPaper");
        rScissList = GameObject.FindGameObjectsWithTag("rSciss");

        blueRockList = new List<GameObject>(blueRockList);
        bluePaperList = new List<GameObject>(bPaperList);
        blueScissList = new List<GameObject>(bScissList);
        redRockList = new List<GameObject>(rRockList);
        redPaperList = new List<GameObject>(rPaperList);
        redScissList = new List<GameObject>(rScissList);


        rList = rRockList;
        rList = rList.Concat(rScissList).ToArray();
        rList = rList.Concat(rPaperList).ToArray();

        bList = bRockList;
        bList = bList.Concat(bScissList).ToArray();
        bList = bList.Concat(bPaperList).ToArray();

        blueList = new List<GameObject>(bList);
        redList = new List<GameObject>(rList);
    }

    // Update is called once per frame
    void Update()

    {

        remBlue = bList.Count();
        remRed = rList.Count();



        //blueList.OfType<GameObject>();
        //blueList.RemoveAll(string.IsNullOrWhiteSpace)
        redList.RemoveAll(item => item == null);
        blueList.RemoveAll(item => item == null);


        blueRockList.RemoveAll(item => item == null);
        bluePaperList.RemoveAll(item => item == null);
        blueScissList.RemoveAll(item => item == null);
        redRockList.RemoveAll(item => item == null);
        redPaperList.RemoveAll(item => item == null);
        redScissList.RemoveAll(item => item == null);



        if(blueList.Count<1||redList.Count<1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /*
        if (redList.Count < 3)
        {
          
            // SceneManager.LoadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("bitti oyun");
        }
        */


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
