using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashForce = 50f;
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {            
            AudioManager.PlaySound("dash");
            StartCoroutine(Dash());           
        }
    }

    public IEnumerator Dash()
    {
        rb.AddForce(Camera.main.transform.forward * dashForce,ForceMode.VelocityChange);
        yield return new WaitForSeconds (dashDuration);
        rb.velocity = Vector3.zero;
    }


}
