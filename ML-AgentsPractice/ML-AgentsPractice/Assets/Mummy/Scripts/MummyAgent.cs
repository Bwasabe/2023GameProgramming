using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class MummyAgent : Agent
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Material _blue;
    [SerializeField]
    private Material _red;
    [SerializeField]
    private MeshRenderer _floor;
    
    private Rigidbody _rb;

    private Material _gray;
    private bool _isEnd;

    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _gray = _floor.material;
    }
    public override void OnEpisodeBegin()
    {
        _isEnd = false;
        _floor.material = _gray;
        float rangeMax = 4f;
        float rangeMin = -4f;

        transform.localPosition = new Vector3(Random.Range(rangeMin, rangeMax), 0.05f, Random.Range(rangeMin, rangeMax));
        _target.localPosition = new Vector3(Random.Range(rangeMin, rangeMax), .5f, Random.Range(rangeMin, rangeMax));
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxis("Horizontal");
        continuousAction[1] = Input.GetAxis("Vertical");

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position); // 3
        sensor.AddObservation(_target.position);   // 6
        sensor.AddObservation(_rb.velocity);       // 9

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if(_isEnd) return;
        float hori = actions.ContinuousActions[0];
        float verti = actions.ContinuousActions[1];

        _rb.AddForce(new Vector3(hori, 0f, verti).normalized * speed);
        
        AddReward(-.01f);
    }

    private void OnCollisionEnter(Collision other)
    {
        IEnumerator EndGame()
        {
            _isEnd = true;
            _rb.velocity = Vector3.zero;
            yield return new WaitForSecondsRealtime(0.2f);
            EndEpisode();
        }
        
        if(other.transform.CompareTag("TARGET"))
        {
            _floor.material = _blue;
            AddReward(2f);

            StartCoroutine(EndGame());

        }
        else if(other.transform.CompareTag("DEAD_ZONE"))
        {
            _floor.material = _red;
            AddReward(-1f);
            StartCoroutine(EndGame());

        }
    }
}
