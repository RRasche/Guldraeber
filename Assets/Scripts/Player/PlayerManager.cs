using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        if (_instance) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }
}
