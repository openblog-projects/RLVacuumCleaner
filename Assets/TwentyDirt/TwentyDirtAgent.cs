using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;


public class TwentyDirtAgent : Agent
{
    /*
    if agents hits dirt, dirt disappears
    random spawn of dirt
    if agents moves to far away respawn adjustments
    */
    public int counter = 0;

    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 10f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 300;

    [Tooltip("The platform to be moved around")]
    public GameObject goal;
    //ich kann für jedes gameobject ein einzelnes objekt machen

    public GameObject goal2;

    public GameObject goal3;

    public GameObject goal4;

    public GameObject goal5;

    public GameObject goal6;

    public GameObject goal7;

    public GameObject goal8;

    public GameObject goal9;

    public GameObject goal10;

    public GameObject goal11;

    public GameObject goal12;

    public GameObject goal13;

    public GameObject goal14;

    public GameObject goal15;

    public GameObject goal16;

    public GameObject goal17;

    public GameObject goal18;

    public GameObject goal19;

    public GameObject goal20;

    private Vector3 startPosition;
    //private SimpleCharacterController characterController;
    new private Rigidbody rigidbody;

    public override void Initialize()
    {
        startPosition = transform.position;
        //characterController = GetComponent<SimpleCharacterController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position, rotation
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
        rigidbody.velocity = Vector3.zero;

        goal.SetActive(true);
        goal2.SetActive(true);
        goal3.SetActive(true);
        goal4.SetActive(true);
        goal5.SetActive(true);
        goal6.SetActive(true);
        goal7.SetActive(true);
        goal8.SetActive(true);
        goal9.SetActive(true);
        goal10.SetActive(true);
        goal11.SetActive(true);
        goal12.SetActive(true);
        goal13.SetActive(true);
        goal14.SetActive(true);
        goal15.SetActive(true);
        goal16.SetActive(true);
        goal17.SetActive(true);
        goal18.SetActive(true);
        goal19.SetActive(true);
        goal20.SetActive(true);

        // Reset platform position (5 meters away from the agent in a random direction)
        goal.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal2.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal3.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal4.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal5.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal6.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal7.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal8.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal9.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal10.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal11.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal12.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal13.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal14.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal15.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal16.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal17.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
        goal18.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 15f;
        goal19.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
        goal20.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 10f;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Read input values and round them. GetAxisRaw works better in this case
        // because of the DecisionRequester, which only gets new decisions periodically.
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);

        // Convert the actions to Discrete choices (0, 1, 2)
        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = vertical >= 0 ? vertical : 2;
        actions[1] = horizontal >= 0 ? horizontal : 2;
        actions[2] = jump ? 1 : 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Punish and end episode if the agent strays too far
        if (Vector3.Distance(startPosition, transform.position) > 25f)
        {
            AddReward(-1f);
            EndEpisode();
        }

        // Convert actions from Discrete (0, 1, 2) to expected input values (-1, 0, +1)
        // of the character controller
        float vertical = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
        float horizontal = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;
        bool jump = actions.DiscreteActions[2] > 0;

        /*characterController.ForwardInput = vertical;
        characterController.TurnInput = horizontal;
        characterController.JumpInput = jump;*/

        // Turning
        if (horizontal != 0f)
        {
            float angle = Mathf.Clamp(horizontal, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }

        // Movement
        Vector3 move = transform.forward * Mathf.Clamp(vertical, -1f, 1f) *
            moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + move);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);

        if (other.gameObject.tag == "goal")
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            counter += 1;
            AddReward(1f);
            if(counter == 20){
                counter = 0;
                EndEpisode();
            }
        }
        // If the other object is a collectible, reward and end episode
    }
}
