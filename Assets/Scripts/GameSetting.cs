using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

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

    public List<GameObject> blueList; 
    public List<GameObject> redList;
    public List<GameObject> blueRockList;
    public List<GameObject> bluePaperList;
    public List<GameObject> blueScissList;
    public List<GameObject> redRockList;
    public List<GameObject> redPaperList;
    public List<GameObject> redScissList;

    //public TextAsset score;
    public StreamWriter swWriter;

    
    void Awake ()  //starttý burasý eskiden
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

        //Populate the array of the objects
        rList = rRockList;
        rList = rList.Concat(rScissList).ToArray();
        rList = rList.Concat(rPaperList).ToArray();

        bList = bRockList;
        bList = bList.Concat(bScissList).ToArray();
        bList = bList.Concat(bPaperList).ToArray();

        //Populate the list of the objects
        blueList = new List<GameObject>(bList);
        redList = new List<GameObject>(rList);
       // swWriter = new StreamWriter(Application.dataPath + "\\Score.txt");
       // RecordScore();

    }

    //void FixedUpdate()
        void Update()

    {
        
        //Bu þekilde açýk yazýnca çalýþýyor ama sýkýntýsý diðer türlü sankim?
        //RecordScore();

        remBlue = bList.Count();
        remRed = rList.Count();

        //Remove the destroyed objects
        redList.RemoveAll(item => item == null);
        blueList.RemoveAll(item => item == null);
        blueRockList.RemoveAll(item => item == null);
        bluePaperList.RemoveAll(item => item == null);
        blueScissList.RemoveAll(item => item == null);
        redRockList.RemoveAll(item => item == null);
        redPaperList.RemoveAll(item => item == null);
        redScissList.RemoveAll(item => item == null);


        //Game ending condition:

       // swWriter.WriteLine("deneme89");
//        swWriter.WriteLine(blueList.Count.ToString() + "," + redList.Count.ToString());
        
        /*
        if (blueList.Count <1 || redList.Count < 1)


        {
            swWriter = new StreamWriter(Application.dataPath + "\\Score.txt");
            //   RecordScore();
            swWriter.WriteLine("deneme89");
            //swWriter.Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */



         
        //asýl bu aþaðýdaki!!

        if (blueList.Count == 0 || redList.Count == 0)
        {
          swWriter = new StreamWriter(Application.dataPath + "\\Score.txt",true);
            //RecordScore();
         //   swWriter.WriteLine("deneme89");
          swWriter.WriteLine(blueList.Count.ToString()+","+redList.Count.ToString());
          swWriter.Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        



        //Alternative game end conditions
        /*

        if(blueList.Count==2 && redList.Count==2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (blueList.Count == 1 || redList.Count == 1 || blueList.Count==0||redList.Count==0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */

        /*
        if (redList.Count < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */



    }
    public void RecordScore()
    {
        //swWriter.WriteLine(remBlue.ToString()+","+remRed.ToString());
        //swWriter.WriteLine("deneme89");

        //niye çalýþmýyor burasý!!

        //File.WriteAllText(Application.dataPath + "/score.txt",remBlue.ToString());
      //  swWriter.Flush();
      

      // swWriter.Close();
    }
}
