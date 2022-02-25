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
    //rays geändert
    //plattform kleiner
    //imitation learning 5 runden
    //negativer reward bei mauer hit und konstanter mauer hit

    How to use the ray sensor?

    wenn i hit dirt than i need to let the dirt dissappear 

    if all objects dissappeared add reward and endepisode (for that i could implement something like an counter till 20)

    //I can make imitation learning with the agent and catch the first 5 dirt that he realizes that this is good

    //ich mache die objekte zwar nicht sichtbar aber funktionell sind sie immer noch da

    //wenn ein ray sensor trifft bekomme ich winzig kleinen reward, d

    //check if the agent dont move 
    //log if the agent doesnt move

    //ich könnte negativen reward einbauen wenn mein agent die mauer trifft

    //behavioral_cloning:
      strength: 0.5
      demo_path: Demos/Demo7.demo

    gail:
        strength: 0.5
        demo_path: Demos/Demo7.demo

    //vielleicht habe ich auch falsch durch imitation learning trainiert. mein agent hat mit rays vielleicht die ein dirt material gesichtet ich bin dann aber nicht zu diesem material sondern zu einem anderen und das irritiert den algorithmus



    wenn das komische stehen bleiben behavior durch imitation learning besteht kann ich imi auch weg lassen um zu schauen ob das komische behavior auch dann noch auftaucht


    behaviors:
  VacuumCleaner:
    trainer_type: ppo
    hyperparameters:
      batch_size: 10
      buffer_size: 100
      learning_rate: 3.0e-4
      beta: 5.0e-4
      epsilon: 0.2
      lambd: 0.99
      num_epoch: 3
      learning_rate_schedule: linear
    network_settings:
      normalize: false
      hidden_units: 128
      num_layers: 2
    reward_signals:
      extrinsic:
        gamma: 0.99
        strength: 1.0
      gail:
        strength: 1.0
        demo_path: Demos/Demo7.demo
    behavioral_cloning:
      strength: 0.5
      demo_path: Demos/Demo7.demo
    max_steps: 1500000
    time_horizon: 64
    summary_freq: 10000

    */
    public int counter = 0;

    public override void OnEpisodeBegin(){
        GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
        foreach (GameObject goal in goals)
        {
            goal.transform.localPosition = new Vector3(Random.Range(-36.0f,36.0f),0.5999991f,Random.Range(-36.0f,36.0f));
        }
        //Transform goalTransform = goal.transform;
        // get player position
        //goal.transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
        //hier muss die spawn methode aus dirtscript aufgerufen werden
        transform.localPosition = new Vector3(0, 1, 0);
        //geht das auch ohne das objekt weiter oben zu erstellen?
        //DirtScript dirt = new DirtScript();
    }


    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions){
        //Debug.Log(MaxStep);
        if(counter == 20){
            GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
            foreach (GameObject goal in goals)
            {
                goal.transform.localPosition = new Vector3(Random.Range(-36.0f,36.0f),0.5999991f,Random.Range(-36.0f,36.0f));
            }
            AddReward(1f);
            counter = 0;
            EndEpisode();
        }
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];    
        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
       // AddReward(-1f / MaxStep);
    }

    private void OnTriggerEnter(Collider other){
        //
        //Debug.Log(other.tag);

        //wenn der trigger nicht den tag wall hat oder den tag platform dann reiceive one reward
        if(other.tag == "goal"){
            counter += 1;
            //disables the components meshrenderer 
            //set position which is not on platform otherwise agent can still detect dirt objects
            other.gameObject.transform.localPosition += new Vector3(100, -100, 100);
            AddReward(1f);
            Debug.Log(counter);
        }
        /*else if(other.tag=="wall"){
            //-0.2 reward zu hoch? und deswegen traut sich die ai nicht zu bewegen?
            AddReward(-0.2f);
        }*/
        //um die collision zu registrieren muss ich box collider und rigidbodys adden wahrscheinlich
    }

    private void OnTriggerStay(Collider other){
        if(other.tag == "wall"){
            Debug.Log("wall");
            AddReward(-0.05f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log(collision.gameObject.tag);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
