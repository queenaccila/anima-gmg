using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI itemText;

    public static InventoryUI Instance;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //itemText = GetComponent<TextMeshProUGUI>();
        itemText.text = count.ToString() + "/4";
    }

    public void UpdateItemText()
    {
        count++;
        Debug.Log("count" + count);
        itemText.text = count.ToString() + "/4";
    }

    public void CheckCount()
    {
        if(count == 4)
        {
            Debug.Log("pipe clogged");
            // add animation for pipe clogged
        }
    }

    void Awake()
    {
        Instance = this;
    }
}
