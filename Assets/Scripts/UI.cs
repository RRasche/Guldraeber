using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = (1.0f/Time.deltaTime).ToString("#.0");
    }
}
