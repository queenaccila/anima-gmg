using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimations : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _textMeshPro;
    [SerializeField] public float timeBetweenCharacters = .2f;
    [SerializeField] public float timebetweenWords = 1f;
    [SerializeField] public bool turnInvisibleWhenDone = false;
    [SerializeField] public float timeBeforeTurningInvisible = 1f;
    [SerializeField] public Collider2D collider;
    [SerializeField] public string tag;
    int i = 0;
    public string[] stringArray;
    // Start is called before the first frame update
    void Start()
    {
        //EndCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
            
        }
        else
        {
            if (turnInvisibleWhenDone) {
                StartCoroutine(turnInvisible());
            }
        }
    }
    public IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;

        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke("EndCheck", timebetweenWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBetweenCharacters);
            
        }
    }

    public IEnumerator turnInvisible()
    {
        yield return new WaitForSeconds(timeBeforeTurningInvisible);
        gameObject.SetActive(false);
    }

    
}
