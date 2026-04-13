using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Transform[] wayPoints;

    [SerializeField] private float waitTime;
    private int currentWaypoint;
    private bool isWaiting;

    private Transform EnemyTransform;
    //private Transform Player;
    //private float distance;
    private StateMachine StateMach;
    //[SerializeField] private float PersecutionDistance;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StateMach = GetComponent<StateMachine>();
        //Player = GameObject.FindWithTag("Player").transform;
        EnemyTransform = GetComponent<Transform>();

        //currentWaypoint = 0;
        agent.SetDestination(wayPoints[currentWaypoint].position);
    }


    void Update()
    {
        //Distance();

        if (!agent.pathPending && agent.remainingDistance < 0.1f && !isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait(){

        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        SetNextWaypoint();

        isWaiting = false;

        //Flip();
        //gire los ojos del enemigo hacia el siguiente waypoint
    }

    // private void Flip(){

    //     if (transform.position.x > wayPoints[currentWaypoint].position.x)
    //     {
    //         transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //     }
    //     else{

    //         transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    //     }
    // }

    void SetNextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % wayPoints.Length;
        agent.SetDestination(wayPoints[currentWaypoint].position);
    }

    //   private void Distance(){

    //     if (Player != null && EnemyTransform != null)
    //     {
    //         // Calcula la distancia entre el player y la hormiga.
    //         distance = Vector3.Distance(Player.position, EnemyTransform.position);
            
    //     }

    //     if(distance < PersecutionDistance)
    //     {

    //         StateMach.ActivateState(StateMach.stateArray[1]);
    //     }
    // }
}
