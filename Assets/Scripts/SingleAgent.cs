using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;
using System.IO;

public class SingleAgent : Agent

//Agent class'ýnýn kopyasý odu artýk
{
    private GameSetting gSet;
    private Rigidbody rBody;
    private Figth figth;
    private float forceMultiplier;
    private Pawns pawnObje;
    float speed;
    private float xRange;
    private float zRange;
    private string preyType;
    private string notrType;
    private string predatorType;
    private float timer;
    public string enemyTeam;
    private GameObject[] enemyArray;
    private List<GameObject> enemyList;
    private List<GameObject> allyList;
    //public StreamWriter swWriter2;
    void Start()
    {
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        rBody = GetComponent<Rigidbody>();
        pawnObje = GetComponent<Pawns>();
        figth = GetComponent<Figth>();
        forceMultiplier = 5;
        speed = 5; //normalde 5'di ama þimdi hýzlandýrdýk
        xRange = 30;
        zRange = 20;


        if (pawnObje.team == "blue") { enemyTeam = "red"; }
        else if (pawnObje.team == "red") { enemyTeam = "blue"; }



        if (pawnObje.type == "rock")
        {
            preyType = "scissors";
            notrType = "rock";
            predatorType = "paper";
        }
        else if (pawnObje.type == "scissors")
        {
            preyType = "paper";
            notrType = "scissors";
            predatorType = "rock";
        }
        else if (pawnObje.type == "paper")
        {
            preyType = "rock";
            notrType = "paper";
            predatorType = "scissors";
        }

    }


    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {

        sensor.AddObservation((transform.localPosition.x + xRange));
        sensor.AddObservation((transform.localPosition.z + zRange));
        sensor.AddObservation(GetComponent<Pawns>().typeNo);
        /*
        if (enemyTeam == "red")
        {
            enemyArray = gSet.rList;
            enemyList = gSet.redList;
            allyList = gSet.blueList;

        }
        else if (enemyTeam == "blue")
        {
            enemyArray = gSet.bList;
            enemyList = gSet.blueList;
            allyList = gSet.redList;
        }
        */
        if (enemyTeam == "blue")
        {
            for (int i = 0; i < gSet.bList.Length; i++)
            {
                if (gSet.bList[i] == null)
                {
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                }
                else
                {
                    //Relative position and types of the enemy objects
                    sensor.AddObservation((gSet.bList[i].transform.localPosition.x - transform.localPosition.x));
                    sensor.AddObservation((gSet.bList[i].transform.localPosition.z - transform.localPosition.z));
                    sensor.AddObservation(gSet.bList[i].GetComponent<Pawns>().typeNo);
                }
            }
        }

        else if (enemyTeam == "red")
        {

            for (int i = 0; i < gSet.rList.Length; i++)
            {
                if (gSet.rList[i] == null)
                {
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                }
                else
                {
                    //Relative position and types of the enemy objects
                    sensor.AddObservation((gSet.rList[i].transform.localPosition.x - transform.localPosition.x));
                    sensor.AddObservation((gSet.rList[i].transform.localPosition.z - transform.localPosition.z));
                    sensor.AddObservation(gSet.rList[i].GetComponent<Pawns>().typeNo);
                }
            }

        }
        /*
          else //bu da böyle bir deneme, hadi bakalým:
        {
            for (int i = 0; i < gSet.bList.Length; i++)
            {
                if (gSet.bList[i] == null)
                {
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                    sensor.AddObservation(0);
                }
                else
                {
                    //Relative position and types of the enemy objects
                    sensor.AddObservation((gSet.bList[i].transform.localPosition.x - transform.localPosition.x));
                    sensor.AddObservation((gSet.bList[i].transform.localPosition.z - transform.localPosition.z));
                    sensor.AddObservation(gSet.bList[i].GetComponent<Pawns>().typeNo);
                }
            }
        }
        */

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    public override void OnActionReceived(ActionBuffers actions)
    {

        Vector3 controlSignal = Vector3.zero;
        //Size of the control action should be defined in behaviour parameter
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(Time.deltaTime * controlSignal.x * speed, 0, Time.deltaTime * controlSignal.z * speed);

        //kýsa süreliðine iptal ettik burayý!
        
        /*
        if (figth.type2 == preyType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(0.01f);
        }
        else if (figth.type2 == predatorType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(-0.01f);

        }
        else if (figth.type2 == notrType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(0.005f);

        }
        */
        if (enemyTeam == "blue")

        {
            //Game ending conditions:

            if (gSet.blueList.Count < 2) //win the round
            {
                 SetReward(1);
                EndEpisode(); 
                //Academy.Instance.OnEnvironmentReset();
               //   gSet.RecordScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            if (gSet.redList.Count < 2) //lost the round
            {
                SetReward(-1);
                EndEpisode();
                //   gSet.RecordScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }

        else if (enemyTeam == "red")
        {

            //Game ending conditions:

            if (gSet.redList.Count < 2) //win the round
            {
                SetReward(1);
                EndEpisode();
                //Academy.Instance.OnEnvironmentReset();
                // gSet.RecordScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            if (gSet.blueList.Count < 2) //lost the round
            {
                SetReward(-1);
                EndEpisode();

             //   gSet.RecordScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

        }
        //If object is about to die:
        if (pawnObje.health < 50)
        {
            //SetReward(-1);
            //EndEpisode();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //If object pass the border of the game:

        //aþaðýsý önemli
        /*
        if (transform.position.x < -xRange)
        {
            AddReward(-1);
        }
        if (transform.position.x > xRange)
        {
            AddReward(-1);
        }

        if (transform.position.z < -zRange)
        {
            AddReward(-1);
        }
        if (transform.position.z > zRange)
        {
            AddReward(-1);
        }
        */

    }
    //private void FixedUpdate()
    private void Update()
    {
        //If game is not ending for a chosen time, it will end manually:
        timer += Time.deltaTime;
        if (timer > 60) //120de de tutabiliriz.60-240 arasý kullandýk bunu asýl!
        {

            if (enemyTeam == "blue")

            {
                
                if (gSet.blueList.Count < gSet.redList.Count) //win the round
                {
                    SetReward(1);
                    EndEpisode();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

               else if (gSet.redList.Count < gSet.blueList.Count) //lost the round
                {
                    SetReward(-1);
                    EndEpisode();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            else if (enemyTeam == "red")
            {

                //Game ending conditions:

                if (gSet.redList.Count < gSet.blueList.Count) //win the round
                {
                    SetReward(1);
                    EndEpisode();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                }

                if (gSet.blueList.Count < gSet.redList.Count) //lost the round
                {
                    SetReward(-1);
                    EndEpisode();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                }

            }

            //aþaðýsý asýl: üst taraf iptal olacak
            //EndEpisode();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);



        }

    }
}
