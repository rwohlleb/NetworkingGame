using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float lifeTime = 10f;

    void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0)
            {
                Destruction();
            }
        }

        if(this.transform.position.y <= -5)
        {
            Destruction();
        }   
    }
    // Use this for initialization
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "destroyer")
        {
            Destroy(gameObject);
        }
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
