using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController<T> : MonoBehaviour where T : Enum
{
    private readonly int STATE_HASH = Animator.StringToHash("State");

    public Animator Animator{get; init;}

    private T _currentAnimationState;
    
    public void SetAnimationState(T animationState)
    {
        _currentAnimationState = animationState;
        Animator.SetInteger(STATE_HASH, Define.EnumToInt(animationState));
    }

    public void TrySetAnimationState(T animationState)
    {
        int currentState = Define.EnumToInt(_currentAnimationState);
        int parameterState = Define.EnumToInt(animationState);
            
        if (currentState != parameterState)
            SetAnimationState(animationState);
    }
    
    protected virtual void Awake() {
        Animator animator = GetComponent<Animator>();
        
    }

}
