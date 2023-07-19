using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyOnCollisionWithTag : MonoBehaviour
{
    [SerializeField] string tag;
    string shooterTag;
    [SerializeField] bool destroyBulletOnCollisionWithTag;
    [SerializeField] bool destroyBulletOnColiisionWithAnything;
    public AudioClip soundFile;
    AudioSource audioSrc;
    private void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = soundFile;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
                if(collision.CompareTag("Enemy"))Destroy(collision.gameObject);
                if (destroyBulletOnCollisionWithTag || destroyBulletOnColiisionWithAnything)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(collision.gameObject);
                if (destroyBulletOnCollisionWithTag || destroyBulletOnColiisionWithAnything)
                {
                    
                    Destroy(gameObject);
                }
            }
        }
        if (destroyBulletOnColiisionWithAnything && collision.tag != gameObject.tag)
        {
            Debug.Log(collision.tag);
            Destroy(gameObject);
        }
    }
}
