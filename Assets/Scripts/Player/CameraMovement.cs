using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;
    
    private Vector3 _distanceToPlayer;

    // Start is called before the first frame update
    public void setPlayerTransform(Transform playertransform)
    {
        this.playerTransform = playertransform;
        this._distanceToPlayer = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null) { 
            transform.position = playerTransform.position + this._distanceToPlayer;
        }
    }
}
