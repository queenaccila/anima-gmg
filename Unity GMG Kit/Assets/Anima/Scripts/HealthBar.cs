using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Animator fadeAnimate;    

    public Slider slider;

    private float secondDelay = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
        //fadeAnimate = GetComponent<Animator>();
    }

    public void SetHealth(float health)
    {
        slider.value = slider.value + health;

        if(slider.value == 0)
        {
            fadeAnimate.Play("FadeOut");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
