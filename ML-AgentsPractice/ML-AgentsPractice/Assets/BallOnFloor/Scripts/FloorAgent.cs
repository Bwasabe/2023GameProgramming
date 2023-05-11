using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class FloorAgent : Agent
{
    [SerializeField]
    private GameObject _ball;
    private Rigidbody _ballRb;
    
    // 초기화
    public override void Initialize()
    {
        _ballRb = _ball.GetComponent<Rigidbody>();
    }
    
    // 한 행동(에피소드)가 시작될 때 실행
    public override void OnEpisodeBegin()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.right, Random.Range(-10f, 10f));
        transform.Rotate(Vector3.forward, Random.Range(-10f, 10f));

        _ballRb.velocity = Vector3.zero;

        _ball.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, Random.Range(-1.5f, 1.5f));

    }

    // 관측을 시킬 때 사용
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.rotation.x); // 1 (float)
        sensor.AddObservation(transform.rotation.z); // 2 (float)
        
        sensor.AddObservation(_ball.transform.position - transform.position); //3, 4, 5 (Vector3)
        sensor.AddObservation(_ballRb.velocity); // 6, 7, 8 (Vector3)
    }
    
    public override void OnActionReceived(ActionBuffers actions) // Continuous X회전, Z회전 2가지 행동
    {
        float z_rotation = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        float x_rotation = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);

        transform.Rotate(Vector3.forward, z_rotation);
        transform.Rotate(Vector3.right, x_rotation);

        // 공을 떨궜을 경우
        if(DropBall())
        {
            // 감점
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            // 보상(에피소드마다)
            SetReward(.1f);
        }
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionOut = actionsOut.ContinuousActions;
        continuousActionOut[0] = Input.GetAxis("Horizontal");
        continuousActionOut[1] = Input.GetAxis("Vertical");
    }
    
    private bool DropBall()
    {
        return  Mathf.Abs(_ball.transform.position.y - transform.position.y) < -2f || 
                Mathf.Abs(_ball.transform.position.x - transform.position.x) > 2.5f ||
                Mathf.Abs(_ball.transform.position.z - transform.position.z) > 2.5f;
    }

}
