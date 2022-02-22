using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method which calls position etc ca
    void spawnDirtObjects(){
        transform.localPosition = new Vector3(Random.Range(-74.0f,74.0f),0.5999991f,Random.Range(-74.0f,74.0f));

    }
}
