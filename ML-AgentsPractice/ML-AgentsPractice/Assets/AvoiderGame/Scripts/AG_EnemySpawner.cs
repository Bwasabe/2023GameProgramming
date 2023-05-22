using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AG_EnemySpawner : MonoBehaviour
{
    private List<AG_Enemy> _enemyList;
    
    [SerializeField]
    private float _radius;

    [SerializeField]
    private Transform _floor;

    private AvoiderAgent _player;
    private void Awake()
    {
        _enemyList = GetComponentsInChildren<AG_Enemy>().ToList();
        _player = GetComponentInChildren<AvoiderAgent>();
        _player.OnEpisodeBeginAction += ResetEnemy;
    }

    private void Start()
    {
        foreach (AG_Enemy agEnemy in _enemyList)
        {
            agEnemy.SetTarget(_player.transform);
        }
        _player.RegisterEnemyList(_enemyList);
    }

    private void ResetEnemy()
    {
        foreach (AG_Enemy enemy in _enemyList)
        {
            Vector3 dir = new Vector3(Random.Range(-1f, 1f),0f, Random.Range(-1f, 1f)).normalized;
            enemy.transform.localPosition = dir * _radius;
        }


    }
    
    private void OnDrawGizmos()
    {
        if(_floor == null) return;
        Gizmos.DrawWireSphere(_floor.transform.position, _radius);
    }
}
