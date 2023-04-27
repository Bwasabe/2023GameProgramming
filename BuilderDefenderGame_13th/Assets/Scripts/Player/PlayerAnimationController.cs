using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle = 0,
    Move = 1,
    Attack = 2
}

public class PlayerAnimationController : AnimationController<PlayerAnimationState>
{
    
}
