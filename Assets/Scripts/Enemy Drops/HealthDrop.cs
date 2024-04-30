using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : EnemyDrop
{
    protected override void DropEffect(Character character)
    {
        character.ChangeHealth(1);
    }
}
