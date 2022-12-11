using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static int burn_count = 0;
    public static int dead_count = 0;

    public TMP_Text burning_text;
    public TMP_Text dead_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // burning_text.text = burn_count.ToString();
       // dead_text.text = burn_count.ToString();

        if(burn_count <= 0)
        {
            PlayerPrefs.SetInt("deadTrees", dead_count);
            SceneManager.LoadScene(2);
        }
        
    }
}
