using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float bulletSpeed = 50.0f;
    

    private float zBound = 20f;
    private float xBound = 30f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        MoveProjectile();

        DestroyBullet();

        
    }

    // Moves the projectile in the direction the player is facing
    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    // Destroys bullet if outside play area
    void DestroyBullet()
    {
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
        }

        if(transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }

        if(transform.position.z > zBound)
        {
            Destroy(gameObject);
        }

        if(transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    
}
