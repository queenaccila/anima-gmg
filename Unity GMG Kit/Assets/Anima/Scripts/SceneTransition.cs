using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;

    public string sceneName;

    public void ChooseScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DelayScene()
    {
        Invoke("ChooseScene", 1);
        animator.Play("FadeOut");
    }
}
