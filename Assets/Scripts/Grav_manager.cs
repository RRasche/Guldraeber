using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grav_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, 0.0f, 9.81f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
