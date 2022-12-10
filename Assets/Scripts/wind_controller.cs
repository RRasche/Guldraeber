using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_controller : MonoBehaviour
{
    public static Vector2 cur_wind_direction;

    [SerializeField]
    public Vector2 wind_direction_temp;

    [SerializeField]
    public float windMean = 0.0f;
    Vector2 new_direction;
    Vector2 old_direction;
    float changeWind;

    // Variables for the animation function
    bool animating;
    float fn_val;
    float t;
    float x;

     private IEnumerator animated;

    // Start is called before the first frame update
    void Start()
    {   
        animating = false;
        cur_wind_direction = new Vector2(0.0f, 0.0f);
        old_direction = new Vector2(0.0f, 0.0f);
        new_direction = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!animating)
        {
            generate_wind();
        }

        animate_direction();
        wind_direction_temp = cur_wind_direction;
    }


    void generate_wind()
    {
        old_direction = new_direction;
        new_direction.x = Random.Range(0.0f , 1.0f);
        new_direction.y = inverse_cauchy_cdf(Random.Range(0.0f , 1.0f));
        changeWind = Time.time + Random.Range(7.0f, 15.0f);
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

    void animate_direction()
    {
        if(!animating)
        {
            t = Time.time;
            x = 0.0f; 
            animating = true;
        }
        else if(x >=1)
        {
            animating = false;
            return;
        }
        

        float duration = changeWind - t; 
        Vector2 change_amount = new_direction - old_direction;

        x = (Time.time - t) / duration; 

        fn_val = x < 0.5f ? 4.0f * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 3.0f) / 2.0f;
        //fn_val = x < 0.5f ? (1.0f - bounc_fn(1.0f - 2.0f * x)) / 2.0f : (1.0f + bounc_fn(2.0f * x - 1.0f)) / 2.0f;
        
        cur_wind_direction.x = old_direction.x + fn_val * change_amount.x; 
        cur_wind_direction.y = old_direction.y + fn_val * change_amount.y;
    }

    float inverse_cauchy_cdf(float u)
    {
        float gamma = 0.3f;

        float val =  gamma * Mathf.Tan(Mathf.PI * (u - 0.5f));

        return val + windMean;
        
    }
}
