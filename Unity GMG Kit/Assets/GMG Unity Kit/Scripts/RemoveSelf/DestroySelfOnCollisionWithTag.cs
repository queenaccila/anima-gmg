using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnCollisionWithTag : MonoBehaviour
{
    public string tag;
    public float waitSecondsBeforeDestroy = 0f;
    public AudioClip soundFile;
    AudioSource audioSrc;
    private void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = soundFile;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag)) {
            if (!audioSrc.isPlaying) audioSrc.Play();
            Destroy(gameObject, waitSecondsBeforeDestroy);
        }
    }
}
    
