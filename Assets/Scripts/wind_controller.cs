using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_controller : MonoBehaviour
{

    Vector2 new_direction;
    Vector2 old_direction;

    float changeWind;

    // Start is called before the first frame update
    void Start()
    {
        old_direction = new Vector2(0.0f, 0.0f);
        new_direction = new Vector2(0.0f, 0.0f);
        generate_wind();

        changeWind = Time.time + Random.Range()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate_wind()
    {
        old_direction = new_direction;
        new_direction.x = Random.Range(0.0f , 1.0f);
        new_direction.y = Random.Range(0.0f , 2.0f * Mathf.PI);
    }


    float bounc_fn(float t)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1.0f / d1) 
        {
            return n1 * t * t;
        } 
        else if (t < 2.0f / d1) 
        {
            t -= 1.5f;
            return n1 * (t / d1) * t + 0.75f;
        } 
        else if (t < 2.5f / d1) 
        {
            t -= 2.25f;
            return n1 * (t / d1) * t + 0.9375f;
        } 
        else 
        {
            t -= 2.625f;
            return n1 * (t / d1) * t + 0.984375f;
        }
    }

    void animate_direction(float t)
    {
         float fn_val;
         fn_val = t < 0.5f ? (1.0f - bounc_fn(1.0f - 2.0f * t)) / 2.0f : (1.0f + bounc_fn(2.0f * t - 1.0f)) / 2.0f;


    }
}
