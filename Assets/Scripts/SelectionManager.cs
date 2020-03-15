using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{      
    CrossbowShoot cs ;

    private void Start() {
        cs = FindObjectOfType<CrossbowShoot>();
    }
    // Update is called once per frame
    void Update()
    {        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,5f))
        {
            var selection  = hit.transform;
            if(selection.CompareTag("Ammo")){
               
                if(Input.GetKeyDown(KeyCode.F))
                {
                    //do some ui                    
                    cs.RecoverAmmo();
                    Destroy(selection.gameObject);
                }
            }else
                return;
        }
    }
}
