using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM : MonoBehaviour
{
    public static int burn_count = 0;
    public static int dead_count = 0;

    public TMP_Text burning_text;
    public TMP_Text dead_text;

    public GameObject steamAnim;

    public static GM instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        burning_text.text = burn_count.ToString();
        dead_text.text = burn_count.ToString();
    }
}
