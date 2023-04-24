using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance{ get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] private float _moveSpeed = 50f;

    private float orthographicSize;
    private float targetOrthographicSize;
    private bool edgeScrolling;
    private CinemachineConfiner confiner;
    private Vector3 _moveDir;
    private Vector3 _prevDir;

    private void Awake()
    {
        Instance = this;
        edgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 0) == 1;
        confiner = cinemachineVirtualCamera.GetComponent<CinemachineConfiner>();
    }

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void LateUpdate()
    {
        Vector3 prevPos = transform.position;
        
        
        if(confiner.CameraWasDisplaced(cinemachineVirtualCamera))
        {
            
            Vector3 camPos = (Vector2)Camera.main.transform.position;
            Vector3 dir = transform.position - camPos;

            transform.position = camPos - (_moveSpeed* 3f * Time.deltaTime * dir.normalized);

        }
        else
        {
            HandleMovement();
        }

        HandleZoom();
    }


    private void ReturnMovement()
    {
        transform.position -= _moveSpeed * Time.deltaTime * _prevDir;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(edgeScrolling)
        {
            float edgeScrollingSize = 30;
            if(Input.mousePosition.x > Screen.width - edgeScrollingSize)
            {
                x = +1f;
            }

            if(Input.mousePosition.x < edgeScrollingSize)
            {
                x = -1f;
            }

            if(Input.mousePosition.y > Screen.height - edgeScrollingSize)
            {
                y = +1f;
            }

            if(Input.mousePosition.y < edgeScrollingSize)
            {
                y = -1f;
            }
        }

        _moveDir = new Vector3(x, y).normalized;

        transform.position += _moveSpeed * Time.deltaTime * _moveDir;

    }

    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 3f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void SetEdgeScrolling(bool edgeScrolling)
    {
        this.edgeScrolling = edgeScrolling;
        PlayerPrefs.SetInt("edgeScrolling", edgeScrolling ? 1 : 0);
    }

    public bool GetEdgeScrolling()
    {
        return edgeScrolling;
    }
}
