using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    private Vector3 _distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        this._distanceToPlayer = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + this._distanceToPlayer;
    }
}
