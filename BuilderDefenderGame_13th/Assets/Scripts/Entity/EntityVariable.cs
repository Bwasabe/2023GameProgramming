using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public partial class EntityVariable : BT_Variable
{
    public bool IsClickMoving{get;set;}

    public Rigidbody2D Rigidbody{get;set;}

}
