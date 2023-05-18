using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    public float fishSpeed;

    private float _randomizedSpeed = 0f;
    private float _nextActionTime = -1f;

    private Vector3 _targetPosition;

    private void FixedUpdate()
    {
        if(fishSpeed > 0f)
        {
            Swim();
        }
    }
    private void Swim()
    {
        if(Time.fixedTime >= _nextActionTime)
        {
            _randomizedSpeed = fishSpeed * Random.Range(.5f, 1.5f);

            _targetPosition = PenguinArea.ChooseRandomPosition(transform.parent.position, 100f, 260f, 2f, 13f) + Vector3.up * 5f;
            transform.rotation = Quaternion.LookRotation(_targetPosition - transform.position, Vector3.up);

            float timeToGetThere = Vector3.Distance(_targetPosition, transform.position) / _randomizedSpeed;
            _nextActionTime = Time.fixedTime + timeToGetThere;
        }
        else
        {
            Vector3 moveVector = _randomizedSpeed *  Time.fixedDeltaTime * transform.forward;

            if(moveVector.magnitude <= Vector3.Distance(_targetPosition, transform.position))
            {
                transform.position += moveVector;
            }
            else
            {
                transform.position = _targetPosition;
                _nextActionTime = Time.fixedTime;
            }
        }
    }
}
