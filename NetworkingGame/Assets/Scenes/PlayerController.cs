using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float speed = 5.0f;
    private Plane m_ClickPlane;

    RaycastHit hit;
    private float raycastlenth = 500;

    void Start()
    {
        m_ClickPlane = new Plane(-Vector3.forward, Vector3.zero);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;
            if (m_ClickPlane.Raycast(ray, out dist))
            {
                Vector3 dir = (ray.GetPoint(dist) - bulletSpawn.position).normalized;
                GameObject fireball = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                fireball.GetComponent<Rigidbody>().AddForce(dir * 500, ForceMode.Impulse);
                Destroy(fireball, 2.0f);
            }
        }*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raycastlenth, Color.green); // to see Raycast trajectory from camera chosen

        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 aimPoint; //point that cursor will mark

            if (Physics.Raycast(ray, out hit, 100))
            {

                aimPoint = hit.point;

            }
            else
            {                                        //if  ray doesn't hit anything, just make a point 100 units out into ray, to referece

                aimPoint = ray.origin + (ray.direction * 100);//aimPoint is some point 100 unitys into ray line

            }

            GameObject projectile = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as GameObject;

            projectile.transform.LookAt(aimPoint);          //fixes when hit point was = (0,0,0);

            Debug.DrawLine(bulletSpawn.transform.position, hit.point, Color.yellow); // to see trajectory of projectile


            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 250; //this plus the LookAt aimpoint sends a bullet on the correct ray

            Destroy(projectile, 1.0f);

        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision Detected");
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Cube")
        {
            Destroy(col.gameObject);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Detected: OnCollisionEnter");
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Cube")
        {
            Destroy(col.gameObject);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
    }
}