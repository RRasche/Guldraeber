using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndUI : MonoBehaviour
{
    public TMP_Text end_text;
    // Start is called before the first frame update
    void Start()
    {
        int num = PlayerPrefs.GetInt("deadTrees");
        end_text.text = num.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
