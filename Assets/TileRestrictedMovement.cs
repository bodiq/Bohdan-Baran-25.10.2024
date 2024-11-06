using Player;
using UnityEngine;

public class TileRestrictedMovement : MonoBehaviour
{
    private Vector3 _lastValidPosition;

    private void Start()
    {
        _lastValidPosition = transform.position; 
    }

    private void FixedUpdate()
    {
        CheckIfOnTile();
    }

    private void CheckIfOnTile()
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                _lastValidPosition = transform.position;
            }
            else
            {
                transform.position = _lastValidPosition;
            }
        }
    }
}