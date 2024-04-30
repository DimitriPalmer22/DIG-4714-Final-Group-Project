using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class EnemyDrop : MonoBehaviour
{
    private bool _pickedUp;
    
    protected abstract void DropEffect(Character character);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the drop has already been picked up, return
        if (_pickedUp)
            return;
        
        // if the other collider is not the player, return
        if (!other.CompareTag("Player"))
            return;
        
        var character = other.GetComponent<Character>();
        
        // if the character is null, return
        if (character == null)
            return;
        
        // apply the drop effect
        DropEffect(character);
        
        // update the flag
        _pickedUp = true;
        
        // destroy the drop
        Destroy(gameObject);
    }
}
