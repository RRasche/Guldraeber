using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeforestController : DrivingController
{
    [SerializeField] private string forestLayer = "Default";
    [SerializeField] private float deforesterDist = 0.5f;
    [SerializeField] private float demolishStrength = 1;
    [SerializeField] private float demolishSpeedMultiplier = 0.3f;
    [SerializeField] private GameObject demolisherParticlesPrefab;

    private Vector2 lastDir = Vector2.up;

    private Tile _currentDemolishTile = null;
    private GameObject demolisherParticles;

    void Update()
    {
        float speedMultiplier = this._firePressed > 0.5f ? demolishSpeedMultiplier : 1;

        drive(speedMultiplier);
        

        if (this._firePressed > 0.5f) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, deforesterDist, LayerMask.GetMask(forestLayer));

            if (hit.collider != null && hit.collider.transform.parent != null) {
                Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                if (tile != null) {
                    tile.Demolish_Me_a_BIT(demolishStrength);
                    if (tile != _currentDemolishTile) {
                        _currentDemolishTile = tile;
                        Debug.Log("babababababa");
                        demolisherParticles = Instantiate(demolisherParticlesPrefab, tile.transform.position, Quaternion.identity);
                    }
                }
                else {
                    destroyParticles();
                }
            }
            else {
                destroyParticles();
            }
        }
        else {
            destroyParticles();
        }
    }

    void destroyParticles() {
        if (_currentDemolishTile != null) {
            Destroy(demolisherParticles);
        }
        _currentDemolishTile = null;
    }
}
