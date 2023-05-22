using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PenguinAgent : Agent
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public GameObject heartPrefab;
    public GameObject regurgitatedFishPrefab;

    private PenguinArea _penguinArea;
    private Rigidbody _rigidbody;
    private GameObject _baby;

    private bool _isFull;

    public override void Initialize()
    {
        _penguinArea = GetComponentInParent<PenguinArea>();
        _rigidbody = GetComponent<Rigidbody>();
        _baby = _penguinArea.BabyPenguin;
    }

    public override void OnEpisodeBegin()
    {
        _isFull = false;
        _penguinArea.ResetArea();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;

        forwardAmount = actions.DiscreteActions[0];

        if(actions.DiscreteActions[1] == 1)
        {
            turnAmount = -1f;
        }
        else if(actions.DiscreteActions[1] == 2)
        {
            turnAmount = 1f;
        }
        
        _rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        Vector3 rotateEuler = transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime;

        transform.Rotate(rotateEuler);

        if(MaxStep > 0)
        {
            AddReward(-1f/MaxStep);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;

        if(Input.GetKey(KeyCode.W))
        {
            forwardAction = 1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            turnAction = 1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            turnAction = 2;
        }

        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
        
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_isFull); // 1개
        sensor.AddObservation(Vector3.Distance(_baby.transform.position, transform.position)); // 1개
        sensor.AddObservation((_baby.transform.position - transform.position).normalized); // 3개
        sensor.AddObservation(transform.forward); // 3개
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Fish"))
        {
            EatFish(other.gameObject);
        }
        else if(other.transform.CompareTag("Baby"))
        {
            RegurgitateFish();
        }
    }

    private void EatFish(GameObject fishObject)
    {
        if(_isFull) return;
        _isFull = true;

        _penguinArea.RemoveSpecificFish(fishObject);
        AddReward(1f);
    }

    private void RegurgitateFish()
    {
        if(!_isFull)return;
        _isFull = false;

        GameObject regurgitatedFish = Instantiate(regurgitatedFishPrefab);
        regurgitatedFish.transform.parent = transform.parent;
        regurgitatedFish.transform.position = _baby.transform.position;
        Destroy(regurgitatedFish,4f);
        
        GameObject heart = Instantiate(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.position = _baby.transform.position + Vector3.up;
        Destroy(heart,4f);
        
        AddReward(1f);

        if(_penguinArea.FishRemaining <= 0)
        {
            EndEpisode();
        }
    }
}
