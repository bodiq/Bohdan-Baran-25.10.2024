using Player;
using UnityEngine;

public class TileRestrictedMovement : MonoBehaviour
{
    [SerializeField] private float powerMove = 15f;
    
    private Vector3 _lastValidPosition;
    private CharacterController _characterController;
    private PlayerCharacter _playerCharacter;
    private bool _isOnTile;

    public bool IsOnTile => _isOnTile;

    private void Start()
    {
        _lastValidPosition = transform.position; 
        _characterController = GetComponent<CharacterController>();
        _playerCharacter = GetComponent<PlayerCharacter>();
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
                _playerCharacter.StepDustParticle.SetActive(true);
            }
            else
            {
                _isOnTile = false;
                _playerCharacter.StepDustParticle.SetActive(false);
            }
        }
    }
}