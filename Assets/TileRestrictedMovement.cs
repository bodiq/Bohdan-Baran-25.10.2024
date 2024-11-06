using Player;
using UnityEngine;

public class TileRestrictedMovement : MonoBehaviour
{
    [SerializeField] private float powerMove = 15f;
    
    private Vector3 _lastValidPosition;
    private CharacterController _characterController;
    private bool _isOnTile;

    public bool IsOnTile => _isOnTile;

    private void Start()
    {
        _lastValidPosition = transform.position; 
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckIfOnTile();

        if (!_isOnTile)
        {
            var position = transform.position;
            var direction = (_lastValidPosition - position).normalized;
            var distance = Vector3.Distance(position, _lastValidPosition);
            _characterController.Move(direction * Mathf.Min(Time.deltaTime * powerMove, distance));
        }
    }

    private void CheckIfOnTile()
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                _lastValidPosition = transform.position;
                _isOnTile = true;
            }
            else
            {
                _isOnTile = false;
            }
        }
    }
}