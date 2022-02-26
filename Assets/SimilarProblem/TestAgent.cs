using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using System.Globalization;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TestAgent : Agent
{   
    //Notes
    /*
    */
    public int counter = 0;

    public int testCounter = 0;

    [SerializeField] private Transform targetTransform;

   // public int StepCount { get; }


    public override void OnEpisodeBegin(){
        targetTransform.localPosition = new Vector3(Random.Range(-6.9f,6.9f),0.5999999f,Random.Range(-6.9f,6.9f));
        //here I access probably all my objects with
        /*GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
        foreach (GameObject goal in goals)
        {
            goal.transform.localPosition = new Vector3(Random.Range(-6.9f,6.9f),0.5999999f,Random.Range(-6.9f,6.9f));
        }*/
        transform.localPosition = new Vector3(1, 1, 1);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions){
        //bei 1500 steps wird die Episode beendet 
        if(this.StepCount == 1500){
            EndEpisode();
        }
        //Debug.Log(this.StepCount);
        if(counter == 1){
            testCounter += 1;

            GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
            foreach (GameObject goal in goals)
            {
                goal.transform.localPosition = new Vector3(Random.Range(-6.9f,6.9f),0.5999999f,Random.Range(-6.9f,6.9f));
            }
            AddReward(1f);
            counter = 0;
            EndEpisode();
        }
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];    
        float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

     private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "wall"){
            counter += 1;
            AddReward(1f);
            Debug.Log("This works");
        }
        /*else if(other.tag=="wall"){
            AddReward(-0.5f);
        }*/
        
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
