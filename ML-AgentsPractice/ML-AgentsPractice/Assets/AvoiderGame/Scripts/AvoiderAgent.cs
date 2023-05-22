using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AvoiderAgent : Agent
{
    private Rigidbody _rb;
    private List<AG_Enemy> _enemyList;
    public event Action OnEpisodeBeginAction;
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        OnEpisodeBeginAction?.Invoke();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach (AG_Enemy agEnemy in _enemyList)
        {
            sensor.AddObservation(agEnemy.RB.velocity); // 9
            sensor.AddObservation(agEnemy.transform.localPosition); // 9
        }
        sensor.AddObservation(_rb.velocity); //3
        
    }

    public void RegisterEnemyList(List<AG_Enemy> enemyList)
    {
        _enemyList = enemyList;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float hori = actions.ContinuousActions[0];
        float verti = actions.ContinuousActions[1];

        float speed = 3f;
        _rb.velocity = new Vector3(hori, 0f, verti).normalized * speed;
        
        AddReward(.1f);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxis("Horizontal");
        continuousAction[1] = Input.GetAxis("Vertical");
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("AG_Enemy"))
        {
            OnHitEnemy();
            EndEpisode();
        }
    }

    private void OnHitEnemy()
    {
        AddReward(-1f);
    }
}