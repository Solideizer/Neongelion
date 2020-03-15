using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 6000f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damage = 20f;   
    private int maxAmmo = 6;
    private int currentAmmo;
    [SerializeField] private float reloadTime = 3.2f;    
    [SerializeField] private float fireRate = 1f;
    private float nextTimeToFire = 0f;

    private GameObject crosshair;

    private Animator gunAnim;

    private float range = 20f;

    private Camera cam;
   

    // Start is called before the first frame update    
    void Start()
    {       
        crosshair = GameObject.FindWithTag("Crosshair");
        gunAnim = GetComponentInChildren<Animator>();
        cam = GetComponentInParent<Camera>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            //anim.SetBool("shoot", true);
            Shoot();
            nextTimeToFire = Time.time + 1f / fireRate;
        }
        /*else
        {
            anim.SetBool("shoot", false);
        }*/

         if (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
        }
        Zoom();
    }

    private void Shoot()
    {
        AudioManager.PlaySound("gunshoot");
        currentAmmo--;

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,range))
        {
            if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }


        //Instantiation of projectiles
        GameObject bullet = Instantiate(bulletPrefab);

        //Physics.IgnoreCollision(bulletCollider,gunCollider);

        bullet.transform.position = bulletSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x,transform.eulerAngles.y,rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet,lifeTime));
    }

    private IEnumerator Reload()
    {
        AudioManager.PlaySound("reload");
        gunAnim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bulletPrefab,float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(bulletPrefab);

    }
    private void Zoom()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            gunAnim.SetBool("Zoom",true);
            //crosshair.SetActive(false);
        }
        if(Input.GetButtonUp("Fire2"))
        {
            gunAnim.SetTrigger("zoomOut");
            gunAnim.SetBool("Zoom",false);    
            //crosshair.SetActive(true);        
            
        }
    }
}






