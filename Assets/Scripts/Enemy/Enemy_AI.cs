using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{   //Player Position
    [SerializeField] Transform target;
    [SerializeField] float chase_range = 10.0f;
    NavMeshAgent enemy;
    float distance_between_player_and_target;
    Vector3 starting_pos;
    bool ischasing = false;
    void Start()
    {
        starting_pos = transform.localPosition;
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      /*  Debug.Log(enemy.stoppingDistance + "Stoping_pos");
        Debug.Log(transform.position + "Enemy postion");
        Debug.Log(distance_between_player_and_target + "distance_between_player_and_target");
        Debug.Log(target.position+ "Player Position");*/

        distance_between_player_and_target = Vector3.Distance(target.position, transform.position);
        if (ischasing)
        {
            Engage();
        }
        else if(distance_between_player_and_target < chase_range)
        {
            ischasing = true;   
        }
        else if (distance_between_player_and_target > chase_range)
        {
            enemy.SetDestination(starting_pos);
        }
        
    }

    void Engage()
    {
            if(distance_between_player_and_target >= enemy.stoppingDistance)
        {
            chase_target();
        }
            else if(distance_between_player_and_target <= enemy.stoppingDistance)
        {
            attack_target();
        }
    }

    private void attack_target()
    {
      
    }

    private void chase_target()
    {
        enemy.SetDestination(target.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chase_range);
    }
}
