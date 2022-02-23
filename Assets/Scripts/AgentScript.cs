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
    

    //ich mache die objekte zwar nicht sichtbar aber funktionell sind sie immer noch da
    */
    public int counter = 0;

    public override void OnEpisodeBegin(){
        GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
        foreach (GameObject goal in goals)
        {
            goal.transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
        }
        
        //Transform goalTransform = goal.transform;
        // get player position
        //goal.transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
        //hier muss die spawn methode aus dirtscript aufgerufen werden
        transform.position = new Vector3(0, 1, 0);
        //geht das auch ohne das objekt weiter oben zu erstellen?
        //DirtScript dirt = new DirtScript();

    }


    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions){
        if(counter == 20){
            GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
            foreach (GameObject goal in goals)
            {
                goal.transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
            }
            AddReward(1f);
            counter = 0;
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
            //disables the components meshrenderer 
            //set position which is not on platform otherwise agent can still detect dirt objects
            other.gameObject.transform.position += new Vector3(100, -100, 100);
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
