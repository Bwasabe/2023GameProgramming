using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    private State _state;

    [SerializeField]
    private List<Transform> _spawnPositionTransformList;
    
    [SerializeField]
    private Transform _nextWaveSpawnPositionTransform;

    public event EventHandler OnWaveNumberChanged;

    private float _nextWaveSpawnTimer;

    private float _nextEnemySpawnTimer;
    private int _remainingEnemySpawnAmount;

    private Vector3 _spawnPosition;

    private int _waveNumber;



    private void Start()
    {
        _nextWaveSpawnTimer = 3f;
        _state = State.WaitingToSpawnNextWave;
        _spawnPosition = _spawnPositionTransformList[Random.Range(0,_spawnPositionTransformList.Count)].position;
        _nextWaveSpawnPositionTransform.position = _spawnPosition;
        SpawnWave();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToSpawnNextWave:
                _nextWaveSpawnTimer -= Time.deltaTime;
                if(_nextWaveSpawnTimer < 0f)
                    SpawnWave();
                
                break;
            case State.SpawningWave:
                if(_remainingEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if(_nextEnemySpawnTimer < 0f)
                    {
                        _nextEnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(_spawnPosition + UtilClass.GetRandomDir() * Random.Range(0f, 10f));
                        _remainingEnemySpawnAmount--;

                        if(_remainingEnemySpawnAmount <= 0f)
                        {
                            _state = State.WaitingToSpawnNextWave;
                            _spawnPosition = _spawnPositionTransformList[Random.Range(0,_spawnPositionTransformList.Count)].position;
                            _nextWaveSpawnPositionTransform.position = _spawnPosition;
                            _nextWaveSpawnTimer = 10f;

                        }
                    }
                }
                break;
        }

    }

    public int GetWaveNumber()
    {
        return _waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return _nextWaveSpawnTimer;
    }
    
    private void SpawnWave()
    {
        _waveNumber++;
        OnWaveNumberChanged?.Invoke(this,EventArgs.Empty);
        
        _remainingEnemySpawnAmount = 5 + 3*_waveNumber;

        _state = State.SpawningWave;
    }
}
