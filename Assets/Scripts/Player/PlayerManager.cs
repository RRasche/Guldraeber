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

    [SerializeField] private GameObject[] _controllerPrefabs = new GameObject[4];
    [SerializeField] private Transform planeSpawnPoint;
    [SerializeField] private Transform deforesterSpawnPoint;
    [SerializeField] private Transform waterSpawnPoint;
    [SerializeField] private Transform landSpawnPoint;

    public GameObject[] controllerPrefabs {get {return _controllerPrefabs;}}

    void Awake() {
        if (_instance) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

    void OnPlayerJoined(PlayerInput playerInput) {
        Transform spawnPoint = null;

        switch(_controllerPrefabs[playerInput.playerIndex].GetComponent<PlayerController>()) {
            case FlyingController fcon:
                spawnPoint = planeSpawnPoint;
                break;
            case DeforestController dcon:
                spawnPoint = deforesterSpawnPoint;
                break;
            case LandFireFightController lcon:
                spawnPoint = landSpawnPoint;
                break;
            case WaterFireFightController wcon:
                spawnPoint = waterSpawnPoint;
                break;
            default:
                Debug.Log("World");
                break;
        }

        if (spawnPoint != null) {
            playerInput.transform.SetPositionAndRotation(
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
    }
}
