using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class MummyRayAgent : Agent
{
    [SerializeField]
    private MeshRenderer _floor;
    [SerializeField]
    private Material _blue;
    [SerializeField]
    private Material _red;
    
    [SerializeField]
    private GameObject _goodItem;
    [SerializeField]
    private GameObject _badItem;

    [SerializeField]
    private int _goodItemCount = 30;
    [SerializeField]
    private int _badItemCount = 10;

    private List<GameObject> _goodItemList = new();
    private List<GameObject> _badItemList = new();
    
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
        float rangeMax = 25;
        float rangeMin = -25;
        
        for (int i = 0; i < _goodItemCount; ++i)
        {
            _goodItemList.Add(Instantiate(_goodItem, 
                transform.parent.position + new Vector3(Random.Range(rangeMin, rangeMax), 0f, Random.Range(rangeMin, rangeMax)), Quaternion.identity, transform.parent));
            
        }

        for (int i = 0; i < _badItemCount; ++i)
        {
            _badItemList.Add(Instantiate(_badItem, transform.parent.position + new Vector3(Random.Range(rangeMin, rangeMax), 0f, Random.Range(rangeMin, rangeMax)), Quaternion.identity, transform.parent));
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
        else if(Input.GetKey(KeyCode.S))
        {
            forwardAction = 2;
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
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;

        forwardAmount = actions.DiscreteActions[0];
        
        if(actions.DiscreteActions[0] == 1)
        {
            forwardAmount = 1f;
        }
        else if(actions.DiscreteActions[0] == 2)
        {
            forwardAmount = -1f;
        }

        if(actions.DiscreteActions[1] == 1)
        {
            turnAmount = -1f;
        }
        else if(actions.DiscreteActions[1] == 2)
        {
            turnAmount = 1f;
        }
        
        _rb.MovePosition(transform.position + transform.forward * forwardAmount * 5f * Time.fixedDeltaTime);
        Vector3 rotateEuler = transform.up * turnAmount * 180f * Time.fixedDeltaTime;

        transform.Rotate(rotateEuler);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        
    }
    

    private void OnCollisionEnter(Collision other)
    {
        IEnumerator EndGame(Action endCallback = null)
        {
            _isEnd = true;

            _rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.2f);

            _floor.material = _gray;
            
            endCallback?.Invoke();
        }

        if(other.transform.CompareTag("GOOD_ITEM"))
        {
            AddReward(1f);
            _goodItemList.Remove(other.gameObject);
            Destroy(other.gameObject);

            _floor.material = _blue;

            StartCoroutine(EndGame());

            if(_goodItemList.Count <= 0)
            {
                foreach (GameObject o in _badItemList)
                {
                    Destroy(o);
                }
                
                _badItemList.Clear();

                EndEpisode();
            }
        }
        else if(other.transform.CompareTag("BAD_ITEM"))
        {
            _floor.material = _red;

            AddReward(-1f);
            Destroy(other.gameObject);
            
            
            foreach (GameObject o in _goodItemList)
            {
                Destroy(o);
            }
            _goodItemList.Clear();
            
            foreach (GameObject o in _badItemList)
            {
                Destroy(o);
            }
            _badItemList.Clear();

            StartCoroutine(EndGame(EndEpisode));
        }
        else if(other.transform.CompareTag("WALL"))
        {
            AddReward(-0.1f);
        }
    }
}
