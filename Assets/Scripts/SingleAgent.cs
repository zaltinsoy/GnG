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
    private float forceMultiplier;
    private Pawns pawnObje;
    float speed;

    void Start()
    {
        gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        rBody = GetComponent<Rigidbody>();
        pawnObje = GetComponent<Pawns>();
        forceMultiplier = 5;
        speed = 5;

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
        sensor.AddObservation(transform.localPosition); //kendi yerini biliyor.

        //rakibin yeri bilgisi verdik
        /*
        for(int i = 0; i < gSet.bList.Length;i++)

        {
            sensor.AddObservation(gSet.bList[i].transform);
        }
        */
        for (int i = 0; i < gSet.blueList.Count; i++)

        {
            sensor.AddObservation(gSet.blueList[i].transform);
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

        if (gSet.blueList.Count < 2)
        {
            SetReward(3);
            EndEpisode();
            //Academy.Instance.OnEnvironmentReset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //if (gSet.rList.Length<3)
        //if(gSet.remRed<3)

        /*
        if (gSet.redList.Count < 2)
        {
            SetReward(-1);
            EndEpisode();
            // SceneManager.LoadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
         */


        if (pawnObje.health < 20)
        {
            SetReward(-2);
            EndEpisode();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }


    void Update()
    {

    }
}
