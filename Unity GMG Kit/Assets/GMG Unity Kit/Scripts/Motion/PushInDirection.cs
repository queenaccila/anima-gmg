using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInDirection : MonoBehaviour
{
    public string Tag = "";
    public float force = 1f;
    public AudioClip soundFile;
    //public bool LoopSound = false;
    AudioSource audioSrc;
    private void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = soundFile;
        //audioSrc.loop = LoopSound;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag)){
            if(collision.gameObject.GetComponent<Rigidbody2D>() == null)
            {
                Debug.LogError("No Rigidbody provided");
            }
            else
            {
                if (!audioSrc.isPlaying) audioSrc.Play();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            }
        }
    }
}
