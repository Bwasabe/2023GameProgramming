using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : BehaviorTree
{
    [SerializeField]
    protected EntityVariable _entityVariable;

    protected override void Awake()
    {
        base.Awake();
        _entityVariable.Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AddTarget()
    {
        foreach (GameObject outlineObject in _entityVariable.OutlineObjects)
        {
            outlineObject.SetActive(true);
        }
    }

    public void SetTarget(Vector3 movePos)
    {
        _entityVariable.IsClickMoving = true;
        _entityVariable.IsSelected = true;
        _entityVariable.ClickMovePos = movePos;
    }

    public void RemoveSelected()
    {
        _entityVariable.IsSelected = false;

        foreach (GameObject outlineObject in _entityVariable.OutlineObjects)
        {
            outlineObject.SetActive(false);
        }
    }

    public void Flip(Vector2 dir)
    {
        if(dir.x > 0)
            FlipRight();
        else
            FlipLeft();
    }

    private void FlipLeft()
    {
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);

        scale.x = scaleX * -1f;

        transform.localScale = scale;
    }

    private void FlipRight()
    {
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);

        scale.x = scaleX;

        transform.localScale = scale;
    }
}

public partial class EntityVariable
{
    [field: SerializeField] public List<GameObject> OutlineObjects{ get; private set; }
}
