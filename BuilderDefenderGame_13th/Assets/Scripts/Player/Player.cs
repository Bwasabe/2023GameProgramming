using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    protected override BT_Node SetupTree()
    {
        return new BT_Selector(this, new() {
            new EntityClickMoveCondition(this, new() {
                new EntityClickMove(this)
            }),
        });
    }
}
