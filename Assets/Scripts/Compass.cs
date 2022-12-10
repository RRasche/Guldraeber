using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 wind_direction;
    void Start()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        wind_direction = wind_controller.cur_wind_direction;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f * wind_direction.y/(Mathf.PI) - 90.0f);
    }
}
