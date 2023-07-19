using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMovement : MonoBehaviour
{

    Rigidbody2D body2D;
    [Range(0.0f,1f)]
    [SerializeField] float leftPushRange;
    [Range(0.0f, 1f)]
    [SerializeField] float rightPushRange;
    [Range(50f, 500f)]
    [SerializeField] float velocityThreshold;
    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        body2D.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    private void Push()
    {
        if(transform.rotation.z> 0 && transform.rotation.z < rightPushRange 
            && (body2D.angularVelocity > 0 )
            && body2D.angularVelocity < velocityThreshold){
            body2D.angularVelocity = velocityThreshold;
        }
        else if(transform.rotation.z < 0 && transform.rotation.z > leftPushRange 
            && (body2D.angularVelocity < 0) 
            && body2D.angularVelocity > velocityThreshold * -1)
        {
            body2D.angularVelocity = velocityThreshold * -1;
        }
    }
}
