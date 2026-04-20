using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPersecution : MonoBehaviour
{
    [SerializeField] private Transform target;

    NavMeshAgent agent;

    private Transform EnemyTransform;
    private Transform Player;
    private float distance;
    private StateMachine StateMach;
    [SerializeField] private float PersecutionDistance;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StateMach = GetComponent<StateMachine>();
        Player = GameObject.FindWithTag("Player").transform;
        EnemyTransform = GetComponent<Transform>();

        //agent.speed = 2f;
    }

    private void Update()
    {
        Distance();

        if (distance < PersecutionDistance)
        {
            agent.SetDestination(target.position);
            //Debug.Log("movete");

        }
        else
        {
            // Si el enemigo no está persiguiendo al jugador, detenemos su movimiento
            //agent.ResetPath();
        }

    }

    private void Distance()
    {

        if (Player != null && EnemyTransform != null)
        {
            // Calcula la distancia entre el player y el enemigo.
            distance = Vector3.Distance(Player.position, EnemyTransform.position);

        }

        if (distance > PersecutionDistance)
        {
            //Debug.Log("no te muevas");
            StateMach.ActivateState(StateMach.stateArray[0]);
            

        }

    }

} 