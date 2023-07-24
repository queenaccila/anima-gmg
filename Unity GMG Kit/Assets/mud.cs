using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mud : MonoBehaviour
{
    private void Start()
    {
        
    }    

    void OnTriggerEnter2D (Collider2D other)
    {
            //other.getcomponent<Move>().speed = 0.5;
    }
}
