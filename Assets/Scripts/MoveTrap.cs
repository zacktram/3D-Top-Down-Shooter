using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float trapLength = 5.65f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCutWheel();
    }

    void MoveCutWheel()
    {
        
        if(transform.position.x >= trapLength)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x <= -trapLength)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
