using DG.Tweening;
using UnityEngine;

public class PlayerConstruction : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Transform _maskParentTransform;

    [SerializeField]
    private float _maskEndScale = 3.7f;

    [SerializeField]
    private float _spawnDuration = 1f;

    private void Start()
    {
        _maskParentTransform.DOScaleY(_maskEndScale, _spawnDuration).OnComplete(OnPlayerSpawnComplete);
    }
    private void OnPlayerSpawnComplete()
    {
        Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
