using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float startHealth = 100f;
    private float currentHealth;
    private Animator anim;
    private NavMeshAgent agent;

    private EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();

    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            EnemyAI.isAlive = false;
            agent.isStopped = true;
            anim.SetInteger("State",3);
            Destroy(gameObject,10f);
        }
    }
}
