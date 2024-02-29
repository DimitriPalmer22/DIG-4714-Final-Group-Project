using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Fields

    /// <summary>
    /// The character's rigid body 
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// The character's actor script
    /// </summary>
    private Actor _actorScript;

    #endregion Fields
    
    #region Unity Methods
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the character's rigid body
        _rb = GetComponent<Rigidbody2D>();
        
        // Get the character's actor script
        _actorScript = GetComponent<Actor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the movement input from the player
        GetMovementInput();
    }

    void FixedUpdate()
    {
        // Move the character based on the movement input vector
        MovePlayer();
    }

    #endregion Unity Methods

    #region Methods

    
    /// <summary>
    /// Get the movement input vector from the player's input.
    /// Supposed to be called in Update.
    /// </summary>
    private void GetMovementInput()
    {
        // Create a vector2 of the horizontal and vertical input axes
        _actorScript.MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalize the vector to prevent faster diagonal movement
        _actorScript.MovementInput.Normalize();
        
        // Take damage if the player presses the "q" key
        if (Input.GetKeyDown(KeyCode.Q))
            _actorScript.ChangeHealth(-10);
        
        // Heal if the player presses the "e" key
        if (Input.GetKeyDown(KeyCode.E))
            _actorScript.ChangeHealth(10);
    }

    /// <summary>
    /// Apply the movement input to the character's rigid body.
    /// Supposed to be called in FixedUpdate.
    /// </summary>
    private void MovePlayer()
    {
        // Apply the movement input to the character's rigid body
        _rb.velocity = _actorScript.MovementInput * _actorScript.Speed;
    }

    #endregion Methods
    
}
