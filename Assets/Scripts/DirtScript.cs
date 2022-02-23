using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScript : MonoBehaviour
{
    /*
    either i coutn in any way here the dirt objects which are disabled or in the agentscript with an public var 

    */
    // Start is called before the first frame update
    void Start()
    {
        AgentScript agent = new AgentScript();
        if(agent.counter == 20 || agent.counter == 0){
            //transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
        }
       // transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method which calls position etc ca
    /*public void spawnDirtObjects(){
        transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));

    }*/
}
