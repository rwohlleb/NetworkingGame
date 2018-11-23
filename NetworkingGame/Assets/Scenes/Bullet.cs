using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        		
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Cube")
        {
            Destroy(col.gameObject);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
    }
}
