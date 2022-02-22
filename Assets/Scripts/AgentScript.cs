using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentScript : Agent
{   
    //Notes
    /*
    How to use the ray sensor?

    wenn i hit dirt than i need to let the dirt dissappear 

    if all objects dissappeared add reward and endepisode (for that i could implement something like an counter till 20)
    
    */
    private int counter = 0;

    DirtScript dirtObject = gameObject.GetComponent<DirtScript>()

    public override void OnEpisodeBegin(){
        //hier muss die spawn methode aus dirtscript aufgerufen werden
        transform.position = new Vector3(0, 1, 0);
        myObject.GetComponent<MyScript>().MyFunction();
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions){
        if(counter == 20){
            AddReward(1f);
            EndEpisode();
        }
        

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];    
        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other){

        //wenn der trigger nicht den tag wall hat oder den tag platform dann reiceive one reward
        if(other.tag == "goal"){
            counter += 1;
            other.GetComponent<MeshRenderer>().enabled = false;
            AddReward(0.1f);
            Debug.Log(counter);
        }
        //um die collision zu registrieren muss ich box collider und rigidbodys adden wahrscheinlich
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
