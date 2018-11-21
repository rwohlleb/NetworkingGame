using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();

        // Create the Bullet from the Bullet Prefab
        /*var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);*/

        var bullet = (GameObject)Instantiate(
                                 bulletPrefab,
                                 transform.position + (Vector3)(direction * 0.5f),
                                 Quaternion.identity);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = direction * 1000;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}