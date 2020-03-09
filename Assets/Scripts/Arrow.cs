using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rbody;
    private void Start() 
    {
        rbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag != "Enemy"){
            rbody.constraints = RigidbodyConstraints.FreezeAll;   
        }
         

    }

    
}
