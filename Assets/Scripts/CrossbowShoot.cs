using UnityEngine;
using TMPro;
public class CrossbowShoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowLocation;
    private Camera fpsCam;
    public float shotPower = 2000f;
    private float crossbowRange = 200f;
    private float crossbowDamage = 34f;
    private Animator crossbowAnim;

    private int currentAmmo;
    private int maxAmmo = 10;    
    public TextMeshProUGUI ammoText;

    [SerializeField] private float fireRate = 1f;
    private float nextTimeToFire = 0f;

    void Start()
    {
        if (arrowLocation == null){
            arrowLocation = transform;
        }

        fpsCam = GetComponentInParent<Camera>();
        crossbowAnim = GetComponent<Animator>();

        currentAmmo = maxAmmo;
    }

        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0) 
            {            
                ShootCrossbow();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
            Zoom();

            ammoText.text = currentAmmo + "/10";
        }

        
        void ShootCrossbow()
        {
            AudioManager.PlaySound("crossbowShoot");
            currentAmmo--;           
            
                Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotPower);
                RaycastHit hit;
                
                if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,crossbowRange))
                {
                    if(hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(crossbowDamage);
                    }
                }        
            
        }


private void Zoom()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            crossbowAnim.SetBool("zoom",true);
            //crosshair.SetActive(false);
        }
        if(Input.GetButtonUp("Fire2"))
        {
            crossbowAnim.SetTrigger("zoomOut");
            crossbowAnim.SetBool("zoom",false);    
            //crosshair.SetActive(true);        
            
        }
    }

    public void RecoverAmmo()
    {
        if(currentAmmo < 10)
        {
            currentAmmo++;
        }
        
    }



}

