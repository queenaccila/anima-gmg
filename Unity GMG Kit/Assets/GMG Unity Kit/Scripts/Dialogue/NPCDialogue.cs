using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _textMeshPro;
    [SerializeField] GameObject advanceText;
    [Range(0f,1f)]
    [SerializeField] public float timeBetweenCharacters = .2f;
    [Range(0f, 1f)]
    [SerializeField] public float timebetweenWords = 1f;
    //[SerializeField] public bool turnInvisibleWhenDone = false;
    //[SerializeField] public float timeBeforeTurningInvisible = 1f;
    [SerializeField] public KeyCode advanceTextButton = KeyCode.E;
    [SerializeField] public Collider2D collider;
    [SerializeField] public string tag;
    int i = 0;
    public string[] stringArray;
    bool doOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        advanceText.GetComponent<TextMeshProUGUI>().text = "Press " + advanceTextButton + " To advance";
        advanceText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            Debug.Log("I is now: " + i + stringArray[i]);
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());

        }
        else
        {
           // if (turnInvisibleWhenDone)
            //{
                turnInvisible();
           // }
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

            
            if (visibleCount >= totalVisibleCharacters)
            {
                advanceText.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(advanceTextButton));
                advanceText.SetActive(false);
                Debug.Log("I before: "+ i);
                i += 1;
                Invoke("EndCheck", timebetweenWords);
                break;
                 
            }

            counter += 1;
            yield return new WaitForSeconds(timeBetweenCharacters);

        }
    }

    public void turnInvisible()
    {
        //yield return new WaitForSeconds(timeBeforeTurningInvisible);
        gameObject.SetActive(false);
    }
}
