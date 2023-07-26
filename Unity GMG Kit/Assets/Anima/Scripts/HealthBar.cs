using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;
    public Black black;
    
    // Start is called before the first frame update
    public void SetHealth(float health)
    {
        slider.value = slider.value + health;

        if(slider.value == 0)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
