using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    GameObject player;
    [SerializeField]
    [Range (5.0f,20.0f)]
    float range = 10.0f;
    [SerializeField]
    [Range(0.5f,5f)]
    float fireRate =1f;
    float nextFire;
    [SerializeField]
    [Range(5.0f, 20.0f)]
    float bulletSpeed = 7.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("player");
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < range){
            CheckIfTimeToFire();
        }
        //CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<EnemyBulletBehavior>().moveSpeed = bulletSpeed;
            nextFire = Time.time + fireRate;
        }

    }

}
