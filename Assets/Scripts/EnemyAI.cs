using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   
    Animator anim;
    NavMeshAgent agent;
    GameObject playerObject;    
    [SerializeField] private float attackDistance = 10f;
    public static bool isAlive = true;
    PlayerHealth playerHealth;
    [SerializeField] private float enemyDamage = 10f;

    enum EnemyStates{
        Idle = 0,
        Run = 1,
        Attack = 2,
        Dead = 3
    }
    EnemyStates enemyStates;

    void Start()
    {
        enemyStates = EnemyStates.Idle;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("FPPlayer");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }

   
    void Update()
    {
        if(isAlive)
        {
            switch (enemyStates)
            {
                case EnemyStates.Idle:
                SearchForTarget();
                break;

                case EnemyStates.Run:
                SearchForTarget();
                break;

                case EnemyStates.Attack:
                Attack();
                break;

                case EnemyStates.Dead:
                KillEnemy();
                break;
            
            }
        }
    }


    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position,playerObject.transform.position);
        if(distance < 2f )
        {
            Attack();
        }
        else if(distance < attackDistance)
        {
            MoveToPlayer();

        }else{  

            SetState(EnemyStates.Idle);
            agent.isStopped = true;
        }
    }

    void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
        SetState(EnemyStates.Run);
    }

    void SetState(EnemyStates state)
    {
        enemyStates = state;
        anim.SetInteger("State",(int)state);
    }

    void KillEnemy()
    {
        SetState(EnemyStates.Dead);
        agent.isStopped = true;
    }

    void Attack()
    {
        SetState(EnemyStates.Attack);
        agent.isStopped = true;
        
    }

    void AttackEvent()
    {
        Debug.Log("Attack Event");
        playerHealth.TakeDamage(enemyDamage);
        SearchForTarget();
    }

}
