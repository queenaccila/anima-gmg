using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    GameObject target;
    Vector2 moveDirection;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            target = GameObject.FindGameObjectWithTag("player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Cat"))
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }
}
