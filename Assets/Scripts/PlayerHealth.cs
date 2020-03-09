using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;

        if(playerHealth <= 0)
        {
            Debug.Log("You died");
        }
    }
}
