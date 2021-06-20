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
    private float forceMultiplier;
    //episode baþýna döndürme komutu
    public override void OnEpisodeBegin()
    {
        //episode resetleme burada, hmm bu iþ zor görünüyor

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //burada çeþitli bilgileri yüklüyoruz, agent'ýn bildiði þeyler.
        sensor.AddObservation(transform.localPosition); //kendi yerini biliyor.
       
        //rakibin yeri bilgisi verdik
        for(int i = 0; i < gSet.bList.Length;i++)

        {
            sensor.AddObservation(gSet.bList[i].transform);
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
        rBody.AddForce(controlSignal * forceMultiplier);

        //Þu olursa bitir ve puaný ver:
        if(gSet.bList.Length<3)
        {
            SetReward(1);
            EndEpisode();
            //Academy.Instance.OnEnvironmentReset();
            //  SceneManager.LoadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("bitti oyun");


        }

        if (gSet.rList.Length<3)
        {
            SetReward(-1);
            EndEpisode();
            // SceneManager.LoadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("bitti oyun");
        }


    }
    void Start()
    {
    gSet = GameObject.Find("ScriptHolder").GetComponent<GameSetting>();
        rBody = GetComponent<Rigidbody>();
        forceMultiplier = 10;

}

// Update is called once per frame
    void Update()
    {
        
    }
}
