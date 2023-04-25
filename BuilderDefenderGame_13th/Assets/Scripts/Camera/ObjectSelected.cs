using System;
using System.Collections;
using System.Collections.Generic;
using EventManagers;
using UnityEngine;

public class ObjectSelected : MonoBehaviour
{
    public const string ARRIVED_CLICKPOS = "ArrivedClickPos";
    [SerializeField]
    private GameObject _clickPosObject;
    
    private SpriteRenderer _selectedImage;

    private Vector2 _startPos;
    private Vector2 _nowPos;
    
    private bool _isClicked;
    private Camera _camera;

    private Vector2 _size;

    private List<Entity> _entityList;

    private void Awake()
    {
        _entityList = new();
        _camera = Camera.main;
        _selectedImage = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        EventManager.StartListening(ARRIVED_CLICKPOS,ArriveObjectInClickPos);
    }

    private void ArriveObjectInClickPos()
    {
        _clickPosObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _selectedImage.gameObject.SetActive(true);
            _isClicked = true;
            _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = _startPos;
        }

        if(_isClicked)
        {
            _nowPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _size = _startPos - _nowPos;
            Vector3 position =  _size * -0.5f;
            
            _selectedImage.transform.localPosition = position;
            _selectedImage.size = _size;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _selectedImage.gameObject.SetActive(false);
            _isClicked = false;

            _entityList.Clear();    
            SetTarget(_nowPos);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Vector3 inputPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            inputPos.z = 0f;

            if(_entityList.Count <= 0) return;
            _clickPosObject.gameObject.SetActive(true);
            _clickPosObject.transform.position = inputPos;
            
            foreach(var entity in _entityList)
            {
                entity.SetTarget(inputPos);
            }
        }
    }

    private void SetTarget(Vector3 pos)
    {
        Vector2 point = new Vector2(_startPos.x + (_nowPos.x - _startPos.x) * 0.5f, _startPos.y + (_nowPos.y - _startPos.y) * 0.5f);
        Vector2 size = new Vector2(Mathf.Abs(_size.x), Math.Abs(_size.y));
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0f);


        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent<Entity>(out Entity entity))
            {
                _entityList.Add(entity);
            }
        }
    }
}
