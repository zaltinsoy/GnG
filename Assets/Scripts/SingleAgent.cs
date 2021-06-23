using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;

public class SingleAgent : Agent

//Agent class'ýnýn kopyasý odu artýk
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

    //episode baþýna döndürme komutu
    public override void OnEpisodeBegin()
    {
        //episode resetleme burada, hmm bu iþ zor görünüyor
        //baðýmsýz yaptýk, bakalým nasýl olacak

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //burada çeþitli bilgileri yüklüyoruz, agent'ýn bildiði þeyler.
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
        //action ve reward tanýmlarý burada.
        Vector3 controlSignal = Vector3.zero;
        //asýl kontrol ettiðimiz "action" ý burada tanýmlýyoruz.
        //cont. action boyutunu behaviour parameter'dan tanýmladýk

        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        //rBody.AddForce(controlSignal * forceMultiplier);

        transform.localPosition += new Vector3(Time.deltaTime * controlSignal.x * speed, 0, Time.deltaTime * controlSignal.z * speed);

        //Þu olursa bitir ve puaný ver:
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


        if (pawnObje.health < 1) //þunu arttýrabilirim hem deðerini hem etkisini
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
