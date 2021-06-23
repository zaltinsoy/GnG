using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;

public class SingleAgent : Agent

//Agent class'�n�n kopyas� odu art�k
{
    private GameSetting gSet;
    private Rigidbody rBody;
    private Figth figth;
    //  private float forceMultiplier;
    private Pawns pawnObje;
    float speed;
    private float xRange;
    private float zRange;
    private string preyType;
    private string notrType;
    private string predatorType;

    void Start()
    {
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        rBody = GetComponent<Rigidbody>();
        pawnObje = GetComponent<Pawns>();
        figth = GetComponent<Figth>();
        // forceMultiplier = 5;
        speed = 5;
        xRange = 30;
        zRange = 20;
        

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

    //episode ba��na d�nd�rme komutu
    public override void OnEpisodeBegin()
    {
        //episode resetleme burada, hmm bu i� zor g�r�n�yor
        //ba��ms�z yapt�k, bakal�m nas�l olacak

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //burada �e�itli bilgileri y�kl�yoruz, agent'�n bildi�i �eyler.
        //kendi yerinin bilgisi:
        sensor.AddObservation((transform.localPosition.x + xRange) / (2 * xRange));
        sensor.AddObservation((transform.localPosition.z + zRange) / (2 * zRange));
        sensor.AddObservation(GetComponent<Pawns>().typeNo);

        //rakibin yeri bilgisi verdik
        /*
        for(int i = 0; i < gSet.bList.Length;i++)

        {
            sensor.AddObservation(gSet.bList[i].transform);
        }
        */
        // for (int i = 0; i < gSet.bList.Length; i++)
        for (int i = 0; i < gSet.blueList.Count; i++)

        {
            //   sensor.AddObservation(gSet.bList[i].transform.localPosition-transform.localPosition);
            sensor.AddObservation((gSet.blueList[i].transform.localPosition.x - transform.localPosition.x + xRange) / (2 * xRange));
            sensor.AddObservation((gSet.blueList[i].transform.localPosition.z - transform.localPosition.z + zRange) / (2 * zRange));
            sensor.AddObservation(gSet.blueList[i].GetComponent<Pawns>().typeNo);
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //action ve reward tan�mlar� burada.
        Vector3 controlSignal = Vector3.zero;
        //as�l kontrol etti�imiz "action" � burada tan�ml�yoruz.
        //cont. action boyutunu behaviour parameter'dan tan�mlad�k

        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        //rBody.AddForce(controlSignal * forceMultiplier);

        transform.localPosition += new Vector3(Time.deltaTime * controlSignal.x * speed, 0, Time.deltaTime * controlSignal.z * speed);

        //�u olursa bitir ve puan� ver:
        //if(gSet.bList.Length<3)
        //if(gSet.remBlue<3)

        // if (gameObject)

        SetReward(0.01f);

        if(figth.type2==preyType)
        { SetReward(-1); }
        else if (figth.type2 == predatorType)
        { SetReward(-1); }
        else if(figth.type2==notrType)
        { SetReward(-1); }


        if (gSet.blueList.Count < 1) //win the round
        {
            //SetReward(1);
            EndEpisode();
            //Academy.Instance.OnEnvironmentReset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (gSet.redList.Count < 1) //lost the round
        {
          //  SetReward(-1);
            EndEpisode();
            // SceneManager.LoadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (pawnObje.health < 1) //�unu artt�rabilirim hem de�erini hem etkisini
        {
            SetReward(-1);
            //EndEpisode();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }


    void Update()
    {

    }
}
