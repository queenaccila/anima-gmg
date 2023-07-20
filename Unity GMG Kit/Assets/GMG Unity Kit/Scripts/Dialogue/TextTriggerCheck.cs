using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTriggerCheck : MonoBehaviour
{
    [SerializeField] string tag = "Player";
    [SerializeField] GameObject textBox;
    bool doOnce = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag) && doOnce)
        {
            doOnce = false;
            textBox.GetComponent<TextAnimations>().EndCheck();
        }
    }
}
