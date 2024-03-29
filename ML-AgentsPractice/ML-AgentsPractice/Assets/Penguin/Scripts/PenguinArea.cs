using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PenguinArea : MonoBehaviour
{
    public PenguinAgent _penguinAgent;
    public GameObject BabyPenguin;
    public TextMeshPro _cumulativeRewardText;
    public Fish _fishPrefab;
    private List<GameObject> _fishList;

    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        float angle = minAngle;

        if(maxRadius > minRadius)
        {
            radius = Random.Range(minRadius, maxRadius);
        }

        if(maxAngle > minAngle)
        {
            angle = Random.Range(minAngle, maxAngle);
        }

        return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
    }

    private void Update()
    {
        _cumulativeRewardText.text = _penguinAgent.GetCumulativeReward().ToString("0.00");
    }

    public void RemoveSpecificFish(GameObject fishObject)
    {
        _fishList.Remove(fishObject);
        Destroy(fishObject);
    }

    public int FishRemaining => _fishList.Count;

    private void Start()
    {
        ResetArea();
    }

    public void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlaceBaby();
        SpawnFish(4, 0.5f);
    }

    private void PlacePenguin()
    {
        Rigidbody rigidbody = _penguinAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        _penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        _penguinAgent.transform.rotation = Quaternion.Euler(0f,Random.Range(0f,360f), 0f);
    }

    private void PlaceBaby()
    {
        Rigidbody rigidbody = BabyPenguin.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        BabyPenguin.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
        BabyPenguin.transform.rotation = Quaternion.Euler(0f,180f, 0f);
    }

    private void SpawnFish(int count, float fishSpeed)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject fishObject = Instantiate(_fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * 5f;
            fishObject.transform.rotation = Quaternion.Euler(0f,Random.Range(0f,360f),0f);
            
            fishObject.transform.SetParent(transform);
            _fishList.Add(fishObject);

            fishObject.GetComponent<Fish>().fishSpeed = fishSpeed;
        }
    }

    private void RemoveAllFish()
    {
        if(_fishList != null)
        {
            for (int i = 0; i < _fishList.Count; ++i)
            {
                if(_fishList[i] != null)
                {
                    Destroy(_fishList[i]);
                }
            }
        }

        _fishList = new();
    }
    
    
}